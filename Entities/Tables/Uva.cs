using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Tables
{
  [Table("Uva")]
  public class Uva
  {
    public Uva()
    {
      this.Produtos = new HashSet<Produto>();
    }
    [Key]
    public int Id { get; set; }
    [Display(Name = "Uva")]
    public string Descricao { get; set; }

    public ICollection<Produto> Produtos { get; set; }
  }
}
