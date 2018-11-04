using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Tables
{
  [Table("HistoricoEstoque")]
  public class HistoricoEstoque
  {
    [Key]
    public int Id { get; set; }
    public int Ajuste { get; set; }
    public int Quantidade { get; set; }
    public int ProdutoId { get; set; }
    public virtual Produto Produto { get; set; }
    public DateTime CriadoEm { get; set; }
    public int? PedidoId { get; set; }
    public virtual Pedido Pedido{ get; set; }
  }
}
