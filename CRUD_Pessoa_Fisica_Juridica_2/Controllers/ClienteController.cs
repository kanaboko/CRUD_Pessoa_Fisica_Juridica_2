using CRUD_Pessoa_Fisica_Juridica_2.Service;
using CRUD_Pessoa_Fisica_Juridica_2.ViewModel;
using System;
using System.Collections.Generic;
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
        public ActionResult Create(AdicionarClienteViewModel cliente)
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
            ViewBag.PessoaId = id;
            ViewBag.TipoPessoa = pessoa.TipoPessoa;
            switch (pessoa.TipoPessoa)
            {
                case TipoPessoaViewModel.PessoaFisica:
                    return View("EditPessoaFisica", pessoa.PessoaFisica);                    
                case TipoPessoaViewModel.PessoaJuridica:
                    return View("EditPessoaJuridica", pessoa.PessoaJuridica);
                default:
                    break;
            }
            return RedirectToAction("Index");
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult EditPessoaFisica(PessoaFisicaViewModel obj)
        {
            try
            {
                _service.Atualizar(obj);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(obj);
            }
        }

        [HttpPost]
        public ActionResult EditPessoaJuridica(PessoaJuridicaViewModel obj)
        {
            try
            {
                _service.Atualizar(obj);

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
    }
}
