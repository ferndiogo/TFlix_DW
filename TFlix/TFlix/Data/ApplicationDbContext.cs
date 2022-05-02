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

        // define table on the database

        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Subscricao> Subscricoes { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Aluga> Aluguers { get; set; }

    }

}