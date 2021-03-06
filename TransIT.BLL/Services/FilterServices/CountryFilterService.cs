﻿using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class CountryFilterService : BaseFilterService<Country, CountryDTO>
    {
        public CountryFilterService(IQueryRepository<Country> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
