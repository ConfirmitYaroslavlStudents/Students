using System.Net.Http;
using System.Text;

namespace ToDoListApp.Client
{
    public class BodyForRequest
    {
        public StringContent GetBodyForPostRequest(string description)
        {
            return new StringContent($@"{{""description"" : ""{description}""}}", Encoding.UTF8, "application/json");
        }
        public StringContent GetBodyForPatchRequest(string description)
        {
            return new StringContent($@"{{""description"" : ""{description}""}}", Encoding.UTF8, "application/json");
        }
        public StringContent GetBodyForPatchRequest(long status)
        {
            return new StringContent($@"{{""status"" : ""{status}""}}", Encoding.UTF8, "application/json");
        }
    }
}
