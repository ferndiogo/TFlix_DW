using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
    public class Aluga
    {
        //public int Id { get; set; }

        [ForeignKey("Utilizador")]
        public int UtilizadorFK { get; set; }
        public Utilizador Utilizador { get; set; }

        [Key, ForeignKey("Filme")]
        public int FilmeFK { get; set; }
        public Filme Filme { get; set; }

        public double Preco { get; set; }

        public int TempoAluguer { get; set; }

        

    }



}
