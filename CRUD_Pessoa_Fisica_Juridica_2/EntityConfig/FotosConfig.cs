using CRUD_Pessoa_Fisica_Juridica_2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CRUD_Pessoa_Fisica_Juridica_2.EntityConfig
{
    public class FotoConfig: EntityTypeConfiguration<Foto>
    {
        public FotoConfig()
        {
            HasKey(f => f.Id);

            ToTable("Fotos");

        }
    }
}