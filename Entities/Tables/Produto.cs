using Entities.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Entities.Tables
{
  [Table("Produto")]
  public partial class Produto
  {
    public Produto()
    {
      this.Uvas = new HashSet<Uva>().ToList();
    }
    [Key]
    public int Id { get; set; }

    public string Imagem { get; set; }
    [NotMapped]
    public string PostedImg { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(150)]
    [Display(Name = "Nome")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo Descriçao é obrigatório.")]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; }

    [Display(Name = "Uva")]
    public Uva Uva { get; set; }

    [Display(Name = "Teor Alcoolico")]
    public string Teor_Alcolico { get; set; }

    [Required(ErrorMessage = "O campo Custo unitário é obrigatório.")]
    [Display(Name = "Custo Unitario")]
    public decimal CustoUnitario { get; set; }

    [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
    [Display(Name = "Quantidade")]
    public int Quantidade { get; set; }

    [StringLength(100)]
    [Display(Name = "Volume")]
    public string Volume { get; set; }

    [Required(ErrorMessage = "O campo Preco de venda é obrigatório.")]
    public decimal PrecoVenda { get; set; }

    [Display(Name = "Data de Validade")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
    public DateTime? DataValidade { get; set; }

    [Display(Name = "Status")]
    public bool Status { get; set; }

    public int PaisId { get; set; }
    public Pais Pais { get; set; }

    public int SafraId { get; set; }
    public Safra Safra { get; set; }

    public int? PedidoId { get; set; }
    public virtual ICollection<Pedido> Pedidos { get; set; }
    
    public virtual ICollection<Uva> Uvas
    {
      get;
      set;
    }
    [NotMapped]
    public int[] selectedUvas { get; set; }

    public int ClasseId { get; set; }
    public virtual Classe Classe { get; set; }

    public int TipoId { get; set; }

    public virtual Tipo Tipo { get; set; }

    public int? HistoricoEstoqueId { get; set; }
    public virtual ICollection<HistoricoEstoque> HistoricoEstoque { get; set; }
  }

  //public class PresentationProduto
  //{
  //  public int Id { get; set; }
  //  public string Nome { get; set; }
  //  public int Quantidade { get; set; }
  //}
}
