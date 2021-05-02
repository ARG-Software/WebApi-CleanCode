using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RF.Application.Core.UseCases.Template.Commands.Delete;
using RF.Application.Core.UseCases.Template.Commands.Insert;
using RF.Application.Core.UseCases.Template.Commands.Update;
using RF.Application.Core.UseCases.Template.Queries.GetById;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Bus;

namespace RF.WebApi.Controllers
{
    /// <summary>
    /// Template Controller
    /// </summary>
    /// <seealso cref="Controller" />
    [Route("api/Template")]
    public class TemplateController : Controller
    {
        /// <summary>
        /// The bus
        /// </summary>
        private readonly IMemoryBus _bus;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateController"/> class.
        /// </summary>
        /// <param name="bus">The bus.</param>
        public TemplateController(IMemoryBus bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// Gets the template by Id.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{templateId}")]
        [SwaggerResponse(typeof(TemplateDto))]
        public async Task<IActionResult> GetTemplateById(int templateId, CancellationToken cancellationToken)
        {
            var template = await _bus.Send(new GetTemplateByIdQuery()
            {
                Id = templateId
            }, cancellationToken);

            return new ObjectResult(template);
        }

        /// <summary>
        /// Adds a template to database.
        /// </summary>
        /// <param name="template">The template to insert.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(typeof(bool))]
        public async Task<IActionResult> InsertTemplate([FromBody] TemplateDto template, CancellationToken cancellationToken)
        {
            await _bus.Send(new InsertTemplateCommand()
            {
                Template = template
            }, cancellationToken);

            return new NoContentResult();
        }

        /// <summary>
        /// Updates the template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerResponse(typeof(bool))]
        public async Task<IActionResult> UpdateTemplate([FromBody] TemplateDto template, CancellationToken cancellationToken)
        {
            await _bus.Send(new UpdateTemplateCommand()
            {
                Template = template
            }, cancellationToken);

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes the template.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <param name="cancellationToken"></param>ze
        /// <returns></returns>
        [HttpDelete]
        [Route("{templateId}")]
        [SwaggerResponse(typeof(bool))]
        public async Task<IActionResult> DeleteTemplate(int templateId, CancellationToken cancellationToken)
        {
            await _bus.Send(new DeleteTemplateCommand()
            {
                Id = templateId
            }, cancellationToken);

            return new NoContentResult();
        }
    }
}