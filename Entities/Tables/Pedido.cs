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
      this.PedidosProdutos = new HashSet<PedidoProduto>();
    }
    public Pedido(bool isVenda,bool isPessoaFisica,DateTime dataPedido)
    {
      this.Produtos = new HashSet<Produto>();
      this.isVenda = isVenda;
      this.isPessoaFisica = isPessoaFisica;
      this.DataPedido = dataPedido;
    }
    [Key]
    public int Id { get; set; }

    public bool? isVenda { get; set; }

    public bool? isPessoaFisica { get; set; }

    public bool? isEmitido { get; set;}

    public DateTime DataPedido { get; set; }

    public int Quantidade { get; set; }

    public decimal? Total { get; set; }

    public int? PessoaId { get; set; }
    public virtual Pessoa Pessoa { get; set; }

    public virtual ICollection<Produto> Produtos { get; set; }

    //public int? HistoricoEstoqueId { get; set; }//talvez nem use isso
    //public virtual ICollection<HistoricoEstoque> HistoricoEstoque { get; set; }//nem isso

    public virtual ICollection<PedidoProduto> PedidosProdutos { get; set; }

  }
}
