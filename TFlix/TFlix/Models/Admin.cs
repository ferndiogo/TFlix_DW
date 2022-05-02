using TFlix.Models;

namespace TFlix
{
    public class Admin
    {
        public Admin()
        {
            Utilizadores = new HashSet<Utilizador>();
        }


        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<Utilizador> Utilizadores { get; set; }
    }
}
