using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using CRUD_Pessoa_Fisica_Juridica_2.AttributeHelper;

namespace CRUD_Pessoa_Fisica_Juridica_2.ViewModel
{
    public class ClienteViewModel
    {
        public PessoaViewModel Pessoa { get; set; }
        public PessoaFisicaViewModel PessoaFisica { get; set; }
        public PessoaJuridicaViewModel PessoaJuridica { get; set; }
        public EnderecoViewModel Endereco { get; set; }    
        
        [FileSize(10240000)]
        [FileTypes("jpg,jpeg,png")]        
        public HttpPostedFileBase Foto { get; set; }

        public List<string> Foto2 { get; set; }
    }
}