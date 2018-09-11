using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities.Tables;

namespace Admin.Models
{
  public class ClienteEnderecoViewModel
  {
    public Pessoa Pessoa { get; set; }
    public Endereco Endereco { get; set; }
    public List<Endereco> Enderecos { get; set; }
  }
}