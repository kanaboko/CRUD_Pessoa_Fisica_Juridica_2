using AutoMapper;
using CRUD_Pessoa_Fisica_Juridica_2.Models;
using CRUD_Pessoa_Fisica_Juridica_2.ViewModel;

namespace CRUD_Pessoa_Fisica_Juridica_2.AutoMapper
{
    internal class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<PessoaViewModel, Pessoa>();
            CreateMap<PessoaFisicaViewModel, PessoaFisica>();
            CreateMap<PessoaJuridicaViewModel, PessoaJuridica>();
            CreateMap<EnderecoViewModel, Endereco>();
        }
    }
}