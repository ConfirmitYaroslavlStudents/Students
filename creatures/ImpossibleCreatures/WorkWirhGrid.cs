using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using CellsAutomate.Constants;

namespace ImpossibleCreatures
{
    public static class WorkWirhGrid
    {
        private static readonly SolidColorBrush StrokeColor = new SolidColorBrush(Colors.Transparent);

        public static void MarkTable(Grid grid, int column, int row)
        {
            for (int i = 0; i < column; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < row; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
        }

        public static void InitalizeGrid(Grid grid, Rectangle[,] rectanglesDouble, int column, int row)
        {
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    var rectengle = new Rectangle { Stroke = StrokeColor, StrokeThickness = LogConstants.StrokeThickness };

                    Grid.SetColumn(rectengle, i);
                    Grid.SetRow(rectengle, j);
                    grid.Children.Add(rectengle);
                    grid.UpdateLayout();
                    rectanglesDouble[i, j] = rectengle;
                }
            }
        }

        /// <summary>
        ///     Разлинеивает таблицу
        /// </summary>
        /// <param name="grid">Таблица</param>
        /// <param name="column">Количество колонок</param>
        /// <param name="row">Количество строчек</param>
        public static void InitalizeGrid(Grid grid, int column, int row)
        {
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    var rectengle = new Rectangle { Stroke = StrokeColor, StrokeThickness = 0 };

                    Grid.SetColumn(rectengle, i);
                    Grid.SetRow(rectengle, j);
                    grid.Children.Add(rectengle);
                    grid.UpdateLayout();
                }
            }
        }
    }
}