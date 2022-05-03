namespace TFlix.Models
{
    public class Serie
    {
        public Serie()
        {
            Subscricoes = new HashSet<Subscricao>();

        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Imagem { get; set; }

        public string Sinopse { get; set; }

        public DateTime Data { get; set; }

        public int Classificacao { get; set; }

        public string Elenco { get; set; }

        public string Genero { get; set; }

        public ICollection<Subscricao> Subscricoes { get; set; }
    }
}
