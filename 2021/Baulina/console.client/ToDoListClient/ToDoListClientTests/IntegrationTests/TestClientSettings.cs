using Microsoft.Extensions.Options;
using ToDoApp.Settings;

namespace ToDoListClientTests.IntegrationTests
{
    public class TestClientSettings: IOptions<ClientSettingsConfiguration>
    {
        public TestClientSettings()
        {
            Value = new ClientSettingsConfiguration
            {
                Parameters = new Parameters {ApplicationUrl = "http://localhost:5000/", RequestUri = "todo-list"}
            };
        }
        public ClientSettingsConfiguration Value  { get; }
    }
}
