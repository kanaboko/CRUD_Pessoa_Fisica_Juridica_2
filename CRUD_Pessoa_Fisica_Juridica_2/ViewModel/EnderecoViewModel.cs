using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Pessoa_Fisica_Juridica_2.ViewModel
{
    public class EnderecoViewModel
    {
        public EnderecoViewModel()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Preencha o campo Logradouro")]
        [MaxLength(200, ErrorMessage = "Foi ultrapassado o limite de 200 caracteres")]
        public string Logradouro { get; set; }
        [Display(Name = "Número")]
        [Required(ErrorMessage = "Preencha o campo Número")]
        [MaxLength(10, ErrorMessage = "Foi ultrapassado o limite de 10 caracteres")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "Preencha o campo Bairro")]
        [MaxLength(50, ErrorMessage = "Foi ultrapassado o limite de 50 caracteres")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Preencha o campo Cidade")]
        [MaxLength(50, ErrorMessage = "Foi ultrapassado o limite de 50 caracteres")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Preencha o campo Estado")]
        [MaxLength(2, ErrorMessage = "Foi ultrapassado o limite de 2 caracteres")]
        public string Estado { get; set; }
    }
}