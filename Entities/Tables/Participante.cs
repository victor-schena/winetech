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
  [Table("Participante")]
  public class Participante
  {
    #region Constructor

    public Participante() { }

    #endregion

    #region Properties

    public int Id { get; set; }

    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    [StringLength(150)]
    [Display(Name = "Nome")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo cpf é obrigatório.")]
    [StringLength(20)]
    [Display(Name = "Cpf")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "O campo email é obrigatório.")]
    [StringLength(150)]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo senha é obrigatório.")]
    [StringLength(150)]
    [Display(Name = "Senha")]
    public string Senha { get; set; }

    [Required(ErrorMessage = "O campo sexo é obrigatório.")]
    [StringLength(20)]
    [Display(Name = "Sexo")]
    public string Sexo { get; set; }

    [Required(ErrorMessage = "O campo endereço é obrigatório.")]
    [StringLength(250)]
    [Display(Name = "Endereço")]
    public string Endereco { get; set; }

    [Required(ErrorMessage = "O campo cep é obrigatório.")]
    [StringLength(10)]
    [Display(Name = "Cep")]
    public string Cep { get; set; }

    [Required(ErrorMessage = "O campo cidade é obrigatório.")]
    [StringLength(50)]
    [Display(Name = "Cidade")]
    public string Cidade { get; set; }

    [Required(ErrorMessage = "O campo estado é obrigatório.")]
    [StringLength(50)]
    [Display(Name = "Estado")]
    public string Estado { get; set; }

    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Data de Nascimento")]
    public DateTime? DataNasc { get; set; }

    [Required(ErrorMessage = "O campo telefone é obrigatório.")]
    [StringLength(20)]
    [Display(Name = "Telefone")]
    public string Telefone { get; set; }

    [StringLength(20)]
    [Display(Name = "Celular")]
    public string Celular { get; set; }

    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Data de Cadastro")]
    public DateTime? DataCadastro { get; set; }

    public int StatusId { get; set; }

    [NotMapped]
    public Status Status
    {
      get
      {
        return Status.All().Where(d => d.Id == this.StatusId).FirstOrDefault();
      }
    }

    #endregion
  }
}
