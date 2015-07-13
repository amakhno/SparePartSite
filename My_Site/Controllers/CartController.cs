using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My_Site.Models;

namespace My_Site.Controllers
{
    [Authorize]
    public partial class CartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        public virtual ViewResult Index(Cart cart, string returnUrl = null)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        [AllowAnonymous]
        public virtual RedirectToRouteResult AddToCart(Cart cart, int spareId, string returnUrl)
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
        public virtual RedirectToRouteResult RemoveFromCart(Cart cart, int spareId, string returnUrl)
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
        public virtual PartialViewResult SummaryInformation(Cart cart)
        {
            return PartialView(cart);
        }


        public virtual ActionResult Checkout(string returnUrl)
        {
            return View();
        }
	}
}