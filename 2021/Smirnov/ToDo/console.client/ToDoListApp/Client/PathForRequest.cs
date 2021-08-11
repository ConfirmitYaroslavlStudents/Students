namespace ToDoListApp.Client
{
    public class PathForRequest // bad name
    {
        private const string APP_PATH = "https://localhost:5001";

        public string GetPathForGetRequest()
        {
            return APP_PATH + "/api/ToDoItems";
        }
        public string GetPathForPostRequest()
        {
            return APP_PATH + "/api/ToDoItems";
        }
        public string GetPathForPatchRequest(long id)
        {
            return APP_PATH + $"/api/ToDoItems/{id}";
        }
        public string GetPathForDeleteRequest(long id)
        {
            return APP_PATH + $"/api/ToDoItems/{id}";
        }
    }
}
