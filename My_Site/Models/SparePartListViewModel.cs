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
    }
}