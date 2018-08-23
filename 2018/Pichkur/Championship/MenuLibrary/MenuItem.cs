using System;

namespace MenuLibrary
{
    public class MenuItem
    {
        public MenuItem()
        {
            Title = "";
        }
        public MenuItem(Action action, string title)
        {
            Action = action;
            Title = title;
        }

        public Action Action;
        public string Title { get; set; }
    }
}
