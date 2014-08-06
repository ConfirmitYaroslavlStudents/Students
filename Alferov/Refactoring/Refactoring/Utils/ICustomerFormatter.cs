namespace Refactoring.Utils
{
    public interface ICustomerFormatter
    {
        string Serialize(Customer customer);
        Customer Deserialize(string serializedData);
    }
}