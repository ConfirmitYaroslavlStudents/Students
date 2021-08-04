using System;

namespace ToDoApp
{
    public class ToDoItem : IEquatable<ToDoItem>
    {
        public int Id { get; set; }
        public bool IsComplete { get; set; }
        public string Description { get; set; }
        public bool Equals(ToDoItem other)
        {
            return other != null && Id == other.Id && IsComplete == other.IsComplete && Description.Equals(other.Description);
        }
    }
}