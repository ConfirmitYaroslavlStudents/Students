using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToDoListProject
{
    public class FileWorkHandler : IToDoListLoaderSaver
    {
        private string _fileName;

        public FileWorkHandler(string fileName)
        {
            _fileName = fileName;
        }

        public ToDoList Load()
        {
            if (!File.Exists(_fileName))
                return new ToDoList();
            var tasks = File.ReadLines(_fileName);
            var toDoList = new ToDoList();
            foreach(var listItem in tasks)
            {
                int textStartIndex = listItem.IndexOf('.') + 2;
                int textEndIndex = listItem.IndexOf('[') - 2;
                string text = listItem.Substring(textStartIndex, textEndIndex - textStartIndex);
                var task = new Task(text);
                if (listItem[textEndIndex+3] == 'v')
                    task.ChangeStatus();
                toDoList.Add(task);
            }
            return toDoList;
        }

        public void Save (ToDoList toDoList)
        {
            if (toDoList.Count != 0)
                File.WriteAllText(_fileName, toDoList.ToString());
        }
    }
}
