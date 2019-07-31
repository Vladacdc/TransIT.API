using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.DTOs;
using TransIT.BLL.Factory;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "ADMIN,ENGINEER,ANALYST")]
    public class DocumentController : FilterController<DocumentDTO>
    {
        private readonly IServiceFactory _serviceFactory;

        public DocumentController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        [HttpGet("~/api/v1/IssueLog/{issueLogId}/Document")]
        public async Task<IActionResult> GetByIssueLog(int issueLogId)
        {
            var result = await _serviceFactory.DocumentService.GetRangeByIssueLogIdAsync(issueLogId);

            if (result != null)
            {
                return Json(result);
            }

            return BadRequest();
        }

        [HttpGet("~/api/v1/Document/{id}/file")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var document = await _serviceFactory.DocumentService.GetDocumentWithData(id);

            return File(document.Data, document.ContentType);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DocumentDTO documentDto)
        {
            if (documentDto.File == null && documentDto.File?.Length == 0)
            {
                return Content("file not selected");
            }

            var createdEntity = await _serviceFactory.DocumentService.CreateAsync(documentDto);

            if (documentDto.ContentType != "application/pdf")
            {
                return Content("format is not pdf");
            }

            if (createdEntity != null)
            {
                return CreatedAtAction(nameof(Create), createdEntity);
            }

            return BadRequest();
        }

        [HttpDelete("~/api/v1/Document/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceFactory.DocumentService.DeleteAsync(id);
            return NoContent();
        }
    }
}