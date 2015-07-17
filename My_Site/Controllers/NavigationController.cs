using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My_Site.Models;

namespace My_Site.Controllers
{
    public partial class NavigationController : Controller
    {
        readonly ISparePartRepository _db;

        public NavigationController(ISparePartRepository db)
        {
            _db=db;
        }

        public virtual PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            return PartialView(_db.TakeCategories());
        }
	}
}