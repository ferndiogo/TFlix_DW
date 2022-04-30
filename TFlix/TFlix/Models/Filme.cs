namespace TFlix.Models
{
    public class Filme
    {
        public Filme() {
            TemFilmes = new HashSet<TemF>();

            Aluguer = new HashSet<Aluga>();
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Imagem { get; set; }

        public string Sinopse { get; set; }

        public DateTime DataC { get; set; }

        public string Diretor { get; set; }

        public string Atores { get; set; }

        public string Género { get; set; }

        public ICollection<TemF> TemFilmes { get; set; }

        public ICollection<Aluga> Aluguer { get; set; }
    }
}
