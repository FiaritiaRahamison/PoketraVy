using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PoketraVy_backoffice.Models;

namespace PoketraVy_backoffice.Data
{
    public class PoketraVy_backofficeContext : DbContext
    {
        public PoketraVy_backofficeContext (DbContextOptions<PoketraVy_backofficeContext> options)
            : base(options)
        {
        }

        public DbSet<PoketraVy_backoffice.Models.Utilisateur> Utilisateurs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Utilisateur>().ToTable("utilisateur");
            modelBuilder.Entity<Budget>().ToTable("budget");
        }

        public DbSet<PoketraVy_backoffice.Models.Budget> Budgets { get; set; }
    }
}
