using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Tables
{
  public class ProdutoUva
  {
    [Key, Column(Order = 0)]
    public int ProdutoId { get; set; }
    [Key, Column(Order = 0)]
    public int UvaId { get; set; }
  }
}
