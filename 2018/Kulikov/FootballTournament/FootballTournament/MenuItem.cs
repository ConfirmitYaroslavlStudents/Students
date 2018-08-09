using System;

namespace FootballTournament
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

        public Action Action { get; set; }

        public string Title { get; set; }
    }
}
