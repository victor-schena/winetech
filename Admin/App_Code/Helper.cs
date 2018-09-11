using Admin.Tables;
using Admin.DataContexts;
using Entities.Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Admin.App_Code
{
  public class Helper
  {
    public static bool HasCredentials(string user, string action, string controller, string param = null)
    {
      if (HttpContext.Current.User.Identity.IsAuthenticated)
      {
        IdentityDb _db = new IdentityDb();

        // Primeiramente, selecionamos os papeis do usuário logado
        List<string> usrRoles = _db.Users
          .Where(u => u.UserName == user)
          .FirstOrDefault().Roles.Select(r => r.RoleId).ToList();

        // Em seguida, selecionamos todas as credenciais deste usuário
        List<Credential> usrCredentials = _db.ApplicationRoles.Join(usrRoles,
          a => a.Id,
          r => r,
          (a, r) => a).SelectMany(c => c.Credentials).ToList();

        return (usrCredentials.Where(c => c.Action.Contains(action) && c.Controller == controller && c.Param == param).Count() > 0);
      }
      else
        return false;
    }
  }
}