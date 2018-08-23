using System;
using System.Windows;
using TournamentLibrary;

namespace WpfTournament
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Tournament _tournament;
        private RichTextBoxDrawer _drawer;
        public bool ShowMainMenu = false;

        public GameWindow(Tournament tournament)
        {
            InitializeComponent();
            DrawBox.Document.PageWidth = 1000;
            _tournament = tournament;
            _drawer = new RichTextBoxDrawer(DrawBox);
            ListBox_Names.ItemsSource = _tournament.Players.Keys;
            _drawer.DrawTable(_tournament);
        }
        
        private void Button_PlayGame_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_Names.SelectedItem != null)
            {
                string winner = (string)ListBox_Names.SelectedItem;
                try
                {
                    _tournament.PlayGame(winner);
                    DrawBox.Document.Blocks.Clear();
                    _drawer.DrawTable(_tournament);
                    SaveController.Save(_tournament);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Pick a player.");
            }
        }

        private void Button_Quit_Click(object sender, RoutedEventArgs e)
        {
            SaveController.Save(_tournament);
            ShowMainMenu = true;
            Close();
        }
    }
}
