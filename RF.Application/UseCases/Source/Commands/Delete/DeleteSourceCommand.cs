using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RF.Application.Core.Common.Exceptions;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Domain.Interfaces.UnitOfWork;

namespace RF.Application.Core.UseCases.Source.Commands.Delete
{
    public class DeleteSourceCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteSourceCommandHandler : IRequestHandler<DeleteSourceCommand>
    {
        private readonly IWriteRepository<Domain.Entities.Source> _sourceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSourceCommandHandler(IWriteRepository<Domain.Entities.Source> sourceRepository, IUnitOfWork unitOfWork)
        {
            _sourceRepository = sourceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteSourceCommand request, CancellationToken cancellationToken)
        {
            var sourceId = request.Id;

            _sourceRepository.DeleteRFEntityById(sourceId);

            var wasDeleted = await _unitOfWork.CommitAsync();

            if (!wasDeleted)
            {
                throw new DeleteFailureException(nameof(Domain.Entities.Source), request.Id);
            }

            return Unit.Value;
        }
    }
}