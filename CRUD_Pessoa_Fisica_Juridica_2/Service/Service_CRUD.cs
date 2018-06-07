using AutoMapper;
using CRUD_Pessoa_Fisica_Juridica_2.Models;
using CRUD_Pessoa_Fisica_Juridica_2.Repository;
using CRUD_Pessoa_Fisica_Juridica_2.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Pessoa_Fisica_Juridica_2.Service
{
    public class Service_CRUD
    {
        protected readonly RepositoryEndereco repositoryEndereco;
        protected readonly RepositoryPessoaFisica repositoryPessoaFisica;
        protected readonly RepositoryPessoaJuridica repositoryPessoaJuridica;
        protected readonly RepositoryPessoa repositoryPessoa;
        protected readonly RepositoryFoto repositoryFoto;
        public Service_CRUD()
        {
            repositoryEndereco = new RepositoryEndereco();
            repositoryPessoaFisica = new RepositoryPessoaFisica();
            repositoryPessoaJuridica = new RepositoryPessoaJuridica();
            repositoryPessoa = new RepositoryPessoa();
            repositoryFoto = new RepositoryFoto();
        }
        public IEnumerable<PessoaViewModel> ObterTodos()
        {
            return Mapper.Map<IEnumerable<PessoaViewModel>>(repositoryPessoa.ObterTodos());
        }

        public void Adicionar(ClienteViewModel cliente)
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
                    if (SalvarImagemCliente(cliente.Foto2, pessoa.Id)) {
                        break;
                    }
                    break;
                case TipoPessoa.PessoaJuridica:
                    var pessoaJuridica = Mapper.Map<PessoaJuridica>(cliente.PessoaJuridica);
                    pessoaJuridica.Endereco.Add(endereco);
                    pessoa.PessoaJuridica = pessoaJuridica;
                    repositoryPessoa.Adicionar(pessoa);
                    SalvarImagemCliente(cliente.Foto2, pessoa.Id);
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

        public void AtualizarPessoaFisica (ClienteViewModel obj)
        {
            var pessoaFisica = obj.PessoaFisica;
            var fotos = obj.Foto2;
            if(fotos != null)
            {
                SalvarImagemCliente(fotos, pessoaFisica.Id);
            };
            repositoryPessoaFisica.Atualizar(Mapper.Map<PessoaFisica>(pessoaFisica));
        }

        public void AtualizarPessoaJuridica(ClienteViewModel obj)
        {
            var pessoaJuridica = obj.PessoaJuridica;
            var fotos = obj.Foto2;
            if (fotos != null)
            {
                SalvarImagemCliente(fotos, pessoaJuridica.Id);
            };
            repositoryPessoaJuridica.Atualizar(Mapper.Map<PessoaJuridica>(pessoaJuridica));
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

        public bool SalvarImagemCliente(HttpPostedFileBase img, Guid id)
        {
            if (img == null || img.ContentLength <= 0) return false;

            const string directory = @"C:\Users\Alexandre\Documents\Visual Studio 2017\Projects\CRUD_Pessoa_Fisica_Juridica_2\CRUD_Pessoa_Fisica_Juridica_2\src\contents\clientes\";
            var fileName = id + Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
            img.SaveAs(Path.Combine(directory, fileName));
            return File.Exists(Path.Combine(directory, fileName));
        }

        public bool SalvarImagemCliente(List<string> base64StringList, Guid id)
        {
            foreach (var base64String in base64StringList)
            {
                var base64 = base64String.Substring(base64String.IndexOf(',') + 1);
                byte[] imageBytes = Convert.FromBase64String(base64);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);

                if (image == null) return false;

                const string directory = @"C:\Users\Alexandre\Documents\Visual Studio 2017\Projects\CRUD_Pessoa_Fisica_Juridica_2\CRUD_Pessoa_Fisica_Juridica_2\src\contents\clientes\";
                var fileName = id.ToString() + Guid.NewGuid().ToString() + ".jpg";
                image.Save(Path.Combine(directory, fileName));
                
                var foto = new Foto();
                foto.FileName = fileName;
                foto.FilePath = Path.Combine(directory, fileName);
                foto.Pessoa_Id = id;
                repositoryFoto.Adicionar(foto);
                if (!File.Exists(Path.Combine(directory, fileName))) {
                    return false;
                };
            }

            return true;
        }

        public ICollection<FotoViewModel> ObterImagemCliente(Guid id)
        {
            return Mapper.Map<List<FotoViewModel>>(repositoryFoto.ObterTodasFotosPorClienteId(id));
        }

        public void DeletarImagemCliente(string imagemPath, Guid id)
        {
            var foto = repositoryFoto.ObterPorId(id);
            repositoryFoto.Remover(foto);
            File.Delete(imagemPath); 
        }
    }
}