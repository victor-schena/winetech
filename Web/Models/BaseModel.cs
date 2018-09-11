using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
  public class BaseModel
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public string Robots { get; set; }
    public string Canonical { get; set; }
  }
}