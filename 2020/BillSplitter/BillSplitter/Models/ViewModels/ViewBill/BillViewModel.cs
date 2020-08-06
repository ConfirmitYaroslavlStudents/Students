using System.Collections.Generic;

namespace BillSplitter.Models.ViewModels.ViewBill
{
    public class BillViewModel
    {
        public bool HasManageAccess { get; set; }
        public Bill Bill { get; set; }
        public List<PositionViewModel> Positions { get; set; }
        public decimal CustomerSum { get; set; }
    }
}