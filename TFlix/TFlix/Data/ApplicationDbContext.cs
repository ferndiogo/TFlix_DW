using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TFlix.Models;

namespace TFlix.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //Configure domain classes using modelBuilder here   

        //    modelBuilder.Entity<Aluga>()
        //        .HasKey(o => new { o.Utilizador, o.Filme });

        //    modelBuilder.Entity<Gere>()
        //        .HasKey(p => new { p.Admin, p.Utilizador });

        //    modelBuilder.Entity<TemF>()
        //        .HasKey(v => new { v.Subcricao, v.Filme });

        //    modelBuilder.Entity<TemS>()
        //        .HasKey(j => new { j.Subcricao, j.Serie });

        //}

        // define table on the database

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Gere> Gestao { get; set; }
        public DbSet<Subscricao> Subscricoes { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<TemF> SubFilme { get; set; }
        public DbSet<Aluga> Aluguers { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<TemS> SubSerie { get; set; }

    }

}