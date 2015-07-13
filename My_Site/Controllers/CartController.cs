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
        public virtual ActionResult Index(Cart cart, string returnUrl = null)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        [AllowAnonymous]
        public virtual ActionResult AddToCart(Cart cart, int spareId, string returnUrl)
        {
            SparePart sparepart = db.SpareParts
                .FirstOrDefault(e => e.Id == spareId);

            if (sparepart != null)
            {
                cart.AddItem(sparepart, 1);
            }
                return View(Views.Index, new CartIndexViewModel
                    {
                        Cart = cart,
                        ReturnUrl = returnUrl
                    });
        }

        [AllowAnonymous]
        public virtual ActionResult RemoveFromCart(Cart cart, int spareId, string returnUrl)
        {
            SparePart sparepart = db.SpareParts
                .FirstOrDefault(g => g.Id == spareId);

            if (sparepart != null)
            {
                cart.RemoveLine(sparepart);
            }
            return RedirectToAction(Views.Index, new { returnUrl });
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