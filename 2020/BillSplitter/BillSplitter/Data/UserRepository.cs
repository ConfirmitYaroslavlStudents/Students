using BillSplitter.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BillSplitter.Data
{
    public class UserRepository
    {
        private readonly DbContext _context;

        public UserRepository(DbContext context)
        {
            _context = context;
        }

        public User GetByName(string name)
        {
            return _context.Set<User>().FirstOrDefault(u => u.Name == name);
        }

        public User GetById(int userId)
        {
            return _context.Set<User>().FirstOrDefault(u => u.Id == userId);
        }

        public void Add(User user)
        {
            _context.Set<User>().Add(user);
        }
    }
}
