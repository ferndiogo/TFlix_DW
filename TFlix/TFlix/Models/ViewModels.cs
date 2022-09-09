using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
    public class AlugaViewModel { 
    
        public int Id { get; set; }
        public string NomeFilme { get; set; }
        public string NomeUtilizador { get; set; }
        public int UtilizadorFK { get; set; }
        public int FilmeFK { get; set; }
        public string AuxPreco { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

    }


    public class SubsViewModel
    {

        public int Id { get; set; }
        public string NomeUtilizador { get; set; }
        public int Duracao { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public ICollection<Serie> NomeSerie { get; set; }
        public ICollection<Filme> NomeFilme { get; set; }
        public string AuxPreco { get; set; }



    }
}
