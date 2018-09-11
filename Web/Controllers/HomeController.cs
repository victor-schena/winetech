using Entities.Classes;
using Entities.Contexts;
using Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
  public class HomeController : Controller
  {
    #region Constructor

    public HomeController()
    {
    }

    #endregion

    #region Properties

    private EntitiesDb db = new EntitiesDb();

    #endregion

    #region Actions

    public ActionResult Index()
    {

      var model = new HomeModel();

      model.Participantes = db.Participantes.Where(p => p.StatusId == Status.Ativo.Id).ToList();
      model.Paises = db.Paises.ToList();
      model.Contador = 1;
      ViewBag.Partic = db.Participantes.Where(p => p.StatusId == Status.Ativo.Id).ToList();

      /* base model defaults */
      model.Title = "Base Project | Home";
      model.Description = "";
      model.Robots = "index";
      //model.Canonical = "";

      return View(model);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    #endregion
  }
}
