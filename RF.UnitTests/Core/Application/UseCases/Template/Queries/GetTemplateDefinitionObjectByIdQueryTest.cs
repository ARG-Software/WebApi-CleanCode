using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using AutoFixture;
using FakeItEasy;
using RF.Application.Core.Common.Exceptions;
using RF.Application.Core.UseCases.Template.Queries.GetTemplateDefinitionObjectById;
using RF.Domain.Interfaces.Entities;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Domain.Interfaces.ValueObjects;
using RF.Domain.ValueObjects;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Template.Queries
{
    public class GetTemplateDefinitionObjectByIdQueryTest
    {
        private readonly IReadRepository<RF.Domain.Entities.Template> _fakeTemplateRepository;
        private readonly GetTemplateDefinitionObjectByIdQueryHandler _handler;
        private readonly GetTemplateDefinitionObjectByIdQuery _request;
        private readonly GetTemplateDefinitionObjectByIdQueryValidator _validator;
        private readonly Fixture _fixture;

        public GetTemplateDefinitionObjectByIdQueryTest()
        {
            _fakeTemplateRepository = A.Fake<IReadRepository<RF.Domain.Entities.Template>>();
            _handler = new GetTemplateDefinitionObjectByIdQueryHandler(_fakeTemplateRepository);

            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _fixture.Register<ITemplate>(() => _fixture.Create<RF.Domain.Entities.Template>());
            _fixture.Register<ITemplateDefinition>(() => _fixture.Create<TemplateDefinition>());
            _fixture.Register<ITemplateFieldDefinition>(() => _fixture.Create<TemplateFieldDefinition>());

            _request = new GetTemplateDefinitionObjectByIdQuery();
            _validator = new GetTemplateDefinitionObjectByIdQueryValidator();
        }

        [Fact]
        public async void GetTemplateDefinitionObjectByIdQueryHandler_Success()
        {
            //Arrange
            const int fakeInputId = 1;
            _request.Id = fakeInputId;

            var fakeTemplate = A.Fake<RF.Domain.Entities.Template>(x => x.Implements<ITemplate>());
            var fakeTemplateDefinition = _fixture.Create<ITemplateDefinition>();
            A.CallTo(fakeTemplate).WithReturnType<ITemplateDefinition>().Returns(fakeTemplateDefinition);

            A.CallTo(() => _fakeTemplateRepository.SingleAsync(
                    A<Expression<Func<RF.Domain.Entities.Template, bool>>>.Ignored,
                    A<Func<IQueryable<RF.Domain.Entities.Template>, IOrderedQueryable<RF.Domain.Entities.Template>>>.Ignored,
                    A<bool>.Ignored
                ))
                .Returns(fakeTemplate);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.Equal(result, fakeTemplateDefinition);
        }

        [Fact]
        public async void GetTemplateDefinitionObjectByIdQueryHandler_IfTemplateNotFound_ThrowException()
        {
            //Arrange
            const int fakeInputId = 1;
            _request.Id = fakeInputId;
            A.CallTo(() => _fakeTemplateRepository.SingleAsync(
                    A<Expression<Func<RF.Domain.Entities.Template, bool>>>.Ignored,
                    A<Func<IQueryable<RF.Domain.Entities.Template>, IOrderedQueryable<RF.Domain.Entities.Template>>>.Ignored,
                    A<bool>.Ignored
                ))
                .Returns((RF.Domain.Entities.Template)null);

            //Act && Assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(_request, CancellationToken.None));

            Assert.Equal($"Entity \"{nameof(RF.Domain.Entities.Template)}\" ({_request.Id}) was not found.", ex.Message);
        }

        [Fact]
        public async void CreateGetTemplateDefinitionObjectByIdCommand_Success()
        {
            //Arrange
            _request.Id = 1;

            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.Empty(validationResult.Errors);
        }

        [Fact]
        public async void CreateGetTemplateDefinitionObjectByIdCommand_WithIdEqual0_Fail()
        {
            //Arrange
            _request.Id = 0;

            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.NotNull(validationResult.Errors);
            Assert.Equal(1, validationResult.Errors.Count);
        }

        [Fact]
        public async void CreateGetTemplateDefinitionObjectByIdCommand_WithIdNotDefined_Fail()
        {
            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.NotNull(validationResult.Errors);
            Assert.Equal(1, validationResult.Errors.Count);
        }
    }
}