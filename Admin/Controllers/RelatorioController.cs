using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
  [Authorize(Roles ="Gerente")]
  public class RelatorioController : Controller
    {
    // GET: Relatorio
    
    public ActionResult Index()
        {
            return View();
        }
    }
}