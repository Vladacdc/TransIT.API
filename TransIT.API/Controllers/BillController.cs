using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ENGINEER,REGISTER,ANALYST")]
    public class BillController : DataController<int, Bill, BillDTO>
    {
        private readonly IBillService _billService;
        
        public BillController(
        IMapper mapper, 
        IBillService billService,
        IFilterService<int, Bill> odService
        ) : base(mapper, billService, odService)
        {
            _billService = billService;
        }
    }
}
