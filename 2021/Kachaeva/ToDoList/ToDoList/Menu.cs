namespace ToDoListProject
{
    public struct Menu
    {
        public const string DisplayToDoList = "1";
        public const string AddTask = "2";
        public const string RemoveTask = "3";
        public const string ChangeTaskText = "4";
        public const string ChangeTaskStatus = "5";
        public const string Quit = "q";

        public static void PrintMenu(IWriterReader writerReader)
        {
            writerReader.Write("Что вы хотели бы сделать? Введите:");
            writerReader.Write("1 - просмотреть список");
            writerReader.Write("2 - добавить задание");
            writerReader.Write("3 - удалить задание");
            writerReader.Write("4 - изменить текст задания");
            writerReader.Write("5 - изменить статус задания");
            writerReader.Write("q - выйти\r\n");
        }
    }
}
