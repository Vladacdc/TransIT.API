using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using TransIT.BLL.DTOs;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.UnitOfWork;
using TransIT.DAL.FileStorage;
using TransIT.BLL.Exceptions;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Document CRUD service
    /// </summary>
    /// <see cref="IDocumentService"/>
    public class DocumentService : IDocumentService
    {
        private readonly IFileStorage _storageLogger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="mapper">Mapper</param>
        public DocumentService(IUnitOfWork unitOfWork, IMapper mapper, IFileStorage fileStorage)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageLogger = fileStorage;
        }

        public async Task<IEnumerable<DocumentDTO>> GetRangeByIssueLogIdAsync(int issueLogId)
        {
            var entities = await _unitOfWork.DocumentRepository.GetQueryable()
                .Where(i => i.IssueLogId == issueLogId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<DocumentDTO>>(entities);
        }

        public async Task<DocumentDTO> GetAsync(int id)
        {
            return _mapper.Map<DocumentDTO>(await _unitOfWork.DocumentRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<DocumentDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await _unitOfWork.DocumentRepository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<DocumentDTO>>(entities);
        }

        public async Task<IEnumerable<DocumentDTO>> SearchAsync(string search)
        {
            var documents = await _unitOfWork.DocumentRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
                );

            return _mapper.Map<IEnumerable<DocumentDTO>>(await documents.ToListAsync());
        }

        public async Task<DocumentDTO> GetDocumentWithData(int documentId)
        {
            var result = await GetAsync(documentId);

            result.Data = _storageLogger.Download(result.Path);

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(Path.GetFileName(result.Path), out string contentType))
            {
                contentType = "application/octet-stream";
            }

            result.ContentType = contentType;

            return result;
        }

        public async Task<DocumentDTO> CreateAsync(DocumentDTO dto)
        {
            if (dto.File == null && dto.File?.Length == 0)
            {
                throw new EmptyDocumentException();
            }

            var provider = new FileExtensionContentTypeProvider();

            _ = provider.TryGetContentType(Path.GetFileName(dto.File.FileName), out string contentType);

            if (contentType != "application/pdf")
            {
                throw new DocumentContentException();
            }

                dto.ContentType = contentType;
            dto.Path = _storageLogger.Create(dto.File);


            var model = _mapper.Map<Document>(dto);

            await _unitOfWork.DocumentRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<DocumentDTO>(model);
        }

        public async Task<DocumentDTO> UpdateAsync(DocumentDTO dto)
        {
            var model = _mapper.Map<Document>(dto);

            _unitOfWork.DocumentRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<DocumentDTO>(model);
        }

        public async Task DeleteAsync(int id)
        {
            var documents = await _unitOfWork.DocumentRepository.GetByIdAsync(id);
            await Task.Run(() => _storageLogger.Delete(documents.Path));
            _unitOfWork.DocumentRepository.Remove(documents);
            await _unitOfWork.SaveAsync();
        }
    }
}