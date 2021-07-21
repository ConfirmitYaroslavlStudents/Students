using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public static class Messages
    {
        internal static string WrongFormatOfInputData()
        {
            return ("Введенные данные некорректны");
        }
        internal static  string ToDoListIsEmpty()
        {
            return ("Лист пуст");
        }
        internal static string RequestNumber()
        {
            return("Введите порядковый номер задания: ");
        }

        internal static string RequestDescription()
        {
            return("Введите описание задачи: ");
        }
    }
}
