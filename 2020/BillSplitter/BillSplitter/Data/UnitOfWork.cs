using Microsoft.EntityFrameworkCore;

namespace BillSplitter.Data
{
    public class UnitOfWork
    {
        public readonly BillRepository Bills;
        public readonly UserRepository Users;
        public readonly MemberRepository Members;
        public readonly OrderRepository Orders;
        public readonly PositionsDbAccessor Positions;

        private readonly DbContext _context;

        public UnitOfWork(BillContext context)
            : this((DbContext) context)
        {
        }

        public UnitOfWork(DbContext context)
        {
            Bills = new BillRepository(context);
            Users = new UserRepository(context);
            Members = new MemberRepository(context);
            Orders = new OrderRepository(context);
            Positions = new PositionsDbAccessor(context);
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
