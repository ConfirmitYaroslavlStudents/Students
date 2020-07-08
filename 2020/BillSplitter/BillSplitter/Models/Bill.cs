using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillSplitter.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public List<Position> Positions { get; }
        public string Url { get; }
        // to-add State
    }
}
