using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My_Site.Models;

namespace My_Site.Models
{
    public interface ISparePartRepository
    {
        IEnumerable<SparePart> TakeAll();
        SparePartListViewModel Search(string category, int page, string search, int pageSize);
        SparePart FindById(int spareId);
        void Remove(int spareId);
        void SavePart(SparePart sparePart);
    }
}
