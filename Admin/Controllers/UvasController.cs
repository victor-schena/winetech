using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entities.Contexts;
using Entities.Tables;

namespace Admin.Controllers
{
  public class UvasController : Controller
  {
    private EntitiesDb db = new EntitiesDb();

    // GET: Uvas
    public async Task<ActionResult> Index()
    {
      try
      {
        return View(await db.Uvas.ToListAsync());
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
      }
    }

    // GET: Uvas/Details/5
    public async Task<ActionResult> Details(int? id)
    {
      try
      {
        if (id == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Uva uva = await db.Uvas.FindAsync(id);
        if (uva == null)
        {
          return HttpNotFound();
        }
        return View(uva);

      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
      }
    }

    // GET: Uvas/Create
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
      }
    }

    // POST: Uvas/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create([Bind(Include = "Id,Descricao")] Uva uva)
    {
      try
      {
        if (ModelState.IsValid)
        {
          uva.Status = true;
          db.Uvas.Add(uva);
          await db.SaveChangesAsync();
          return RedirectToAction("Index");
        }

        return View(uva);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
      }
    }

    // GET: Uvas/Edit/5
    public async Task<ActionResult> Edit(int? id)
    {
      try
      {
        if (id == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Uva uva = await db.Uvas.FindAsync(id);
        if (uva == null)
        {
          return HttpNotFound();
        }
        return View(uva);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
      }
    }

    // POST: Uvas/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit([Bind(Include = "Id,Descricao")] Uva uva)
    {
      try
      {
        if (ModelState.IsValid)
        {
          db.Entry(uva).State = EntityState.Modified;
          await db.SaveChangesAsync();
          return RedirectToAction("Index");
        }
        return View(uva);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
      }
    }

    // GET: Uvas/Delete/5
    public async Task<ActionResult> Delete(int? id)
    {
      try
      {
        if (id == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Uva uva = await db.Uvas.FindAsync(id);
        if (uva == null)
        {
          return HttpNotFound();
        }
        return View(uva);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
      }
    }

    // POST: Uvas/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
      try
      {
        Uva uva = await db.Uvas.FindAsync(id);
        uva.Status = false;
        db.Entry(uva).State= EntityState.Modified;
        await db.SaveChangesAsync();
        TempData["Error"] = "Não é possível deletar esse registro! Esta Uva já está vinculada a um produto.";
        return RedirectToAction("Index");
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
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
