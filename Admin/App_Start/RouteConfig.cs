using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Routing;

namespace Admin
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapMvcAttributeRoutes();

      routes.MapRoute(
          name: "DefaultAdm",
          url: "{controller}/{action}/{id}",
          defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional },
          namespaces: new[] { "Admin.Controllers" }
      );
    }
  }
}
