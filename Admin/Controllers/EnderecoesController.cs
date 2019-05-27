using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entities.Contexts;
using Entities.Tables;

namespace Admin.Controllers
{
  [Authorize]
  public class EnderecoesController : Controller
  {
    private EntitiesDb db = new EntitiesDb();

    // GET: Enderecoes
    [Authorize]
    public ActionResult Index()
    {
      try
      {
        var enderecos = db.Enderecos.Include(e => e.Pessoa);
        return View(enderecos.ToList());
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

    // GET: Enderecoes/Details/5
    [Authorize]
    public ActionResult Details(int? id)
    {
      try
      {
        if (id == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Endereco endereco = db.Enderecos.Find(id);
        if (endereco == null)
        {
          return HttpNotFound();
        }
        return View(endereco);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }
      finally
      {
        db.Dispose();
      }
    }

    // GET: Enderecoes/Create
    [Authorize]
    public ActionResult Create()
    {
      try
      {
        ViewBag.PessoaId = new SelectList(db.Pessoas, "Id", "RazaoSocial");
        return View();
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }
      finally
      {
        db.Dispose();
      }
    }

    // POST: Enderecoes/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,Estado,Cidade,Bairro,CEP,Rua,Numero,Complemento,Status,PessoaId")] Endereco endereco)
    {
      try
      {
        if (ModelState.IsValid)
        {
          db.Enderecos.Add(endereco);
          db.SaveChanges();
          TempData["Success"] = "Registro salvo com sucesso..";
          return RedirectToAction("Index");
        }

        ViewBag.PessoaId = new SelectList(db.Pessoas, "Id", "RazaoSocial", endereco.PessoaId);
        return View(endereco);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }
      finally
      {
        db.Dispose();
      }

    }

    // GET: Enderecoes/Edit/5
    [Authorize]
    public ActionResult Edit(int? id)
    {
      try
      {
        if (id == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Endereco endereco = db.Enderecos.Find(id);
        if (endereco == null)
        {
          return HttpNotFound();
        }

        ViewBag.PessoaId = new SelectList(db.Pessoas, "Id", "RazaoSocial", endereco.PessoaId);
        return View(endereco);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }
      finally
      {
        db.Dispose();
      }

    }

    // POST: Enderecoes/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,Estado,Cidade,Bairro,CEP,Rua,Numero,Complemento,Status,PessoaId")] Endereco endereco)
    {
      try
      {
        if (ModelState.IsValid)
        {
          db.Entry(endereco).State = EntityState.Modified;
          db.SaveChanges();
          TempData["Success"] = "Registro salvo com sucesso.";
          return RedirectToAction("Index");
        }
        ViewBag.PessoaId = new SelectList(db.Pessoas, "Id", "RazaoSocial", endereco.PessoaId);
        return View(endereco);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }
      finally
      {
        db.Dispose();
      }

    }

    // GET: Enderecoes/Delete/5
    [Authorize]
    public ActionResult Delete(int? id)
    {
      try
      {
        if (id == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Endereco endereco = db.Enderecos.Find(id);
        if (endereco == null)
        {
          return HttpNotFound();
        }
        return View(endereco);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }
      finally
      {
        db.Dispose();
      }
    }

    // POST: Enderecoes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id,string caminho)
    {
      try
      {
        Endereco endereco = db.Enderecos.Find(id);
        endereco.Status = false;
        db.SaveChanges();
        TempData["Success"] = "Registro excluido com sucesso.";
        return RedirectToAction(caminho);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }
      finally
      {
        db.Dispose();
      }

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
