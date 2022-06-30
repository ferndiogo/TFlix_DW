using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TFlix.Models;

namespace TFlix.Data
{

    /// <summary>
    /// class that represents new User data
    /// </summary>
    public class ApplicationUser : IdentityUser
    {

        /// <summary>
        /// personal name of user to be used at interface
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// registration date
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DataRegisto { get; set; }


    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// it executes code before the creation of model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // imports the previous execution of this method
            base.OnModelCreating(modelBuilder);

            // seed the Roles data
            modelBuilder.Entity<IdentityRole>().HasData(
              new IdentityRole { Id = "a", Name = "Administrador", NormalizedName = "ADMINISTRADOR" },
              new IdentityRole { Id = "c", Name = "Cliente", NormalizedName = "CLIENTE" }
              );

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