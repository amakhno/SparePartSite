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
        public virtual PartialViewResult Check()
        {
            bool isAdmin = false;
            if (User.Identity.IsAuthenticated)
            {
                isAdmin = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Roles.Where(x => x.Role.Name == "Admin").Count()>0;
            }
            return PartialView(isAdmin);
        }

        //MainAdminPage
        public virtual ViewResult Index( int page = 1)
        {
            return View(CreateIndex());
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
                SavePart(spare);
                TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", spare.MarkWithModel);
                return View("Index", CreateIndex());
            }
            return View(spare);
        }

        private void SavePart(SparePart sparepart)
        {
            if (sparepart.Id == 0)
                db.SpareParts.Add(sparepart);
            else
            {
                SparePart dbEntry = db.SpareParts.Find(sparepart.Id);
                db.Entry(dbEntry).CurrentValues.SetValues(sparepart);
            }
            db.SaveChanges();
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
            return View("Index", CreateIndex());
        }

        //Инициализация поиска
        [HttpPost]
        public virtual ActionResult SearchResult(SparePartListViewModel listView)
        {
            SparePartListViewModel searchMemory = new SparePartListViewModel();
            searchMemory.SpareParts = db.SpareParts
                .Where(x => (listView.Search != null) && ((x.Mark.ToUpper().Contains(listView.Search.ToUpper())) || (x.Mark.ToUpper().Contains(listView.Search.ToUpper()))))
                .OrderBy(x => x.Id)
                .Take(pageSize);
            searchMemory.PagingInfo = new PagingInfo
                {
                    CurrentPage = 1,
                    ItemsPerPage = pageSize,
                    TotalSpare = db.SpareParts
                    .Where(x => (listView.Search != null) && ((x.Mark.ToUpper().Contains(listView.Search)) || (x.Mark.ToUpper().Contains(listView.Search)))).Count()
                };
            searchMemory.Search = listView.Search;
            return View(searchMemory);
        }

        //Переход по страницам поиска
        public virtual ActionResult SearchResult(string search, int page)
        {
            SparePartListViewModel searchMemory = new SparePartListViewModel();
            searchMemory.SpareParts = db.SpareParts
                .Where(x => (search != null) && ((x.Mark.ToUpper().Contains(search.ToUpper())) || (x.Mark.ToUpper().Contains(search.ToUpper()))))
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            searchMemory.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalSpare = db.SpareParts.Where(x => (search != null) && ((x.Mark.ToUpper().Contains(search)) || (x.Mark.ToUpper().Contains(search)))).Count()
            };
            searchMemory.Search = search;
            return View(searchMemory);
        }

        private SparePartListViewModel CreateIndex()
        {
            SparePartListViewModel model = new SparePartListViewModel
            {
                SpareParts = db.SpareParts
                .OrderBy(x => x.Id)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = 1,
                    ItemsPerPage = pageSize,
                    TotalSpare = db.SpareParts.Count()
                },
            };
            return(model);
        }
	}    
}