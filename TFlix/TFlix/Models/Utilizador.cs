using System.ComponentModel.DataAnnotations;

namespace TFlix.Models
{
    /// <summary>
    /// Descreve os dados do utilizador
    /// </summary>
    public class Utilizador
    {
        public Utilizador()
        {

            Subscricoes = new HashSet<Subscricao>();

            Aluguer = new HashSet<Aluga>();
        }

        /// <summary>
        /// PK da tabela dos utilizadores
        /// </summary>
        public int Id { get; set; }


        public string Morada { get; set; }

        /// <summary>
        /// País do utilizador
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [Display(Name = "País")]
        public string Pais { get; set; }

        /// <summary>
        /// Código Postal do utilizador
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [Display(Name = "Código Postal")]
        [RegularExpression("[0-9]{4}[-][0-9]{3}", ErrorMessage = "O {0} deve ter o seguinte formato: xxxx-xxx.")]
        public string CodPostal { get; set; }

        /// <summary>
        /// Seexo do utilizador
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [Display(Name = "Sexo")]
        [RegularExpression("[MmFf]", ErrorMessage = "Só pode usar F, ou M, no campo {0}")]
        public string Sexo { get; set; }

        /// <summary>
        /// Data de nascimento do utilizador
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DataNasc { get; set; }

        /// <summary>
        /// Define os utilizadores subscritos
        /// </summary>
        public ICollection<Subscricao> Subscricoes { get; set; }

        /// <summary>
        /// Define os utilizadores que realizaram aluguers
        /// </summary>
        public ICollection<Aluga> Aluguer { get; set; }

        /// <summary>
        /// FK usada para conectar 'business data' à 'authetication DB'
        /// </summary>
        [Display(Name = "ID Utilizador")]
        public string UserID { get; set; }

        /// <summary>
        /// FK usada para conectar 'business data' à 'authetication DB'
        /// </summary>
        [Display(Name = "Tipo de Utilizador")]
        public string UserF { get; set; }

    }
}
