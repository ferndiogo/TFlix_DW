namespace TFlix.Models
{
    public class Subscricao
    {
        public Subscricao(){
        }

        public int idSb { get; set; }

        //chave estrangeira
        public string idUt { get; set; }

        public int Duracao { get; set; }

        public int Preco { get; set; }

        public DateTime DataSub { get; set; }
    }
}
