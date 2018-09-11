using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Classes
{
  public class PessoaFisica
  {
    public int Id { get; set; }
    public string NomeCompleto { get; set; }
    public string RG { get; set; }
    public string CPF { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Celular { get; set; }
    public bool Status { get; set; }
    public decimal LimiteCredito { get; set; }
    public int PapelPessoaId { get; set; }

    public PessoaFisica()
    { }

  }
}