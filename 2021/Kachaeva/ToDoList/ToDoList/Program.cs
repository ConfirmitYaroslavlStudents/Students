using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ToDoListProject
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var fileName = "ToDoList.txt";
            var binFormat = new BinaryFormatter();
            ToDoList toDoList;

            if (!File.Exists(fileName))
                toDoList = new ToDoList();
            else
            {
                using (Stream fStream = File.OpenRead(fileName))
                    toDoList = (ToDoList)binFormat.Deserialize(fStream);
            }

            while (true)
            {
                if (toDoList.Count == 0)
                {
                    Console.WriteLine("Начните создавать свой список дел! Введите текст своего первого задания или нажмите q чтобы выйти");
                    var selectedAction = Console.ReadLine();
                    Console.WriteLine();
                    if (selectedAction == "q")
                    {
                        if (File.Exists(fileName))
                            File.Delete(fileName);
                        break;
                    }
                    toDoList.Add(selectedAction);
                }
                else
                {
                    Console.WriteLine("Что вы хотели бы сделать? Введите:");
                    Console.WriteLine("1 - просмотреть список");
                    Console.WriteLine("2 - добавить задание");
                    Console.WriteLine("3 - удалить задание");
                    Console.WriteLine("4 - изменить текст задания");
                    Console.WriteLine("5 - изменить статус задания");
                    Console.WriteLine("q - выйти");
                    Console.WriteLine();
                    var selectedAction = Console.ReadLine();
                    Console.WriteLine();

                    if (selectedAction == "q")
                    {
                        using (var fStream = new FileStream(fileName, FileMode.Create))
                            binFormat.Serialize(fStream, toDoList);
                        break;
                    }

                    switch (selectedAction)
                    {
                        case "1":
                            Console.WriteLine(toDoList.ToString());
                            break;

                        case "2":
                            Console.WriteLine("Введите текст задания");
                            var text = Console.ReadLine();
                            Console.WriteLine();
                            if (!String.IsNullOrEmpty(text))
                                toDoList.Add(text);
                            break;

                        case "3":
                            Console.WriteLine("Введите номер задания для удаления");
                            var number = int.Parse(Console.ReadLine());
                            Console.WriteLine();
                            if (number > toDoList.Count || number < 1)
                            {
                                Console.WriteLine("Задания с таким номером не существует");
                                Console.WriteLine();
                            }
                            else
                                toDoList.Remove(number - 1);
                            break;

                        case "4":
                            Console.WriteLine("Введите номер задания для изменения текста");
                            number = int.Parse(Console.ReadLine());
                            Console.WriteLine();
                            if (number > toDoList.Count || number < 1)
                            {
                                Console.WriteLine("Задания с таким номером не существует");
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("Введите новый текст");
                                var newText = Console.ReadLine();
                                Console.WriteLine();
                                if (!String.IsNullOrEmpty(newText))
                                    toDoList[number - 1].ChangeText(newText);
                            }
                            break;

                        case "5":
                            Console.WriteLine("Введите номер задания для изменения статуса");
                            number = int.Parse(Console.ReadLine());
                            Console.WriteLine();
                            if (number > toDoList.Count || number < 1)
                            {
                                Console.WriteLine("Задания с таким номером не существует");
                                Console.WriteLine();
                            }
                            else
                                toDoList[number - 1].ChangeStatus();
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }
}
