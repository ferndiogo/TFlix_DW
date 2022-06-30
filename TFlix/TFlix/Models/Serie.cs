using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
    public class Serie
    {
        public Serie()
        {
            Subscricoes = new HashSet<Subscricao>();

        }

        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Imagem { get; set; }

        public string Sinopse { get; set; }

        public string DataCriacao { get; set; }

        public int Classificacao { get; set; }

        public string Elenco { get; set; }

        public string Genero { get; set; }

        public int Temporada { get; set; }

        public int Episodio { get; set; }

        public ICollection<Subscricao> Subscricoes { get; set; }
    }
}
