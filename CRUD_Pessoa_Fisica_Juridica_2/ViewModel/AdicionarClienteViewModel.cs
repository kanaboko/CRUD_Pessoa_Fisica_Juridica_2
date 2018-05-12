using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Pessoa_Fisica_Juridica_2.ViewModel
{
    public class AdicionarClienteViewModel
    {
        public PessoaViewModel Pessoa { get; set; }
        public PessoaFisicaViewModel PessoaFisica { get; set; }
        public PessoaJuridicaViewModel PessoaJuridica { get; set; }
        public EnderecoViewModel Endereco { get; set; }        
    }
}