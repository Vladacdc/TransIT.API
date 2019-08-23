using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.Tests.Helper;
using Xunit;
using Xunit.Abstractions;

namespace TransIT.Tests.Repository
{
    public class PartsInRepositoryTests
    {
        [Fact]
        public async Task PartsInRepository_Should_Add_Item()
        {
            // Arrange
            var context = TestSetUpHelper.CreateDbContext();
            var repository = new PartsInRepository(context);
            var currency = new Currency()
            {
                Id = 5,
                FullName = "Australian dollar",
            };
            var unit = new Unit()
            {
                Id = 4,
                Name = "wtf"
            };
            var part = new Part()
            {
                Id = 121,
                Name = "Артек шоколадний.",
                Manufacturer = new Manufacturer()
                {
                    Id = 1212,
                    Name = "Світоч"
                }
            };
            var expectedEntity = new PartIn()
            {
                Id = 20,
                Amount = 5,
                Batch = "abcdf12345",
                ArrivalDate = DateTime.Now,
                Price = 15.0f,
                CurrencyId = currency.Id,
                Currency = currency,
                UnitId = unit.Id,
                Unit = unit,
                Part = part,
                PartId = part.Id,
            };

            // Act
            await context.AddAsync(currency);
            await context.AddAsync(unit);
            await context.AddAsync(part);
            await context.AddAsync(part.Manufacturer);
            await context.SaveChangesAsync();
            await repository.AddAsync(expectedEntity);
            await context.SaveChangesAsync();
            var actualEntity = await repository.GetByIdAsync(expectedEntity.Id);

            // Assert
            Assert.Equal(expectedEntity, actualEntity);
        }

        [Fact]
        public async Task PartsInRepository_Should_Update_Item()
        {
            // Arrange
            var context = TestSetUpHelper.CreateDbContext();
            var repository = new PartsInRepository(context);
            var currency = new Currency()
            {
                Id = 5,
                FullName = "Australian dollar",
            };
            var newCurrency = new Currency()
            {
                Id = 55,
                FullName = "Гривня",
            };
            var unit = new Unit()
            {
                Id = 4,
                Name = "wtf"
            };
            var newUnit = new Unit()
            {
                Id = 44,
                Name = "Метр квадратний"
            };
            var manufacturer = new Manufacturer()
            {
                Id = 1212,
                Name = "Світоч"
            };
            var part = new Part()
            {
                Id = 121,
                Name = "Артек шоколадний.",
                Manufacturer = manufacturer
            };
            var newPart = new Part()
            {
                Id = 122,
                Name = "Артек горіховий.",
                Manufacturer = manufacturer
            };
            var expectedEntity = new PartIn()
            {
                Id = 20,
                Amount = 5,
                Batch = "abcdf12345",
                ArrivalDate = DateTime.Now,
                Price = 15.0f,
                CurrencyId = currency.Id,
                Currency = currency,
                UnitId = unit.Id,
                Unit = unit,
                Part = part,
                PartId = part.Id
            };

            // Act
            await context.AddAsync(manufacturer);
            await context.AddRangeAsync(new Currency[] { currency, newCurrency });
            await context.AddRangeAsync(new Unit[] { unit, newUnit });
            await context.AddRangeAsync(new Part[] { part, newPart });
            await context.SaveChangesAsync();

            await repository.AddAsync(expectedEntity);
            await context.SaveChangesAsync();

            expectedEntity.Price = 10;
            expectedEntity.UnitId = newUnit.Id;
            expectedEntity.CurrencyId = newCurrency.Id;
            expectedEntity.PartId = newPart.Id;

            repository.Update(expectedEntity);

            await context.SaveChangesAsync();

            var actualEntity = await repository.GetByIdAsync(expectedEntity.Id);

            // Assert
            Assert.Equal(expectedEntity, actualEntity);
        }
    }
}
