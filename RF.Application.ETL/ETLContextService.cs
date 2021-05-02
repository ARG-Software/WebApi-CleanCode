using System;
using System.Collections.Generic;
using System.Linq;
using RF.Application.Interfaces.ETL;
using RF.Application.Interfaces.Exceptions;
using RF.Application.Services;
using RF.Domain.Entities;
using RF.Domain.Enum;

namespace RF.Application.ETL
{
    public class ETLContextService : CommonService, IETLContextService
    {
        public ITemplateService TemplateService { private get; set; }
        private readonly IDictionary<string, HashSet<string>> _processmentErrors;
        private readonly List<StatementDetail> _statementDetailsToBeInserted;
        private PaymentReceived _paymentForETLProcessment;
        private int _sourceIdForETLProcessment;

        public ETLContextService()
        {
            _paymentForETLProcessment = new PaymentReceived();
            _processmentErrors = new Dictionary<string, HashSet<string>>();
            _statementDetailsToBeInserted = new List<StatementDetail>();
        }

        public IDictionary<string, HashSet<string>> GetETLParsingErrors()
        {
            return _processmentErrors;
        }

        public void AddErrorToETLParsingErrors(ETLErrorEnum keyError, string error)
        {
            var keyAlreadyExistsOnDictionary = _processmentErrors.TryGetValue($"{keyError}", out var listOfErrorsForGivenKey);
            if (keyAlreadyExistsOnDictionary)
            {
                listOfErrorsForGivenKey.Add(error);
            }
            else
            {
                _processmentErrors.Add($"{keyError}", new HashSet<string> { error });
            }
        }

        public IEnumerable<StatementDetail> GetStatementDetailsToBeInserted()
        {
            return _statementDetailsToBeInserted;
        }

        public void AddStatementDetailToInsert(StatementDetail statement)
        {
            _statementDetailsToBeInserted.Add(statement);
        }

        public virtual PaymentReceived GetPaymentForETLProcessment()
        {
            return _paymentForETLProcessment;
        }

        public void SetPaymentForETLProcessment(int paymentId)
        {
            var paymentReceivedForProcessment =
                PaymentReceivedRepository.Single(x => x.Id == paymentId && x.Reconciled == false, null, false);
            this._paymentForETLProcessment = paymentReceivedForProcessment ?? throw new RFServiceException("A payment with the Id " + paymentId + ", doesn't exist on the database");
        }

        public int? GetSourceIdForETLProcessment()
        {
            return _sourceIdForETLProcessment;
        }

        public void SetSourceIdForETLProcessment(int templateId)
        {
            var sourceIdForProcessment = TemplateService.GetSourceIdByTemplateId(templateId);
            this._sourceIdForETLProcessment = sourceIdForProcessment;
        }

        public bool CheckIfPaymentCanBeReconciled()
        {
            var payments = StatementHeaderRepository.GetAll(x => x.PaymentReceivedId == GetPaymentForETLProcessment().Id && x.PaymentReceived.Reconciled == false, null,
                true, x => x.PaymentReceived).ToList();

            if (!payments.Any())
            {
                return false;
            }

            var totalPaymentsSum = payments.Sum(x => x.TotalLocal);

            return Math.Abs(totalPaymentsSum - GetPaymentForETLProcessment().GrossAmountForeign) <= 0.05;
        }

        public void MarkPaymentAsReconciled()
        {
            var paymentUpdated = GetPaymentForETLProcessment();
            paymentUpdated.Reconciled = true;
            PaymentReceivedRepository.UpdateRFEntity(paymentUpdated);
        }
    }
}