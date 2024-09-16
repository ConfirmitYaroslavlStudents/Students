using System.Collections.Generic;
using BillSplitter.Models;

namespace BillSplitterTests
{
    public class PositionSeeder
    {
        public List<Position> Seed()
        {
            Position a = new Position
            {
                Name = "a",
                Price = 2,
                Quantity = 10
            };
            Position b = new Position
            {
                Name = "b",
                Price = 3,
                Quantity = 20
            };
            Position c = new Position
            {
                Name = "c",
                Price = 1,
                Quantity = 3
            };

            return new List<Position> {a, b, c};
        }
    }
}