using System.ComponentModel.DataAnnotations;

namespace TFlix.Models
{
    /// <summary>
    /// Descreve os dados do utilizador
    /// </summary>
    public class Utilizador
    {
        public Utilizador() {

            Subscricoes = new HashSet<Subscricao>();

            Aluguer = new HashSet<Aluga>();
        }

        /// <summary>
        /// PK da tabela dos utilizadores
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do utilizador
        /// </summary>
        /// [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [StringLength(30, ErrorMessage = "O {0} não pode ter mais do que {1} carateres.")]
        [Display(Name = "Nome")]
        [RegularExpression("[A-ZÂÓÍa-záéíóúàèìòùâêîôûãõäëïöüñç '-]+", ErrorMessage = "Só pode escrever letras no {0}")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        public string Nome { get; set; }

        /// <summary>
        /// Email do utilizador
        /// </summary>
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Escreva um {0} válido, por favor.")]
        public string Email { get; set; }

        /// <summary>
        /// NIF do utilizador
        /// </summary>
        /// [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [Display(Name = "NIF")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "O {0} tem de ter 9 carateres.")]
        [RegularExpression("[123578][0-9]{8}", ErrorMessage = "O {0} deve começar por 1,2,3,5,7,8 seguido de 8 digitos numéricos.")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        public string NIF { get; set; }

        /// <summary>
        /// Morada do utilizador
        /// </summary>
        [Display(Name = "Morada")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
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
        public string UserID { get; set; }

        /// <summary>
        /// FK usada para conectar 'business data' à 'authetication DB'
        /// </summary>
        public string UserF { get; set; }
        
    }
}
