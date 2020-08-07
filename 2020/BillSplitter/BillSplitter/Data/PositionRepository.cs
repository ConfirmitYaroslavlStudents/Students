using System.Linq;
using BillSplitter.Models;

namespace BillSplitter.Data
{
    public class PositionsDbAccessor
    {
        private readonly BillContext _context;

        public PositionsDbAccessor(BillContext context)
        {
            _context = context;
        }

        public void Add(Position pos)
        {
            _context.Positions.Add(pos);
        }

        public Position GetById(int positionId)
        {
            return _context.Positions.FirstOrDefault(x => x.Id == positionId);
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
            _context.Positions.Remove(GetById(positionId));
        }
    }
}