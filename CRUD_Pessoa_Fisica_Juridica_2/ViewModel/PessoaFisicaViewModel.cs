using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Pessoa_Fisica_Juridica_2.ViewModel
{
    public class PessoaFisicaViewModel
    {
        public PessoaFisicaViewModel()
        {
            Id = Guid.NewGuid();
            Endereco = new List<EnderecoViewModel>();
        }
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Preencha o campo Nome")]
        [MaxLength(150, ErrorMessage = "Foi ultrapassado o limite de 150 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Preencha o campo Nome")]
        [MaxLength(10, ErrorMessage = "Foi ultrapassado o limite de 10 caracteres")]
        public string RG { get; set; }
        [Required(ErrorMessage = "Preencha o campo Nome")]
        [MaxLength(11, ErrorMessage = "Foi ultrapassado o limite de 11 caracteres")]
        public string CPF { get; set; }

        public virtual ICollection<EnderecoViewModel> Endereco { get; set; }
        public virtual PessoaViewModel Pessoa { get; set; }
    }
}