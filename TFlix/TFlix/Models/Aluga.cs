using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
    public class Aluga
    {
        [Key, ForeignKey(nameof(Filme))]
        public int FilmeFK { get; set; }
        public Filme Filme { get; set; }

        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFK { get; set; }
        public Utilizador Utilizador { get; set; }

        public double Preco { get; set; }

        public int TempoAluguer { get; set; }

        

    }



}
