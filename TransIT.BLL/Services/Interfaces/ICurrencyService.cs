﻿using System.Threading.Tasks;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Services.Interfaces
{
    /// <summary>
    /// Currency model CRUD
    /// </summary>
    public interface ICurrencyService : ICrudService<CurrencyDTO>
    {
        Task<CurrencyDTO> CreateAsync(int userId, CurrencyDTO dto);

        Task<CurrencyDTO> UpdateAsync(int userId, CurrencyDTO dto);
    }
}
