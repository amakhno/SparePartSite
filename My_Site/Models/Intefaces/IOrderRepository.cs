using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Site.Models.Intefaces
{
    interface IOrderRepository
    {
        Adress TakeOldAdress(string userName);
        ApplicationUser FindUserByName(string userName);
    }
}
