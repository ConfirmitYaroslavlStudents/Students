using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TournamentLibrary;

namespace WpfTournament
{
    /// <summary>
    /// Логика взаимодействия для CreationWindow.xaml
    /// </summary>
    public partial class CreationWindow : Window
    {
        private List<string> _names = new List<string>();
        private int _numberOfPlayers;
        public Tournament CreatedTournament;

        public CreationWindow()
        {
            InitializeComponent();
        }

        private void TextBox_NumberOfPlayers_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            int parsed;

            if (!int.TryParse(TextBox_NumberOfPlayers.Text, out parsed) || parsed < 2)
            {
                if (_numberOfPlayers == 0)
                {
                    TextBox_NumberOfPlayers.Clear();
                }
                else
                {
                    TextBox_NumberOfPlayers.Text = _numberOfPlayers.ToString();
                }
            }
            else
            {
                _numberOfPlayers = parsed;

                if (_numberOfPlayers < _names.Count)
                {
                    _names.RemoveRange(_numberOfPlayers, _names.Count - _numberOfPlayers);
                    PrintNames();
                }
            }
        }

        private void TextBox_NameInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string newName = TextBox_NameInput.Text;

                if (NameValidator.Validate(newName, _names))
                {
                    _names.Add(newName);

                    if (_names.Count > _numberOfPlayers)
                    {
                        _numberOfPlayers = _names.Count;
                        TextBox_NumberOfPlayers.Text = _numberOfPlayers.ToString();
                    }

                    TextBox_NameInput.Clear();
                    PrintNames();
                }
            }
        }

        private void PrintNames()
        {
            var builder = new StringBuilder();

            foreach (var name in _names)
            {
                builder.AppendLine(name);
            }

            TextBox_Names.Text = builder.ToString();
        }

        private void Button_ClearAll_Click(object sender, RoutedEventArgs e)
        {
            CheckBox_DoubleElimination.IsChecked = false;
            _names = new List<string>();
            _numberOfPlayers = 0;
            TextBox_NumberOfPlayers.Clear();
            PrintNames();
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            if (_names.Count >= 2)
            {
                CreatedTournament = new Tournament(_names.ToArray(), CheckBox_DoubleElimination.IsChecked.Value);
                SaveController.Save(CreatedTournament);
                Close();
            }
        }
    }
}
