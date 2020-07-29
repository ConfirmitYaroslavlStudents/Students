using BillSplitter.Models;
using System.Linq;

namespace BillSplitter.Data
{
    public class UsersDbAccessor
    {
        private BillContext _context;

        public UsersDbAccessor(BillContext context)
        {
            _context = context;
        }

        public User GetUserByName(string name)
        {
            return _context.Users.FirstOrDefault(u => u.Name == name);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
             _context.SaveChanges();
        }
    }
}
