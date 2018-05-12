using CRUD_Pessoa_Fisica_Juridica_2.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUD_Pessoa_Fisica_Juridica_2.Repository
{
    public class RepositoryPessoaFisica: RepositoryBase<PessoaFisica>
    {
        public RepositoryPessoaFisica() : base()
        {

        }

        public PessoaFisica ObterPorIdEF(Guid id)
        {
            return dbSet.Find(id);
        }

        public override PessoaFisica ObterPorId(Guid id)
        {
            
            var con = context.Database.Connection;
            var pessoaFisica = new List<PessoaFisica>();
            var sql = "SELECT * FROM dbo.PessoaFisicas f " +
                "LEFT JOIN dbo.PessoaFisica_Endereco x ON x.PessoaFisicaId = f.Id " +
                "LEFT JOIN dbo.Enderecos e ON e.Id = x.EnderecoId " +
                "WHERE f.Id = @sid; ";
            con.Query<PessoaFisica, Endereco, PessoaFisica>(sql, (f, e) =>
            {
                if (f != null && !pessoaFisica.Exists(src => src.Id == f.Id))
                {
                    pessoaFisica.Add(f);
                }
                if (e != null)
                {
                    pessoaFisica[0].AdicionarEndereco(e);
                }
                return pessoaFisica.FirstOrDefault();
            }, new { sid = id });

            return pessoaFisica.FirstOrDefault();
        }
    }
}