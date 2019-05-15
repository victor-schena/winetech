using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Tables
{

  [Table("Safra")]
  public class Safra
  {
    #region Constructor

    public Safra() { }

    #endregion

    #region Properties
    [Key]
    public int Id { get; set; }
    [StringLength(4)]
    [Display(Name = "Safra")]
    public string Ano { get; set; }
    //public bool Status { get; set; }
    public virtual ICollection<Produto> Produtos { get; set; }

    #endregion

  }
}
