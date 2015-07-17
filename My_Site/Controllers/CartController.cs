using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using My_Site.Models;

namespace My_Site.Controllers
{
    [Authorize]
    public partial class CartController : Controller
    {
        readonly ISparePartRepository _db;

        public CartController(ISparePartRepository db)
        {
            _db=db;
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
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser applicationUser = db.Users.First(x => x.UserName == User.Identity.Name);
            Order order = new Order
            {
                CartPositions = cart.Positions.ToArray(),
                Date = DateTime.Now,
                ApplicationUserId = applicationUser.Id,
                Adress = new Adress 
                { 
                    Country = "Russia",
                    Region = "Vrn",
                    City = "Voronezh",
                    ZipCode = 396250,
                    House = 19,
                    Appartments = 0
                }
            };
            db.Orders.Add(order);
            db.SaveChanges();
            return View();
        }

        [HttpPost]
        public virtual ActionResult Checkout(Adress adress)
        {
            return View();
        }
	}
}