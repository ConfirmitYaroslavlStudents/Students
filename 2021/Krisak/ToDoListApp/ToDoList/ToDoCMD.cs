using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToDoList
{
    public class ToDoCMD
    {
        List<Note> _notes = new List<Note>();
        Loggers.ConsoleLogger _consoleLogger = new Loggers.ConsoleLogger();
        SaveAndLoadNotes _saveAndLoad = new SaveAndLoadNotes("MyNotes.txt");

        public ToDoCMD()
        {
            LoadNotes();
        }

        public void DoWork(string[] command)
        {
            try
            {
                HandlingEnteredCommandInCMD.HandleInput(_notes, command);
                SaveNotes();
            }

            catch (WrongEnteredCommandException e)
            {
                _consoleLogger.Log(e.Message);
            }

            catch (Exception e)
            {
                var fileAndConsoleLogger = new Loggers.FileLoggerDecorator(_consoleLogger, "Log.txt");
                fileAndConsoleLogger.Log(e.Message);
            }
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
                fileAndConsoleLogger.Log(e.Message);
            }
        }
    }
}
