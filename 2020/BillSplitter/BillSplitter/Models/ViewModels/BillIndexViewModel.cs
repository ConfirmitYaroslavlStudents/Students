using System.Collections.Generic;

namespace BillSplitter.Models.ViewModels
{
    public class BillIndexViewModel
    {
        public List<Bill> AdminBills { get; set; }
        public List<Bill> CustomerBills { get; set; }
    }
}
