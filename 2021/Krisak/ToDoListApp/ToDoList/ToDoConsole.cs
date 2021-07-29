using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ToDoList
{
    public class ToDoConsole
    {
        List<Note> _notes = new List<Note>();
        HandlingEnteredCommandInConsole _handlingEnteredCommandInConsol = new HandlingEnteredCommandInConsole();
        bool isWork;
        Loggers.ConsoleLogger _consoleLogger = new Loggers.ConsoleLogger();
        SaveAndLoadNotes _saveAndLoad = new SaveAndLoadNotes("MyNotes.txt");

        public ToDoConsole()
        {
            LoadNotes();
        }

        public void DoWork()
        {
            isWork = true;

            while (isWork)
            {
                PrintingConsoleMenuToDo.PrintHelp();

                try
                {
                    _handlingEnteredCommandInConsol.HandleInput(_notes, Console.ReadLine());

                    SaveNotes();

                    if (_handlingEnteredCommandInConsol.IsFinished)
                        isWork = false;
                }

                catch (WrongEnteredCommandException e)
                {
                    _consoleLogger.Log(e.Message);
                }

                catch (Exception e)
                {
                    var fileAndConsoleLogger = new Loggers.FileLoggerDecorator(_consoleLogger, "Log.txt");
                    fileAndConsoleLogger.Log(DateTime.Now.ToString());
                    fileAndConsoleLogger.Log(e.Message);
                }
            }

            PrintingConsoleMenuToDo.PrintGoodbye();
        }

        private void SaveNotes()
        {
            _saveAndLoad.Save(_notes);
        }

        private void LoadNotes()
        {
            try
            {
                _notes = _saveAndLoad.Load();
            }

            catch (FileNotFoundException e)
            {
                _notes = new List<Note>();
             
                _consoleLogger.Log(e.Message);
            }

            catch (Exception e)
            {
                var fileAndConsoleLogger = new Loggers.FileLoggerDecorator(_consoleLogger, "Log.txt");
                fileAndConsoleLogger.Log(DateTime.Now.ToString());
                fileAndConsoleLogger.Log(e.Message);
            }
        }
    }
}
