using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace My_Site.Models
{
    public class DbContext : Singleton<DbContext>
    {
        ApplicationDbContext db;

        private DbContext()
        { db = new ApplicationDbContext(); }

        public ApplicationDbContext Get_Context()
        { return db; }

    }
}