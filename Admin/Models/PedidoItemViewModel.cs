using Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models
{
  public class PedidoItemViewModel
  {
    public int idPessoa { get; set; }
    public Pessoa Pessoa { get; set; }
    public Pedido Pedido { get; set; }
    public int Quantidade { get; set; }
    public int idProduto { get; set; }
    public Produto Produto { get; set; }
    public List<FilaCarrinho> Produtos { get; set; }
    public List<PresentationProduto> ProdutosPedido { get; set; }
    public decimal Total{ get; set; }
  }
}