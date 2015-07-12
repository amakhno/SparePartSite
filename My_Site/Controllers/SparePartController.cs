using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My_Site.Models;

namespace My_Site.Controllers
{
    public class SparePartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private int pageSize = 4;

        public ActionResult List(string category = "Двигатель", int page = 1)
        {
            SparePartListViewModel model = new SparePartListViewModel
            {
                SpareParts = db.SpareParts
                .Where(p=>category == null||p.Category==category)
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalSpare = category == null ?
                    db.SpareParts.Count() :
                    db.SpareParts.Where(game => game.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public ActionResult Show(int spareid, string returnUrl)
        {
            SparePart sparepart = db.SpareParts.Where(x => x.Id == spareid).FirstOrDefault();
            return View(sparepart);
        }
    }
}