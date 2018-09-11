using Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Classes
{
  public class RelatorioFinanceiro
  {
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }

    public List<Pedido> Pedidos { get; set; }




    public decimal Total { get; set; }
  }
}
