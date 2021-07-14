using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace ToDoListLib
{
    public class Task: IEquatable<Task>
    {
        public Task(string name, string description, TaskStatus status)
        {
            Name = name;
            Description = description;
            Status = status;
        }
        public Task()
        {
            Name = "";
            Description = "";
            Status = TaskStatus.NotDone;
        }
        [XmlElement("Name")]
        public string Name { set; get; }
        [XmlAttribute("Value")]
        public string Description { set; get; }
        [XmlIgnore]
        public TaskStatus Status { set; get; }

        public bool Equals([AllowNull] Task other)
        {
            if (this.Name == other.Name && this.Description == other.Description && this.Status == other.Status)
                return true;
            return false;
        }
    }
}
