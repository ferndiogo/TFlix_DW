using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
    public class Gere
    {
        public Gere()
        {
            Admins = new HashSet<Admin>();
            Utilizadores = new HashSet<Utilizador>();
        }

        [ForeignKey("Admin")]
        public int AdminFK { get; set; }
        public Admin Admin { get; set; }

        [ForeignKey("Utilizador")]
        public int UtilizadorFK { get; set; }
        public Utilizador Utilizador { get; set; }

        public ICollection<Admin> Admins { get; set; }  
        public ICollection<Utilizador> Utilizadores { get; set; }
    }
}
