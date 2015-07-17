using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace My_Site.Models.Repository
{
    public class BaseRepository
    {
        protected void Run(Action<ApplicationDbContext> dbAction)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                dbAction(db);
            }
        }
    }
}