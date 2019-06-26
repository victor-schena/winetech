using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Tables
{
  [Table("Producao")]
  public partial class Producao
  {
    public int Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public int TipoId { get; set; }
    public virtual Tipo Tipo { get; set; }
    public int UvaId { get; set; }
    public virtual Uva Uva { get; set; }
    public int ClasseId { get; set; }
    public virtual Classe Classe { get; set; }
    public string Volume { get; set; }
    public decimal KgUva { get; set; }
    public decimal KgAcucar { get; set; }
    public decimal Vasilhame { get; set; }
    public bool Status { get; set; }
  }
}
