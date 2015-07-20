using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using My_Site.Models;
using System.ComponentModel.DataAnnotations;

namespace My_Site.Models
{
    public class CartPosition
    {
        public SparePart SparePart { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        private List<CartPosition> PositionCollection = new List<CartPosition>();

        public void AddItem(SparePart sparepart, int quantity)
        {
            CartPosition CurrentPosition = PositionCollection
                .Where(g => g.SparePart.Id == sparepart.Id)
                .FirstOrDefault();

            if (CurrentPosition == null)
            {
                PositionCollection.Add(new CartPosition
                    {
                        SparePart = sparepart,
                        Quantity = quantity
                    });                    
            }
            else
            {
                CurrentPosition.Quantity += quantity;
            }
        }

        public void RemoveLine(SparePart sparepart)
        {
            var a = PositionCollection.RemoveAll(line => line.SparePart.Id == sparepart.Id);
                
        }

        public decimal TotalPrice()
        {
            return PositionCollection.Sum(e => e.SparePart.Price * e.Quantity);
        }

        public void Clear()
        {
            PositionCollection.Clear();
        }

        public IEnumerable<CartPosition> Positions
        {
            get { return PositionCollection; }
        }
    }
}