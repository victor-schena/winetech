using Admin.DataContexts;
using Admin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Admin.Filters
{
  public class IdentityFilter : ActionFilterAttribute
  {    
    public override void OnResultExecuting(ResultExecutingContext filterContext)
    {
      IdentityDb context = new IdentityDb();
      var store = new UserStore<ApplicationUser>(context);
      var manager = new UserManager<ApplicationUser>(store);

      if (HttpContext.Current.User.Identity.IsAuthenticated)
      {
        var userId = HttpContext.Current.User.Identity.GetUserId();
        filterContext.Controller.ViewBag.UserRoles = manager.GetRoles(userId);

        ApplicationUser user = manager.FindById(userId);
        filterContext.Controller.ViewBag.UserInfo = user;
      }
    }
  }
}