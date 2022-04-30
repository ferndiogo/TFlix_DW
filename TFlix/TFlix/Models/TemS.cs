namespace TFlix.Models
{
    public class TemS
    {
        public TemS()
        {
            Subscricoes = new HashSet<Subscricao>();
            Serie = new HashSet<Series>();
        }

        public ICollection<Subscricao> Subscricoes { get; set; } 
        public ICollection<Series> Serie { get; set; }
    }
}
