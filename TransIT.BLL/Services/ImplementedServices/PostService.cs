using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TransIT.BLL.DTOs;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Post CRUD service
    /// </summary>
    /// <see cref="IPostService"/>
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PostDTO> GetAsync(int id)
        {
            return _mapper.Map<PostDTO>(await _unitOfWork.PostRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<PostDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await _unitOfWork.PostRepository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<PostDTO>>(entities);
        }

        public async Task<IEnumerable<PostDTO>> SearchAsync(string search)
        {
            var posts = await _unitOfWork.PostRepository.SearchExpressionAsync(
                search
                    .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToUpperInvariant())
                );

            return _mapper.Map<IEnumerable<PostDTO>>(await posts.ToListAsync());
        }

        public async Task<PostDTO> CreateAsync(PostDTO dto)
        {
            var model = _mapper.Map<Post>(dto);
            await _unitOfWork.PostRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return await GetAsync(model.Id);
        }

        public async Task<PostDTO> UpdateAsync(PostDTO dto)
        {
            var model = _mapper.Map<Post>(dto);
            _unitOfWork.PostRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.PostRepository.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}