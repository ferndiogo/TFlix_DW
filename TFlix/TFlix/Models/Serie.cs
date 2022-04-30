namespace TFlix.Models
{
    public class Serie
    {
        public Serie()
        {
            TemSeries = new HashSet<TemS>();

        }


        public int Id { get; set; }

        public string Nome { get; set; }

        public string Imagem { get; set; }

        public string Sinopse { get; set; }

        public DateTime DataC { get; set; }

        public string Diretor { get; set; }

        public string Atores { get; set; }

        public string Género { get; set; }

        public ICollection<TemS> TemSeries { get; set; }
    }
}
