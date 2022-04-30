using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
    public class TemS
    {
        public int Id { get; set; }

        [ForeignKey("Subscricao")]
        public int SubcricaoFK { get; set; }
        public Subscricao Subcricao { get; set; }

        [ForeignKey("Serie")]
        public int SerieFK { get; set; }
        public Serie Serie { get; set; }


    }
}
