﻿using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        /// <summary>
        /// Data de inicio da subscrição
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")]
        [Display(Name = "Data de Início")]
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

    }



}
