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
using SkillTree;

namespace SkillBoard
{
    public partial class SkillTreeConstructor : Page
    {
        private Graph discipline;

        private string path;

        private double XLeft;

        private double YTop;

        public SkillTreeConstructor(string path)
        {
            InitializeComponent();
            if (File.Exists(path))
            {
                discipline = Graph.Load(path, Board);
            }
            else
            {
                discipline = new Graph(Board);
            }
            discipline.OpenSkillInformation = new Action(() => OpenSkillInformation());
            discipline.CloseSkillInformation = new Action(() => CloseSkillInformation());
            this.path = path;           
        }

        private void SetXYPosition(object sender, ContextMenuEventArgs e)
        {
            var point = Mouse.GetPosition(Board);
            XLeft = point.X;
            YTop = point.Y;
        }

        private int GetIDOfEllipse(Ellipse ellipse) => int.Parse((string)(ellipse.Tag));

        private void AddSkill(object sender, RoutedEventArgs e)
        {
            discipline.AddVertex(new Skill(), XLeft, YTop);
        }

        private void DeleteSkill(object sender, RoutedEventArgs e)
        {
            discipline.RemoveVertex(discipline.FocusVertex.Id);
        }

        private void CreatуDependencу(object sender, RoutedEventArgs e)
        {
            discipline.IsAddDependenceModActive = true;
        }

        private void OpenSkillInformation()
        {
            Description.Visibility = Visibility.Visible;
            DescriptionLabel.Visibility = Visibility.Visible;
            Complexity.Visibility = Visibility.Visible;
            ComplexityLabel.Visibility = Visibility.Visible;
            Timer.Visibility = Visibility.Visible;
            TimerLabel.Visibility = Visibility.Visible;
            Save.Visibility = Visibility.Visible;
            Description.Text = discipline.FocusVertex.Skill.Description;
            Complexity.SelectedIndex = (int)(discipline.FocusVertex.Skill.Complexity);
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
            Save.Visibility = Visibility.Hidden;
        }

        private void SaveSkill(object sender, RoutedEventArgs e)
        {
            discipline.FocusVertex.Skill.Description = Description.Text;
            discipline.FocusVertex.Skill.Complexity = (Skill_Complexity)Complexity.SelectedIndex;
            discipline.FocusVertex.Skill.TrainingTime = Timer.Text;
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

    }
}
