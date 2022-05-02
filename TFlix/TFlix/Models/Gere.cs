using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
    public class Gere
    {
        //public int Id { get; set; }

        [Key, ForeignKey(nameof(Admin))]
        public int AdminFK { get; set; }
        public Admin Admin { get; set; }

        [Key, ForeignKey(nameof(Utilizador))]
        public int UtilizadorFK { get; set; }

        public Utilizador Utilizador { get; set; }
        
    }
}
