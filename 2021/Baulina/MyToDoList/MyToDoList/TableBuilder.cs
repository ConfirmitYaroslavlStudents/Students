using System.Collections.Generic;
using MyToDoList;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ToDoApp
{
    internal class TableBuilder
    {
        private readonly ToDoList _myToDoList;

        public TableBuilder(ToDoList list) => _myToDoList = list;

        public IRenderable FormATable()
        {
            var table = new Table();
            table.AddColumn(new TableColumn("[darkorange]No.[/]"));
            table.AddColumn(new TableColumn("[darkorange]Task[/]").Centered());
            table.AddColumn(new TableColumn("[darkorange]IsCompleted[/]").Centered());
            table.Border(TableBorder.Rounded);

            for (var i = 0; i < _myToDoList.Count; i++)
            {
                var newRow = FormTableRow(_myToDoList[i], i);
                table.AddRow(newRow);
            }

            return table;
        }

        public IEnumerable<IRenderable> FormTableRow(ToDoItem item, int number)
        {
            var state = new Markup("[red]-[/]");
            var description = item.Description;
            if (item.IsComplete)
                state = new Markup("[green]+[/]");
            return new[] {new Markup(number.ToString()), new Markup(description), state};
        }
    }
}
