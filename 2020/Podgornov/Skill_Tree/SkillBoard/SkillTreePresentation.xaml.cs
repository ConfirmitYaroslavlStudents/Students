using SkillTree;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace SkillBoard
{
    public partial class SkillTreePresentation : Page
    {
        private Graph discipline;

        private string path;

        public SkillTreePresentation(string path)
        {
            InitializeComponent();
            if (File.Exists(path))
            {
                discipline = Graph.Load(path, Board, true);
                this.path = path;
                discipline.OpenSkillInformation = new Action(() => OpenSkillInformation());
                discipline.CloseSkillInformation = new Action(() => CloseSkillInformation());
            }
            else throw new ArgumentException("Not Correct Path.");
        }

        private void OpenSkillInformation()
        {
            Description.Visibility = Visibility.Visible;
            DescriptionLabel.Visibility = Visibility.Visible;
            Complexity.Visibility = Visibility.Visible;
            ComplexityLabel.Visibility = Visibility.Visible;
            Timer.Visibility = Visibility.Visible;
            TimerLabel.Visibility = Visibility.Visible;
            Recognize.Visibility = Visibility.Visible;
            Description.Text = discipline.FocusVertex.Skill.Description;
            Complexity.Text = discipline.FocusVertex.Skill.Complexity.ToString();
            Timer.Text = discipline.FocusVertex.Skill.TrainingTime;
        }

        private void CloseSkillInformation()
        {
            Description.Visibility = Visibility.Hidden;
            DescriptionLabel.Visibility = Visibility.Hidden;
            Complexity.Visibility = Visibility.Hidden;
            ComplexityLabel.Visibility = Visibility.Hidden;
            Timer.Visibility = Visibility.Hidden;
            TimerLabel.Visibility = Visibility.Hidden;
            Recognize.Visibility = Visibility.Hidden;
        }

        private void SaveCurrentDiscipline(object sender, RoutedEventArgs e)
        {
            discipline.Save(path);
            MessageBox.Show("Discipline Saved.");
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void RecognizeSkill(object sender, RoutedEventArgs e)
        {
            try
            {
                discipline.FocusVertex.Recognize();
            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
            
        }
    }
}
