namespace ToDoLibrary
{
    public class Task
    {
        public string Text = "";
        public StatusTask Status;
        public long TaskId ;

        public override string ToString()
        {
            return Status switch
            {
                StatusTask.Done => $"[{TaskId}] {Text} ●",
                StatusTask.IsProgress => $"[{TaskId}] {Text} ○",
                _ => $"[{TaskId}] {Text}",
            };
        }
    }
}
