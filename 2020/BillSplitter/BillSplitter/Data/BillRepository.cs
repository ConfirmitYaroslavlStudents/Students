using System.Collections.Generic;
using System.Linq;
using BillSplitter.Models;
using Microsoft.EntityFrameworkCore;

namespace BillSplitter.Data
{
    public class BillRepository 
    {
        private DbContext _context;

        public BillRepository(DbContext context)
        {
            _context = context;
        }

        public void Add(Bill bill)
        {
            _context.Set<Bill>().Add(bill);
        }

        public List<Bill> GetByMemberUserId(int userId)
        {
            return _context.Set<Member>().Where(c => c.UserId == userId).Select(c => c.Bill).ToList();
        }

        public Bill GetBillById(int billId)
        {
            var bill = _context.Set<Bill>().FirstOrDefault(x => x.Id == billId);

            return bill;
        }
    
        public bool Exist(int billId)
        {
            return _context.Set<Bill>().FirstOrDefault(x => x.Id == billId) != null;
        }

        public void DeleteById(int billId)
        {
            var bill = GetBillById(billId);
            _context.Set<Bill>().Remove(bill);
        }
    }
}