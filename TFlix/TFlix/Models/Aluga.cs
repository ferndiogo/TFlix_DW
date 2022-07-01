using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
 
    /// <summary>
    /// Descreve os aluguers feitos
    /// </summary>
    public class Aluga
    {
        /// <summary>
        /// PK da tabela de aluguers
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// FK para referenciar os filmes no aluguer
        /// </summary>
        [ForeignKey(nameof(Filme))]
        public int FilmeFK { get; set; }
        public Filme Filme { get; set; }

        /// <summary>
        /// FK para referenciar os utilizadores  que fizeram aluguers
        /// </summary>
        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFK { get; set; }
        public Utilizador Utilizador { get; set; }

        /// <summary>
        /// Preço da subscrição
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [Display(Name = "Preço")]
        [RegularExpression("[0-9]{4}[,][0-9]{2}", ErrorMessage = "O {0} deve ter o seguinte formato xxxx,xx.")]
        public double Preco { get; set; }

        /// <summary>
        /// Data de inicio da subscrição
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")]
        [Display(Name = "Data de Inicio")]
        public string DataInicio { get; set; }

        /// <summary>
        /// Data de fim da subscrição
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")]
        [Display(Name = "Data de Fim")]
        public string DataFim { get; set; }

    }



}
