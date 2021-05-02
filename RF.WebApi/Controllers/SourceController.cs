using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RF.Application.Core.UseCases.Source.Commands.Delete;
using RF.Application.Core.UseCases.Source.Commands.Insert;
using RF.Application.Core.UseCases.Source.Commands.Update;
using RF.Application.Core.UseCases.Source.Queries.GetById;
using RF.Application.Core.UseCases.Source.Queries.GetSourcePaged;
using RF.Application.Core.UseCases.Source.Queries.GetSourceWithTemplatesPaged;
using RF.Domain.Dto;
using RF.Domain.Interfaces.Bus;

namespace RF.WebApi.Controllers
{
    /// <summary>
    /// Source Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/Source")]
    public class SourceController : Controller
    {
        /// <summary>
        /// The bus
        /// </summary>
        private readonly IMemoryBus _bus;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceController"/> class.
        /// </summary>
        /// <param name="bus">The bus.</param>
        public SourceController(IMemoryBus bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{sourceId}")]
        [SwaggerResponse(typeof(SourceDto))]
        public async Task<IActionResult> GetSource(int sourceId, CancellationToken cancellationToken)
        {
            var source = await _bus.Send(new GetSourceByIdQuery()
            {
                Id = sourceId
            }, cancellationToken);
            return new ObjectResult(source);
        }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="options">Options for pagination</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("sourceWithTemplates/{sourceId}")]
        [SwaggerResponse(typeof(SourceDto))]
        public async Task<IActionResult> GetSourceWithTemplates([FromHeader] int sourceId, [FromBody] PagingOptionsDto options, CancellationToken cancellationToken)
        {
            var source = await _bus.Send(new GetSourceWithTemplatesPagedQuery()
            {
                SourceId = sourceId,
                Options = options
            }, cancellationToken);
            return new ObjectResult(source);
        }

        /// <summary>
        /// Gets the source list paged.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getSourcePaged")]
        [SwaggerResponse(typeof(PagedSetDto<SourceDto>))]
        public async Task<IActionResult> GetSourceListPaged([FromBody] PagingOptionsDto options, CancellationToken cancellationToken)
        {
            var sourcesPaged = await _bus.Send(new GetSourcePagedQuery()
            {
                Options = options
            }, cancellationToken);

            return new ObjectResult(sourcesPaged);
        }

        /// <summary>
        /// Adds a source to database.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(typeof(bool))]
        public async Task<IActionResult> InsertSource([FromBody] SourceDto source, CancellationToken cancellationToken)
        {
            await _bus.Send(new InsertSourceCommand()
            {
                Source = source
            }, cancellationToken);

            return new NoContentResult();
        }

        /// <summary>
        /// Updates the source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerResponse(typeof(bool))]
        public async Task<IActionResult> UpdateSource([FromBody] SourceDto source, CancellationToken cancellationToken)
        {
            await _bus.Send(new UpdateSourceCommand()
            {
                Source = source
            }, cancellationToken);

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes the source.
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{sourceId}")]
        [SwaggerResponse(typeof(bool))]
        public async Task<IActionResult> DeleteSource(int sourceId)
        {
            await _bus.Send(new DeleteSourceCommand()
            {
                Id = sourceId
            });
            return new NoContentResult();
        }
    }
}