using CRUD_Pessoa_Fisica_Juridica_2.Service;
using CRUD_Pessoa_Fisica_Juridica_2.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Pessoa_Fisica_Juridica_2.Controllers
{
    public class ClienteController : Controller
    {
        protected Service_CRUD _service;
        public ClienteController()
        {
            _service = new Service_CRUD();
            
        }
        // GET: Cliente
        public ActionResult Index()
        {
            return View(_service.ObterTodos());
        }

        // GET: Cliente/Details/5
        public ActionResult Details(Guid id)
        {
            var pessoa = _service.ObterPorIdPessoa(id);
            var fotos = _service.ObterImagemCliente(id);
            pessoa.Fotos = fotos;
            
            return View(pessoa);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        public PartialViewResult CreatePessoaFisica_PartialView()
        {
            return PartialView("_AdicionarClienteFisico");
        }

        public PartialViewResult CreatePessoaJuridica_PartialView()
        {
            return PartialView("_AdicionarClienteJuridico");
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteViewModel cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.Adicionar(cliente);
                    return RedirectToAction("Index");
                }
                return View(cliente);
                
            }
            catch
            {
                return View(cliente);
            }
        }

        public ActionResult CreateEndereco(Guid pessoaId, string tipoPessoa = null)
        {
            ViewBag.PessoaId = pessoaId;
            ViewBag.TipoPessoa = tipoPessoa;
            return PartialView("_AdicionarEndereco");
        }

        [HttpPost]
        public ActionResult CreateEndereco(EnderecoViewModel obj, Guid pessoaId, string tipoPessoa = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.AdicionarEndereco(obj, pessoaId, tipoPessoa);
                    string url = Url.Action("ListarEnderecos", "Cliente", new { id = pessoaId, tipoPessoa = tipoPessoa });
                    return Json(new { success = true, url = url });
                }
                return View(obj);

            }
            catch
            {
                return View(obj);
            }
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(Guid id)
        {            
            var pessoa = _service.ObterPorIdPessoa(id);
            var cliente = new ClienteViewModel();
            ViewBag.FotoList = _service.ObterImagemCliente(id);
            ViewBag.PessoaId = id;
            ViewBag.TipoPessoa = pessoa.TipoPessoa;
            switch (pessoa.TipoPessoa)
            {
                case TipoPessoaViewModel.PessoaFisica:
                    cliente.PessoaFisica = pessoa.PessoaFisica;                    
                    return View("EditPessoaFisica", cliente);                    
                case TipoPessoaViewModel.PessoaJuridica:
                    cliente.PessoaJuridica = pessoa.PessoaJuridica;
                    return View("EditPessoaJuridica", cliente);
                default:
                    break;
            }
            return RedirectToAction("Index");
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult EditPessoaFisica(ClienteViewModel obj)
        {
            try
            {
                _service.AtualizarPessoaFisica(obj);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(obj);
            }
        }

        [HttpPost]
        public ActionResult EditPessoaJuridica(ClienteViewModel obj)
        {
            try
            {
                _service.AtualizarPessoaJuridica(obj);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(obj);
            }
        }

        // GET: Cliente/EditEndereco/5
        public ActionResult EditEndereco(Guid id, Guid pessoaId, string tipoPessoa = null)
        {
            ViewBag.PessoaId = pessoaId;
            ViewBag.TipoPessoa = tipoPessoa;
            var endereco = _service.ObterPorIdEndereco(id);
            
            return PartialView("_EditEndereco", endereco);
        }

        // POST: Cliente/EditEndereco/5
        [HttpPost]
        public ActionResult EditEndereco(EnderecoViewModel obj, Guid pessoaId, string tipoPessoa = null)
        {
            try
            {
                _service.AtualizarEndereco(obj);
                string url = Url.Action("ListarEnderecos", "Cliente", new { id = pessoaId, tipoPessoa = tipoPessoa });
                return Json(new { success = true, url = url });
            }
            catch
            {
                return View(obj);
            }
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(Guid id)
        {
            var pessoa = _service.ObterPorIdPessoa(id);
            return View(pessoa);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                
                _service.Deletar(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cliente/Delete/5
        public ActionResult DeleteEndereco(Guid id, Guid pessoaId, string tipoPessoa = null)
        {
            ViewBag.PessoaId = pessoaId;
            ViewBag.TipoPessoa = tipoPessoa;
            var endereco = _service.ObterPorIdEndereco(id);
            return PartialView("_DeletarEndereco", endereco);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("DeleteEndereco")]
        public ActionResult DeleteEnderecoConfirmed(Guid id, Guid pessoaId, string tipoPessoa = null)
        {
            try
            {
                _service.DeletarEndereco(id);
                string url = Url.Action("ListarEnderecos", "Cliente", new { id = pessoaId, tipoPessoa = tipoPessoa });
                return Json(new { success = true, url = url });
                
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        
        public ActionResult DeleteImagem(Guid pessoaId, string filePath, Guid id)
        {
            try
            {
                _service.DeletarImagemCliente(filePath, id);
                string url = Url.Action("ListarImagens", "Cliente", new { id = pessoaId });
                return Json(new { success = true, url = url });

            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult ListarEnderecos(Guid id, string tipoPessoa = null)
        {
            switch (tipoPessoa)
            {
                case "PessoaFisica":
                    var pessoaFisica = _service.ObterPorIdPessoaFisica(id);
                    return PartialView("_ListarEndereco", pessoaFisica.Endereco);
                case "PessoaJuridica":
                    var pessoaJuridica = _service.ObterPorIdPessoaJuridica(id);
                    return PartialView("_ListarEndereco", pessoaJuridica.Endereco);
                default:
                    break;
            }
            return null;
        }

        public ActionResult ListarImagens(Guid id)
        {
            ViewBag.FotoList = _service.ObterImagemCliente(id);
            return PartialView("_ListarImagem", ViewBag.FotoList);
        }

        //public ActionResult ObterImagem(Guid id)
        //{
        //    const string root = @"C:\Users\Alexandre\Documents\Visual Studio 2017\Projects\CRUD_Pessoa_Fisica_Juridica_2\CRUD_Pessoa_Fisica_Juridica_2\src\contents\clientes\";
        //    var foto = Directory.GetFiles(root, id+"*").FirstOrDefault();
        //    return File(foto, "image/jpeg");            
        //}

        public ActionResult ObterImagem(string path)
        {
            const string root = @"C:\Users\Alexandre\Documents\Visual Studio 2017\Projects\CRUD_Pessoa_Fisica_Juridica_2\CRUD_Pessoa_Fisica_Juridica_2\src\contents\clientes\";
            var foto = Directory.GetFiles(root, path).FirstOrDefault();
            return File(foto, "image/jpeg");
        }

        
        public ActionResult SalvarImagem(List<string> img, Guid idPessoa)
        {
            _service.SalvarImagemCliente(img, idPessoa);
            return RedirectToAction("Index");
        }

        

        

        
    }
}
