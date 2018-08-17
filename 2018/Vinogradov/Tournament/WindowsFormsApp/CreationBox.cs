using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TournamentLibrary;

namespace WindowsFormsApp
{
    public partial class CreationBox : Form
    {
        public const int MinChars = 2;
        public const int MaxChars = 8;

        private List<string> _names;
        public Tournament CreatedTournament;

        public CreationBox()
        {
            InitializeComponent();
            _names = new List<string>();
        }

        protected bool NameValidation(string newName, List<string> names)
        {
            if (newName.Length < MinChars || newName.Length > MaxChars)
            {
                return false;
            }

            foreach (char symbol in newName)
            {
                if (!char.IsLetterOrDigit(symbol))
                {
                    return false;
                }
            }

            foreach (var name in names)
            {
                if (name == newName)
                {
                    return false;
                }
            }

            return true;
        }

        private void textBoxInputName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                var newName = textBoxInputName.Text;

                if (_names.Count < numericNumberOfPlayers.Value)
                {
                    if (NameValidation(newName, _names))
                    {
                        _names.Add(newName);
                        PrintNames();
                    }
                }

                textBoxInputName.Clear();
            }
        }

        private void PrintNames()
        {
            var builder = new StringBuilder();

            foreach (var name in _names)
            {
                builder.AppendLine(name);
            }

            textBoxNames.Text = builder.ToString();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (_names.Count == numericNumberOfPlayers.Value)
            {
                CreatedTournament = new Tournament(_names.ToArray(), checkBoxDoubleElimination.Checked);
                Close();
            }
            else
            {
                MessageBox.Show("Number of names and chosen number of players must be the same.");
            }
        }

        private void numericNumberOfPlayers_ValueChanged(object sender, EventArgs e)
        {
            if (numericNumberOfPlayers.Value < _names.Count)
            {
                _names.RemoveAt(_names.Count - 1);
                PrintNames();
            }
        }

        private void buttonClearNames_Click(object sender, EventArgs e)
        {
            _names = new List<string>();
            PrintNames();
        }
    }
}
