namespace TFlix.Models
{
    public class Utilizador
    {
        public Utilizador() {

            Subscricoes = new HashSet<Subscricao>();

            Aluguer = new HashSet<Aluga>();
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string NIF { get; set; }

        public string Morada { get; set; }

        public string Pais { get; set; }

        public string CodPostal { get; set; }

        public string Sexo { get; set; }

        public string DataNasc { get; set; }

        public ICollection<Subscricao> Subscricoes { get; set; }

        public ICollection<Aluga> Aluguer { get; set; }

        //########################################################################
        /// <summary>
        /// this FK is used to connect our 'business data' to 'authetication DB'
        /// </summary>
        public string UserID { get; set; }
        //########################################################################

    }
}
