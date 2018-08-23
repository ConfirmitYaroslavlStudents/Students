using Championship;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsUserInterface
{
    public partial class Form1 : Form
    {
        IChampionship manager; 

        public Form1()
        {
            InitializeComponent();
            
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            CreateChampionship();
            TaskWorker.StartNewTask(Magic);
        }

        private void CreateChampionship()
        {
            if (Single.Checked)
            {
                manager = new SingleChampionshipManager(new DataInput(new FormDataInputWorker(MessagesBox,textBox1,TeamListBox)));
            }
            else
            {
                manager = new DoubleChampionshipManager(new DataInput(new FormDataInputWorker(MessagesBox, textBox1, TeamListBox)));
            }
        }
        private void Magic()
        {
            TaskWorker.SetNewTextToControl(TeamListBox, "");
            manager.InputTeams();
            TaskWorker.SetNewTextToControl(MessagesBox, "");

        }

        private void NextRoundButton_Click(object sender, EventArgs e)
        {
            if (manager.Champion != null)
            {
                MessagesBox.Text = Messages.ShowChampion(manager.Champion);
            }
            else
            {
                TaskWorker.StartNewTask(Magic1);
            }
        }

        private void Magic1()
        {
            manager.PlayTour();           
        }

        
    }
}
