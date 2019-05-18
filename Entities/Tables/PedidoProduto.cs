using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Tables
{
  [Table("PedidoProduto")]
  public class PedidoProduto
  {
    [Key, Column(Order = 0)]
    [ForeignKey("Pedido")]
    public int PedidoId { get; set; }
    public  Pedido Pedido { get; set; }
    [Key, Column(Order = 1)]
    [ForeignKey("Produto")]
    public int ProdutoId { get; set; }
    public  Produto Produto { get; set; }
    public decimal PrecoVenda { get; set; }
    public int Quantidade { get; set; }
  }
}
