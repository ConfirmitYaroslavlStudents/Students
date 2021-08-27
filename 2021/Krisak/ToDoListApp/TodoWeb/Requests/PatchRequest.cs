namespace TodoWeb.Requests
{
    public class PatchRequest: Request
    {
        public long TaskId;
        public string Text;
        public string Status;
    }
}