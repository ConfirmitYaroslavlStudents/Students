using System.Collections.Generic;
using System.Linq;
using BillSplitter.Models;

namespace BillSplitter.Data
{
    public class BillsDbHelper
    {
        private BillContext _context;

        public BillsDbHelper(BillContext context)
        {
            _context = context;
        }

        public void AddBill(Bill bill)
        {
            _context.Add(bill);
            _context.SaveChanges();
        }

        public Bill GetBillById(int billId)
        {
            var bill = _context.Bill.FirstOrDefault(x => x.Id == billId);

            return bill;
        }

        public List<Position> GetPositionsById(int billId)
        {
            return _context.Bill.FirstOrDefault(x => x.Id == billId)?.Positions;
        }

        public List<Customer> GetCustomersById(int billId)
        {
            return _context.Bill.FirstOrDefault(x => x.Id == billId)?.Customers;
        }

        public void UpdateBills()
        {
            _context.SaveChanges();
        }

        public bool DbContains(int billId)
        {
            return _context.Bill.FirstOrDefault(x => x.Id == billId) != null;
        }
    }
}