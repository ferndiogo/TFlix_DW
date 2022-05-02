using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
    public class TemF
    {
        //public int Id { get; set; }

        [Key, ForeignKey("Subscricao")]
        public int SubcricaoFK { get; set; }
        public Subscricao Subscricao { get; set; }

        [Key, ForeignKey("Filme")]
        public int FilmeFK { get; set; }
        public Filme Filme { get; set; }

        
    }
}
