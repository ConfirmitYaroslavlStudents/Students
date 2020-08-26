using System.Collections.Generic;

namespace BillSplitter.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Position> Positions { get; set; } = new List<Position>();
        public virtual List<Member> Members { get; set; } = new List<Member>();
    }
}
