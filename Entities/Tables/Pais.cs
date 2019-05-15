using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Tables
{
  [Table("Pais")]
  public class Pais
  {
    #region Constructor

    public Pais() { }

    #endregion

    #region Properties
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    [StringLength(150)]
    [Display(Name = "Nome")]
    public string Nome { get; set; }
    public virtual ICollection<Produto> Produtos { get; set; }

    #endregion
  }
}
