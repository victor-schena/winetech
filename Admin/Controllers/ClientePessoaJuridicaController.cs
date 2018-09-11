using Admin.Functions;
using Admin.Models;
using Entities.Contexts;
using Entities.Tables;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
  [Authorize]
  public class ClientePessoaJuridicaController : Controller
  {
    private EntitiesDb db = new EntitiesDb();
    // GET: ClientePessoaJuridica
    public ActionResult Index()
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Index", "ClientePessoaJuridica"))
        {
          return RedirectToAction("Index", "Home");
        }
        return View(db.Pessoas.ToList().Where(p => p.PapelPessoaId == 1).Where(p => p.TipoPessoaId == 2).Where(x => x.Status == true));
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }
    }
    public ActionResult Create()
    {
      if (!Validations.HasCredentials(User.Identity.GetUserName(), "Create", "ClientePessoaJuridica"))
      {
        return RedirectToAction("Index", "Home");
      }
      return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(ClienteEnderecoViewModel Cliente, string botao, FormCollection colecao,int? idEndereco)
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Create", "ClientePessoaJuridica"))
        {
          return RedirectToAction("Index", "Home");
        }
        int IdCliente = 0;
        if (botao == "addCliente")
        {
          if (Cliente.Pessoa != null)
          {
            if (!ValidaCamposPessoaJuridica(Cliente.Pessoa))
            {
              return View(Cliente);
            }
            Cliente.Pessoa.Status = true;
            db.Pessoas.Add(Cliente.Pessoa);
            db.SaveChanges();
            IdCliente = Cliente.Pessoa.Id;
            Cliente.Pessoa = Cliente.Pessoa;
            TempData["Success"] = "Registro salvo com sucesso.";
          }
          ViewBag.idCliente = IdCliente;
          return View(Cliente);
        }
        if (botao == "addEndereco")
        {
          if (Cliente.Enderecos == null)
            Cliente.Enderecos = new List<Endereco>();
          Cliente.Endereco.Status = true;
          Cliente.Enderecos.Add(Cliente.Endereco);
          db.Enderecos.Add(Cliente.Endereco);
          db.SaveChanges();
          Cliente.Enderecos = db.Enderecos.AsNoTracking().Where(x => x.PessoaId == Cliente.Endereco.PessoaId).ToList();
          ViewBag.idCliente = Cliente.Endereco.PessoaId;
          TempData["Success"] = "Registro salvo com sucesso.";
          return View(Cliente);
        }
        if (botao == "finalizarCadastro")
        {
          TempData["Success"] = "Registro salvo com sucesso.";
          return RedirectToAction("Index","ClientePessoaJuridica");
        }
        if (botao == "deletarEndereco")
        {
          //var cliente = db.Pessoas.Where(m => m.EnderecoId == idEndereco).First();
          var endereco = db.Enderecos.Include(p => p.Pessoa).Where(e => e.Id == idEndereco).First();
          endereco.Status = false;
          db.Entry(endereco).State = EntityState.Modified;
          db.SaveChanges();

          Cliente.Pessoa = endereco.Pessoa;
          if (Cliente.Enderecos == null)
            Cliente.Enderecos = new List<Endereco>();
          Cliente.Enderecos = db.Enderecos.AsNoTracking().Where(x => x.PessoaId == Cliente.Pessoa.Id).Where(x => x.Status == true).ToList();
          ViewBag.idCliente = Cliente.Pessoa.Id;
          TempData["Success"] = "Registro salvo com sucesso.";
          return View(Cliente);
        }
        return RedirectToAction("Index", "ClientePessoaJuridica");
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }
      
    }
    //GET:
    public ActionResult Edit(int? id)
    {
      try
      {
        if (id == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        ClienteEnderecoViewModel Cliente = new ClienteEnderecoViewModel();
        Cliente.Pessoa = db.Pessoas.Find(id);
        Cliente.Enderecos = db.Enderecos.Where(x => x.PessoaId == id).ToList();
        if (Cliente == null)
        {
          return HttpNotFound();
        }
        return View(Cliente);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(ClienteEnderecoViewModel Cliente, string botao, FormCollection colecao, int? idEndereco)
    {
      try
      {
        if (botao == "addCliente")
        {
          if (!ValidaCamposPessoaJuridica(Cliente.Pessoa))
          {
            return View(Cliente);
          }
          Cliente.Pessoa.Status = true;
          db.Entry(Cliente.Pessoa).State = EntityState.Modified;
          db.SaveChanges();
          //db.Enderecos.AddRange(Cliente.Enderecos);
          Cliente.Enderecos = db.Enderecos.AsNoTracking().Where(x => x.PessoaId == Cliente.Pessoa.Id).ToList();
          ViewBag.idCliente = Cliente.Pessoa.Id;
          TempData["Success"] = "Registro salvo com sucesso.";
          return View(Cliente);
        }
        if (botao == "addEndereco")
        {
          if (Cliente.Enderecos == null)
            Cliente.Enderecos = new List<Endereco>();
          Cliente.Endereco.Status = true;
          Cliente.Enderecos.Add(Cliente.Endereco);
          db.Enderecos.Add(Cliente.Endereco);
          db.SaveChanges();
          Cliente.Enderecos = db.Enderecos.AsNoTracking().Where(x => x.PessoaId == Cliente.Endereco.PessoaId).ToList();
          ViewBag.idCliente = Cliente.Endereco.PessoaId;
          TempData["Success"] = "Registro salvo com sucesso.";
          return View(Cliente);
        }
        if (botao == "deletarEndereco")
        {
          //var cliente = db.Pessoas.Where(m => m.EnderecoId == idEndereco).First();
          var endereco = db.Enderecos.Include(p => p.Pessoa).Where(e => e.Id == idEndereco).First();
          endereco.Status = false;
          db.Entry(endereco).State = EntityState.Modified;
          db.SaveChanges();

          Cliente.Pessoa = endereco.Pessoa;
          if (Cliente.Enderecos == null)
            Cliente.Enderecos = new List<Endereco>();
          Cliente.Enderecos = db.Enderecos.AsNoTracking().Where(x => x.PessoaId == Cliente.Pessoa.Id).Where(x => x.Status == true).ToList();
          ViewBag.idCliente = Cliente.Pessoa.Id;
          TempData["Success"] = "Registro salvo com sucesso.";
          return View(Cliente);
        }
        return RedirectToAction("Index", "ClientePessoaJuridica");
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }
    public ActionResult Delete(int? id)
    {
      try
      {
        if (id == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Pessoa pessoa = db.Pessoas.Find(id);
        if (pessoa == null)
        {
          return HttpNotFound();
        }

        return View(pessoa);

      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      try
      {
        Pessoa pessoa = db.Pessoas.Find(id);
        pessoa.Status = false;
        db.Entry(pessoa).State = EntityState.Modified;
        db.SaveChanges();
        TempData["Success"] = "Registro excluido com sucesso.";
        return RedirectToAction("Index");
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }


    }
    public ActionResult Details(int? id)
    {
      try
      {
        if (id == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        ClienteEnderecoViewModel Cliente = new ClienteEnderecoViewModel();
        Cliente.Pessoa = db.Pessoas.Find(id);
        Cliente.Enderecos = db.Enderecos.Where(x => x.PessoaId == id).ToList();
        if (Cliente == null)
        {
          return HttpNotFound();
        }
        return View(Cliente);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }
    public bool ValidaCamposPessoaJuridica(Pessoa pessoa)
    {
      bool validacao = true;
      ModelState.Clear();
      if (string.IsNullOrEmpty(pessoa.NomeFantasia))
      {
        ModelState.AddModelError("Pessoa.NomeFantasia", "O campo Nome Fantasia é obrigatório! ");
        validacao = false;
      }
      if (string.IsNullOrEmpty(pessoa.RazaoSocial))
      {
        ModelState.AddModelError("Pessoa.RazaoSocial", "O campo Razão Social é obrigatório! ");
        validacao = false;
      }
      if (string.IsNullOrEmpty(pessoa.CNPJ))
      {
        ModelState.AddModelError("Pessoa.CNPJ", "O campo CNPF é obrgatório! ");
        validacao = false;
      }
      if (!Validations.IsCnpj(pessoa.CNPJ))
      {
        ModelState.AddModelError("Pessoa.CNPJ", "O CNPJ informado é inválido!");
        validacao = false;
      }

      return validacao;
    }
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}