using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using RF.Library.ActionFilters;
using Xunit;

namespace RF.UnitTests.Libraries.ActionFilters
{
    public class ValidateModelAttributeTest
    {
        [Fact]
        public void ValidateModelStateAttribute_IfModelIsInvalid_ReturnsBadRequest()
        {
            //Arrange
            var validationFilter = new ValidateModelStateAttribute();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("name", "invalid");

            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                modelState
            );

            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Mock.Of<Controller>()
            );

            //Act
            validationFilter.OnActionExecuting(actionExecutingContext);

            //Assert
            Assert.NotNull(actionExecutingContext.Result);
            Assert.IsType<BadRequestObjectResult>(actionExecutingContext.Result);
        }

        [Fact]
        public void ValidateModelStateAttribute_IfModelIsValid_ResultOfValidationIsNull()
        {
            //Arrange
            var validationFilter = new ValidateModelStateAttribute();
            var modelState = new ModelStateDictionary();

            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                modelState
            );

            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Mock.Of<Controller>()
            );

            //Act
            validationFilter.OnActionExecuting(actionExecutingContext);

            //Assert
            Assert.Null(actionExecutingContext.Result);
        }
    }
}