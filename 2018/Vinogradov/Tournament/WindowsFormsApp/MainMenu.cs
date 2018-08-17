using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TournamentLibrary;

namespace WindowsFormsApp
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            var creator = new CreationBox();
            Visible = false;
            creator.ShowDialog();

            if (creator.CreatedTournament == null)
            {
                Close();
            }
            else
            {
                CreateGame(creator.CreatedTournament);
            }
        }

        private void buttonLoadGame_Click(object sender, EventArgs e)
        {
            Tournament tournament = SaveController.Load();

            if (tournament == null)
            {
                buttonNewGame_Click(sender, e);
            }
            else
            {
                CreateGame(tournament);
            }
        }

        private void CreateGame(Tournament tournament)
        {
            var game = new PlayForm(tournament);
            game.ShowDialog();
            Close();
        }
    }
}
