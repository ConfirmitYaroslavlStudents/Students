using BillSplitter.Models;
using System.Linq;

namespace BillSplitter.Data
{
    public class UserRepository
    {
        private readonly BillContext _context;

        public UserRepository(BillContext context)
        {
            _context = context;
        }

        public User GetByName(string name)
        {
            return _context.Users.FirstOrDefault(u => u.Name == name);
        }

        public User GetById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }
    }
}
