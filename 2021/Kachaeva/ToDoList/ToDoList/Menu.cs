using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListProject
{
    public struct Menu
    {
        public const string displayToDoList = "1";
        public const string addTask = "2";
        public const string removeTask = "3";
        public const string changeTaskText = "4";
        public const string changeTaskStatus = "5";
        public const string quit = "q";

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
