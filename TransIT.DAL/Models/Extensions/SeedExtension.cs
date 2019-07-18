using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Extensions
{
    public static class SeedExtension
    {
        public static ModelBuilder SeedStates(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>().HasData(
                new State { Id = 1, Name = "NEW", TransName = "Нова" },
                new State { Id = 2, Name = "VERIFIED", TransName = "Верифіковано" },
                new State { Id = 3, Name = "REJECTED", TransName = "Відхилено" },
                new State { Id = 4, Name = "TODO", TransName = "До виконання" },
                new State { Id = 5, Name = "EXECUTING", TransName = "В роботі" },
                new State { Id = 6, Name = "DONE", TransName = "Готово" },
                new State { Id = 7, Name = "CONFIRMED", TransName = "Підтверджено" },
                new State { Id = 8, Name = "UNCONFIRMED", TransName = "Не підтверджено" });
            return modelBuilder;
        }
    }
}
