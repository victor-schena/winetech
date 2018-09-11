using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Tables
{
  [Table("Produto")]
  public partial class Produto
  {
    public Produto()
    {
      this.Pedidos = new HashSet<Pedido>();
    }
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(150)]
    [Display(Name = "Nome")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo Descriçao é obrigatório.")]
    [StringLength(2000)]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; }

    [Display(Name = "Uva")]
    public string Uva { get; set; }

    [Display(Name = "Classe")]
    public string Classe { get; set; }

    [Display(Name = "Teor Alcoolico")]
    public string Teor_Alcolico { get; set; }

    [Display(Name = "Tipo")]
    public string Tipo { get; set; }

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


    //  public decimal Unidade { get; set; }
    //  public Enum Unidade{get;set;}
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [Display(Name = "Data de Validade")]
    public DateTime? DataValidade { get; set; }

    [Display(Name = "Status")]
    public bool Status  { get; set; }


    public int PaisId { get; set; }
    public /*virtual*/ Pais Pais { get; set; }

    public int SafraId { get; set; }
    public /*virtual*/ Safra Safra { get; set; }

    //public int ForcenedorId { get; set; }
    //public Pessoa Pessoa { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; }

    
  }

  public class PresentationProduto
  {
    public int Id;
    public string Nome;
  }
}
