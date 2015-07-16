using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace My_Site.Models
{
    public class SparePartListViewModel
    {
        public IEnumerable<SparePart> SpareParts { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public string Search { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();


        public SparePartListViewModel()
        {
            ;
        }        

        public SparePartListViewModel(int pageSize)
        {
            this.SpareParts = db.SpareParts
            .OrderBy(x => x.Id)
            .Take(pageSize);
            this.PagingInfo = new PagingInfo
            {
                CurrentPage = 1,
                ItemsPerPage = pageSize,
                TotalSpare =
                db.SpareParts.Count()
            };
        }

        public SparePartListViewModel(string category, int page, string search, int pageSize)
        {
            if (search != null)
            {
                search = search.ToUpper();
            }
            this.SpareParts = db.SpareParts
            .Where(p => category == null || p.Category == category)
            .Where(p => search == null || p.Mark.ToUpper().Contains(search) || p.Model.ToUpper().Contains(search))
            .OrderBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
            this.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalSpare = db.SpareParts
                .Where(p => category == null || p.Category == category)
                .Where(p => search == null || p.Mark.ToUpper().Contains(search) || p.Model.ToUpper().Contains(search)).Count()
            };
            this.CurrentCategory = category;
            this.Search = search;
        }

        
    }
}