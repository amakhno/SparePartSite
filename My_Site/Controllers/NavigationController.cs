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
        ApplicationDbContext db = new ApplicationDbContext();

        public virtual PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = db.SpareParts
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
	}
}