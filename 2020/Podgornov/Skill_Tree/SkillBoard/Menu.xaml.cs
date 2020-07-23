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
using System.IO;

namespace SkillBoard
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        private readonly string disciplines = @"./Disciplines";

        private readonly string myCourses = @"./MyCourses";

        private bool IsDisciplinesOnFocus;

        public Menu()
        {
            InitializeComponent();
            if (!Directory.Exists(disciplines))
                Directory.CreateDirectory(disciplines);
            if (!Directory.Exists(myCourses))
                Directory.CreateDirectory(myCourses);
            ChangeToMyCurses(this, new EventArgs());           
        }

        private IEnumerable<DockPanel> CreateNewListBox(string[] buttons,string path , RoutedEventHandler [] events)
        {
            var docks = new List<DockPanel>();
            foreach (var i in Directory.GetFiles(path))
            {
                var name = i.Split(new char[] { '/', '\\', '.'}, StringSplitOptions.RemoveEmptyEntries)[1];
                var dock = new DockPanel();
                for (int j = 0; j < buttons.Length; j++) 
                {
                    var button = new Button { Content = buttons[j], Name = name };
                    button.Click += events[j];
                    dock.Children.Add(button);                    
                }
                dock.Children.Add(new Label() { Content = name });
                docks.Add(dock);
            }
            return docks;
        }

        private void ChangeToDiscipline(object sender, EventArgs e)
        {
            IsDisciplinesOnFocus = true;
            listbox.ItemsSource = CreateNewListBox(
                new string[] { "Download","Edit","Delete"}, 
                disciplines,
                new RoutedEventHandler[]
                {
                    Download,
                    Edit,
                    Delete
                }
                );
        }

        private void ChangeToMyCurses(object sender, EventArgs e)
        {
            IsDisciplinesOnFocus = false;
            listbox.ItemsSource = CreateNewListBox(
                new string[] { "Learn", "Delete" }, 
                myCourses,
                new RoutedEventHandler[]
                {
                    Learn,
                    Delete
                }
                );
        }

        private string GetPath(string name, string directory) => $"{directory}/{name}.xml";

        private void Download(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string source = GetPath(button.Name, disciplines);
            File.Copy(source, GetPath(button.Name, myCourses), true);
            MessageBox.Show("File was downloaded.");
        }

        private void Delete(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var directory = IsDisciplinesOnFocus ? disciplines : myCourses;
            File.Delete(GetPath(button.Name, directory));
            if (IsDisciplinesOnFocus)
                ChangeToDiscipline(this, new EventArgs());
            else
                ChangeToMyCurses(this, new EventArgs());
            MessageBox.Show("File was deleted.");
        }

        private void Edit(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var path = GetPath(button.Name, disciplines);
            GoToSkillConstructor(path);
        }

        private void Learn(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var path = GetPath(button.Name, myCourses);
            try
            {
                NavigationService.Content = new SkillTreePresentation(path);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

        private void TextboxKeyDown(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsLetterOrDigit(e.Text, 0));        
        }
        
        private void AddNewDiscipline(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textbox.Text))
            {
                MessageBox.Show("Invalid name.");
                return;
            }
            var name = textbox.Text.Replace(" ", "__");
            string path = GetPath(name,disciplines);
            if(File.Exists(path))
            {
                MessageBox.Show("File exists.");
            }
            else
            {
                GoToSkillConstructor(path);
            }
        }

        private void GoToSkillConstructor(string path)
        {
            try
            {
                NavigationService.Content = new SkillTreeConstructor(path);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

    }
}
