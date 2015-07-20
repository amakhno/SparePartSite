using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using My_Site.Models;
using System.Data.Entity;

namespace My_Site.Models.Repository
{
    public class SparePartRepository : BaseRepository ,ISparePartRepository
    {
        public IEnumerable<SparePart> TakeAll()
        {
            IEnumerable<SparePart> sparePart = null;
            Run(contex =>
            { sparePart = contex.SpareParts.ToArray(); }
            );
            return sparePart;
        }

        public SparePartListViewModel Search(string category, int page, string search, int pageSize)
        {
            SparePartListViewModel temp = new SparePartListViewModel();      
            Run(db =>
                {
                    if (search != null)
                    {
                        search = search.ToUpper();
                    }
                    temp.SpareParts = db.SpareParts
                    .Where(p => category == null || p.Category == category)
                    .Where(p => search == null || p.Mark.ToUpper().Contains(search) || p.Model.ToUpper().Contains(search))
                    .OrderBy(x => x.Id).
                    ToArray();
                    temp.CurrentCategory = category;
                    temp.Search = search;
                });
            return temp;
            
        }        

        public SparePart FindById(int spareId)
        {
            SparePart temp = new SparePart();
            Run(db =>
            {
                temp = db.SpareParts.Where(x => x.Id == spareId).FirstOrDefault();
            });
            return temp;
        }

        public void Remove(int spareId)
        {
            Run(db=> 
                {
                    SparePart temp = db.SpareParts.First(x => x.Id == spareId);
                    db.SpareParts.Remove(temp);
                    db.SaveChanges();
                });
        }

        public void SavePart(SparePart sparePart)
        {
            Run(db =>
            {
                if (sparePart.Id == 0)
                {
                    db.SpareParts.Add(sparePart);
                }
                else
                {
                    SparePart dbEntry = db.SpareParts.Find(sparePart.Id);
                    db.Entry(dbEntry).CurrentValues.SetValues(sparePart.Id);
                }
                db.SaveChanges();
            });
        }

        public IEnumerable<string> TakeCategories()
        {
            IEnumerable<string> strings = null;
            Run(db =>
            {
                strings = db.SpareParts
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x).ToArray();
            });
            return strings;
        }

    }
}