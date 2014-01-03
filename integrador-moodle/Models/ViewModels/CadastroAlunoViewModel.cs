using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace integrador_moodle.Models.ViewModels
{
    public class CadastroAlunoViewModel
    {
        [StringLength(100, ErrorMessage = "O tamanho máximo são 50 caracteres."), Required]
        public string nome { get; set; }

        [StringLength(100, ErrorMessage = "O tamanho máximo são 50 caracteres."), Required]
        public string sobrenome { get; set; }

        [StringLength(11, ErrorMessage = "O tamanho máximo são 11 caracteres."), Required]
        public string cpf { get; set; }

        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public System.DateTime dataNascimento { get; set; }

        [Required]
        public int sexo { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string celular { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string telefoneFixo { get; set; }

        [Required]
        public string endereco { get; set; }

        [Required]
        public string bairro { get; set; }

        [Required]
        public string cidade { get; set; }

        [Required]
        public string cep { get; set; }

        [Required]
        public string estado { get; set; }

        [Required]
        public string senha { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
    }
}