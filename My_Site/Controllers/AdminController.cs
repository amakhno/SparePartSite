using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My_Site.Models;
using Autofac;
using Autofac.Integration.Mvc;



namespace My_Site.App_Start
{
    [Authorize(Roles="Admin")]
    public partial class AdminController : Controller
    {
        private readonly ISparePartRepository _db;
        int pageSize = 20;

        public AdminController(ISparePartRepository repo)
        {
           _db = repo;
        }

        //MainAdminPage
        public virtual ActionResult Index(int page = 1, string search = null)
        {
                return View(new SparePartListViewModel(null, page, search, pageSize));
        }

        public virtual ViewResult Create()
        {
            return Edit(new SparePart());
        }

        public virtual ViewResult Edit(int spareId)
        {
            SparePart spare = _db.TakeAll().First(x => x.Id == spareId);
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
            SparePart deleted = _db.TakeAll().First(x => x.Id == spareId);
            if (deleted != null)
            {
                _db.TakeAll().Remove(deleted);
               // db.SaveChanges();
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