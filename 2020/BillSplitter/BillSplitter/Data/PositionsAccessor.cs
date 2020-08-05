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

        public void AddPosition(Position pos)
        {
            _context.Positions.Add(pos);
            _context.SaveChanges();
        }

        public Position GetPositionById(int positionId)
        {
            return _context.Positions.FirstOrDefault(x => x.Id == positionId);
        }

        public void UpdateById(int positionId, Position positionData)
        {
            var position = GetPositionById(positionId);
            
            position.Name = positionData.Name;
            position.Price = positionData.Price;
            position.Quantity = positionData.Quantity;

            _context.SaveChanges();
        }

        public void DeleteById(int positionId)
        {
            _context.Positions.Remove(GetPositionById(positionId));
            _context.SaveChanges();
        }
    }
}