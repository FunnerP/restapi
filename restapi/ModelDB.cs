using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace restapi
{
    public class ModelDB : DbContext
    {
        public ModelDB(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Passanger>? Passangers { get; set; }
        public DbSet<Registration>? Registrations { get; set; }
        public DbSet<Baggage>? Baggages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Baggage>().HasData(
                new Baggage { Id = 1, Weight = "1" },
                new Baggage { Id = 2, Weight = "1" }
                );
            modelBuilder.Entity<Passanger>().HasData(
                new Passanger
                {
                    Id = 1,
                    Name = "Гадик",
                    FirstName = "Петров",
                    LastName = "Петрович",
                    RegID = 1
                },
                new Passanger
                {
                    Id = 2,
                    Name = "Мразик",
                    FirstName = "Ифанов",
                    LastName = "Иванович",
                    RegID = 2
                }
                );
            //        modelBuilder.Entity<User>().HasData(
            //            new User { id=1, EMail= "kosha@mail.ru", Password = "123456" },
            //new User {id=1, EMail = "zinovievmaksim@mail.ru", Password = "11111" }
            //            );
        }
    }
}