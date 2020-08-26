﻿using System.Collections.Generic;

namespace BillSplitter.Models.ViewModels.ViewBill
{
    public class BillViewModel
    {
        public bool isModerator { get; set; }
        public bool isAdmin { get; set; }
        public Bill Bill { get; set; }
        public Dictionary<string, decimal> Payments { get; set;}
        public Dictionary<string, decimal> Debts { get; set; }
        public List<PositionViewModel> Positions { get; set; }
        public decimal MemberSum { get; set; }
    }
}