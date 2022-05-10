using System.ComponentModel.DataAnnotations;

namespace TFlix.Models
{
    public class Utilizador
    {
        public Utilizador() {

            Administradores = new HashSet<Admin>();

            Subscricoes = new HashSet<Subscricao>();

            Aluguer = new HashSet<Aluga>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage ="O {0} é de preenchimento obrigatório.")]
        [StringLength(30, ErrorMessage ="O {0} não pode ter mais de {1} caractéres.")]
        [Display(Name = "Nome")]
        [RegularExpression("[A-ZÂÓÍa-záéíóúàèìòùâêîôûãõäëïöüñç '-]+", ErrorMessage = "Só pode escrever letras no {0}")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="O {0} é de preenchimento obrigatório.")]
        [EmailAddress(ErrorMessage ="Escreva um {0} válido, por favor.")]
        public string Email { get; set; }

        [Required(ErrorMessage ="O {0} é de preenchimento obrigatório.")]
        [MinLength(8, ErrorMessage ="A {0} tem de ter mais de {1} caractéres.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "O {0} tem de ter {1} carateres.")]
        [RegularExpression("[123578][0-9]{8}", ErrorMessage = "O {0} deve começar por 1,2,3,5,7,8 seguido de 8 digitos numéricos.")]
        public string NIF { get; set; }

        [Required(ErrorMessage ="O {0} é de preenchimento obrigatório.")]
        
        public string Morada { get; set; }

        public ICollection<Admin> Administradores { get; set; }

        public ICollection<Subscricao> Subscricoes { get; set; }

        public ICollection<Aluga> Aluguer { get; set; }
    }
}
