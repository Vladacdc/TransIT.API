using Microsoft.EntityFrameworkCore;
using System;
using TransIT.DAL.Models;

namespace TransIT.Tests
{
    public class DbContextFromMemory : TransITDBContext
    {
        public DbContextFromMemory() : base(
            new DbContextOptionsBuilder<TransITDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options
            )
        {

        }
    }
}
