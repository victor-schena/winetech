using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;

namespace Admin.Controllers
{
    public class CarrinhoController : Controller
    {
        // GET: Carrinho
        public ActionResult Index()
        {
            return View();
        }
    public RedirectToActionResult AddToCart(int productId, string returnUrl)
    {
      Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
      if (product != null) { Cart cart = GetCart(); cart.AddItem(product, 1); SaveCart(cart); }
      return RedirectToAction("Index", new { returnUrl });
    }

  }
}