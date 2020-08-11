﻿using System.Collections.Generic;
using System.Linq;
using BillSplitter.Models;

namespace BillSplitter.Data
{
    public class BillRepository
    {
        private BillContext _context;

        public BillRepository(BillContext context)
        {
            _context = context;
        }

        public void Add(Bill bill)
        {
            _context.Bills.Add(bill);
        }

        public List<Bill> GetByCustomerUserId(int userId)
        {
            return _context.Customers.Where(c => c.UserId == userId).Select(c => c.Bill).Where(b => b.UserId != userId).ToList();
        }
        public Bill GetBillById(int billId)
        {
            var bill = _context.Bills.FirstOrDefault(x => x.Id == billId);

            return bill;
        }
    
        public bool Exist(int billId)
        {
            return _context.Bills.FirstOrDefault(x => x.Id == billId) != null;
        }

        public void DeleteById(int billId)
        {
            _context.Bills.Remove(GetBillById(billId));
        }
    }
}