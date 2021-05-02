using RF.Application.Core.UseCases.Template.Commands.Update;
using RF.Domain.Interfaces.Entities;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Domain.Interfaces.UnitOfWork;

namespace RF.UnitTests.Core.Application.UseCases.Template.Command
{
    public class UpdateTemplateCommandTest
    {
        private readonly IReadWriteRepository<ITemplate> _fakeTemplateRepository;
        private readonly IReadRepository<ISource> _fakeSourceRepository;
        private readonly IUnitOfWork _fakeUnitOfWork;

        private readonly UpdateTemplateCommandHandler _handler;
        private readonly UpdateTemplateCommand _request;
        private readonly UpdateTemplateCommandValidator _validator;

        //public UpdateTemplateCommandTest()
        //{
        //    _fakeSourceRepository = A.Fake<IReadWriteRepository<ISource>>();
        //    _fakeTemplateRepository = A.Fake<IReadWriteRepository<ITemplate>>();
        //    _fakeUnitOfWork = A.Fake<IUnitOfWork>();

        //    _handler = new UpdateTemplateCommandHandler()
        //    {
        //        UnitOfWork = _fakeUnitOfWork,
        //        TemplateRepository = _fakeTemplateRepository,
        //        SourceRepository = _fakeSourceRepository
        //    };
        //    _request = new UpdateTemplateCommand();
        //    _validator = new UpdateTemplateCommandValidator();
        //}

        //[Fact]
        //public async void UpdateTemplateCommandHandler_Success()
        //{
        //    //Arrange
        //    const int fakeSourceId = 1;
        //    _request.Template = new TemplateDto()
        //    {
        //        Definition = "fakeDefinition",
        //        Name = "fakeName",
        //        SourceId = fakeSourceId
        //    };
        //    var fakeTemplate = A.Fake<ITemplate>();

        //    A.CallTo(() => fakeTemplate.SetTemplateDefinition(A<string>.Ignored)).DoesNothing();

        //    A.CallTo(() => _fakeSourceRepository.SingleAsync(
        //            A<Expression<Func<ISource, bool>>>.Ignored,
        //            A<Func<IQueryable<ISource>, IOrderedQueryable<ISource>>>.Ignored,
        //            false
        //        ))
        //        .Returns(A.Dummy<ISource>());
        //    A.CallTo(() => _fakeTemplateRepository.SingleAsync(
        //            A<Expression<Func<ITemplate, bool>>>.Ignored,
        //            A<Func<IQueryable<ITemplate>, IOrderedQueryable<ITemplate>>>.Ignored,
        //            A<bool>.Ignored))
        //        .Returns(fakeTemplate);

        //    A.CallTo(() => _fakeUnitOfWork.CommitAsync())
        //        .Returns(true);

        //    //Act
        //    await _handler.Handle(_request, CancellationToken.None);

        //    //Assert
        //    A.CallTo(() => _fakeTemplateRepository.UpdateRFEntity(A<ITemplate>.Ignored)).MustHaveHappenedOnceExactly();
        //    A.CallTo(() => _fakeUnitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

        //}
    }
}