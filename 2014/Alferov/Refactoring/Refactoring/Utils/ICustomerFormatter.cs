namespace Refactoring.Utils
{
    public interface ICustomerFormatter
    {
        void Serialize(SerializedData data, Customer customer);
    }
}