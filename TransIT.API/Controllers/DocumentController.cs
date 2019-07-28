using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services.Interfaces;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using Microsoft.AspNetCore.Cors;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "ADMIN,ENGINEER,ANALYST")]
    public class DocumentController : Controller
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet("~/api/v1/" + nameof(IssueLog) + "/{issueLogId}/" + nameof(Document))]
        public async Task<IActionResult> GetByIssueLog(int issueLogId)
        {
            var result = await _documentService.GetRangeByIssueLogIdAsync(issueLogId);

            if (result != null)
            {
                return Json(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("~/api/v1/" + nameof(Document) + "/{id}/file")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var document = await _documentService.GetDocumentWithData(id);

            return File(document.Data, document.ContentType);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DocumentDTO documentDTO)
        {
            if (documentDTO.File == null && documentDTO.File.Length == 0)
            {
                return Content("file not selected");
            }

            var createdEntity = await _documentService.CreateAsync(documentDTO);

            if (documentDTO.ContentType != "application/pdf")
            {
                return Content("format is not pdf");
            }

            if (createdEntity != null)
            {
                return CreatedAtAction(nameof(Create), createdEntity);
            }
            else
            { 
                return BadRequest();
            }
        }

        [HttpDelete("~/api/v1/" + nameof(Document) + "/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _documentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
