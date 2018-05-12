using System;
using System.Collections.Generic;

namespace CRUD_Pessoa_Fisica_Juridica_2.Models
{
    public class PessoaJuridica
    {
        public PessoaJuridica()
        {
            Id = Guid.NewGuid();
            Endereco = new List<Endereco>();
        }
        public Guid Id { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }

        public virtual Pessoa Pessoa { get; set; }
        public virtual ICollection<Endereco> Endereco { get; set; }

        public void AdicionarEndereco(Endereco endereco)
        {
            Endereco.Add(endereco);
        }

    }
}