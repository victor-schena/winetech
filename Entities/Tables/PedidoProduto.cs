﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Tables
{
  public class PedidoProduto
  {
    [Key, Column(Order = 0)]
    public int PedidoId { get; set; }
    [Key, Column(Order = 1)]
    public int ProdutoId { get; set; }
  }
}
