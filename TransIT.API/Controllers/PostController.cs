using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class PostController : DataController<int, Post, PostDTO>
    {
        private readonly IPostService _postService;
        
        public PostController(
            IMapper mapper, 
            IPostService postService,
            IFilterService<int, Post> odService
            ) : base(mapper, postService, odService)
        {
            _postService = postService;
        }
    }
}
