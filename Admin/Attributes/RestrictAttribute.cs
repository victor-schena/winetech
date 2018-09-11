using Admin.DataContexts;
using Admin.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Attributes
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
  public class RestrictAttribute : AuthorizeAttribute
  {
    #region Methods

    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
      if (!HttpContext.Current.User.Identity.IsAuthenticated)
      {
        return false;
      }

      List<Credential> usrCredentials = getCredentials(httpContext);

      // Identifica o controller e a action da requisição
      HttpRequestBase request = httpContext.Request;
      string controller = request.RequestContext.RouteData.Values["controller"].ToString();
      string action = request.RequestContext.RouteData.Values["action"].ToString();

      // Verifica se o usuário possui credenciais para esta área
      bool permission = (usrCredentials.Where(c => c.Action.Contains(action) && c.Controller == controller).Count() > 0);

      return permission;
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
      filterContext.Result = new HttpUnauthorizedResult();
      filterContext.HttpContext.Response.StatusCode = 403;
    }

    protected List<Credential> getCredentials(HttpContextBase httpContext)
    {
      IdentityDb _db = new IdentityDb();

      // Primeiramente, selecionamos os papeis do usuário logado
      List<string> usrRoles = _db.Users
        .Where(u => u.UserName == httpContext.User.Identity.Name)
        .FirstOrDefault().Roles.Select(r => r.RoleId).ToList();

      // Em seguida, selecionamos todas as credenciais deste usuário
      List<Credential> usrCredentials = _db.ApplicationRoles.Join(usrRoles,
        a => a.Id,
        r => r,
        (a, r) => a).SelectMany(c => c.Credentials).ToList();

      return usrCredentials;
    }

    #endregion
  }
}
