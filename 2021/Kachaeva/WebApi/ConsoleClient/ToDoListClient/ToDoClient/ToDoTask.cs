namespace ToDoClient
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }

        public override string ToString()
        {
            if (IsDone == true)
                return $"{Id}. {Text}  [v]";
            return $"{Id}. {Text}  [ ]";
        }
    }
}