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
using Admin.DataContexts;

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
        if (!Validations.HasCredentials(User.Identity.GetUserName(), "Index", "Pedidos de Venda"))
        {
          return RedirectToAction("Index", "Home");
        }
        var pedidos = db.Pedidos.Include(p => p.Pessoa).OrderByDescending(p=>p.Id).ToList();
        return View(pedidos);
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
        Pedido pedido = db.Pedidos.Include("PedidosProdutos").Include("Pessoa").Where(p => p.Id == id).FirstOrDefault();

        List<Produto> produtos = new List<Produto>();

        foreach (var item in pedido.PedidosProdutos)
        {
          produtos.Add(db.Produtos.Find(item.ProdutoId));
        }

        PedidoItemViewModel pedidoViewModel = new PedidoItemViewModel();
        if (pedido.Pessoa == null)
          pedidoViewModel.Pessoa = new Pessoa();
        else
          pedidoViewModel.Pessoa = pedido.Pessoa;
        pedidoViewModel.Produtos = produtos;
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
        Pedido pedido = new Pedido { DataPedido = DateTime.Now, isVenda = true, isEmitido = false };
        ViewBag.Users = new SelectList(new IdentityDb().Users.AsNoTracking().OrderBy(x => x.Name), "Id", "Name", new { Id = 0, Nome = "Selecione" });
        db.Pedidos.Add(pedido);
        db.SaveChanges();
        ViewBag.PedidoId = pedido.Id;
        PedidoItemViewModel pedidoViewModel = new PedidoItemViewModel();
        pedidoViewModel.Pedido = pedido;
        CarrinhoViewModel.Clear();
        return View(pedidoViewModel);
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
    public ActionResult AddItem(int PedidoId, string UserId, int PessoaId, int ProdutoId, int Quantidade = 1)
    {
      try
      {
        Produto produto = new Produto();
        Pedido pedido = new Pedido();
        PedidoItemViewModel pedidoItemviewModel = new PedidoItemViewModel();
        //busca o cliente e adiciona ao pedido
        Pessoa pessoa = new Pessoa();
        pessoa = db.Pessoas.AsNoTracking().Where(p => p.Id == PessoaId).FirstOrDefault();

        //busca o produto e adiciona ao carrinho
        produto = db.Produtos.Find(ProdutoId);
        CarrinhoViewModel.AddItem(produto, Quantidade);

        //busca a pessoa e adiciona ao pedido - atualiza o pedido
        pedido.Id = PedidoId;
        pedido.PessoaId = PessoaId;
        pedido.Quantidade = CarrinhoViewModel.Lines.Sum(e => e.Quantidade);
        pedido.Produtos = CarrinhoViewModel.Lines.Select(p => new Produto { Id = p.Produto.Id, Nome = p.Produto.Nome, Quantidade = p.Quantidade, PrecoVenda = p.Produto.PrecoVenda }).ToList();
        pedido.Total = CarrinhoViewModel.ComputeTotalValue();
        pedido.UserId = UserId;
        pedido.DataPedido = DateTime.Now;
        pedido.isEmitido = false;
        pedido.isVenda = true;
        pedido.isPessoaFisica = pessoa.TipoPessoaId == 1 ? true : false;
        db.Entry(pedido).State = EntityState.Modified;
        db.SaveChanges();
        pedidoItemviewModel.Pedido = pedido;
        pedidoItemviewModel.PessoaId = PessoaId;
        //pedidoItemviewModel.Pessoa = pessoa;
        pedidoItemviewModel.Produtos = pedido.Produtos.ToList();
        pedidoItemviewModel.Pedido.UserId = UserId;


        ViewBag.Users = new SelectList(new IdentityDb().Users.AsNoTracking().OrderBy(x => x.Name), "Id", "Name", new { Id = 0, Nome = "Selecione" });
        return Json(pedidoItemviewModel);
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        RedirectToAction("Index");
        throw ex;
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
        //if (id == null)
        //{
        //  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //}
        //Pedido pedido = db.Pedidos.Find(id);
        //var result = (from p in db.Pedidos
        //              from pr in p.Produtos
        //              join ppr in db.Produtos on pr.Id equals ppr.Id
        //              where p.Id == id
        //              select new FilaCarrinho
        //              {
        //                ID = pr.Id,
        //                Produto = pr
        //              }).ToList();
        //PedidoItemViewModel pedidoViewModel = new PedidoItemViewModel();
        ////pedidoViewModel.Produtos = result;
        //pedidoViewModel.PessoaId = pedido.PessoaId;
        //pedidoViewModel.Total = (decimal)pedido.Total;

        //foreach (var item in result)
        //{
        //  CarrinhoViewModel.AddItem(item.Produto);
        //}

        //ViewBag.PessoaId = new SelectList(db.Pessoas.Where(p => p.PapelPessoaId == 1)
        //  .Where(p => p.TipoPessoaId == 1)
        //  .Where(x => x.Status == true)
        //  .OrderBy(x => x.NomeCompleto), "Id", "NomeCompleto", pedido.PessoaId).FirstOrDefault();
        //ViewBag.ProdutoId = new SelectList(db.Produtos.Where(x => x.Status == true).OrderBy(x => x.Nome), "Id", "Nome").FirstOrDefault();
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
        Pedido pedido = db.Pedidos.Find(id);
        db.Pedidos.Remove(pedido);
        db.SaveChanges();
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
    //[HttpPost]
    //public JsonResult TestePost(int Id, int qtde)
    //{
    //  try
    //  {
    //    //buscar produto pelo id
    //    Produto produto = db.Produtos.Find(Id);
    //    if (produto != null && produto.Quantidade >= qtde)
    //    {
    //      CarrinhoViewModel.AddItem(produto, qtde);

    //      var produtos = CarrinhoViewModel.Lines.Select(p =>
    //      new
    //      {
    //        qtde = CarrinhoViewModel.Lines.Count,
    //        nome = p.Produto.Nome,
    //      }).ToList();

    //      return Json(produtos
    //        , JsonRequestBehavior.AllowGet);
    //    }
    //    else
    //    {
    //      //var soma = idProduto + qtde;
    //      return Json("Falha ao adicionar!", JsonRequestBehavior.AllowGet);
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
    //    // return RedirectToAction("Index");
    //  }
    //  finally
    //  {
    //    db.Dispose();
    //  }
    //}
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
    public ActionResult FinalizarPedido(int PedidoId, string UserId, int PessoaId)
    {
      Produto produto = new Produto();
      try
      {
        //buscar o pedido na base
        Pedido pedido = db.Pedidos.Find(PedidoId);
        pedido.isEmitido = true;
        pedido.UserId = UserId;
        pedido.PessoaId = PessoaId;
        db.Entry(pedido).State = EntityState.Modified;
        db.SaveChanges();
        foreach (var item in CarrinhoViewModel.Lines.Select(p => new Produto { Id = p.Produto.Id, Nome = p.Produto.Nome, Descricao = p.Produto.Descricao, ClasseId = p.Produto.ClasseId, TipoId = p.Produto.TipoId, PaisId = p.Produto.PaisId, SafraId = p.Produto.SafraId, Quantidade = p.Quantidade, PrecoVenda = p.Produto.PrecoVenda }).ToList())
        {
          db.Produtos.Add(item);
          db.Produtos.Attach(item);
          db.Pedidos.Add(pedido);
          db.Pedidos.Attach(pedido);
          pedido.PedidosProdutos.Add(new PedidoProduto { PedidoId = pedido.Id, ProdutoId = item.Id, Quantidade = item.Quantidade, PrecoVenda = item.PrecoVenda });
          db.SaveChanges();
        }
        return Json("/Pedido/Index");
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }
    public void EditarPedido()
    {

    }
    public ActionResult RemoverItem(int PedidoId, int ProdutoId)
    {
      try
      {
        //busca o produto e remove 
        Produto produto = new Produto();
        Pedido pedido = new Pedido();
        PedidoItemViewModel pedidoItemviewModel = new PedidoItemViewModel();
        //busca o produto e remove do carrinho
        produto = db.Produtos.Find(ProdutoId);
        CarrinhoViewModel.RemoveLine(produto);

        pedido.Id = PedidoId;
        pedido.Quantidade = CarrinhoViewModel.Lines.Sum(e => e.Quantidade);
        pedido.Produtos = CarrinhoViewModel.Lines.Select(p => new Produto { Id = p.Produto.Id, Nome = p.Produto.Nome, Quantidade = p.Quantidade, PrecoVenda = p.Produto.PrecoVenda }).ToList();
        pedido.Total = CarrinhoViewModel.ComputeTotalValue();
        pedido.DataPedido = DateTime.Now;
        pedido.isEmitido = false;
        pedido.isVenda = true;
        db.Entry(pedido).State = EntityState.Modified;
        db.SaveChanges();
        pedidoItemviewModel.Pedido = pedido;
        //pedidoItemviewModel.Pessoa = pessoa;
        pedidoItemviewModel.Produtos = pedido.Produtos.ToList();

        return Json(pedidoItemviewModel);
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }
    public void Clear(int PedidoId)
    {
      try
      {
        CarrinhoViewModel.Clear();
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

    public int CreatePedido(int? ProdutoId)
    {
      try
      {
        var produto = db.Produtos.Find(ProdutoId);
        Pedido pedido = new Pedido();
        pedido.DataPedido = DateTime.Now;
        pedido.isVenda = true;
        pedido.isPessoaFisica = true;
        pedido.isEmitido = false;
        foreach (var prod in CarrinhoViewModel.Lines)
        {
          //pedido.Produtos.Add(prod.Produto);

        }
        pedido.Quantidade = CarrinhoViewModel.Lines.Sum(i => i.Quantidade);
        pedido.Total = CarrinhoViewModel.ComputeTotalValue();

        db.Pedidos.Add(pedido);
        db.SaveChanges();
        return pedido.Id;
      }
      catch (Exception)
      {

        throw;
      }
      finally
      {
        db.Dispose();
      }
    }
  }
}
