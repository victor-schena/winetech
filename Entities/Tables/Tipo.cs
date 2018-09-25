﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Tables
{
  [Table("Tipo")]
  public class Tipo
  {
    [Key]
    public int Id { get; set; }
    public string Descricao { get; set; }

    public ICollection<Produto> Produtos { get; set; }
  }
}
