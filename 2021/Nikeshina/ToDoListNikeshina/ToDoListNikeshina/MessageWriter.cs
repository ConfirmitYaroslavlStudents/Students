using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public static class MessageWriter
    {
        internal static void WrongFormatOfInputData()
        {
            Console.WriteLine("Введенные данные некорректны");
        }
        internal static  void ToDoListIsEmpty()
        {
            Console.WriteLine("Лист пуст");
        }
        internal static void RequestNumber()
        {
            Console.Write("Введите порядковый номер задания: ");
        }

        internal static void RequestDescription()
        {
            Console.Write("Введите описание задачи: ");
        }
    }
}
