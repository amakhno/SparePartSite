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
            return View(_db.Search(null, page, search, pageSize));
        }

        public virtual ViewResult Create()
        {
            return View("Edit", new SparePart());
        }

        public virtual ViewResult Edit(int spareId)
        {
            SparePart spare = _db.FindById(spareId);
            return View(spare);
        }
        
        [HttpPost]
        public virtual ViewResult Edit(SparePart sparePart)
        {
            if (ModelState.IsValid)
            {
                _db.SavePart(sparePart);
                TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", sparePart.MarkWithModel);
                return View("Index", _db.Search(null, 1, null, pageSize));
            }
            return View(sparePart);
        }

        public virtual ActionResult Delete(int spareId)
        {
            SparePart deleted = _db.FindById(spareId);
            if (deleted != null)
            {
                _db.Remove(spareId);

                TempData["message"] = string.Format("Товар \"{0}\" был удален",
                    deleted.MarkWithModel);
            }
            return View("Index", _db.Search(null, 1, null, pageSize));
        }

        [HttpPost]
        public virtual ViewResult Index(SparePartListViewModel listView)
        {
            return View(_db.Search(null, 1, listView.Search, pageSize));
        }
	}    
}