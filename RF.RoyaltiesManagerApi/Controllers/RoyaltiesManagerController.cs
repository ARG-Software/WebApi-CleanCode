using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RF.Domain.Dto;
using RF.Services.Interfaces.ETL;

namespace RF.RoyaltiesManagerApi.Controllers
{
    /// <summary>
    ///  Statement Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/RoyaltiesManager")]
    [ApiController]
    public class RoyaltiesManagerController : Controller
    {
        /// <summary>
        /// The statement service
        /// </summary>
        private readonly IETLService _etlService;

        /// <summary>
        /// The background job client
        /// </summary>
        private readonly IBackgroundJobClient _backgroundJobClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoyaltiesManagerController"/> class.
        /// </summary>
        /// <param name="etlService">The elt service.</param>
        /// <param name="backgroundJobClient"></param>
        public RoyaltiesManagerController(IETLService etlService, IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
            _etlService = etlService;
        }

        /// <summary>
        /// Uploads the statement.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="templateId">The template id for processing the royalties</param>
        /// <param name="paymentId">The payment id for processing the royalties</param>
        /// <param name="tags">The tags to mark the job with</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, typeof(string))]
        public async Task<IActionResult> ProcessStatement(IFormFile file, [FromHeader] int templateId, [FromHeader] int paymentId, [FromHeader] string[] tags)
        {
            if (templateId == 0 || paymentId == 0)
            {
                var message = (templateId == 0)
                    ? "Template Id is not valid, equal to 0"
                    : "Payment Id is not valid, equal to 0";
                return BadRequest(message);
            }

            var filePath = Path.GetTempFileName();

            if (file == null || file.Length <= 0)
            {
                return BadRequest("File Is Empty Or Null");
            }

            var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            try
            {
                var statementListParsed = await _etlService.ParseStatementFile(templateId, stream);
                var jobName = file.FileName + " processment";
                var royaltiesManagerProcessmentDto = new RoyaltyManagerProcessmentDto
                {
                    PaymentId = paymentId,
                    TemplateId = templateId,
                    Tags = tags,
                    StatementList = statementListParsed
                };

                _backgroundJobClient.Enqueue<IETLService>(x => x.ProcessFileInBackground(jobName, royaltiesManagerProcessmentDto, null));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return new ObjectResult("Statement added to queue to be processed");
        }
    }
}