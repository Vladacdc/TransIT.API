using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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


        public DocumentService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageLogger = LoggerProviderFactory.GetFileStorageLogger();
        }

        public async Task<IEnumerable<DocumentDTO>> GetRangeByIssueLogIdAsync(int issueLogId)
        {
            List<DocumentDTO> documentDTOs = new List<DocumentDTO>();
            foreach (var i in await _unitOfWork.DocumentRepository.GetAllAsync(i => i.IssueLogId == issueLogId))
            {
                documentDTOs.Add(_mapper.Map<DocumentDTO>(i));
            }
            return documentDTOs;
        }
        public async Task<DocumentDTO> GetAsync(int id)
        {
            return _mapper.Map<DocumentDTO>(await _unitOfWork.DocumentRepository.GetByIdAsync(id));
        }
        public async Task<IEnumerable<DocumentDTO>> GetRangeAsync(uint offset, uint amount)
        {
            return (await _unitOfWork.DocumentRepository.GetRangeAsync(offset, amount)).AsQueryable().ProjectTo<DocumentDTO>();
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
        public async Task<DocumentDTO> CreateAsync(DocumentDTO dto)
        {
            var model = _mapper.Map<Document>(dto);
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


