using MyToDoList;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ToDoListConsole
{
    internal class TableBuilder
    {
        private readonly ToDoList _myToDoList;

        public TableBuilder(ToDoList list)
        {
            _myToDoList = list;
        }

        public IRenderable FormATable()
        {
            var table = new Table();
            table.AddColumn(new TableColumn("[darkorange]No.[/]"));
            table.AddColumn(new TableColumn("[darkorange]Task[/]").Centered());
            table.AddColumn(new TableColumn("[darkorange]IsCompleted[/]").Centered());
            table.Border(TableBorder.Rounded);

            for (int i = 0; i < _myToDoList.Count; i++)
            {
                var state = new Markup("[red]-[/]");
                var number = i.ToString();
                var description = _myToDoList[i].Description;
                if (_myToDoList[i].IsComplete)
                    state = new Markup("[green]+[/]");
                table.AddRow(new Markup(number), new Markup(description), state);
            }

            return table;
        }
    }
}
