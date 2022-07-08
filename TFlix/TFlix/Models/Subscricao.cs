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
        public int Duracao { get; set; }

        /// <summary>
        /// auxiliary attribute to help the app to collect the appointement's price
        /// </summary>
        [NotMapped]  // this anotation tells the EF that this attribute must not be represented on database
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [RegularExpression("4.99|8.99|80", ErrorMessage = "O {0} só pode ser. 4.99, 8.99 ou 80 correspondente ás diferentes subscrições.")]
        [Display(Name = "Preço")]
        public string AuxPreco { get; set; }

        /// <summary>
        /// Preço da subscrição
        /// </summary>
       public decimal Preco { get; set; }


        /// <summary>
        /// Data de inicio da subscrição
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")]
        [Display(Name = "Data de Inicio")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        /// <summary>
        /// Data de fim da subscrição
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")]
        [Display(Name = "Data de Fim")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
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

