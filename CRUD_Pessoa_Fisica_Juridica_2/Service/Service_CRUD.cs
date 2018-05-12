using AutoMapper;
using CRUD_Pessoa_Fisica_Juridica_2.Models;
using CRUD_Pessoa_Fisica_Juridica_2.Repository;
using CRUD_Pessoa_Fisica_Juridica_2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Pessoa_Fisica_Juridica_2.Service
{
    public class Service_CRUD
    {
        protected readonly RepositoryEndereco repositoryEndereco;
        protected readonly RepositoryPessoaFisica repositoryPessoaFisica;
        protected readonly RepositoryPessoaJuridica repositoryPessoaJuridica;
        protected readonly RepositoryPessoa repositoryPessoa;
        public Service_CRUD()
        {
            repositoryEndereco = new RepositoryEndereco();
            repositoryPessoaFisica = new RepositoryPessoaFisica();
            repositoryPessoaJuridica = new RepositoryPessoaJuridica();
            repositoryPessoa = new RepositoryPessoa();
        }
        public IEnumerable<PessoaViewModel> ObterTodos()
        {
            return Mapper.Map<IEnumerable<PessoaViewModel>>(repositoryPessoa.ObterTodos());
        }

        public void Adicionar(AdicionarClienteViewModel cliente)
        {
            var pessoa = Mapper.Map<Pessoa>(cliente.Pessoa);
            var endereco = Mapper.Map<Endereco>(cliente.Endereco);
            pessoa.DataCadastro = DateTime.Now.Date;
            switch (pessoa.TipoPessoa)
            {
                case TipoPessoa.PessoaFisica:
                    var pessoaFisica = Mapper.Map<PessoaFisica>(cliente.PessoaFisica);
                    pessoaFisica.Endereco.Add(endereco);
                    pessoa.PessoaFisica = pessoaFisica;
                    repositoryPessoa.Adicionar(pessoa);
                    break;
                case TipoPessoa.PessoaJuridica:
                    var pessoaJuridica = Mapper.Map<PessoaJuridica>(cliente.PessoaJuridica);
                    pessoaJuridica.Endereco.Add(endereco);
                    pessoa.PessoaJuridica = pessoaJuridica;
                    repositoryPessoa.Adicionar(pessoa);
                    break;
                default:

                    break;
            }
        }

        public void AdicionarEndereco(EnderecoViewModel obj, Guid pessoaId, string tipoPessoa = null)
        {
            switch (tipoPessoa)
            {
                case "PessoaFisica":
                    var pessoaFisica = repositoryPessoaFisica.ObterPorIdEF(pessoaId);
                    pessoaFisica.AdicionarEndereco(Mapper.Map<Endereco>(obj));
                    repositoryPessoaFisica.Atualizar(pessoaFisica);
                    break;
                case "PessoaJuridica":
                    var pessoaJuridica = repositoryPessoaJuridica.ObterPorIdEF(pessoaId);
                    pessoaJuridica.AdicionarEndereco(Mapper.Map<Endereco>(obj));
                    repositoryPessoaJuridica.Atualizar(pessoaJuridica);
                    break;
                default:
                    break;
            }
        }

        public PessoaViewModel ObterPorIdPessoa(Guid id)
        {
            var pessoa = Mapper.Map<PessoaViewModel>(repositoryPessoa.ObterPorId(id));
            return pessoa;
        }

        public PessoaFisicaViewModel ObterPorIdPessoaFisica(Guid id)
        {
            var pessoa = Mapper.Map<PessoaFisicaViewModel>(repositoryPessoaFisica.ObterPorId(id));
            return pessoa;
        }

        public PessoaJuridicaViewModel ObterPorIdPessoaJuridica(Guid id)
        {
            var pessoa = Mapper.Map<PessoaJuridicaViewModel>(repositoryPessoaJuridica.ObterPorId(id));
            return pessoa;
        }
        public EnderecoViewModel ObterPorIdEndereco(Guid id)
        {
            var endereco = Mapper.Map<EnderecoViewModel>(repositoryEndereco.ObterPorId(id));
            return endereco;
        }

        public void Atualizar (PessoaFisicaViewModel obj)
        {
            repositoryPessoaFisica.Atualizar(Mapper.Map<PessoaFisica>(obj));
        }

        public void Atualizar(PessoaJuridicaViewModel obj)
        {
            repositoryPessoaJuridica.Atualizar(Mapper.Map<PessoaJuridica>(obj));
        }

        public void AtualizarEndereco(EnderecoViewModel obj)
        {
            repositoryEndereco.Atualizar(Mapper.Map<Endereco>(obj));
        }

        public void DeletarEndereco(Guid id)
        {
            var endereco = repositoryEndereco.ObterPorId(id);
            
            repositoryEndereco.Remover(endereco);
        }

        public void Deletar(Guid id)
        {
            var pessoa = repositoryPessoa.ObterPorId(id);
            repositoryPessoa.Remover(pessoa);
        }
    }
}