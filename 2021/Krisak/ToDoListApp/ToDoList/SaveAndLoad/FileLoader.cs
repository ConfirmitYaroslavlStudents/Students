using System;
using System.Collections.Generic;
using System.IO;
using ToDoLibrary.Loggers;

namespace ToDoLibrary.SaveAndLoad
{
    public class FileLoader: ILoadTasks
    {
        private readonly string _fileName;
        private readonly ILogger _logger;

        public FileLoader(string fileName, ILogger logger)
        {
            _fileName = fileName;
            _logger = logger;
        }

        public List<Task> Load()
        {
            try
            {
                if (!File.Exists(_fileName))
                    throw new FileNotFoundException("Saved data was not found. New list created.");

                return TasksToSaveAndLoadConverter.ConvertTasksAfterLoading(File.ReadAllLines(_fileName));
            }

            catch (Exception e)
            {
                _logger.Log(e);
                return new List<Task>();
            }
        }
    }
}