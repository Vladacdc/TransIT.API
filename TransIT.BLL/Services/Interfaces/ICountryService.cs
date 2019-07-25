﻿using System.Threading.Tasks;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Services.Interfaces
{
    /// <summary>
    /// Country model CRUD
    /// </summary>
    public interface ICountryService : ICrudService<CountryDTO>
    {
        Task<CountryDTO> CreateAsync(int userId, CountryDTO dto);

        Task<CountryDTO> UpdateAsync(int userId, CountryDTO dto);
    }
}
