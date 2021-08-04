using System.Collections.Generic;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ToDoApp
{
    internal class TableBuilder
    {
        private readonly IEnumerable<ToDoItem> _myToDoList;

        public TableBuilder(IEnumerable<ToDoItem> list) => _myToDoList = list;

        public IRenderable FormATable()
        {
            var table = new Table();
            table.AddColumn(new TableColumn("[darkorange]Id[/]"));
            table.AddColumn(new TableColumn("[darkorange]Task[/]").Centered());
            table.AddColumn(new TableColumn("[darkorange]IsCompleted[/]").Centered());
            table.Border(TableBorder.Rounded);

            foreach (var toDoItem in _myToDoList)
            {
                var newRow = FormTableRow(toDoItem);
                table.AddRow(newRow);
            }

            return table;
        }

        public IEnumerable<IRenderable> FormTableRow(ToDoItem item)
        {
            var state = new Markup("[red]-[/]");
            var description = item.Description;
            if (item.IsComplete)
                state = new Markup("[green]+[/]");
            return new[] {new Markup(item.Id.ToString()), new Markup(description), state};
        }
    }
}
