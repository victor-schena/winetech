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
  public class FornecedorPessoaFisicaController : Controller
  {
    private EntitiesDb db = new EntitiesDb();
    // GET: FornecedorPessoaFisica
    public ActionResult Index()
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Index", "Fornecedor Pessoa Fisica"))
        {
          return RedirectToAction("Index", "Home");
        }
        return View(db.Pessoas.ToList().Where(p => p.PapelPessoaId == 2).Where(p => p.TipoPessoaId == 1).Where(x => x.Status == true));
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
      }
      finally
      {
        db.Dispose();
      }
    }
    public ActionResult Create()
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Create", "Fornecedor Pessoa Fisica"))
        {
          return RedirectToAction("Index", "Home");
        }
        ViewBag.Enderecos = new SelectList(new EntitiesDb().Enderecos.ToList(), "Name", "Name");
        return View();
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
      }
      finally
      {
        db.Dispose();
      }
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(ClienteEnderecoViewModel Cliente, string botao, FormCollection colecao, int? idEndereco)
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Create", "Fornecedor Pessoa Fisica"))
        {
          return RedirectToAction("Index", "Home");
        }
        int IdCliente = 0;
        if (botao == "addCliente")
        {
          if (Cliente.Pessoa != null)
          {
            if (!ValidaCamposPessoaFisica(Cliente.Pessoa))
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
          ViewBag.idCliente = Cliente.Endereco.PessoaId;
          if (!ValidaEndereco(Cliente.Endereco))
            return View(Cliente);
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
          return RedirectToAction("Index", "FornecedorPessoaFisica");
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
        return RedirectToAction("Index", "FornecedorPessoaFisica");
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
      }
      finally
      {
        db.Dispose();
      }
    }

    public ActionResult Edit(int? id)
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Edit", "Fornecedor Pessoa Fisica"))
        {
          return RedirectToAction("Index", "Home");
        }
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
        TempData["Success"] = "Registro salvo com sucesso.";
        return View(Cliente);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
      }
      finally
      {
        db.Dispose();
      }

    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(ClienteEnderecoViewModel Cliente, string botao, FormCollection colecao, int? idEndereco)
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Edit", "Fornecedor Pessoa Fisica"))
        {
          return RedirectToAction("Index", "Home");
        }
        if (botao == "addCliente")
        {
          if (!ValidaCamposPessoaFisica(Cliente.Pessoa))
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
        return RedirectToAction("Index");
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
      }
      finally
      {
        db.Dispose();
      }
    }
    public ActionResult Delete(int? id)
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Delete", "Fornecedor Pessoa Fisica"))
        {
          return RedirectToAction("Index", "Home");
        }
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
      }
      finally
      {
        db.Dispose();
      }
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Delete", "Fornecedor Pessoa Fisica"))
        {
          return RedirectToAction("Index", "Home");
        }
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
      }
      finally
      {
        db.Dispose();
      }
    }
    public ActionResult Details(int? id)
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Details", "Fornecedor Pessoa Fisica"))
        {
          return RedirectToAction("Index", "Home");
        }
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
      }
      finally
      {
        db.Dispose();
      }

    }
    public bool ValidaCamposPessoaFisica(Pessoa pessoa)
    {
      ModelState.Clear();
      bool validacao = true;
      if (string.IsNullOrEmpty(pessoa.NomeCompleto))
      {
        ModelState.AddModelError("Pessoa.NomeCompleto", "O campo Nome é obrigatório!");
        validacao = false;
      }
      if (string.IsNullOrEmpty(pessoa.CPF))
      {
        ModelState.AddModelError("Pessoa.CPF", "O campo CPF é obrigatório!");
        validacao = false;
      }
      if (pessoa.DataNascimento.HasValue && !Validations.VerificaMarioridadePenal((DateTime)pessoa.DataNascimento))
      {
        ModelState.AddModelError("Pessoa.DataNascimento", "O campo Data de Nascimento é obrigatório!");
        validacao = false;
      }
      if (pessoa.DataNascimento.HasValue && !VerificaMarioridadePenal((DateTime)pessoa.DataNascimento))
      {
        ModelState.AddModelError("Pessoa.DataNascimento", "Não é possível cadastrar menores de 18 anos!");
        validacao = false;
      }

      if (!string.IsNullOrEmpty(pessoa.CPF) && !Validations.IsCpf(pessoa.CPF))
      {
        ModelState.AddModelError("Pessoa.CPF", "CPF Inválido!");
        validacao = false;
      }

      return validacao;
    }
    public bool ValidaEndereco(Endereco endereco)
    {
      ModelState.Clear();
      bool validacao = true;
      if (string.IsNullOrEmpty(endereco.CEP))
      {
        ModelState.AddModelError("Endereco.CEP", "O campo CEP é Obrigatório!");
        validacao = false;
      }
      if (string.IsNullOrEmpty(endereco.Estado))
      {
        ModelState.AddModelError("Endereco.Estado", "O campo estado é Obrigatório!");
        validacao = false;
      }
      if (string.IsNullOrEmpty(endereco.Cidade))
      {
        ModelState.AddModelError("Endereco.Cidade", "O campo cidade é Obrigatório!");
        validacao = false;
      }
      if (string.IsNullOrEmpty(endereco.Bairro))
      {
        ModelState.AddModelError("Endereco.Bairro", "O campo bairro é Obrigatório!");
        validacao = false;
      }
      if (string.IsNullOrEmpty(endereco.Rua))
      {
        ModelState.AddModelError("Endereco.Rua", "O campo rua é Obrigatório!");
        validacao = false;
      }
      if (string.IsNullOrEmpty(endereco.Numero) || endereco.Numero == " ")
        endereco.Numero = "Sem número";

      return validacao;
    }
    public bool VerificaMarioridadePenal(DateTime DataNascimento)
    {
      int AnoBase = DateTime.Today.Year - 18;
      if (DataNascimento.Year < AnoBase)
      {
        return true;
      }
      if (AnoBase == DataNascimento.Year)
      {
        if (DataNascimento.Month < DateTime.Now.Month)
        {
          if (DataNascimento.Day <= DateTime.Now.Day)
          {
            return true;
          }
        }
      }
      return false;
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