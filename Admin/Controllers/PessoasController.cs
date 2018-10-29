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
using Entities.Classes;

namespace Admin.Controllers
{
  [Authorize]
  public class PessoasController : Controller
  {
    private EntitiesDb db = new EntitiesDb();

    // GET: Pessoas
    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      base.OnActionExecuting(filterContext);

    }
    public ActionResult Index()
    {
      return View(db.Pessoas.ToList());
    }
    public ActionResult IndexCJ()
    {
      return View(db.Pessoas.ToList().Where(p => p.PapelPessoaId == 1).Where(p => p.TipoPessoaId == 2));
    }
    public ActionResult IndexFF()
    {
      return View(db.Pessoas.ToList().Where(p => p.PapelPessoaId == 2).Where(p => p.TipoPessoaId == 1));
    }
    public ActionResult IndexFJ()
    {
      return View(db.Pessoas.ToList().Where(p => p.PapelPessoaId == 2).Where(p => p.TipoPessoaId == 2));
    }
    // GET: Pessoas/Details/5

    public ActionResult Details(int? id)
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

    // GET: Pessoas/Create

    public ActionResult Create()
    {
      return View();
    }

    // POST: Pessoas/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,RazaoSocial,NomeFantasia,NomeCompleto,RG,CPF,DataNascimento,CNPJ,Email,Telefone,Celular,Status,LimiteCredito,TipoPessoaId,PapelPessoaId")] Pessoa pessoa, string viewName)
    {
      if (ModelState.IsValid)
      {
        db.Pessoas.Add(pessoa);
        db.SaveChanges();
      }
      return View(pessoa);
    }

    // GET: Pessoas/Edit/5

    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Pessoa pessoa = db.Pessoas.Find(id);
      //ViewBag.TipoPessoaId = new SelectList(db.Pessoas,TipoPessoa.Fisica.Id ,"Fisica");
      if (pessoa == null)
      {
        return HttpNotFound();
      }
      return View(pessoa);
    }
    public ActionResult EditCF(int? id)
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
      //ViewBag.PaisId = new SelectList(db.Pessoas,"Id","Nome",pessoa);
      return View(pessoa);
    }
    public ActionResult EditCJ(int? id)
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


    // POST: Pessoas/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,RazaoSocial,NomeFantasia,NomeCompleto,RG,CPF,DataNascimento,CNPJ,Email,Telefone,Celular,Status,LimiteCredito,TipoPessoaId,PapelPessoaId")] Pessoa pessoa, string viewName)
    {
      if (ModelState.IsValid)
      {
        db.Entry(pessoa).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction(viewName);
      }
      return View(pessoa);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditCF([Bind(Include = "Id,RazaoSocial,NomeFantasia,NomeCompleto,RG,CPF,DataNascimento,CNPJ,Email,Telefone,Celular,Status,LimiteCredito,TipoPessoaId,PapelPessoaId")] Pessoa pessoa)
    {
      if (ModelState.IsValid)
      {
        db.Entry(pessoa).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("IndexCF");
      }
      return View(pessoa);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditCJ([Bind(Include = "Id,RazaoSocial,NomeFantasia,NomeCompleto,RG,CPF,DataNascimento,CNPJ,Email,Telefone,Celular,Status,LimiteCredito,TipoPessoaId,PapelPessoaId")] Pessoa pessoa)
    {
      if (ModelState.IsValid)
      {
        db.Entry(pessoa).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("IndexCJ");
      }
      return View(pessoa);
    }

    // GET: Pessoas/Delete/5

    public ActionResult Delete(int? id)
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

    // POST: Pessoas/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id, string viewName)
    {
      Pessoa pessoa = db.Pessoas.Find(id);
      pessoa.Status = false;
      db.SaveChanges();
      return RedirectToAction(viewName);

    }

    public ActionResult Search(string doc)
    {

      try
      {
        var list = db.Pessoas.Where(p => p.CPF.Contains(doc) || p.CNPJ.Contains(doc))
          //.Select(p => 
          //new Pessoa
          //{
          //  Id = p.Id,
          //  NomeCompleto = p.NomeCompleto,
          //  NomeFantasia = p.NomeFantasia,
          //  CPF = p.CPF,
          //  CNPJ = p.CNPJ
          //})
          .FirstOrDefault();
        return Json(list);
      }
      catch (Exception ex)
      {

        throw ex;
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
