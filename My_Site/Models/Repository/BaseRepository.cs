using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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

        protected void RunUser(Action<UserManager<ApplicationUser>> dbAction)
        {
            using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                dbAction(userManager);
            }
        }
    }
}