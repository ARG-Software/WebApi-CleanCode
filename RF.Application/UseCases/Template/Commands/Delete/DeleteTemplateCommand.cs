using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Application.Core.Common.Exceptions;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Domain.Interfaces.UnitOfWork;

namespace RF.Application.Core.UseCases.Template.Commands.Delete
{
    public class DeleteTemplateCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTemplateCommandHandler : IRequestHandler<DeleteTemplateCommand>
    {
        private readonly IWriteRepository<Domain.Entities.Template> _templateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTemplateCommandHandler(IWriteRepository<Domain.Entities.Template> templateRepository,
            IUnitOfWork unitOfWork)
        {
            _templateRepository = templateRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
        {
            var templateId = request.Id;

            _templateRepository.DeleteRFEntityById(templateId);
            var wasDeleted = await _unitOfWork.CommitAsync();

            if (!wasDeleted)
            {
                throw new DeleteFailureException(nameof(Domain.Entities.Template), request.Id);
            }

            return Unit.Value;
        }
    }
}