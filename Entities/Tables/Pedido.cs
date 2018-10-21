using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Tables
{
  [Table("Pedido")]
  public partial class Pedido
  {
    public Pedido()
    {
      this.Produtos = new HashSet<Produto>();
    }
    [Key]
    public int Id { get; set; }

    public bool? isVenda { get; set; }

    public bool? isPessoaFisica { get; set; }

    public DateTime DataPedido { get; set; }

    public int Quantidade { get; set; }

    public decimal? Total { get; set; }

    public int? PessoaId { get; set; }
    public virtual Pessoa Pessoa { get; set; }

    public virtual ICollection<Produto> Produtos{get;set;}
    
  }
}
