using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My_Site.Models;
using Autofac;
using Autofac.Integration.Mvc;
using My_Site.Models.Repository;



namespace My_Site.App_Start
{
    [Authorize(Roles="Admin")]
    public partial class AdminController : Controller
    {
        private readonly ISparePartRepository _db;
        int pageSize = 50;

        public AdminController(ISparePartRepository repo)
        {
           _db = repo;
        }

        //MainAdminPage
        public virtual ActionResult Index(int page = 1, string search = null)
        {
            return View("Index",_db.Search(null, page, search, pageSize));
        }

        public virtual ViewResult Create()
        {
            return View("Edit", new SparePart());
        }

        public virtual ActionResult Edit(int spareId)
        {
            SparePart spare = _db.FindById(spareId);
            return PartialView(spare);
        }
        
        [HttpPost]
        public virtual ActionResult Edit(SparePart sparePart)
        {
            if (ModelState.IsValid)
            {
                _db.SavePart(sparePart);
                TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", sparePart.MarkWithModel);
                return RedirectToAction("Index");
            }
            return View(sparePart);
        }

        public virtual ActionResult Delete(int spareId)
        {
            SparePart deleted = _db.FindById(spareId);
            if (deleted != null)
            {
                _db.Remove(spareId);
                TempData["message"] = string.Format("Товар \"{0}\" был удален", deleted.MarkWithModel);
            }
            return RedirectToAction("Index");
        }
	}    
}