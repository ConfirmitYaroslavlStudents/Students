using System.Collections.Generic;
using ToDoLibrary;

namespace TodoWeb
{
    public class GetRequest: Request
    {
        public List<Task> Tasks;
    }
}