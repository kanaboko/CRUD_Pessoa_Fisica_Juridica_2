using System;

namespace CRUD_Pessoa_Fisica_Juridica_2.Models
{
    public class Endereco
    {
        public Endereco()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}