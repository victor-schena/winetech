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
  public class SafrasController : Controller
    {
        private EntitiesDb db = new EntitiesDb();

    // GET: Safras
    
    public ActionResult Index()
        {
            return View(db.Safras.ToList());
        }

    // GET: Safras/Details/5
    [Authorize]
    public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Safra safra = db.Safras.Find(id);
            if (safra == null)
            {
                return HttpNotFound();
            }
            return View(safra);
        }

    // GET: Safras/Create
    [Authorize]
    public ActionResult Create()
        {
            return View();
        }

        // POST: Safras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ano")] Safra safra)
        {
            if (ModelState.IsValid)
            {
                //safra.Status = true;
                db.Safras.Add(safra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(safra);
        }

    // GET: Safras/Edit/5
    [Authorize]
    public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Safra safra = db.Safras.Find(id);
            if (safra == null)
            {
                return HttpNotFound();
            }
            return View(safra);
        }

        // POST: Safras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ano")] Safra safra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(safra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(safra);
        }

    // GET: Safras/Delete/5
    [Authorize]
    public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Safra safra = db.Safras.Find(id);
            if (safra == null)
            {
                return HttpNotFound();
            }
            return View(safra);
        }

        // POST: Safras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Safra safra = db.Safras.Find(id);
            //safra.Status = false;
            db.SaveChanges();
            return RedirectToAction("Index");
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
