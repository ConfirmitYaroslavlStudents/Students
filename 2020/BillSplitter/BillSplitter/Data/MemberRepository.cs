using System.Linq;
using BillSplitter.Models;
using Microsoft.EntityFrameworkCore;

namespace BillSplitter.Data
{
    public class MemberRepository
    {
        private readonly DbContext _context;

        public MemberRepository(DbContext context)
        {
            _context = context;
        }

        public void Add(Member member)
        {
            _context.Set<Member>().Add(member);
        }

        public Member GetById(int memberId)
        {
            return _context.Set<Member>().FirstOrDefault(x => x.Id == memberId);
        }

        public void UpdateMemberRoleById(int memberId, string role)
        {
            var member = GetById(memberId);
            member.Role = role;
        }

        public void DeleteById(int memberId)
        {
            var member = GetById(memberId);

            var orders = member.Orders;

            _context.Set<Order>().RemoveRange(orders);
            _context.Set<Member>().Remove(member);
        }
    }
}