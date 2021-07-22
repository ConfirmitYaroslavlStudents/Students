namespace ToDo
{
    public struct Menu
    {
        public const string DisplayToDoList = "list";
        public const string AddTask = "add";
        public const string RemoveTask = "remove";
        public const string ChangeTaskText = "text";
        public const string ChangeTaskStatus = "status";
        public const string Quit = "q";

        public static void PrintMenu(IWriterReader writerReader)
        {
            writerReader.Write("Что вы хотели бы сделать? Введите:");
            writerReader.Write("list - просмотреть список");
            writerReader.Write("add - добавить задание");
            writerReader.Write("remove - удалить задание");
            writerReader.Write("text - изменить текст задания");
            writerReader.Write("status - изменить статус задания");
            writerReader.Write("q - выйти\r\n");
        }
    }
}
