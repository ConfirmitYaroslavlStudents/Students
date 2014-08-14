namespace Refactoring.Utils
{
    public interface ICustomerDeserializer
    {
        Customer Deserialize(SerializedData serializedData);
    }
}
