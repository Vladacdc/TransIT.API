using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.BLL.Helpers.FileStorageLogger.FileStorageInterface;
using TransIT.BLL.Helpers.FileStorageLogger;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER,ANALYST")]
    public class DocumentController : DataController<Document, DocumentDTO>
    {
        private readonly IDocumentService _documentService;
        private readonly IFileStorageLogger _storageLogger;

        public DocumentController(
            IMapper mapper,
            IDocumentService documentService,
            IFilterService<Document> odService
            ) : base(mapper, documentService, odService)
        {
            _documentService = documentService;
            _storageLogger = LoggerProviderFactory.GetFileStorageLogger();
        }

        [HttpGet("~/api/v1/" + nameof(IssueLog) + "/{issueLogId}/" + nameof(Document))]
        public async virtual Task<IActionResult> GetByIssueLog(int issueLogId)
        {
            var result = await _documentService.GetRangeByIssueLogIdAsync(issueLogId);
            return result != null
                ? Json(_mapper.Map<IEnumerable<DocumentDTO>>(result))
                : (IActionResult) BadRequest();
        }

        [HttpGet("~/api/v1/" + nameof(Document) + "/{id}/file")]
        public async virtual Task<IActionResult> DownloadFile(int id)
        {
            var result = await _documentService.GetAsync(id);
            byte[] fileData = _storageLogger.Download(result.Path);
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(Path.GetFileName(result.Path), out contentType))
            {
                contentType = "application/octet-stream";
            }
            return File(fileData, contentType);
        }


        [HttpPost]
        public override async Task<IActionResult> Create([FromForm] DocumentDTO document)
        {
            if (document.File == null&& !(document.File.Length > 0))
                return Content("file not selected");
            var provider = new FileExtensionContentTypeProvider();

            _ = provider.TryGetContentType(Path.GetFileName(document.File.FileName), out string contentType);
            if (contentType!=  "application/pdf") return Content("format is not pdf");

            document.Path = _storageLogger.Create(document.File);
            var entity = _mapper.Map<Document>(document);
            var userId = GetUserId();

             

            var createdEntity = await _documentService.CreateAsync(entity);

            return createdEntity != null
                ? CreatedAtAction(nameof(Create), _mapper.Map<DocumentDTO>(createdEntity))
                : (IActionResult)BadRequest();
        }
        [HttpDelete("~/api/v1/" + nameof(Document) + "/{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            await _documentService.DeleteAsync(id);
            return NoContent();
        }


    }
}
