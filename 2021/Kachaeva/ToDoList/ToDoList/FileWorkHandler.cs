using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ToDoListProject
{
    public class FileWorkHandler : IToDoListLoaderSaver
    {
        private BinaryFormatter _binaryFormatter = new BinaryFormatter();
        private string _fileName;

        public FileWorkHandler(string fileName)
        {
            _fileName = fileName;
        }

        public ToDoList Load()
        {
            if (!File.Exists(_fileName))
                return new ToDoList();
            using (Stream fStream = File.OpenRead(_fileName))
                return (ToDoList)_binaryFormatter.Deserialize(fStream);
        }

        public void Save (ToDoList toDoList)
        {
            if (toDoList.Count != 0)
            {
                using (var fStream = new FileStream(_fileName, FileMode.Create))
                    _binaryFormatter.Serialize(fStream, toDoList);
            }
        }
    }
}
