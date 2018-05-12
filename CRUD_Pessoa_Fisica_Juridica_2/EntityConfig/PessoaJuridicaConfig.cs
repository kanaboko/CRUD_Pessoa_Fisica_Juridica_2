using CRUD_Pessoa_Fisica_Juridica_2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CRUD_Pessoa_Fisica_Juridica_2.EntityConfig
{
    public class PessoaJuridicaConfig: EntityTypeConfiguration<PessoaJuridica>
    {
        public PessoaJuridicaConfig()
        {
            HasKey(pj => pj.Id);
            Property(pj => pj.RazaoSocial)
                .IsRequired()
                .HasMaxLength(150);
            Property(pj => pj.CNPJ)
                .IsRequired()
                .HasMaxLength(15);
            

            HasMany(pj => pj.Endereco)
                .WithMany()
                .Map(me =>
                {
                    me.MapLeftKey("PessoaJuridicaId");
                    me.MapRightKey("EnderecoId");
                    me.ToTable("PessoaJuridica_Endereco");
                });
            ToTable("PessoaJuridicas");
        }
    }
}