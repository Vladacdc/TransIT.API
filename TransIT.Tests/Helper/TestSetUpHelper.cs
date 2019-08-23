using System;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models;

namespace TransIT.Tests.Helper
{
    public static class TestSetUpHelper
    {
        public static TransITDBContext CreateDbContext()
        {
            return new TransITDBContext(
                new DbContextOptionsBuilder<TransITDBContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .EnableSensitiveDataLogging()
                    .Options
            );
        }
    }
}