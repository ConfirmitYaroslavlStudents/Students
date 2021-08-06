namespace ToDoLibrary
{
    public class Task
    {
        public string Text;
        public StatusTask Status = StatusTask.ToDo;

        public override string ToString()
        {
            return Status switch
            {
                StatusTask.Done => Text + " [X]",
                StatusTask.IsProgress => Text + " []",
                _ => Text
            };
        }
    }
}
