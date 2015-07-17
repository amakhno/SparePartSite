using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using My_Site.Models.Intefaces;
using My_Site.Models;

namespace My_Site.Models.Repository
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public Adress TakeOldAdress(string userName)
        {
            Adress adress = null;
            Run(db => 
            {
                RunUser(async userManager => 
                    {
                        ApplicationUser applicationUser = await userManager.FindByNameAsync("userName");
                        adress = db.Orders.OrderBy(x => x.Date).First(x => x.ApplicationUserId == applicationUser.Id).Adress;
                    });                
            });
            return adress;
        }

        public ApplicationUser FindUserByName(string userName)
        {
            ApplicationUser applicationUser = null;
            RunUser(async userManager => 
            {
                applicationUser = await userManager.FindByNameAsync(userName);
            });
            return applicationUser;
        }
    }
}