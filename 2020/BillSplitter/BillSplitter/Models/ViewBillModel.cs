using System.Collections.Generic;

namespace BillSplitter.Models
{
    public class ViewBillModel
    {
        public bool HasManageAccess { get; set; }
        public Bill Bill { get; set; }
        public List<ViewBillPositionModel> Positions { get; set; }
        public decimal CustomerSum { get; set; }
    }
}