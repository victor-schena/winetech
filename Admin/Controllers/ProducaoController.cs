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
    public class ProducaoController : Controller
    {
        private EntitiesDb db = new EntitiesDb();

        // GET: Producao
        public async Task<ActionResult> Index()
        {
            var producao = db.Producao.Include(p => p.Classe).Include(p => p.Tipo).Include(p => p.Uva);
            return View(await producao.ToListAsync());
        }

        // GET: Producao/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producao producao = await db.Producao.FindAsync(id);
            if (producao == null)
            {
                return HttpNotFound();
            }
            return View(producao);
        }

        // GET: Producao/Create
        public ActionResult Create()
        {
            ViewBag.ClasseId = new SelectList(db.Classes, "Id", "Descricao");
            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Descricao");
            ViewBag.UvaId = new SelectList(db.Uvas, "Id", "Descricao");
            return View();
        }

        // POST: Producao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DataCriacao,TipoId,UvaId,ClasseId,Volume,KgUva,KgAcucar,Vasilhame")] Producao producao)
        {
            if (ModelState.IsValid)
            {
                db.Producao.Add(producao);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClasseId = new SelectList(db.Classes, "Id", "Descricao", producao.ClasseId);
            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Descricao", producao.TipoId);
            ViewBag.ClasseId = new SelectList(db.Uvas, "Id", "Descricao", producao.ClasseId);
            return View(producao);
        }

        // GET: Producao/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producao producao = await db.Producao.FindAsync(id);
            if (producao == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClasseId = new SelectList(db.Classes, "Id", "Descricao", producao.ClasseId);
            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Descricao", producao.TipoId);
            ViewBag.UvaId = new SelectList(db.Uvas, "Id", "Descricao", producao.ClasseId);
            return View(producao);
        }

        // POST: Producao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DataCriacao,TipoId,UvaId,ClasseId,Volume,KgUva,KgAcucar,Vasilhame")] Producao producao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClasseId = new SelectList(db.Classes, "Id", "Descricao", producao.ClasseId);
            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Descricao", producao.TipoId);
            ViewBag.ClasseId = new SelectList(db.Uvas, "Id", "Descricao", producao.ClasseId);
            return View(producao);
        }

        // GET: Producao/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producao producao = await db.Producao.FindAsync(id);
            if (producao == null)
            {
                return HttpNotFound();
            }
            return View(producao);
        }

        // POST: Producao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Producao producao = await db.Producao.FindAsync(id);
            db.Producao.Remove(producao);
            await db.SaveChangesAsync();
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
