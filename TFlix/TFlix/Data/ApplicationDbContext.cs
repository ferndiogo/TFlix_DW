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
        public string Funcao { get; set; }
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

            modelBuilder.Entity<Serie>().HasData(
            new Serie
            {
                Id = 1,
                Titulo = "Ratched",
                Imagem = "Ratched.jpeg",
                Sinopse = "Mildred Ratched começa a trabalhar como enfermeira em um hospital psiquiátrico.",
                DataCriacao = "18 de Setembro de 2020",
                Classificacao = 4,
                Elenco = "Sarah Paulson, Finn Wittrock",
                Genero = "Drama",
                Temporada = 1,
                Episodio = 8
            },
            new Serie
            {
                Id = 2,
                Titulo = "Stranger Things",
                Imagem = "StrangerThings.jpg",
                Sinopse = "Um grupo de amigos se envolve em uma série de eventos sobrenaturais na pacata cidade de Hawkins.",
                DataCriacao = "15 de julho de 2016",
                Classificacao = 4,
                Elenco = "Millie Bobby Brown, Finn Wolfhard",
                Genero = "Terror",
                Temporada = 4,
                Episodio = 32
            }
            );

            modelBuilder.Entity<Filme>().HasData(
            new Filme
            {
                Id = 1,
                Titulo = "Doctor Strange in the Multiverse of Madness",
                Imagem = "DoctorStrange.jpeg",
                Sinopse = "O aguardado filme trata da jornada do Doutor Estranho rumo ao desconhecido.",
                DataCriacao = "5 de maio de 2022",
                Classificacao = 4,
                Elenco = "Elizabeth Olsen, Benedict Cumberbatch",
                Genero = "Terror",
            },
            new Filme
            {
                Id = 2,
                Titulo = "Interceptor",
                Imagem = "Interceptor.jpg",
                Sinopse = "Um grupo de amigos se envolve em uma série de eventos sobrenaturais na pacata cidade de Hawkins.",
                DataCriacao = "26 de maio de 2022",
                Classificacao = 3,
                Elenco = "Elsa Pataky, Luke Bracey",
                Genero = "Ação",
            }
            );

        }

        // define table on the database

        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Subscricao> Subscricoes { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Aluga> Aluguers { get; set; }

    }

}