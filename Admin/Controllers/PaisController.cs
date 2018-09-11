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
  public class PaisController : Controller
  {
    private EntitiesDb db = new EntitiesDb();

    // GET: Pais

    public ActionResult Index()
    {
      try
      {
        return View(db.Paises.ToList());
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }
    }

    // GET: Pais/Details/5

    public ActionResult Details(int? id)
    {
      try
      {
        if (id == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Pais pais = db.Paises.Find(id);
        if (pais == null)
        {
          return HttpNotFound();
        }
        return View(pais);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }

    // GET: Pais/Create

    public ActionResult Create()
    {
      try
      {
        return View();
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }
    }

    // POST: Pais/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,Nome")] Pais pais)
    {
      try
      {
        if (ModelState.IsValid)
        {
          pais.Status = true;
          db.Paises.Add(pais);
          db.SaveChanges();
          TempData["Success"] = "Registro salvo com sucesso.";
          return RedirectToAction("Index");
        }

        return View(pais);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }

    // GET: Pais/Edit/5

    public ActionResult Edit(int? id)
    {
      try
      {
        if (id == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Pais pais = db.Paises.Find(id);
        if (pais == null)
        {
          return HttpNotFound();
        }
        return View(pais);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }

    // POST: Pais/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,Nome")] Pais pais)
    {
      try
      {
        if (ModelState.IsValid)
        {
          db.Entry(pais).State = EntityState.Modified;
          db.SaveChanges();
          TempData["Success"] = "Registro salvo com sucesso.";
          return RedirectToAction("Index");
        }
        return View(pais);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }

    // GET: Pais/Delete/5

    public ActionResult Delete(int? id)
    {
      try
      {
        if (id == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Pais pais = db.Paises.Find(id);
        if (pais == null)
        {
          return HttpNotFound();
        }
        return View(pais);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }

    // POST: Pais/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      try
      {
        Pais pais = db.Paises.Find(id);
        pais.Status = false;
        db.SaveChanges();
        TempData["Success"] = "Registro excluído com sucesso.";
        return RedirectToAction("Index");
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
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
