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
using Admin.Models;
using Newtonsoft;
using Newtonsoft.Json;
using Admin.Functions;
using Microsoft.AspNet.Identity;

namespace Admin.Controllers
{
  [Authorize]
  public class PedidoController : Controller
  {
    private EntitiesDb db = new EntitiesDb();
    public ActionResult Index()
    {
      try
      {
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Index", "Pedidos"))
        {
          return RedirectToAction("Index", "Home");
        }
        var pedidos = db.Pedidos.Include(p => p.Pessoa).Include(pr => pr.Produtos);
        return View(pedidos.ToList());
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
        Pedido pedido = db.Pedidos.Find(id);

        var result = (from p in db.Pedidos
                      from pr in p.Produtos
                      join he in db.HistoricoEstoque on pr.Id equals he.ProdutoId
                      join ppr in db.Produtos on pr.Id equals ppr.Id
                      where p.Id == id
                      select  new FilaCarrinho
                      {
                        ID = pr.Id,
                        Produto = pr,
                        Quantidade = he.Quantidade - he.Ajuste
                      }).ToList();

        PedidoItemViewModel pedidoViewModel = new PedidoItemViewModel();
        if (pedido.Pessoa == null)
          pedidoViewModel.Pessoa = new Pessoa();
        else
          pedidoViewModel.Pessoa = pedido.Pessoa;
        pedidoViewModel.Produtos = result;
        pedidoViewModel.PessoaId = pedido.PessoaId;
        pedidoViewModel.PedidoId = (int)id;
        pedidoViewModel.Pedido = pedido;
        pedidoViewModel.Total = (decimal)pedido.Total;
        pedidoViewModel.Quantidade = pedido.Quantidade;
        db.Dispose();
        return View(pedidoViewModel);
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
      try
      {
        ViewBag.PessoaId = new SelectList(db.Pessoas
          .Where(p => p.PapelPessoaId == 1)
          .Where(p => p.TipoPessoaId == 1)
          .Where(x => x.Status == true)
          .OrderBy(x => x.NomeCompleto), "Id", "NomeCompleto");
        ViewBag.ProdutoId = new SelectList(db.Produtos.Where(x => x.Status == true).OrderBy(x => x.Nome), "Id", "Nome");
        ViewBag.idPedido = 0;
        CarrinhoViewModel.Clear();
        return View();
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }
    [HttpPost]
    public ActionResult Create(int? PedidoId, int? ProdutoId, int? Quantidade = 1)
    {
      Produto produto = db.Produtos.Find(ProdutoId);
      try
      {
        if (PedidoId > 0)
        {
          CarrinhoViewModel.AddItem(produto, (int)Quantidade);
          var pedido = db.Pedidos.Find(PedidoId);

          pedido.DataPedido = DateTime.Now;
          pedido.Quantidade = CarrinhoViewModel.Lines.Sum(i=>i.Quantidade);
          pedido.Total = CarrinhoViewModel.ComputeTotalValue();

          pedido = db.Pedidos.Include(r => r.Produtos).Single(r => r.Id == PedidoId);
          db.Entry(pedido).CurrentValues.SetValues(pedido);
          pedido.Produtos.Clear();

          foreach (FilaCarrinho item in CarrinhoViewModel.Lines)
          {
            pedido.Produtos.Add(db.Produtos.Find(item.Produto.Id));
          }

          db.SaveChanges();
          db.Dispose();
          return Json(data: PedidoId);
        }
        else
        {
          //produto = new Produto();
          Pedido pedido = new Pedido();
          CarrinhoViewModel.AddItem(produto, (int)Quantidade);

          pedido.DataPedido = DateTime.Now;
          pedido.isVenda = true;
          pedido.isPessoaFisica = true;
          pedido.Quantidade = CarrinhoViewModel.Lines.Sum(i=>i.Quantidade);
          pedido.Total = CarrinhoViewModel.ComputeTotalValue();

          db.Pedidos.Add(pedido);
          db.SaveChanges();

          db.Pedidos.Add(pedido);
          db.Pedidos.Attach(pedido);
          foreach (var item in CarrinhoViewModel.Lines)
          {
            produto = item.Produto;
            db.Produtos.Attach(produto);
            pedido.Produtos.Add(produto);
          }

          db.SaveChanges();
          db.Dispose();
          var data = pedido.Id;
          return Json(data);
        }
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        RedirectToAction("Index");
        throw ex;
      }

    }
    public ActionResult Edit(int? id)
    {
      try
      {
        if (id == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Pedido pedido = db.Pedidos.Find(id);
        var result = (from p in db.Pedidos
                      from pr in p.Produtos
                      join ppr in db.Produtos on pr.Id equals ppr.Id
                      where p.Id == id
                      select new FilaCarrinho
                      {
                        ID = pr.Id,
                        Produto = pr
                      }).ToList();
        PedidoItemViewModel pedidoViewModel = new PedidoItemViewModel();
        pedidoViewModel.Produtos = result;
        pedidoViewModel.PessoaId = pedido.PessoaId;
        pedidoViewModel.Total = (decimal)pedido.Total;

        foreach (var item in result)
        {
          CarrinhoViewModel.AddItem(item.Produto);
        }

        ViewBag.PessoaId = new SelectList(db.Pessoas.Where(p => p.PapelPessoaId == 1)
          .Where(p => p.TipoPessoaId == 1)
          .Where(x => x.Status == true)
          .OrderBy(x => x.NomeCompleto), "Id", "NomeCompleto", pedido.PessoaId);
        ViewBag.ProdutoId = new SelectList(db.Produtos.Where(x => x.Status == true).OrderBy(x => x.Nome), "Id", "Nome");
        return View(pedidoViewModel);
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
    public ActionResult Edit(PedidoItemViewModel pedido, string botao, int idProduto)
    {
      try
      {

        //ViewBag.PessoaId = new SelectList(db.Pessoas, "Id", "NomeCompleto", pedido.Pedido.PessoaId);
        //return View();
        if (botao == "removerProduto")
        {
          Produto produto = db.Produtos.Find(idProduto);
          CarrinhoViewModel.RemoveLine(produto);
        }

        ViewBag.PessoaId = new SelectList(db.Pessoas.Where(p => p.PapelPessoaId == 1)
         .Where(p => p.TipoPessoaId == 1)
         .Where(x => x.Status == true)
         .OrderBy(x => x.NomeCompleto), "Id", "NomeCompleto", pedido.PessoaId);
        ViewBag.ProdutoId = new SelectList(db.Produtos.Where(x => x.Status == true).OrderBy(x => x.Nome), "Id", "Nome");
        return View(pedido);
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
        Pedido pedido = db.Pedidos.Find(id);
        if (pedido == null)
        {
          return HttpNotFound();
        }
        return View(pedido);
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
        Pedido pedido = db.Pedidos.Find(id);
        db.Pedidos.Remove(pedido);
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }
    [HttpPost]
    public JsonResult TestePost(int Id, int qtde)
    {
      try
      {
        //buscar produto pelo id
        Produto produto = db.Produtos.Find(Id);
        if (produto != null && produto.Quantidade >= qtde)
        {
          CarrinhoViewModel.AddItem(produto, qtde);

          var produtos = CarrinhoViewModel.Lines.Select(p =>
          new
          {
            qtde = CarrinhoViewModel.Lines.Count,
            nome = p.Produto.Nome,
          }).ToList();

          return Json(produtos
            , JsonRequestBehavior.AllowGet);
        }
        else
        {
          //var soma = idProduto + qtde;
          return Json("Falha ao adicionar!", JsonRequestBehavior.AllowGet);
        }
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        // return RedirectToAction("Index");
        throw ex;
      }
    }
    [HttpPost]
    public JsonResult PegarFilaCarrinho(int acionar)
    {
      List<FilaCarrinho> ItensCarrinho = new List<FilaCarrinho>();
      ItensCarrinho.AddRange(CarrinhoViewModel.Lines);

      return Json(ItensCarrinho, JsonRequestBehavior.AllowGet);
    }
    [HttpPost, ActionName("AddToChart")]
    [ValidateAntiForgeryToken]
    public ActionResult AddConfirmed(int id)
    {
      try
      {
        Produto produto = db.Produtos.Find(id);
        produto.Status = false;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }
    public ActionResult FinalizarPedido(int id)
    {
      Produto produto = new Produto();
      try
      {
        var pedido = db.Pedidos.Find(id);
        pedido.DataPedido = DateTime.Now;
        pedido.Quantidade = CarrinhoViewModel.Lines.Sum(i=>i.Quantidade);
        pedido.Total = CarrinhoViewModel.ComputeTotalValue();

        var recipeItem = db.Pedidos.Include(r => r.Produtos).Single(r => r.Id == id);
        db.Entry(recipeItem).CurrentValues.SetValues(pedido);
        recipeItem.Produtos.Clear();

        foreach (FilaCarrinho item in CarrinhoViewModel.Lines)
        {
          produto = db.Produtos.Find(item.Produto.Id);
          HistoricoEstoque he = new HistoricoEstoque();
          he.ProdutoId = item.Produto.Id;
          he.CriadoEm = DateTime.Now;
          he.Quantidade = item.Produto.Quantidade;
          he.Ajuste =  item.Produto.Quantidade - item.Quantidade;
          db.HistoricoEstoque.Add(he);
          produto.Quantidade = item.Produto.Quantidade - item.Quantidade;
          pedido.Produtos.Add(produto);
        }
        db.SaveChanges();
        db.Dispose();
        CarrinhoViewModel.Clear();

        return RedirectToActionPermanent("Index", "Produtos");
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }
    public void EditarPedido()
    {

    }
    public int RemoverItem(int PedidoId, int ProdutoId)
    {


      Produto produto = db.Produtos.Find(ProdutoId);
      CarrinhoViewModel.RemoveLine(produto);
      var pedido = db.Pedidos.Find(PedidoId);

      pedido.DataPedido = DateTime.Now;
      pedido.Quantidade = CarrinhoViewModel.Lines.Sum(i => i.Quantidade);
      pedido.Total = CarrinhoViewModel.ComputeTotalValue();

      //atualiza
      pedido = db.Pedidos.Include(r => r.Produtos).Single(r => r.Id == PedidoId);
      db.Entry(pedido).CurrentValues.SetValues(pedido);
      pedido.Produtos.Clear();

      foreach (FilaCarrinho item in CarrinhoViewModel.Lines)
      {
        pedido.Produtos.Add(db.Produtos.Find(item.Produto.Id));
      }

      db.SaveChanges();
      db.Dispose();
      return PedidoId;



      //Produto produto = db.Produtos.Find(ProdutoId);
      //CarrinhoViewModel.RemoveLine(produto);

      //// return one instance each entity by primary key
      //var _produto = db.Produtos.FirstOrDefault(p => p.Id == ProdutoId);
      //var pedido = db.Pedidos.FirstOrDefault(s => s.Id == PedidoId);
      //pedido.Total = CarrinhoViewModel.ComputeTotalValue();
      //pedido.Quantidade = CarrinhoViewModel.Lines.Sum(i => i.Quantidade);
      //// call Remove method from navigation property for any instance
      //pedido.Produtos.Remove(_produto);
      //// also works
      ////product.Pedidos.Remove(supplier);

      //// call SaveChanges from context
      //db.SaveChanges();
      //db.Dispose();
      //return 1;
    }
    public void Salvarmany(int idProduto, int idPedido)
    {
      Produto p = new Produto { Id = idProduto };

      db.Produtos.Add(p);
      db.Produtos.Attach(p);

      Pedido pe = new Pedido { Id = idPedido };
      db.Pedidos.Add(pe);
      db.Pedidos.Attach(pe);

      //p.Pedidos.Add(pe);

      db.SaveChanges();
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
