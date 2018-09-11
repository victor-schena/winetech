using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Tables
{
  [Table("Endereco")]
  public class Endereco
  {
    public Endereco(){
    }
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo Estado é obrigatório.")]
    [StringLength(150)]
    [Display(Name = "Estado")]
    public string Estado { get; set; }

    [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
    [StringLength(150)]
    [Display(Name = "Cidade")]
    public string Cidade { get; set; }

    [Required(ErrorMessage = "O campo Bairro é obrigatório.")]
    [StringLength(150)]
    [Display(Name = "Bairro")]
    public string Bairro { get; set; }

    [Required(ErrorMessage = "O campo CEP é obrigatório.")]
    [StringLength(8)]
    [Display(Name = "CEP")]
    public string CEP { get; set; }

    [Required(ErrorMessage = "O campo Rua é obrigatório.")]
    [StringLength(150)]
    [Display(Name = "Rua")]
    public string Rua { get; set; }

    [Required(ErrorMessage = "O campo Numero é obrigatório.")]
    [StringLength(150)]
    [Display(Name = "Numero")]
    public string Numero { get; set; }

    [StringLength(150)]
    [Display(Name = "Complemento")]
    public string Complemento { get; set; }

    public bool Status { get; set; }

    public int PessoaId { get; set; }
    public virtual Pessoa Pessoa { get; set; }

    public void validarCamposObrigatorios()
    {

    }
   
  }
}
