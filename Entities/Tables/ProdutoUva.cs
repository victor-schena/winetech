using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Tables
{
  [Table("ProdutoUva")]
  public class ProdutoUva
  {
    [Key]
    public int Id { get; set; }
    [ForeignKey("ProdutoId")]
    //public int ProdutoId { get; set; }
    public virtual Produto Produto { get; set; }
    [ForeignKey("UvaId")]
    //public int UvaId { get; set; }
    public virtual Uva Uva { get; set; }
  }
}
