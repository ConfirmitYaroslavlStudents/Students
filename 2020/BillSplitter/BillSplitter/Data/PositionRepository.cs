using System.Linq;
using BillSplitter.Models;
using Microsoft.EntityFrameworkCore;

namespace BillSplitter.Data
{
    public class PositionsDbAccessor
    {
        private readonly DbContext _context;

        public PositionsDbAccessor(DbContext context)
        {
            _context = context;
        }

        public void Add(Position pos)
        {
            _context.Set<Position>().Add(pos);
        }

        public Position GetById(int positionId)
        {
            return _context.Set<Position>().FirstOrDefault(x => x.Id == positionId);
        }

        public void UpdateById(int positionId, Position positionData)
        {
            var position = GetById(positionId);
            
            position.Name = positionData.Name;
            position.Price = positionData.Price;
            position.Quantity = positionData.Quantity;
        }

        public void DeleteById(int positionId)
        {
            _context.Set<Position>().Remove(GetById(positionId));
        }
    }
}