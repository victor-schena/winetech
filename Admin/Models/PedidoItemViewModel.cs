using Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models
{
  public class PedidoItemViewModel
  {
    //public PedidoItemViewModel()
    //{
    //  this.Produtos = new List<FilaCarrinho>();
    //}

    public int? PessoaId { get; set; }
    public Pessoa Pessoa { get; set; }

    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; }

    public List<FilaCarrinho> Produtos { get; set; }

    public int Quantidade { get; set; }
    public decimal Total{ get; set; }
  }
}