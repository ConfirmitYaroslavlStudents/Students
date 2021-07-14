using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MyTODO
{
    public class ListTODO
    {
        public class ItemTODO
        {
            public string name;
            public int state;
            public ItemTODO(string name)
            {
                this.name = name;
                this.state = 0;
            }
            public ItemTODO(string name, int done)
            {
                this.name = name;
                this.state = done;
            }
            public bool ChangeState(int state)
            {
                if (this.state != 0 || Math.Abs(state) > 1)
                    return false;
                this.state = state;
                return true;
            }
            public void ChangeName(string name)
            {
                if (string.IsNullOrEmpty(name))
                    return;
                this.name = name;
            }
        }

        public List<ItemTODO> items;
        public FileInfo file;

        public int Count 
        {
            get => items.Count;
        }

        public ListTODO()
        {
            items = new List<ItemTODO>();
        }

        public ListTODO(FileInfo input)
        {
            file = input;
            items = new List<ItemTODO>();
            if (!input.Exists)
                return;
            using StreamReader reader = new StreamReader(input.FullName);
            while (!reader.EndOfStream)
            {
                var split = reader.ReadLine().Split(new string[] { "%^&" }, StringSplitOptions.RemoveEmptyEntries);
                items.Add(new ItemTODO(split[0], int.Parse(split[1])));
            }
        }

        public bool Add(string item)
        {
            if (items.FindAll(x => (x.name == item && x.state != -1)).Count!=0 || string.IsNullOrEmpty(item))
                return false;
            items.Add(new ItemTODO(item));
            return true;
        }

        public bool NameAvailable(string name) => !(items.FindAll(x => (x.name == name && x.state == 0)).Count != 0 || string.IsNullOrEmpty(name));

        public void Save()
        {
            using var writer = new StreamWriter(file.FullName);
            foreach (var each in items)
            {
                writer.WriteLine("{0}%^&{1}", each.name, each.state);
            }
        }

        public void SaveToFile(FileInfo output)
        {
            file = output;
            Save();
        }
    }
}
