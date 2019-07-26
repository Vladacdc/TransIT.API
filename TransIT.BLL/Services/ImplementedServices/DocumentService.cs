using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.StaticFiles;
using TransIT.BLL.DTOs;
using TransIT.BLL.Helpers.FileStorageLogger;
using TransIT.BLL.Helpers.FileStorageLogger.FileStorageInterface;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Document CRUD service
    /// </summary>
    /// <see cref="IDocumentService"/>
    public class DocumentService : IDocumentService
    {
        private readonly IFileStorageLogger _storageLogger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="mapper">Mapper</param>
        public DocumentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageLogger = LoggerProviderFactory.GetFileStorageLogger();
        }

        public async Task<IEnumerable<DocumentDTO>> GetRangeByIssueLogIdAsync(int issueLogId)
        {
            return (await _unitOfWork.DocumentRepository.GetAllAsync(i => i.IssueLogId == issueLogId)).AsQueryable()
                .ProjectTo<DocumentDTO>();
        }

        public async Task<DocumentDTO> GetAsync(int id)
        {
            return _mapper.Map<DocumentDTO>(await _unitOfWork.DocumentRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<DocumentDTO>> GetRangeAsync(uint offset, uint amount)
        {
            return (await _unitOfWork.DocumentRepository.GetRangeAsync(offset, amount))
                .AsQueryable().ProjectTo<DocumentDTO>();
        }

        public async Task<IEnumerable<DocumentDTO>> SearchAsync(string search)
        {
            var documents = await _unitOfWork.DocumentRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
                );

            return documents.ProjectTo<DocumentDTO>();
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

        public async Task<DocumentDTO> CreateAsync(DocumentDTO documentDTO, int? userId=null)
        {
            var provider = new FileExtensionContentTypeProvider();

            _ = provider.TryGetContentType(Path.GetFileName(documentDTO.File.FileName), out string contentType);

            documentDTO.ContentType = contentType;
            documentDTO.Path = _storageLogger.Create(documentDTO.File);

            if (userId != null)
            {
                documentDTO.Mod.Id = (int)userId;
                documentDTO.Create.Id = (int)userId;
            }

            var model = _mapper.Map<Document>(documentDTO);

            await _unitOfWork.DocumentRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return await GetAsync(model.Id);
        }

        public async Task<DocumentDTO> UpdateAsync(DocumentDTO dto)
        {
            var model = _mapper.Map<Document>(dto);
            _unitOfWork.DocumentRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return dto;
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