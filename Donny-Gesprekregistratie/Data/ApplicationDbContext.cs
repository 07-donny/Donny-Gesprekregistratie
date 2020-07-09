using System;
using System.Collections.Generic;
using System.Text;
using Donny_Gesprekregistratie.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Donny_Gesprekregistratie.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Gesprek>()
            .HasMany(r => r.Reacties)
            .WithOne(g => g.Gesprek)
            .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Gesprek> Gesprek { get; set; }

        public DbSet<Reactie> Reactie { get; set; }
    }
}