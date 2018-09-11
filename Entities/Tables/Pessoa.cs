using Entities.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Tables
{
  [Table("Pessoa")]
  public class Pessoa
  {
    
    [Key]
    public int Id { get; set; }

    [Display(Name = "Razão Social")]
    public string RazaoSocial { get; set; }

    [Display(Name = "Nome Fantasia")]
    public string NomeFantasia { get; set; }

    [StringLength(150)]
    [Display(Name = "Nome Completo")]
    public string NomeCompleto { get; set; }
    [Display(Name = "RG")]
    public string RG { get; set; }
    [Display(Name = "CPF")]
    public string CPF { get; set; }

    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    [Display(Name = "Data de Nascimento")]
    public DateTime? DataNascimento { get; set; }

    [Display(Name = "CNPJ")]
    public string CNPJ { get; set; }

    [Display(Name = "Email")]
    public string Email { get; set; }

    [Display(Name = "Telefone")]
    public string Telefone { get; set; }

    [Display(Name = "Celular")]
    public string Celular { get; set; }

    [Display(Name = "Status")]
    public bool Status { get; set; }

    //[Display(Name = "Limite de Crédito")]
    //public decimal LimiteCredito { get; set; }

    public int TipoPessoaId { get; set; }

    [NotMapped]
    public TipoPessoa TipoPessoa
    {
      get
      {
        return TipoPessoa.All().Where(d => d.Id == this.TipoPessoaId).FirstOrDefault();
      }
    }

    public int PapelPessoaId { get; set; }

    [NotMapped]
    public PapelPessoa PapelPessoa
    {
      get
      {
        return PapelPessoa.All().Where(d => d.Id == this.PapelPessoaId).FirstOrDefault();
      }
    }

    public  ICollection<Endereco> Enderecos { get; set; }

    [NotMapped]
    public int EnderecoId { get; set; }
    [NotMapped]
    public  Endereco Endereco{get;set;}

    public ICollection<Pedido> Pedidos { get; set; }

    [NotMapped]
    public Pedido Pedido { get; set; }

    public Pessoa()
    { }
  }
}
