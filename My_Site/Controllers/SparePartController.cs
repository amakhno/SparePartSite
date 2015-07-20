using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My_Site.Models;
using My_Site.Models.Repository;

namespace My_Site.Controllers
{
    public partial class SparePartController : Controller
    {        
        private ISparePartRepository _db;
        private int pageSize = 4;

        public SparePartController(ISparePartRepository db)
        {
            _db = db;
        }

        public virtual ActionResult List(string category = "Двигатель", int page = 1)
        {
            SparePartListViewModel model = _db.Search(category, page, null, pageSize);
            return View(model);
        }

        public virtual ActionResult Show(int spareId, string returnUrl)
        {
            SparePart sparepart = _db.FindById(spareId);
            return View(sparepart);
        }
    }
}