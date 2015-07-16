using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My_Site.Models;

namespace My_Site.Controllers
{
    public partial class SparePartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private int pageSize = 4;

        public virtual ActionResult List(string category = "Двигатель", int page = 1)
        {
            SparePartListViewModel model = new SparePartListViewModel(category, page, null, pageSize);
            return View(model);
        }

        public virtual ActionResult Show(int spareid, string returnUrl)
        {
            SparePart sparepart = db.SpareParts.Where(x => x.Id == spareid).FirstOrDefault();
            return View(sparepart);
        }
    }
}