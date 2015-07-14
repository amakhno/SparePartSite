using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My_Site.Models;



namespace My_Site.App_Start
{
    [Authorize]
    public partial class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        int pageSize = 20;

        //
        // GET: /Admin/
        [HttpGet]
        [AllowAnonymous]
        private bool Check()
        {
            bool isAdmin = false;
            if (User.Identity.IsAuthenticated)
            {
                isAdmin = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Roles.Where(x => x.Role.Name == "Admin").Count()>0;
            }
            return isAdmin;
        }

        //MainAdminPage
        public virtual ActionResult Index(int page = 1, string search = null)
        {
            if(Check())
            {
                return View(new SparePartListViewModel(null, page, search, pageSize));
            }
            else 
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public virtual ViewResult Create()
        {
            return View("Edit", new SparePart());
        }

        public virtual ViewResult Edit(int spareId)
        {
            SparePart spare = db.SpareParts.First(x => x.Id == spareId);
            return View(spare);
        }
        
        [HttpPost]
        public virtual ViewResult Edit(SparePart spare)
        {
            if (ModelState.IsValid)
            {
                spare.SavePart();
                TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", spare.MarkWithModel);
                return View("Index", new SparePartListViewModel(pageSize));
            }
            return View(spare);
        }

        public virtual ActionResult Delete(int spareId)
        {
            SparePart deleted = db.SpareParts.First(x => x.Id == spareId);
            if (deleted != null)
            {
                db.SpareParts.Remove(deleted);
                db.SaveChanges();
                TempData["message"] = string.Format("Товар \"{0}\" был удален",
                    deleted.MarkWithModel);
            }
            return View("Index", new SparePartListViewModel(null, 1, null, pageSize));
        }

        [HttpPost]
        public virtual ViewResult Index(SparePartListViewModel listView)
        {            
            return View(new SparePartListViewModel(null, 1, listView.Search, pageSize));
        }
	}    
}