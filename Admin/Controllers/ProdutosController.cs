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
using Admin.Functions;
using Microsoft.AspNet.Identity;
using System.IO;

namespace Admin.Controllers
{
  [Authorize]
  public class ProdutosController : Controller
  {

    private EntitiesDb db = new EntitiesDb();

    // GET: Produtos

    public ActionResult Index()
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Index", "Produtos"))
        {
          return RedirectToAction("Index", "Home");
        }
        var produtos = db.Produtos.Include(uv => uv.Uvas).Include(p => p.Pais).Include(p => p.Safra).Include(cl => cl.Classe).Include(p => p.Tipo).Where(x => x.Status == true).ToList();
        return View(produtos);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }

    // GET: Produtos/Details/5

    public ActionResult Details(int? id)
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Details", "Produtos"))
        {
          return RedirectToAction("Index", "Home");
        }
        if (id == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Produto produto = db.Produtos.Include(p => p.Pais).Include(s => s.Safra).Where(x => x.Id == id).First();
        if (produto == null)
        {
          return HttpNotFound();
        }
        return View(produto);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }

    // GET: Produtos/Create

    public ActionResult Create()
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Create", "Produtos"))
        {
          return RedirectToAction("Index", "Home");
        }
        CarregarForm();

        return View();
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }
    }

    // POST: Produtos/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,Arquivo,Imagem,Nome,Descricao,Uva,ClasseId,Teor_Alcolico,Tipo,CustoUnitario,Quantidade,PrecoVenda,Volume,DataValidade,Status,PaisId,SafraId,UvaId,TipoId")] Produto produto, int[] UvaId)
    {
      //VALIDAR CAMPOS OBRIGATORIOS()
      var img = Request.Files["Imagem"];
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Create", "Produtos"))
        {
          return RedirectToAction("Index", "Home");
        }
        produto.selectedUvas = UvaId;
        CarregarForm();

        if (!ValidaCampos(produto))
        {
          return View(produto);
        }

        if (img != null && img.ContentLength > 0)
          produto.Imagem = FileManager.UploadSingleFile(img, Path.Combine(Server.MapPath("~/Uploads/Produtos")));

        produto.Status = true;

        foreach (var uva in UvaId)
        {
          Uva u = new Uva { Id = uva };
          produto.Uvas.Add(u);
        }

        db.Produtos.Add(produto);
        db.SaveChanges();

        TempData["Success"] = "Registro Salvo.";

        return RedirectToAction("Index");
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }
    }

    // GET: Produtos/Edit/5

    public ActionResult Edit(int? id)
    {
      try
      {
        CarregarForm();
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Edit", "Produtos"))
        {
          return RedirectToAction("Index", "Home");
        }
        if (id == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Produto produto = db.Produtos.Find(id);
        if (produto == null)
        {
          return HttpNotFound();
        }
        ViewBag.PaisId = new SelectList(db.Paises.Where(x => x.Status != false), "Id", "Nome", produto.PaisId);
        ViewBag.SafraId = new SelectList(db.Safras.Where(x => x.Status != false), "Id", "Ano", produto.SafraId);
        return View(produto);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }

    // POST: Produtos/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Produto produto)
    {
      try
      {
        CarregarForm();

        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Edit", "Produtos"))
        {
          return RedirectToAction("Index", "Home");
        }
        if (ModelState.IsValid)
        {
          var img = Request.Files["PostedImg"];
          if (img != null && img.ContentLength > 0)
            produto.Imagem = FileManager.UploadSingleFile(img, Path.Combine(Server.MapPath("~/Uploads/Produtos")));
          else
            produto.Imagem = produto.Imagem;
          if (!ValidaCampos(produto))
          {
            return View(produto);
          }

          produto.Status = true;
          db.Entry(produto).State = EntityState.Modified;
          db.SaveChanges();
          TempData["Success"] = "Registro salvo com sucesso.";
          return RedirectToAction("Index");
        }
        ViewBag.PaisId = new SelectList(db.Paises, "Id", "Nome", produto.PaisId);
        ViewBag.SafraId = new SelectList(db.Safras, "Id", "Ano", produto.SafraId);
        return View(produto);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }

    // GET: Produtos/Delete/5

    public ActionResult Delete(int? id)
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Delete", "Produtos"))
        {
          return RedirectToAction("Index", "Home");
        }
        if (id == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Produto produto = db.Produtos.Find(id);
        if (produto == null)
        {
          return HttpNotFound();
        }
        return View(produto);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }

    // POST: Produtos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Delete", "Produtos"))
        {
          return RedirectToAction("Index", "Home");
        }
        Produto produto = db.Produtos.Find(id);
        produto.Status = false;
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

    [HttpPost, ActionName("AddItem")]
    [ValidateAntiForgeryToken]
    public ActionResult AddConfirmed(int id)
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Create", "Produtos"))
        {
          return RedirectToAction("Index", "Home");
        }

        Produto produto = db.Produtos.Find(id);
        produto.Status = false;
        db.SaveChanges();
        TempData["Success"] = "Usuário Salvo.";
        return RedirectToAction("Index");
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }
    public bool ValidaCampos(Produto produto)
    {
      ModelState.Clear();
      bool validacao = true;
      if (string.IsNullOrEmpty(produto.Nome))
      {
        ModelState.AddModelError("Nome", "O campo nome é obrigatório!");
        validacao = false;
      }
      if (produto.CustoUnitario <= 0)
      {
        ModelState.AddModelError("CustoUnitario", "O campo Preço de Venda é obrigatório!");
        validacao = false;
      }
      if (produto.PrecoVenda <= 0)
      {
        ModelState.AddModelError("PrecoVenda", "O campo Preço de Venda é obrigatório!");
        validacao = false;
      }
      if (produto.Quantidade <= 0)
      {
        ModelState.AddModelError("Quantidade", "O campo quantidade deve ser maior que zero!");
        validacao = false;
      }
      if (produto.DataValidade.Equals(DateTime.MinValue) || string.IsNullOrEmpty(produto.DataValidade.ToString()))
      {
        ModelState.AddModelError("DataValidade", "O campo Data de Validade é obrigatório!");
        validacao = false;
      }
      if (produto.DataValidade < DateTime.Now.Date)
      {
        ModelState.AddModelError("DataValidade", "A data de validade não pode ser menor que a data atual!");
        validacao = false;
      }


      return validacao;
    }
    protected void ProcessarImagem(string Imagem)
    {

    }
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    protected void CarregarForm()
    {
      ViewBag.PaisId = new SelectList(db.Paises.Where(x => x.Status == true).OrderBy(x => x.Nome), "Id", "Nome");
      ViewBag.SafraId = new SelectList(db.Safras.Where(x => x.Status == true).OrderBy(x => x.Ano), "Id", "Ano");
      ViewBag.ClasseId = new SelectList(db.Classes.OrderBy(c => c.Descricao), "Id", "Descricao");
      ViewBag.TipoId = new SelectList(db.Tipos.OrderBy(c => c.Descricao), "Id", "Descricao");
      ViewBag.UvaId = new MultiSelectList(db.Uvas.ToList(), "Id", "Descricao");
    }
  }
}
