using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class Form1 : Form
    {
        Button[] PButtons;
        int _Size = 25;
        Point StartPos;
        int Width = 10;
        int Height = 10;
        int MinesC = 10;
        Element[,] Pole;
        bool GameOver = false;
        public Form1()
        {
            InitializeComponent();
            NewGame();
        }

        void ClearForm()
        {
            if (PButtons != null)
                for (int i = 0; i < PButtons.Length; i++)
                    this.Controls.Remove(PButtons[i]);
        }

        private void NewGame()
        {
            ClearForm();
            GameOver = false;
            StartPos = new Point(20, 50);
            Point Loc = new Point(StartPos.X, StartPos.Y);
            PButtons = new Button[Width * Height];
            Pole = new Element[Height, Width];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Pole[i, j] = new Element();
                    PButtons[i * Width + j] = new Button();
                    PButtons[i * Width + j].Text = "";
                    PButtons[i * Width + j].Location = new Point(Loc.X, Loc.Y);
                    PButtons[i * Width + j].Size = new Size(_Size, _Size);
                    PButtons[i * Width + j].BackColor = Color.FromArgb(255, 255, 255);
                    PButtons[i * Width + j].MouseDown += new MouseEventHandler(btnMouseClick);
                    Loc.X += _Size;
                    this.Controls.Add(PButtons[i * Width + j]);
                }
                Loc.Y += _Size;
                Loc.X = StartPos.X;
            }
            GeneratePole();
        }
        //ставим мины
        void GeneratePole()
        {
            Random rnd = new Random();
            for (int i = 0; i < MinesC; i++)
            {
                while (true)
                {
                    int I = rnd.Next(Height);
                    int J = rnd.Next(Width);
                    if (!Pole[I, J].mine)
                    {
                        Pole[I, J].mine = true;
                        break;
                    }
                }
            }

            GenerateValue();
        }
        //ставим цифирьки
        void GenerateValue()
        {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                {
                    if (i > 0)
                    {
                        if (j > 0)
                            if (Pole[i - 1, j - 1].mine)
                                Pole[i, j].val++;
                        if (Pole[i - 1, j].mine)
                            Pole[i, j].val++;
                        if (j < Width - 1)
                            if (Pole[i - 1, j + 1].mine)
                                Pole[i, j].val++;
                    }

                    if (j > 0)
                        if (Pole[i, j - 1].mine)
                            Pole[i, j].val++;
                    if (j < Width - 1)
                        if (Pole[i, j + 1].mine)
                            Pole[i, j].val++;

                    if (i < Height - 1)
                    {
                        if (j > 0)
                            if (Pole[i + 1, j - 1].mine)
                                Pole[i, j].val++;
                        if (Pole[i + 1, j].mine)
                            Pole[i, j].val++;
                        if (j < Width - 1)
                            if (Pole[i + 1, j + 1].mine)
                                Pole[i, j].val++;
                    }
                }
        }

        private void btnMouseClick(object sender, MouseEventArgs e)
        {
            ((Button)sender).Text = "";
            if (e.Button == MouseButtons.Right)
            {
                int I = PButtons.ToList().IndexOf((Button)sender) / Width;
                int J = PButtons.ToList().IndexOf((Button)sender) % Width;
                Pole[I, J].marked = !Pole[I, J].marked;
            }
            else
            {
                int I = PButtons.ToList().IndexOf((Button)sender) / Width;
                int J = PButtons.ToList().IndexOf((Button)sender) % Width;
                if (Proverka(I, J))
                {
                    for (int i = 0; i < Height; i++)
                        for (int j = 0; j < Width; j++)
                        {
                            Pole[i, j].open = true;
                            PButtons[i * Width + j].Enabled = false;
                        }

                    GameOver = true;
                    MessageBox.Show("Извините, но Вы проиграли.");
                }
            }
            if (!GameOver)
                YouWin();
            this.Invalidate();
        }

        bool Proverka(int i, int j)
        {
            if (!Pole[i, j].open && !Pole[i, j].marked)
            {
                if (!Pole[i, j].mine)
                {
                    Pole[i, j].open = true;
                    PButtons[i * Height + j].Enabled = false;
                    if (Pole[i, j].val == 0)
                    {
                        if (i > 0)
                        {
                            if (j > 0)
                                Proverka(i - 1, j - 1);
                            Proverka(i - 1, j);
                            if (j < Width - 1)
                                Proverka(i - 1, j + 1);
                        }

                        if (j > 0)
                            Proverka(i, j - 1);
                        if (j < Width - 1)
                            Proverka(i, j + 1);

                        if (i < Height - 1)
                        {
                            if (j > 0)
                                Proverka(i + 1, j - 1);
                            Proverka(i + 1, j);
                            if (j < Width - 1)
                                Proverka(i + 1, j + 1);
                        }
                    }
                    PButtons[i * Height + j].BackColor = Color.Silver;
                }
                else
                    return true;
            }

            return false;
        }


        void YouWin()
        {
            int OpnCount = 0;
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    if (Pole[i, j].open)
                        OpnCount++;

            if (OpnCount == (Width * Height - MinesC))
            {
                MessageBox.Show("Поздравляю, Вы выиграли!");
                GameOver = true;
                for (int i = 0; i < PButtons.Length; i++)
                    PButtons[i].Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                {
                    if (Pole[i, j].open && (Pole[i, j].val > 0) && !Pole[i, j].mine)
                        PButtons[i * Width + j].Text = Pole[i, j].val.ToString();
                    else
                        if (!Pole[i, j].open && Pole[i, j].marked)
                            PButtons[i * Width + j].Text = "P";
                    if (GameOver)
                    {
                        if (Pole[i, j].mine && !Pole[i, j].marked)

                            PButtons[i * Width + j].Text = "X";
                    }
                }
        }

    }

    class Element
    {
        public bool mine, marked, open;
        public int val;
    }
}
