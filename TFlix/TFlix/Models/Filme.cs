using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFlix.Models
{
    /// <summary>
    /// Descreve os filmes disponíveis
    /// </summary>
    public class Filme
    {
        public Filme() {

            Aluguer = new HashSet<Aluga>();

            Subscricoes = new HashSet<Subscricao>();
        }

        /// <summary>
        /// PK da tabela de filmes
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Título do filme
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [StringLength(30, ErrorMessage = "O {0} não pode ter mais do que {1} carateres.")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        /// <summary>
        /// Imagem do filme
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")]
        [Display(Name = "Imagem")]
        public string Imagem { get; set; }

        /// <summary>
        /// Sinopse do filme
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")]
        [StringLength(500, ErrorMessage = "O {0} não pode ter mais do que {1} carateres.")]
        [Display(Name = "Sinopse")]
        public string Sinopse { get; set; }

        /// <summary>
        /// Data de criação do filme
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")]
        [Display(Name = "Data de Criação")]
        public string DataCriacao { get; set; }

        /// <summary>
        /// Classificação do filme
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")]
        [Display(Name = "Classificação")]
        [RegularExpression("[0-5]", ErrorMessage = "A {0} deve-se encontrar entre 0-5.")]
        public int Classificacao { get; set; }

        /// <summary>
        /// Elenco do filme
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [Display(Name = "Elenco")]
        [StringLength(50, ErrorMessage = "O {0} não pode ter mais do que {1} carateres.")]
        public string Elenco { get; set; }

        /// <summary>
        /// Genero da série
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [Display(Name = "Genero")]
        [StringLength(20, ErrorMessage = "O {0} não pode ter mais do que {1} carateres.")]
        public string Genero { get; set; }

        /// <summary>
        /// Lista de aluguers
        /// </summary>
        public ICollection<Aluga> Aluguer { get; set; }

        /// <summary>
        /// Lista de subscrições
        /// </summary>
        public ICollection<Subscricao> Subscricoes { get; set; }
    }
}
