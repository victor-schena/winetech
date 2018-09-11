using Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
  public class HomeModel : BaseModel
  {
    public IEnumerable<Participante> Participantes { get; set; }
    public IEnumerable<Pais> Paises { get; set; }
    public int Contador { get; set; }
  }
}