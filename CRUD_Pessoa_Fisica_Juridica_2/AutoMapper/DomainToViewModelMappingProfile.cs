using CRUD_Pessoa_Fisica_Juridica_2.Models;
using CRUD_Pessoa_Fisica_Juridica_2.ViewModel;
using AutoMapper;

namespace CRUD_Pessoa_Fisica_Juridica_2.AutoMapper
{
    public class DomainToViewModelMappingProfile: Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Pessoa, PessoaViewModel>();
            CreateMap<PessoaFisica, PessoaFisicaViewModel>();
            CreateMap<Endereco, EnderecoViewModel>();
            CreateMap<PessoaJuridica, PessoaJuridicaViewModel>();
        }
    }
}