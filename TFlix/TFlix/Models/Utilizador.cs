namespace TFlix.Models
{
    public class Utilizador
    {
        public Utilizador() {

            Administradores = new HashSet<Admin>();

            Subscricoes = new HashSet<Subscricao>();

            Aluguer = new HashSet<Aluga>();
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string NIF { get; set; }

        public string Morada { get; set; }

        public ICollection<Admin> Administradores { get; set; }

        public ICollection<Subscricao> Subscricoes { get; set; }

        public ICollection<Aluga> Aluguer { get; set; }
    }
}
