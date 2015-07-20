using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using My_Site.Models;
using My_Site.Models.Intefaces;

namespace My_Site.Controllers
{
    [Authorize]
    public partial class CartController : Controller
    {
        readonly ISparePartRepository _db;
        readonly IOrderRepository _odb;

        public CartController(ISparePartRepository db, IOrderRepository odb)
        {
            _db=db;
            _odb=odb;
        }

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
            SparePart sparepart = _db.FindById(spareId);

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
            SparePart sparepart = _db.FindById(spareId);

            if (sparepart != null)
            {
                cart.RemoveLine(sparepart);
            }
            return View("Index", new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        [AllowAnonymous]
        public virtual PartialViewResult SummaryInformation(Cart cart)
        {
            return PartialView(cart);
        }

        public virtual ActionResult Checkout(Cart cart)
        {
            ApplicationUser applicationUser = _odb.FindUserByName(User.Identity.Name);
            Adress adress = _odb.TakeOldAdress(applicationUser.Id);
            return View(adress);
        }

        [HttpPost]
        public virtual ActionResult Checkout(Adress adress)
        {
            return View();
        }
	}
}