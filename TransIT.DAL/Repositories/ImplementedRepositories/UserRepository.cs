using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Models;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TransITDBContext _dbContext;

        public UserRepository(TransITDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void UpdateCurrentUserId(string newValue)
        {
            _dbContext.CurrentUserId = newValue;
        }
    }
}
