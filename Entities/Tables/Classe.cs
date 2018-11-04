using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Tables
{
  [Table("Classe")]
  public partial class Classe
  {
    [Key]
    public int Id { get; set; }
    [Display(Name = "Classe")]
    public string Descricao { get; set; }
    public virtual ICollection<Produto> Produtos { get; set; }
    public virtual ICollection<Producao> Producoes { get; set; }
  }
}
