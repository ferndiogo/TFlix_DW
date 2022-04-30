using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
    public class Subscricao
    {
        public Subscricao()
        {
            Series = new HashSet<Serie>();
        }

        public int Id { get; set; }

        [ForeignKey("Utilizador")]
        public int UtilizadorFK { get; set; }
        public Utilizador Utilizador { get; set; }

        public int Duracao { get; set; }

        public double Preco { get; set; }

        public DateTime DataSub { get; set; }

        public ICollection<Serie> Series { get; set; }

    }
}

