using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hospital
{
    internal class CanvasPainter
    {
        private readonly Canvas _currentCanvas;

        internal CanvasPainter(Canvas currentCanvas)
        {
            _currentCanvas = currentCanvas;
        }

        public void PaintCanvasWithTextBoxes(IEnumerable<string> collection, RoutedEventHandler clickHandler)
        {
            const int fontSize = 20;
            const int heightInterval = 40;
            const int widthInterval = 20;
            int currentCoordinate = 10;

            int maxLength = collection.Max(item => item.Length);

            foreach (string item in collection)
            {
                var labelForThisItem = new Label
                {
                    Content = item,
                    Tag = item,
                    FontSize = fontSize,
                    Foreground = Brushes.White
                };
                _currentCanvas.Children.Add(labelForThisItem);
                Canvas.SetLeft(labelForThisItem, 10);
                Canvas.SetTop(labelForThisItem, currentCoordinate);

                var textBoxForThisItem = new TextBox
                {
                    Tag = item,
                    FontSize = fontSize,
                    Width = 300,
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Auto
                };

                _currentCanvas.Children.Add(textBoxForThisItem);
                Canvas.SetLeft(textBoxForThisItem, maxLength*widthInterval/2 + 20);
                Canvas.SetTop(textBoxForThisItem, currentCoordinate);

                currentCoordinate += heightInterval;
            }

            var okButton = new Button
            {
                Content = "OK",
                FontSize = fontSize,
                Foreground = Brushes.White,
                Background = Brushes.Transparent
            };
            okButton.Click += clickHandler;
            okButton.Width = 100;

            _currentCanvas.Children.Add(okButton);
            Canvas.SetLeft(okButton, 10);

            currentCoordinate += heightInterval;
            Canvas.SetTop(okButton, currentCoordinate);
        }

        public void PaintCanvasWithListBox(IEnumerable<string> collection, RoutedEventHandler clickHandler)
        {
            const int fontSize = 30;
            const int heightInterval = 40;
            const int currentCoordinate = 10;

            var itemsTypesListBox = new ListBox
            {
                Height = 200,
                Width = 400,
                FontSize = fontSize,
                Foreground = Brushes.White,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.White,
                BorderThickness = new Thickness(2)
            };
            _currentCanvas.Children.Add(itemsTypesListBox);
            Canvas.SetLeft(itemsTypesListBox, currentCoordinate);
            Canvas.SetTop(itemsTypesListBox, currentCoordinate);

            foreach (string item in collection)
            {
                itemsTypesListBox.Items.Add(item);
            }

            var okButton = new Button
            {
                Content = "OK",
                FontSize = fontSize,
                Foreground = Brushes.White,
                Background = Brushes.Transparent
            };
            okButton.Click += clickHandler;
            okButton.Width = 100;

            _currentCanvas.Children.Add(okButton);
            Canvas.SetLeft(okButton, 10);
            Canvas.SetTop(okButton, currentCoordinate + 200 + heightInterval);
        }
    }
}