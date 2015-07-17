using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Site.Models
{
    public class Order
    {
        public enum EStatus { Waiting, Working, Send, Complete, Closed };

        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public Adress Adress { get; set; }
        public int Status { get; set; }
        public decimal TotalPrice { get; set; }
        public Cart Cart { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}