﻿using System;
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
                        Produto = pr,
                      }).ToList();

        PedidoItemViewModel pedidoViewModel = new PedidoItemViewModel();
        pedidoViewModel.Pessoa = pedido.Pessoa;
        pedidoViewModel.Produtos = result;
        pedidoViewModel.PessoaId = pedido.PessoaId;
        pedidoViewModel.Pedido = pedido;
        pedidoViewModel.Total = (decimal)pedido.Total;
        pedidoViewModel.Quantidade = pedido.Quantidade;

        //foreach (var item in result)
        //{
        //  CarrinhoViewModel.AddItem(item.Produto);
        //}

        ViewBag.PessoaId = new SelectList(db.Pessoas.Where(p => p.PapelPessoaId == 1)
          .Where(p => p.TipoPessoaId == 1)
          .Where(x => x.Status == true)
          .OrderBy(x => x.NomeCompleto), "Id", "NomeCompleto", pedido.PessoaId);
        ViewBag.ProdutoId = new SelectList(db.Produtos.Where(x => x.Status == true).OrderBy(x => x.Nome), "Id", "Nome");
        return View(pedidoViewModel);



        //  if (id == null)
        //  {
        //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //  }
        //  Pedido pedido = db.Pedidos.Find(id);
        //  if (pedido == null)
        //  {
        //    return HttpNotFound();
        //  }
        //  return View(pedido);
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
    public void Create(int? ProdutoId, int? Quantidade = 1)
    {
      try
      {
        PedidoItemViewModel pedidoviewmodel = new PedidoItemViewModel();
        Produto produto = new Produto();
        Pedido pedido = new Pedido();

        if (ProdutoId != null) produto = db.Produtos.Find(ProdutoId);
        else
          throw new Exception("Produto não encontrado!");

        CarrinhoViewModel.AddItem(produto, (int)Quantidade);
        if (pedidoviewmodel.Produtos == null)
          pedidoviewmodel.Produtos = new List<FilaCarrinho>();

        pedidoviewmodel.Produtos.AddRange(CarrinhoViewModel.Lines);
        if (pedidoviewmodel.Pedido == null)
          pedidoviewmodel.Pedido = new Pedido();
        pedidoviewmodel.Total = CarrinhoViewModel.ComputeTotalValue();
        
        //db.Pedidos.Add(pedido);
        //db.SaveChanges();

        //db.Produtos.Add(produto);
        //db.Produtos.Attach(produto);

        //foreach (var prod in pedidoviewmodel.Produtos)
        //{
        //  Produto _prod = new Produto { Id = prod.ID };
        //  db.Produtos.Attach(_prod);
        //  pedido.Produtos.Add(_prod);
        //}
        //db.SaveChanges();

        //return pedido.Id ;
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        RedirectToAction("Index");
        throw ex;
      }

    }
    //public void FinalizarPedido()
    //{
    //  Pedido p = new Pedido();
    //  p.Produtos = new List<FilaCarrinho>();
    //  p.Produtos.AddRange(CarrinhoViewModel.Lines);
    //}

    // GET: Pedido/Edit/5

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

    // POST: Pedido/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

    // GET: Pedido/Delete/5

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

    // POST: Pedido/Delete/5
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
    public ActionResult FinalizarPedido(int idCliente)
    {
      try
      {
        Pedido pedido = new Pedido();

        pedido.DataPedido = DateTime.Now;
        pedido.PessoaId = idCliente;
        pedido.isVenda = true;
        pedido.isPessoaFisica = true;

        var pedidoIns = db.Pedidos.Add(pedido);
        db.SaveChanges();

        db.Pedidos.Add(pedido);
        db.Pedidos.Attach(pedido);



        foreach (var item in CarrinhoViewModel.Lines)
        {
          Produto p = new Produto { Id = item.Produto.Id, Nome = item.Produto.Nome, Descricao = item.Produto.Descricao, CustoUnitario = item.Produto.CustoUnitario, Quantidade = item.Produto.Quantidade, PrecoVenda = item.Produto.PrecoVenda, Status = true };
          db.Produtos.Attach(p);
          pedido.Produtos.Add(p);

        }
        pedido.Quantidade = CarrinhoViewModel.Lines.Count;
        pedido.Total = CarrinhoViewModel.ComputeTotalValue();
        CarrinhoViewModel.Clear();
        //db.Pedidos.Add(pedido);
        db.SaveChanges();

        TempData["Success"] = "Pedido cadastrado com sucesso!";
        return RedirectToAction("Index");
      }
      catch (Exception ex)
      {
        TempData["Error"] = "Ocorreu um erro,entre em contato com o administrador do sistema!";
        return RedirectToAction("Index");
        throw ex;
      }

    }
    public void EditarPedido()
    {

    }
    public int RemoverItem(int idProduto)
    {

      Produto produto = db.Produtos.Find(idProduto);
      CarrinhoViewModel.RemoveLine(produto);

      return 1;
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
