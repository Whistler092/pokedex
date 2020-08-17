using Microsoft.EntityFrameworkCore;
using Pokedex.Entities;
using System;
using System.Collections.Generic;

namespace Pokedex.Core.Core
{
    public class PokedexContext : DbContext
    {
        public PokedexContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Trainer> Trainers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Trainer>().HasData(new List<Trainer>
            {
                new Trainer {
                    Id = Guid.NewGuid(),
                    Email = "iamramiroo@gmail.com",
                    FullName = "Ramiro Andr'es Bedoya Escobar"
                },
            });
        }
    }
}
