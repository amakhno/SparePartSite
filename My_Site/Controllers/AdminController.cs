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
        //
        // GET: /Admin/
        [HttpGet]
        [AllowAnonymous]
        public virtual PartialViewResult Check()
        {
            bool isAdmin = false;
            if (User.Identity.IsAuthenticated)
            {
                isAdmin = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Roles.Where(x => x.Role.Name == "Admin").Count()>0;
            }
            return PartialView(isAdmin);
        }

        [HttpGet]
        public virtual ViewResult Index()
        {
            return View(db.SpareParts);
        }

        public virtual ViewResult Edit(int spareId)
        {
            return View(db.SpareParts.Where(x => x.Id == spareId).First());
        }

        [HttpPost]
        public virtual ViewResult Edit(SparePart model)
        {
            if (ModelState.IsValid)
            {
                SaveGame(model);
                TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены", model.MarkWithModel);
                return View("Index", db.SpareParts);
            }
            return View(model);
        }

        private void SaveGame(SparePart sparepart)
        {
            if (sparepart.Id== 0)
                db.SpareParts.Add(sparepart);
            else
            {
                SparePart dbEntry = db.SpareParts.Find(sparepart.Id);
                db.Entry(dbEntry).CurrentValues.SetValues(sparepart);
            }
            db.SaveChanges();
        }
	}
}