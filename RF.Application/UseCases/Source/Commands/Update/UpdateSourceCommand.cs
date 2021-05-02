using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Application.Core.Common.Exceptions;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Domain.Interfaces.UnitOfWork;

namespace RF.Application.Core.UseCases.Source.Commands.Update
{
    public class UpdateSourceCommand : IRequest
    {
        public SourceDto Source { get; set; }
    }

    public class UpdateSourceCommandHandler : IRequestHandler<UpdateSourceCommand>
    {
        private readonly IReadWriteRepository<Domain.Entities.Source> _sourceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSourceCommandHandler(IReadWriteRepository<Domain.Entities.Source> sourceRepository,
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
            _sourceRepository = sourceRepository;
        }

        public async Task<Unit> Handle(UpdateSourceCommand request, CancellationToken cancellationToken)
        {
            var sourceDto = request.Source;
            var sourceToUpdate = await _sourceRepository.SingleAsync(x => x.Id == sourceDto.Id);

            if (sourceToUpdate == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Source), request.Source.Id);
            }

            sourceToUpdate.Name = sourceDto.Name;

            _sourceRepository.UpdateRFEntity(sourceToUpdate);

            var wasUpdated = await _unitOfWork.CommitAsync();

            if (!wasUpdated)
            {
                throw new UpsertException("Couldn't update" + nameof(Domain.Entities.Source) + " entity ");
            }

            return Unit.Value;
        }
    }
}