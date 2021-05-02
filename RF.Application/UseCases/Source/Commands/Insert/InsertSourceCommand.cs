using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Application.Core.Common.Exceptions;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Domain.Interfaces.UnitOfWork;

namespace RF.Application.Core.UseCases.Source.Commands.Insert
{
    public class InsertSourceCommand : IRequest
    {
        public SourceDto Source { get; set; }
    }

    public class InsertSourceCommandHandler : IRequestHandler<InsertSourceCommand>
    {
        private readonly IWriteRepository<Domain.Entities.Source> _sourceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InsertSourceCommandHandler(IWriteRepository<Domain.Entities.Source> sourceRepository,
            IUnitOfWork unitOfWork)
        {
            _sourceRepository = sourceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(InsertSourceCommand request, CancellationToken cancellationToken)
        {
            var sourcePassed = request.Source;

            var sourceToInsert = new Domain.Entities.Source
            {
                Name = sourcePassed.Name
            };

            await _sourceRepository.InsertRFEntityAsync(sourceToInsert);

            var wasInserted = await _unitOfWork.CommitAsync();

            if (!wasInserted)
            {
                throw new UpsertException("Couldn't insert" + nameof(Domain.Entities.Source) + " entity ");
            }

            return Unit.Value;
        }
    }
}