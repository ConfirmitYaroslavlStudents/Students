using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

            foreach (var item in collection)
            {
                var labelForThisItem = new Label { Content = item, Tag = item,FontSize = fontSize};
                _currentCanvas.Children.Add(labelForThisItem);
                Canvas.SetLeft(labelForThisItem,10);
                Canvas.SetTop(labelForThisItem,currentCoordinate);

                var textBoxForThisItem = new TextBox
                {
                    Tag = item,
                    FontSize = fontSize,
                    Width = 300,
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Auto
                };

                _currentCanvas.Children.Add(textBoxForThisItem);
                Canvas.SetLeft(textBoxForThisItem, maxLength * widthInterval / 2 + 20);
                Canvas.SetTop(textBoxForThisItem, currentCoordinate);

                currentCoordinate += heightInterval;
            }

            var OKButton = new Button {Content = "OK", FontSize = fontSize};
            OKButton.Click += clickHandler;
            OKButton.Width = 100;
            _currentCanvas.Children.Add(OKButton);
            Canvas.SetLeft(OKButton,10);
            Canvas.SetTop(OKButton, currentCoordinate);
        }

        public  void PaintCanvasWithListBox(IEnumerable<string> collection, RoutedEventHandler clickHandler)
        {
            const int fontSize = 30;
            const int heightInterval = 40;
            const int currentCoordinate = 10;

            var itemsTypesListBox = new ListBox {Height = 200, Width = 400, FontSize = fontSize};
            _currentCanvas.Children.Add(itemsTypesListBox);
            Canvas.SetLeft(itemsTypesListBox, currentCoordinate);
            Canvas.SetTop(itemsTypesListBox, currentCoordinate);

            foreach (var item in collection)
            {
                itemsTypesListBox.Items.Add(item);
            }

            var OKButton = new Button { Content = "OK", FontSize = fontSize };
            OKButton.Click += clickHandler;
            OKButton.Width = 100;
            _currentCanvas.Children.Add(OKButton);
            Canvas.SetLeft(OKButton, 10);
            Canvas.SetTop(OKButton, currentCoordinate + 200 + heightInterval);
        }
    }
}