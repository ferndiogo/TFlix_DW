using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
    public class Aluga
    {
        public Aluga()
        {
            Utilizdores = new HashSet<Utilizador>();

            Filme = new HashSet<Filmes>();

        }

        [ForeignKey("Utilizador")]
        public int idUtFK { get; set; }
        public int idUt { get; set; }

        [ForeignKey("Filmes")]
        public int idFilmeFK { get; set; }
        public int idFilme { get; set; }

        public double Preco { get; set; }

        public int TempoAluguer { get; set; }

        public ICollection<Utilizador> Utilizdores { get; set; }

        public ICollection<Filmes> Filme { get; set; }

    }



}
