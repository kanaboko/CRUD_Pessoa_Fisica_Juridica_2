using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Pessoa_Fisica_Juridica_2.ViewModel
{
    public class PessoaViewModel
    {
        public PessoaViewModel()
        {
            Id = Guid.NewGuid();
            Fotos = new List<FotoViewModel>();
        }
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Data de cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-mm-yyyy}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato invalido")]
        public DateTime DataCadastro { get; set; }
        [Required]
        [EnumDataType(typeof(TipoPessoaViewModel))]
        public TipoPessoaViewModel TipoPessoa { get; set; }

        public virtual PessoaFisicaViewModel PessoaFisica { get; set; }
        public virtual PessoaJuridicaViewModel PessoaJuridica { get; set; }
        public virtual ICollection<FotoViewModel> Fotos { get; set; }
    }
}