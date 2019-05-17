using Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models
{
  public class PedidoItemViewModel
  {
    public PedidoItemViewModel()
    {
      this.Produtos = new List<Produto>();
      this.Pessoa = new Pessoa();
      this.Pedido = new Pedido();

    }

    public int? PessoaId { get; set; }
    public Pessoa Pessoa { get; set; }

    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; }
    public Produto Produto { get; set; }
    public List<Produto> Produtos { get; set; }

    public int Quantidade { get; set; }
    public decimal Total{ get; set; }

    public List<Admin.Models.ApplicationUser> Users { get; set; }
  }
}