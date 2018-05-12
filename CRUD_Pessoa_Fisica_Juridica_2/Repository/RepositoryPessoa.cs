using CRUD_Pessoa_Fisica_Juridica_2.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Pessoa_Fisica_Juridica_2.Repository
{
    public class RepositoryPessoa : RepositoryBase<Pessoa>
    {

        public RepositoryPessoa() : base()
        {

        }
        public IEnumerable<Pessoa> ObterTodos()
        {
            var con = context.Database.Connection;
            var pessoa = new List<Pessoa>();
            var sql = "SELECT * FROM dbo.Pessoas p " +
                "LEFT JOIN dbo.PessoaFisicas f ON f.Id = p.Id " +
                "LEFT JOIN dbo.PessoaJuridicas j ON j.Id = p.Id ";
            con.Query<Pessoa, PessoaFisica, PessoaJuridica, Pessoa>(sql, (p, f, j) =>
            {
                if (p != null && !pessoa.Exists(src => src.Id == p.Id))
                {
                    pessoa.Add(p);
                }
                if (pessoa.Count() > 0)
                {
                    for (int i = 0; i < pessoa.Count(); i++)
                    {
                        if (f != null && pessoa[i].Id == f.Id)
                        {
                            pessoa[i].PessoaFisica = f;
                        }
                        if (j != null && pessoa[i].Id == j.Id)
                        {
                            pessoa[i].PessoaJuridica = j;
                        }
                    }
                }
                return pessoa.FirstOrDefault();
            });

            return pessoa;
        }

        public Pessoa ObterPorIdEF(Guid id)
        {
            return dbSet.Find(id);
        }

        public override Pessoa ObterPorId(Guid id)
        {
            var con = context.Database.Connection;
            var pessoa = new List<Pessoa>();
            var sql = "SELECT * FROM dbo.Pessoas p " +
                "LEFT JOIN dbo.PessoaFisicas f ON f.Id = p.Id " +
                "LEFT JOIN dbo.PessoaFisica_Endereco x ON x.PessoaFisicaId = f.Id " +
                "LEFT JOIN dbo.Enderecos e ON e.Id = x.EnderecoId " +
                "WHERE p.Id = @sid;" +
                "SELECT * FROM dbo.Pessoas p " +
                "LEFT JOIN dbo.PessoaJuridicas j ON j.Id = p.Id " +
                "LEFT JOIN dbo.PessoaJuridica_Endereco y ON y.PessoaJuridicaId = j.Id " +
                "LEFT JOIN dbo.Enderecos e ON e.Id = y.EnderecoId " +
                "WHERE p.Id = @sid";
            using (var multi = con.QueryMultiple(sql, new { sid = id }))
            {
                var pessoaFisica = multi.Read<Pessoa, PessoaFisica, Endereco, Pessoa>((p, f, e) =>
                {
                    if (p != null && !pessoa.Exists(src => src.Id == p.Id))
                    {
                        pessoa.Add(p);
                        pessoa[0].PessoaFisica = f;
                    }
                    if (e != null)
                    {
                        pessoa[0].PessoaFisica.Endereco.Add(e);
                    }
                    return pessoa.FirstOrDefault();
                });

                var pessoaJuridica = multi.Read<Pessoa, PessoaJuridica, Endereco, Pessoa>((p, j, e) =>
                {
                    if (p != null && !pessoa.Exists(src => src.Id == p.Id))
                    {
                        pessoa.Add(p);
                        
                    }
                    if (pessoa.Count() > 0)
                    {
                        for (int i = 0; i < pessoa.Count(); i++)
                        {
                            if (j != null && pessoa[i].Id == j.Id)
                            {
                                pessoa[i].PessoaJuridica = j;
                            }
                            if (e != null)
                            {
                                pessoa[i].PessoaJuridica.Endereco.Add(e);
                            }
                        }
                    }
                    return pessoa.FirstOrDefault();
                });
            }


            //con.Query<Pessoa, PessoaFisica, Endereco, Pessoa>(sql, (p, f, e) =>
            //{
            //    if (p != null && !pessoa.Exists(src => src.Id == p.Id))
            //    {
            //        pessoa.Add(p);
            //    }
            //    if (pessoa.Count() > 0)
            //    {
            //        for (int i = 0; i < pessoa.Count(); i++)
            //        {
            //            if (f != null && pessoa[i].Id == f.Id)
            //            {
            //                pessoa[i].PessoaFisica = f;
            //            }
            //            if (e != null)
            //            {
            //                pessoa[i].PessoaFisica.Endereco.Add(e);
            //            }
            //        }
            //    }
            //    return pessoa.FirstOrDefault();
            //});

            return pessoa.FirstOrDefault();

        }
    }
}