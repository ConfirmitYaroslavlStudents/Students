using System.Collections.Generic;

namespace Refactoring.Utils
{
    public class CompositeFormatter : ICustomerFormatter
    {
        private readonly IList<ICustomerFormatter> _formatters = new List<ICustomerFormatter>();

        public void AddFormatter(ICustomerFormatter formatter)
        {
            _formatters.Add(formatter);
        }

        public void Serialize(SerializedData data, Customer customer)
        {
            foreach (var formatter in _formatters)
            {
                formatter.Serialize(data, customer);
            }
        }
    }
}
