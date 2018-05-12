using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Pessoa_Fisica_Juridica_2.Models
{
    public class Pessoa
    {
        public Pessoa()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public TipoPessoa TipoPessoa { get; set; }

        public virtual PessoaFisica PessoaFisica { get; set; }
        public virtual PessoaJuridica PessoaJuridica { get; set; }

    }
}