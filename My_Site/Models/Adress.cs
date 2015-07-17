using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace My_Site.Models
{
    public class Adress
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public int House { get; set; }
        public int Appartments { get; set; }
    }
}