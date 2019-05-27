using Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models
{
  public static class CarrinhoViewModel
  {
    private static List<FilaCarrinho> lineCollection = new List<FilaCarrinho>();
    //add itens
    public static void AddItem(Produto produto, int quantidade=1)
    {
      FilaCarrinho line = lineCollection.Where(p => p.Produto.Id == produto.Id).FirstOrDefault();
      if (line == null){
        lineCollection.Add(
          new FilaCarrinho{
          Produto = produto,
          Quantidade = quantidade
        });
      }
      else {
        line.Quantidade += quantidade;
      }
    }
    //remove itens
    public static void RemoveLine(Produto produto) 
      => lineCollection.RemoveAll(l => l.Produto.Id == produto.Id);
    //calcula valor total
    public static decimal ComputeTotalValue() 
      =>lineCollection.Sum(e => e.Produto.PrecoVenda * e.Quantidade);
    public static decimal ComputeBuyingTotalValue()
      => lineCollection.Sum(e => e.Produto.CustoUnitario * e.Quantidade);
    //limpa carrinho
    public static void Clear() 
      => lineCollection.Clear();
    public static List<FilaCarrinho> Lines => lineCollection;
  }
  public class FilaCarrinho
  {
    public FilaCarrinho()
    {
      Produto = new Produto();
    }

    public int ID { get; set; }
    public Produto Produto { get; set; }
    public int Quantidade { get; set; }
  }
}