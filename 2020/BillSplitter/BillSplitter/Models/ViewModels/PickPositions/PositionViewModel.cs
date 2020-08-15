﻿using System.Collections.Generic;
using System.Linq;

namespace BillSplitter.Models.ViewModels.PickPositions
{
    public class PositionViewModel
    {
        public PositionViewModel(Position position, Order order)
        {
            Name = position.Name;
            Id = position.Id;
            Selected = order != null;
            Quantity = position.Quantity;
            UserQuantity = order != null ? order.Quantity : null;
            RemainsQuantity = Quantity - (position.Orders.Where(o => o.Quantity != null).Sum(o => o.Quantity) ?? 0) + (UserQuantity ?? 0);
            Orders = position.Orders;
            Price = position.Price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal? UserQuantity { get; set; }
        public decimal RemainsQuantity { get; set; }
        public bool Selected { get; set; }
        public decimal Price { get; set; }
        public List<Order> Orders { get; set; }
    }
}
