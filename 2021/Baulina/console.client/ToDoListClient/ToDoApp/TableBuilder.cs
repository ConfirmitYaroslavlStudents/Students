using System.Collections.Generic;
using Spectre.Console;
using Spectre.Console.Rendering;
using ToDoApp.Models;

namespace ToDoApp
{
    internal class TableBuilder
    {
        private readonly IEnumerable<ToDoItem> _myToDoList;

        public TableBuilder(IEnumerable<ToDoItem> list) => _myToDoList = list;

        public IRenderable FormTable()
        {
            var table = new Table();
            table.AddColumn(new TableColumn("[darkorange]Id[/]"));
            table.AddColumn(new TableColumn("[darkorange]Task[/]").Centered());
            table.AddColumn(new TableColumn("[darkorange]Status[/]").Centered());
            table.Border(TableBorder.Rounded);

            foreach (var toDoItem in _myToDoList)
            {
                var newRow = RenderTableRow(toDoItem);
                table.AddRow(newRow);
            }

            return table;
        }

        public IEnumerable<IRenderable> RenderTableRow(ToDoItem item)
        {
            var state = new Markup("[red]-[/]");
            var description = item.Description;
            if (item.Status == ToDoItemStatus.Complete)
                state = new Markup("[green]+[/]");
            return new[] {new Markup(item.Id.ToString()), new Markup(description), state};
        }
    }
}
