using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My_Site.Models;

namespace My_Site.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        public ViewResult Index(Cart cart, string returnUrl = null)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        [AllowAnonymous]
        public RedirectToRouteResult AddToCart(Cart cart, int spareId, string returnUrl)
        {
            SparePart sparepart = db.SpareParts
                .FirstOrDefault(e => e.Id == spareId);

            if (sparepart != null)
            {
                cart.AddItem(sparepart, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        [AllowAnonymous]
        public RedirectToRouteResult RemoveFromCart(Cart cart, int spareId, string returnUrl)
        {
            SparePart sparepart = db.SpareParts
                .FirstOrDefault(g => g.Id == spareId);

            if (sparepart != null)
            {
                cart.RemoveLine(sparepart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        [AllowAnonymous]
        public PartialViewResult SummaryInformation(Cart cart)
        {
            return PartialView(cart);
        }
        

        public ActionResult Checkout(string returnUrl)
        {
            return View();
        }
	}
}