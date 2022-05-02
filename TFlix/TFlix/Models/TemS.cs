using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
    public class TemS
    {
        //public int Id { get; set; }

        [Key, ForeignKey(nameof(Subscricao))]
        public int SubcricaoFK { get; set; }
        public Subscricao Subcricao { get; set; }

        [Key,ForeignKey(nameof(Serie))]
        public int SerieFK { get; set; }
        public Serie Serie { get; set; }


    }
}
