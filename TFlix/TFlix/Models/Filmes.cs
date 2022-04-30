namespace TFlix.Models
{
    public class Filmes
    {
        public Filmes() { 
        
        }

        public int idFilme { get; set; }

        public string Nome { get; set; }

        public string Imagem { get; set; }

        public string Sinopse { get; set; }

        public DateTime DataC { get; set; }

        public string Diretor { get; set; }

        public string Atores { get; set; }

        public string Género { get; set; }
    }
}
