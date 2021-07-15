using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ToDoList
{
    public class ToDoConsole
    {
        NotesList _notesList;
        bool isWork;
        string[] _formatInputHelp = new string[]
        {
            "Input format:",
            "Сreating new note: \"add\"",
            "Edit note: \"edit 00\"",
            "Change flag note: \"flag 00\"",
            "Delete note: \"delete 00\"",
            "Delete all notes: \"all\"",
            "Save in .txt: \"txt\"",
            "Exit: \"bye\"",
        };

        public ToDoConsole()
        {
            _notesList = new NotesList();
        }

        public void DoWork()
        {
            isWork = true;
            try
            {
                _notesList.LoadSerialization();
            }
            catch { }

            while (isWork)
            {
                Console.Clear();
                DisplayMainMenu();
                try
                {
                    var choise = Console.ReadLine().Split(" ");
                    SelectionChoise(choise);

                    _notesList.SaveSerialization();
                }
                catch
                {
                    ProcessException();
                }
            }

            isWork = false;

            Console.Clear();
            Console.WriteLine("Bye Bye ^-^");
            Console.ReadKey();
        }

        void SelectionChoise(string[] choise)
        {
            switch (choise[0])
            {
                case "add":
                    {
                        Add();
                        break;
                    }

                case "edit":
                    {
                        var result = 0;
                        int.TryParse(choise[1], out result);
                        if (result <= 0 || result > _notesList.Count)
                            throw new ArgumentException();

                        result--;

                        Edit(result);
                        break;
                    }

                case "flag":
                    {
                        var result = 0;
                        int.TryParse(choise[1], out result);
                        if (result <= 0 || result > _notesList.Count)
                            throw new ArgumentException();

                        result--;

                        SetFlag(result);
                        break;
                    }

                case "delete":
                    {
                        var result = 0;
                        int.TryParse(choise[1], out result);
                        if (result <= 0 || result > _notesList.Count)
                            throw new ArgumentException();

                        result--;

                        Delete(result);
                        break;
                    }

                case "all":
                    {
                        DeleteAll();
                        break;
                    }

                case "txt":
                    {
                        SaveInTXT();

                        Console.Clear();
                        Console.WriteLine("Txt file saved successfully");
                        Console.ReadKey();
                        break;
                    }

                case "bye":
                    {
                        isWork = false;
                        break;
                    }

                default: { throw new ArgumentException(); }
            }
        }

        void Add()
        {
            Console.Clear();
            Console.WriteLine("Input text of note, then enter Enter:");

            var line = Console.ReadLine();

            _notesList.Add(new Note(line));
        }
        void Edit(int index)
        {
            Console.Clear();
            Console.WriteLine("Make changes text of note, then enter Enter:");
            Console.Write(_notesList[index].Text);
            Console.SetCursorPosition(0, Console.CursorTop);

            var line = Console.ReadLine();

            _notesList.ChangeText(line, index);
        }

        void SetFlag(int index)
        {
            if (_notesList[index] is NoteCompleted)
                _notesList.ChangeStatus(new Note(), index);
            else
                _notesList.ChangeStatus(new NoteCompleted(), index);
        }

        void Delete(int index)
        {
            _notesList.Remove(index);
        }

        void DeleteAll()
        {
            _notesList.RemoveAll();
        }

        void SaveInTXT()
        {
            var sw = new StreamWriter("MyNotes.txt");
            _notesList.SaveInTXT(sw);
            sw.Close();
        }

        void ProcessException()
        {
            Console.Clear();
            Console.WriteLine("You entered wrong command!");
            Console.ReadKey();
        }

        void DisplayMainMenu()
        {
            Console.Clear();
            DisplayAllNotes();
            DisplaySeparator();
            DisplayFormatInputHelp();
            Console.WriteLine();
        }

        void DisplayAllNotes()
        {
            var list = _notesList.OutputNotes();

            for (var i = 0; i < list.Count; i++)
                Console.WriteLine("{0}. {1}", i + 1, list[i]);
        }

        void DisplayFormatInputHelp()
        {
            for (var i = 0; i < _formatInputHelp.Length; i++)
                Console.WriteLine(_formatInputHelp[i]);
        }

        void DisplaySeparator()
        {
            Console.WriteLine("------------");
        }
    }
}
