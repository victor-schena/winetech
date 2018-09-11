using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
  public class ErrorController : Controller
  {
    #region Actions

    public ActionResult NotFound()
    {
      Response.StatusCode = 404;
      return View();
    }

    public ActionResult BadRequest()
    {
      Response.StatusCode = 400;
      return View("NotFound");
    }

    public ActionResult InternalServerError()
    {
      Response.StatusCode = 500;
      return View("NotFound");
    }

    #endregion
  }
}