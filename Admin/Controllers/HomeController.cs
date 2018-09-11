using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Admin.Models;
using Entities.Classes;

namespace Admin.Controllers
{
  [Authorize]
  public class HomeController : Controller
  {
    #region Properties

    private ApplicationUserManager _userManager;
    public ApplicationUserManager UserManager
    {
      get
      {
        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
      }
      private set
      {
        _userManager = value;
      }
    }

    private ApplicationRoleManager _roleManager;
    public ApplicationRoleManager RoleManager
    {
      get
      {
        return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
      }
      private set
      {
        _roleManager = value;
      }
    }

    #endregion

    #region Actions

    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Create()
    {
      return View();
    }

    #endregion
  }
}
