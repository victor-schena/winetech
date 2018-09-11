using Entities.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class PedidoVendaPessoaFisicaController : Controller
    {
        // GET: PedidoVendaPessoaFisica
        public ActionResult Index()
        {
            var db = new EntitiesDb();
            return View(db.Pedidos.Where(x=>x.isVenda==true).Where(x=>x.isPessoaFisica==true).ToList());
        }
    }
}