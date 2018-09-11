using Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Admin.Tables
{
  [Table("Credential")]
  public class Credential
  {
    #region Properties

    public int Id { get; set; }
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Param { get; set; }
    public string Descr { get; set; }
    public virtual ICollection<ApplicationRole> Roles { get; set; }

    #endregion
  }
}