namespace ToDoApp.Settings
{
    public sealed class ClientSettingsConfiguration
    {
        public Parameters Parameters { get; set; }
    }

    public sealed class Parameters
    {
        public string ApplicationUrl { get; set; }
        public string RequestUri { get; set; }
    }
}
