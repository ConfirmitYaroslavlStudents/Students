using System.Threading.Tasks;

namespace ToDoApp
{
    public interface IExecute
    {
        Task ProcessOperation(string operationName);
        Task Add();
        Task Edit();
        Task Complete();
        Task Delete();
        Task List();
        Task Error();
    }

}
