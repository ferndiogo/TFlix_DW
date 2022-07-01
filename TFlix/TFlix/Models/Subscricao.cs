using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
    /// <summary>
    /// Descreve as subscrições realizadas
    /// </summary>
    public class Subscricao
    {
        public Subscricao()
        {
            Series = new HashSet<Serie>();

            Filmes = new HashSet<Filme>();
        }

        /// <summary>
        /// PK da tabela das subscrições
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// FK para referenciar os utilizadores subscritos
        /// </summary>
        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFK { get; set; }
        public Utilizador Utilizador { get; set; }

        /// <summary>
        /// Duração da subscrição
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")]
        [Display(Name = "Duração")]
        [RegularExpression("[0-9]{20}", ErrorMessage = "Só pode escrever números no {0}")]
        public int Duracao { get; set; }

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
        public DateTime DataInicio { get; set; }

        /// <summary>
        /// Data de fim da subscrição
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")]
        [Display(Name = "Data de Fim")]
        public DateTime DataFim { get; set; }

        /// <summary>
        /// Define as séries que pertencem á subscrição
        /// </summary>
        public ICollection<Serie> Series { get; set; }

        /// <summary>
        /// Lista de filmes
        /// </summary>
        public ICollection<Filme> Filmes { get; set; }

    }
}

