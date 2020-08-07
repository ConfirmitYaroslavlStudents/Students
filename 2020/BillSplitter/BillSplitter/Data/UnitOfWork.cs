using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillSplitter.Data
{
    public class UnitOfWork
    {
        private readonly BillContext _context;
        public readonly BillsDbAccessor Bills;
        public readonly UsersDbAccessor Users;
        public readonly CustomersDbAccessor Customers;
        public readonly OrdersDbAccessor Orders;
        public readonly PositionsDbAccessor Positions;
        public UnitOfWork(BillContext context)
        {
            Bills = new BillsDbAccessor(context);
            Users = new UsersDbAccessor(context);
            Customers = new CustomersDbAccessor(context);
            Orders = new OrdersDbAccessor(context);
            Positions = new PositionsDbAccessor(context);
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
