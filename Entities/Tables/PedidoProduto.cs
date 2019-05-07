using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Tables
{
  [Table("ProdutoPedido")]
  public class ProdutoPedido
  {
    [Key, Column(Order = 0)]
    public int ProdutoId { get; set; }
    [Key, Column(Order = 1)]
    public int PedidoId { get; set; }
    public decimal CustoUnitario { get; set; }
    public decimal PrecoUnitário { get; set; }
    public int Quantidade { get; set; }
  }
}
