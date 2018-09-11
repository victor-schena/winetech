using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Classes
{
  public class PessoaJuridica
  {
    public int Id { get; set; }
    public string RazaoSocial { get; set; }
    public string NomeFantasia { get; set; }
    public string CNPJ { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Celular { get; set; }
    public bool Status { get; set; }
    public decimal LimiteCredito { get; set; }

    public PessoaJuridica()
    { }
  }
}