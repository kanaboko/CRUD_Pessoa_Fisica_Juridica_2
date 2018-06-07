using CRUD_Pessoa_Fisica_Juridica_2.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Pessoa_Fisica_Juridica_2.Repository
{
    public class RepositoryFoto: RepositoryBase<Foto>
    {
        public RepositoryFoto():base()
        {

        }
        public ICollection<Foto> ObterTodasFotosPorClienteId(Guid id)
        {
            var con = context.Database.Connection;
            //var FotoList = new List<Foto>();
            var sql = "SELECT * FROM dbo.Fotos f " +
                "WHERE f.Pessoa_Id = @sid; ";

            var FotoList = con.Query<Foto>(sql, new {sid=id}).ToList();

            return FotoList;
        }
        public override Foto ObterPorId(Guid id)
        {
            var con = context.Database.Connection;
            var sql = "SELECT * FROM dbo.Fotos f " +
                "WHERE f.Id = @sid; ";

            var Foto = con.QuerySingle<Foto>(sql, new { sid = id });

            return Foto;
        }
    }
}