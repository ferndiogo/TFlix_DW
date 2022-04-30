namespace TFlix.Models
{
    public class Utilizador
    {
        public Utilizador() { 
        
        }

        public int idUt { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string NIF { get; set; }

        public string Morada { get; set; }
    }
}
