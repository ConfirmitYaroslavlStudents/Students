using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillSplitter.Models.ViewModels.PickPositions
{
    public class PositionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PickedQuantity { get; set; }
        public decimal? UserQuantity { get; set; }
        public bool Selected { get; set; }

        public decimal Price { get; set; }
        public List<Order> Orders { get; set; }
    }
}
