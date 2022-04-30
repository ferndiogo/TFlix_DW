using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
    public class Subscricao
    {
        public Subscricao(){
            Filme = new HashSet<Filmes>();
        }

        public int idSb { get; set; }

        //chave estrangeira
        public string idUt { get; set; }

        public int Duracao { get; set; }

        public double Preco { get; set; }

        public DateTime DataSub { get; set; }

        public ICollection<Filmes> Filme { get; set; }
    }
}

