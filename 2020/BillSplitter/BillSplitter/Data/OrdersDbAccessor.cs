using BillSplitter.Models;

namespace BillSplitter.Data
{
    public class OrdersDbAccessor
    {
        private BillContext _context;
        
        public OrdersDbAccessor(BillContext context)
        {
            _context = context;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        
        public void AddOrders(Customer customer, int[] selected, int[] numerator, int[] denomenator)
        {
            for (int i = 0; i < selected.Length; i++)
            {
                if (1.0 * numerator[i] / denomenator[i] > double.Epsilon)
                {
                    var order = new Order
                    {
                        CustomerId = customer.Id,
                        PositionId = selected[i],
                        Quantity = 1.0 * numerator[i] / denomenator[i]
                    };

                    AddOrder(order);
                }
            }
        }
    }
}