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
    public ActionResult Index(int? id)
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Index", "Produtos"))
        {
          return RedirectToAction("Index", "Home");
        }
        var produtos =
          db.Produtos.
          Include(uv => uv.Uvas).
          Include(p => p.Pais).
          Include(p => p.Safra).
          Include(cl => cl.Classe).
          Include(p => p.Tipo).
          Where(x => x.Status == true).
          ToList();
        if (id > 0)
          ViewBag.PedidoId = id;
        else
          ViewBag.PedidoId = 0;

        return View(produtos);
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
    public ActionResult Create([Bind(Include = "Id,Arquivo,Imagem,Nome,Descricao,Uva,ClasseId,Teor_Alcolico,Tipo,CustoUnitario,Quantidade,PrecoVenda,Volume,DataValidade,Status,PaisId,SafraId,Uvas,TipoId,selectedUvas")] Produto produto)
    {
      //VALIDAR CAMPOS OBRIGATORIOS()
      var img = Request.Files["Imagem"];
      try
      {
        CarregarForm();
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Create", "Produtos"))
        {
          return RedirectToAction("Index", "Home");
        }
        if (!ValidaCampos(produto))
        {
          return View(produto);
        }

        if (img != null && img.ContentLength > 0)
          produto.Imagem = FileManager.UploadSingleFile(img, Path.Combine(Server.MapPath("~/Uploads/Produtos")));

        produto.Status = true;
        //produto.selectedUvas = selectedUvas;


        db.Produtos.Add(produto);
        db.SaveChanges();

        var hist = db.HistoricoEstoque.Add(new HistoricoEstoque {Ajuste = produto.Quantidade,Quantidade = produto.Quantidade,CriadoEm=DateTime.Now});
        db.HistoricoEstoque.Add(hist);
        db.SaveChanges();


        db.Produtos.Add(produto);
        db.Produtos.Attach(produto);

        foreach (var UvaID in produto.selectedUvas)
        {
          Uva uva = new Uva { Id = UvaID };
          db.Uvas.Attach(uva);
          produto.Uvas.Add(uva);
        }

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
        CarregarFormFromDb(produto);
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
    public ActionResult Edit(Produto produto, int[] selectedUvas)
    {
      try
      {
        //CarregarForm();

        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Edit", "Produtos"))
        {
          return RedirectToAction("Index", "Home");
        }
        if (ModelState.IsValid)
        {
          CarregarFormFromDb(produto);

          var img = Request.Files["PostedImg"];
          if (img != null && img.ContentLength > 0)
            produto.Imagem = FileManager.UploadSingleFile(img, Path.Combine(Server.MapPath("~/Uploads/Produtos")));
          else
            produto.Imagem = produto.Imagem;

          if (!ValidaCampos(produto))
          {
            return View(produto);
          }

          //get the id of the current recipe
          produto.Status = true;
          int id = produto.Id;
          //load recipe with ingredients from the database
          var recipeItem = db.Produtos.Include(r => r.Uvas).Single(r => r.Id == id);
          //apply the values that have changed
          db.Entry(recipeItem).CurrentValues.SetValues(produto);
          //clear the ingredients to let the framework know they have to be processed
          recipeItem.Uvas.Clear();
          //now reload the ingredients again, but from the list of selected ones as per model provided by the view
          foreach (int ingId in selectedUvas)
          {
            recipeItem.Uvas.Add(db.Uvas.Find(ingId));
          }
          //finally, save changes as usual
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
    private void UpdateProdutoUvas(int[] selectedUvas, Produto produto)
    {
      if (selectedUvas == null)
      {
        produto.Uvas = new List<Uva>();
        return;
      }

      var selectedUvasHS = new HashSet<int>(selectedUvas);
      var produtoUvas = new HashSet<int>
          (produto.Uvas.Select(u => u.Id));
      foreach (var uva in db.Uvas)
      {
        if (selectedUvasHS.Contains(uva.Id))
        {
          if (!produtoUvas.Contains(uva.Id))
          {
            produto.Uvas.Add(uva);
          }
        }
        else
        {
          if (produtoUvas.Contains(uva.Id))
          {
            produto.Uvas.Remove(uva);
          }
        }
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
        db.Entry(produto).State = EntityState.Modified;
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
      if (produto.selectedUvas == null || produto.selectedUvas.Count() == 0)
      {
        ModelState.AddModelError("selectedUvas", "Selecione ao menos uma uva!!");
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
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    protected void CarregarForm(int? ProdutoId = 0)
    {
      try
      {
        ViewBag.PaisId = new SelectList(db.Paises.Where(x => x.Status == true).AsNoTracking().OrderBy(x => x.Nome), "Id", "Nome",new { Id=0,Nome="Selecione"});
        ViewBag.SafraId = new SelectList(db.Safras.Where(x => x.Status == true).AsNoTracking().OrderBy(x => x.Ano), "Id", "Ano", new { Id = 0, Nome = "Selecione" });
        ViewBag.ClasseId = new SelectList(db.Classes.AsNoTracking().OrderBy(c => c.Descricao), "Id", "Descricao", new { Id = 0, Descricao = "Selecione" });
        ViewBag.TipoId = new SelectList(db.Tipos.AsNoTracking().OrderBy(c => c.Descricao), "Id", "Descricao", new { Id = 0, Descricao = "Selecione" });
        ViewBag.UvaId = new MultiSelectList(db.Uvas.AsNoTracking().OrderBy(u => u.Descricao).ToList(), "Id", "Descricao");
      }
      catch (Exception ex)
      {

        throw;
      }
      finally
      {
        //db.Dispose();
      }
    }
    protected void CarregarFormFromDb(Produto produto)
    {
      try
      {
        ViewBag.PaisId = new SelectList(db.Paises.Where(x => x.Status != false), "Id", "Nome", produto.PaisId);
        ViewBag.SafraId = new SelectList(db.Safras.Where(x => x.Status != false), "Id", "Ano", produto.SafraId);
        ViewBag.ClasseId = new SelectList(db.Classes.AsNoTracking().OrderBy(c => c.Descricao), "Id", "Descricao", produto.ClasseId);
        ViewBag.TipoId = new SelectList(db.Tipos.AsNoTracking().OrderBy(c => c.Descricao), "Id", "Descricao", produto.TipoId);
        ViewBag.UvaId = new MultiSelectList(db.Uvas.AsNoTracking().OrderBy(u => u.Descricao), "Id", "Descricao", produto.Uvas.Select(u => u.Id));
      }
      catch (Exception ex)
      {

        throw;
      }
      finally
      {
        //db.Dispose();
      }
    }
  }
}
