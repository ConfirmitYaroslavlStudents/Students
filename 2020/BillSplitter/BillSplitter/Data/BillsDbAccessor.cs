using System;
using System.Linq;
using BillSplitter.Models;

namespace BillSplitter.Data
{
    public class BillsDbAccessor
    {
        private BillContext _context;

        public BillsDbAccessor(BillContext context)
        {
            _context = context;
        }

        public void AddBill(Bill bill)
        {
            _context.Bill.Add(bill);
            _context.SaveChanges();
        }

        public Bill GetBillById(int billId)
        {
            var bill = _context.Bill.FirstOrDefault(x => x.Id == billId);

            return bill;
        }
    
        public bool DbContains(int billId)
        {
            return _context.Bill.FirstOrDefault(x => x.Id == billId) != null;
        }

        public void DeleteById(int billId)
        {
            _context.Bill.Remove(GetBillById(billId));
            _context.SaveChanges();
        }
    }
}