using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using My_Site.Models;
using System.Data.Entity;

namespace My_Site.Models.Repository
{
    public class SparePartRepository : ISparePartRepository
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public DbSet<SparePart> TakeAll()
        {
            return db.SpareParts;
        }
    }
}