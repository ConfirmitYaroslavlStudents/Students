using System.Collections.Generic;

namespace BillSplitter.Models.ViewModels.ViewBill
{
    public class ViewBillModel
    {
        public bool HasManageAccess { get; set; }
        public Bill Bill { get; set; }
        public List<ViewBillPositionModel> Positions { get; set; }
        public decimal CustomerSum { get; set; }
    }
}