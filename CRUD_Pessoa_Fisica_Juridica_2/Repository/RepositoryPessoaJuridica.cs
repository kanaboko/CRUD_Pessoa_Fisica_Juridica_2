using CRUD_Pessoa_Fisica_Juridica_2.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Pessoa_Fisica_Juridica_2.Repository
{
    public class RepositoryPessoaJuridica: RepositoryBase<PessoaJuridica>
    {
        public RepositoryPessoaJuridica():base()
        {

        }

        public PessoaJuridica ObterPorIdEF(Guid id)
        {
            return dbSet.Find(id);
        }

        public override PessoaJuridica ObterPorId(Guid id)
        {

            var con = context.Database.Connection;
            var pessoaJuridica = new List<PessoaJuridica>();
            var sql = "SELECT * FROM dbo.PessoaJuridicas f " +
                "LEFT JOIN dbo.PessoaJuridica_Endereco x ON x.PessoaJuridicaId = f.Id " +
                "LEFT JOIN dbo.Enderecos e ON e.Id = x.EnderecoId " +
                "WHERE f.Id = @sid; ";
            con.Query<PessoaJuridica, Endereco, PessoaJuridica>(sql, (f, e) =>
            {
                if (f != null && !pessoaJuridica.Exists(src => src.Id == f.Id))
                {
                    pessoaJuridica.Add(f);
                }
                if (e != null)
                {
                    pessoaJuridica[0].AdicionarEndereco(e);
                }
                return pessoaJuridica.FirstOrDefault();
            }, new { sid = id });

            return pessoaJuridica.FirstOrDefault();
        }
    }
}