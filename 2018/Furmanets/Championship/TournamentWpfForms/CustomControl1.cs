using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TournamentWpfForms
{
    /// <summary>
    /// Выполните шаги 1a или 1b, а затем 2, чтобы использовать этот настраиваемый элемент управления в файле XAML.
    ///
    /// Шаг 1a. Использование настраиваемого элемента управления в файле XAML, существующем в текущем проекте.
    /// Добавьте атрибут XmlNamespace к корневому элементу файла разметки, где он 
    /// должен использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TournamentWpfForms"
    ///
    ///
    /// Шаг 1b. Использование этого настраиваемого элемента управления в файле XAML, существующем в текущем проекте.
    /// Добавьте атрибут XmlNamespace к корневому элементу файла разметки, где он 
    /// должен использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TournamentWpfForms;assembly=TournamentWpfForms"
    ///
    /// Потребуется также добавить ссылку на проект из проекта, в котором находится файл XAML
    /// в данный проект и пересобрать во избежание ошибок компиляции:
    ///
    ///     Правой кнопкой мыши щелкните проект в обозревателе решений и выберите команду
    ///     "Добавить ссылку"->"Проекты"->[Выберите этот проект]
    ///
    ///
    /// Шаг 2)
    /// Продолжайте дальше и используйте элемент управления в файле XAML.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class CustomControl1 : Control
    {
        static CustomControl1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomControl1), new FrameworkPropertyMetadata(typeof(CustomControl1)));
        }
    }
}
