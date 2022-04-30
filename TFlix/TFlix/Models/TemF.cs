using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
    public class TemF
    {
        public TemF()
        {
            Filme = new HashSet<Filmes>();

            Sub = new HashSet<Subscricao>();

        }

        [ForeignKey("Subscricao")]
        public int idSubFK { get; set; }
        public Subscricao idSub { get; set; }

        [ForeignKey("Filmes")]
        public int idFilmeFK { get; set; }
        public Filmes idFilme { get; set; }

        public ICollection<Filmes> Filme { get; set; }

        public ICollection<Subscricao> Sub { get; set; }
    }
}
