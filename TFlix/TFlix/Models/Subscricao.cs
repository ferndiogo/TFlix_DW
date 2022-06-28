using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
    public class Subscricao
    {
        public Subscricao()
        {
            Series = new HashSet<Serie>();

            Filmes = new HashSet<Filme>();
        }

        public int Id { get; set; }

        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFK { get; set; }
        public Utilizador Utilizador { get; set; }

        public int Duracao { get; set; }

        public double Preco { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        [ForeignKey(nameof(Filme))]
        public int FilmeFK { get; set; }
        public Utilizador Filme { get; set; }

        [ForeignKey(nameof(Serie))]
        public int SerieFK { get; set; }
        public Utilizador Serie { get; set; }

        public ICollection<Serie> Series { get; set; }

        public ICollection<Filme> Filmes { get; set; }

    }
}

