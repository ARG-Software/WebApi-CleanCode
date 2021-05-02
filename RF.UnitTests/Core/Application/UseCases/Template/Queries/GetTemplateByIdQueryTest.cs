using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using FakeItEasy;
using RF.Application.Core.Common.Exceptions;
using RF.Application.Core.UseCases.Template.Queries.GetById;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Repositories.Generic;
using Xunit;

namespace RF.UnitTests.Core.Application.UseCases.Template.Queries
{
    public class GetTemplateByIdQueryTest
    {
        private readonly IReadRepository<RF.Domain.Entities.Template> _fakeTemplateRepository;
        private readonly GetTemplateByIdQueryHandler _handler;
        private readonly GetTemplateByIdQueryValidator _validator;
        private readonly GetTemplateByIdQuery _request;

        public GetTemplateByIdQueryTest()
        {
            _fakeTemplateRepository = A.Fake<IReadRepository<RF.Domain.Entities.Template>>();
            _handler = new GetTemplateByIdQueryHandler(_fakeTemplateRepository);
            _validator = new GetTemplateByIdQueryValidator();
            _request = new GetTemplateByIdQuery();
        }

        [Fact]
        public async void GetTemplateByIdHandler_Success()
        {
            //Arrange
            const int fakeInputId = 1;
            _request.Id = fakeInputId;
            var fakeResultTemplate = new RF.Domain.Entities.Template()
            {
                Id = fakeInputId,
                Name = "fakeName"
            };

            A.CallTo(() => _fakeTemplateRepository.SingleAsync(
                    A<Expression<Func<RF.Domain.Entities.Template, bool>>>.Ignored,
                    A<Func<IQueryable<RF.Domain.Entities.Template>, IOrderedQueryable<RF.Domain.Entities.Template>>>.Ignored,
                    A<bool>.Ignored
                    ))
                .Returns(fakeResultTemplate);

            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            Assert.IsType<TemplateDto>(result);
            Assert.Equal(fakeInputId, result.Id);
            Assert.Equal("fakeName", result.Name);
        }

        [Fact]
        public async void GetTemplateByIdHandler_SourceDoesntExist_ThrowException()
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
        public async void CreateGetTemplateByIdCommand_Success()
        {
            //Arrange
            _request.Id = 1;

            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.Empty(validationResult.Errors);
        }

        [Fact]
        public async void CreateGetTemplateByIdCommand_WithIdEqual0_Fail()
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
        public async void CreateGetTemplateByIdCommand_WithIdNotDefined_Fail()
        {
            //Act
            var validationResult = await _validator.ValidateAsync(_request);

            //Assert
            Assert.NotNull(validationResult.Errors);
            Assert.Equal(1, validationResult.Errors.Count);
        }
    }
}