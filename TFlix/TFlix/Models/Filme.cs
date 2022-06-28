using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
    public class Filme
    {
        public Filme() {

            Aluguer = new HashSet<Aluga>();

            Subscricoes = new HashSet<Subscricao>();
        }

        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Imagem { get; set; }

        public string Sinopse { get; set; }

        public DateTime DataCriacao { get; set; }

        public int Classificacao { get; set; }

        public string Elenco { get; set; }

        public string Genero { get; set; }

        [ForeignKey(nameof(Subscricao))]
        public int SubscricaoFK { get; set; }
        public Utilizador Subscricao { get; set; }

        public ICollection<Aluga> Aluguer { get; set; }

        public ICollection<Subscricao> Subscricoes { get; set; }
    }
}
