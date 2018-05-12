using CRUD_Pessoa_Fisica_Juridica_2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CRUD_Pessoa_Fisica_Juridica_2.EntityConfig
{
    public class PessoaFisicaConfig: EntityTypeConfiguration<PessoaFisica>
    {
        public PessoaFisicaConfig()
        {
            HasKey(pf => pf.Id);
            Property(pf => pf.Nome)
                .IsRequired()
                .HasMaxLength(150);
            Property(pf => pf.RG)
                .IsRequired()
                .HasMaxLength(10);
            Property(pf => pf.CPF)
                .IsRequired()
                .HasMaxLength(11);

            HasMany(pf => pf.Endereco)
               .WithMany()
               .Map(me =>
               {
                   me.MapLeftKey("PessoaFisicaId");
                   me.MapRightKey("EnderecoId");
                   me.ToTable("PessoaFisica_Endereco");
               }); //.MapToStoredProcedures(s => s.Insert(i => i.HasName("adicionar_endereco_pessoaFisica")
     //              .LeftKeyParameter(p => p.Id, "pessoaFisica_Id")
     //              .RightKeyParameter(t => t.Id, "endereco_Id"))
     //.Delete(d => d.HasName("remover_endereco_pessoaFisica")
     //              .LeftKeyParameter(p => p.Id, "pessoaFisica_Id")
     //              .RightKeyParameter(t => t.Id, "endereco_Id")));

            ToTable("PessoaFisicas");
        }
    }
}