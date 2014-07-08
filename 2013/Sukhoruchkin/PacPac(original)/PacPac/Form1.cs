using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PacPac
{

    public partial class Level_One : Form
    {
        private Timer Evil = new Timer();
        private Point EvilCoord=new Point(250,200);
        private int step = 0;
        private int score = 0;
        private int Winscore;
        private bool Eat=true;
        private int storona = 3;
        Point coord = new Point(50, 50);
        EventHandler tickHandler;
        List<Point> Eda = new List<Point>();
       #region stenki
        private Rectangle LT1= new Rectangle(100, 100, 50,100);
        private Rectangle LT2 = new Rectangle(100, 100, 150, 50);

        private Rectangle LB1 = new Rectangle(100, 250, 50, 150);
        private Rectangle LB2 = new Rectangle(100, 350, 150, 50);


        private Rectangle RT1 = new Rectangle(300, 100, 150,50);
        private Rectangle RT2 = new Rectangle(400, 100, 50, 100);

        private Rectangle RB1 = new Rectangle(400, 250, 50, 100);
        private Rectangle RB2 = new Rectangle(300, 350, 150, 50);

        private Rectangle M1 = new Rectangle(200, 200, 50, 100);
        private Rectangle M2 = new Rectangle(200, 250, 150, 50);
        private Rectangle M3 = new Rectangle(300, 200, 50, 100);


        

       #endregion
        public Level_One()
        {
            #region eda

            Point temp = new Point(50, 0);


            for (int j = 0; j < 9; j++)
            {
                for (int k = 0; k < 8; k++)
                {
                    temp.Y = temp.Y + 50;
                    if (!((LT1.Contains(temp)) || (LT2.Contains(temp)) || (RT1.Contains(temp))
                   || (RT2.Contains(temp)) || (LB1.Contains(temp)) || (LB2.Contains(temp))
                   || (RB1.Contains(temp)) || (RB2.Contains(temp))
                   || (M1.Contains(temp)) || (M2.Contains(temp)) || (M3.Contains(temp))))
                    {
                        Eda.Add(temp);
                    }

                }
                temp.Y = 0;
                temp.X = temp.X + 50;
            }
            Winscore = Eda.Count;
            #endregion
            InitializeComponent();
            CenterToScreen();
            Evil.Enabled = true;
            Evil.Interval = 200;
            tickHandler = new EventHandler(Evil_Steps);
            Evil.Tick +=tickHandler ;

        } 
        private void EvilStop()
        {
            Evil.Tick -= tickHandler;
        }
        void Evil_Steps(object sender, EventArgs e)
        {
            switch (step)
            {
                case 0:
                        EvilCoord.Y = EvilCoord.Y - 50;
                       // System.Threading.Thread.Sleep(500);
                     
                        step++;
                        break;
                    
                case 1:
                         EvilCoord.X = EvilCoord.X - 50;
                        // System.Threading.Thread.Sleep(500);
                        
                         step++;
                         break;


                case 2:
                         EvilCoord.X = EvilCoord.X - 50;
                        // System.Threading.Thread.Sleep(500);
                        
                         step++;
                         break;
                case 3:
                        EvilCoord.Y = EvilCoord.Y + 50;
                        
                       // System.Threading.Thread.Sleep(500);
                        step++;
                        break;
                case 4:
                     EvilCoord.Y = EvilCoord.Y + 50;
                        
                       // System.Threading.Thread.Sleep(500);
                        step++;
                        break;
                case 5:
                     EvilCoord.Y = EvilCoord.Y + 50;
                      
                       // System.Threading.Thread.Sleep(500);
                        step++;
                        break;
                case 6:
                        EvilCoord.X = EvilCoord.X+ 50;
                       // System.Threading.Thread.Sleep(500);
                    
                        step++;
                        break;
                case 7: EvilCoord.X = EvilCoord.X + 50;
                       // System.Threading.Thread.Sleep(500);
                       
                        step++;
                        break;
                case 8: EvilCoord.X = EvilCoord.X + 50;
                        //System.Threading.Thread.Sleep(500);
                      
                        step++;
                        break;
                case 9: EvilCoord.X = EvilCoord.X + 50;
                        //System.Threading.Thread.Sleep(500);
                   
                        step++;
                        break;
                case 10:
                        EvilCoord.Y = EvilCoord.Y - 50;
                        //System.Threading.Thread.Sleep(500);
                        
                        step++;
                        break;
                case 11:
                        EvilCoord.Y = EvilCoord.Y - 50;
                        //System.Threading.Thread.Sleep(500);
                       
                        step++;
                        break;
                case 12:
                        EvilCoord.Y = EvilCoord.Y - 50;
                        //System.Threading.Thread.Sleep(500);
                   
                        step++;
                        break;
                case 13:
                        EvilCoord.X = EvilCoord.X - 50;
                        //System.Threading.Thread.Sleep(500);
                       
                        step++;
                        break;
                case 14:
                        EvilCoord.X = EvilCoord.X - 50;
                       // System.Threading.Thread.Sleep(500);
                     
                        step++;
                        break;
                case 15: EvilCoord.Y = EvilCoord.Y + 50;
                       
                        //System.Threading.Thread.Sleep(500);
                        step=0;
                        break;
                default:
                break;
            }
            if (coord == EvilCoord)
            {
                EvilStop();
                MessageBox.Show("Потрачено");
                this.Close();
                Evil.Tick += tickHandler;
              
            }
            Invalidate();
        }
        private void Level_One_KeyDown(object sender, KeyEventArgs e)
        {
            bool stena = true;

            if (e.KeyCode==Keys.A)
            {
                storona = 4;
                Point temp=new Point(coord.X - 50,coord.Y);

                if(( LT1.Contains(temp))||(LT2.Contains(temp))||(RT1.Contains(temp))
                    ||(RT2.Contains(temp))||(LB1.Contains(temp))||(LB2.Contains(temp))
                    ||(RB1.Contains(temp))||(RB2.Contains(temp))
                    || (M1.Contains(temp)) || (M2.Contains(temp)) || (M3.Contains(temp)))
                {
                    stena = false;
                }

                if ((coord.X - 50 >= 50) && (stena==true))
                {
                    coord.X = coord.X - 50;
                }

                
            }
            if (e.KeyCode == Keys.D)
            {
                storona = 3;
                Point temp = new Point(coord.X + 50, coord.Y);

                if ((LT1.Contains(temp)) || (LT2.Contains(temp)) || (RT1.Contains(temp))
                    || (RT2.Contains(temp)) || (LB1.Contains(temp)) || (LB2.Contains(temp))
                    || (RB1.Contains(temp)) || (RB2.Contains(temp))
                     || (M1.Contains(temp)) || (M2.Contains(temp)) || (M3.Contains(temp)))
                {
                    stena = false;
                }


                if ((coord.X + 50 <= 450)&&(stena==true))
                {
                    coord.X = coord.X + 50;


                }
            }
            if (e.KeyCode == Keys.W)
            {
                storona = 1;
                Point temp = new Point(coord.X, coord.Y-50);

                if ((LT1.Contains(temp)) || (LT2.Contains(temp)) || (RT1.Contains(temp))
                    || (RT2.Contains(temp)) || (LB1.Contains(temp)) || (LB2.Contains(temp))
                    || (RB1.Contains(temp)) || (RB2.Contains(temp))
                     || (M1.Contains(temp)) || (M2.Contains(temp)) || (M3.Contains(temp)))
                {
                    stena = false;
                }
                if ((coord.Y - 50 >= 50)&&(stena==true))
                {
                    coord.Y = coord.Y - 50;
                 
                }
               
            }
            if (e.KeyCode == Keys.S)
            {
                storona = 2;
                Point temp = new Point(coord.X, coord.Y + 50);

                if ((LT1.Contains(temp)) || (LT2.Contains(temp)) || (RT1.Contains(temp))
                    || (RT2.Contains(temp)) || (LB1.Contains(temp)) || (LB2.Contains(temp))
                    || (RB1.Contains(temp)) || (RB2.Contains(temp))
                     || (M1.Contains(temp)) || (M2.Contains(temp)) || (M3.Contains(temp)))
                {
                    stena = false;
                }
                if ((coord.Y + 50 <= 400)&&(stena==true))
                {
                    coord.Y = coord.Y + 50;
                }
          
            }
            if (coord == EvilCoord)
            {
                EvilStop();
                MessageBox.Show("Потрачено");
                this.Close();
               // Evil.Tick += tickHandler;
                
            }
            for (int i = 0; i < Eda.Count;i++ )
            {

                if (Eda[i] == coord)
                {
                    score++;
                    Eda.Remove(Eda[i]);

                }
            }
            if (score == Winscore)
            {
                Eat = false;
                EvilStop();
                Invalidate();
                MessageBox.Show("Победа");
                this.Close();
                // Evil.Tick += tickHandler;
            }
            Invalidate();
            this.Refresh();
         //  System.Threading.Thread.Sleep(200);

        }
        private void Level_One_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            #region stenki
            g.FillRectangle(Brushes.Aquamarine, LT1);
            g.FillRectangle(Brushes.Aquamarine, LT2);
            g.FillRectangle(Brushes.Aquamarine, LB1);
            g.FillRectangle(Brushes.Aquamarine, LB2);
            g.FillRectangle(Brushes.Aquamarine, RT1);
            g.FillRectangle(Brushes.Aquamarine, RT2);
            g.FillRectangle(Brushes.Aquamarine, RB1);
            g.FillRectangle(Brushes.Aquamarine, RB2);
            g.FillRectangle(Brushes.Aquamarine, M1);
            g.FillRectangle(Brushes.Aquamarine, M2);
            g.FillRectangle(Brushes.Aquamarine, M3);
            #endregion
            #region eda
            foreach (Point p in Eda)
            {
                g.FillEllipse(Brushes.White, p.X+25, p.Y+25, 10, 10);

            }
            #endregion
            if (Eat)
            {
                g.FillEllipse(Brushes.Yellow, coord.X, coord.Y, 50, 50);
                Eat = false;


                  switch (storona)
                  {
                      case 1:
                          g.FillEllipse(Brushes.Black, coord.X+35, coord.Y+15, 10, 10);
                          break;
                      case 2: 
                          g.FillEllipse(Brushes.Black, coord.X + 35, coord.Y + 25, 10, 10);
                          break;
                      case 3: 
                          g.FillEllipse(Brushes.Black, coord.X + 25, coord.Y + 5, 10, 10);
                          break;
                      case 4:
                          g.FillEllipse(Brushes.Black, coord.X + 15, coord.Y + 5, 10, 10);
                          break;
                      default:
                          break;
                  }
            }

            else
            {
                  g.FillEllipse(Brushes.Yellow, coord.X, coord.Y, 50, 50);
                  switch (storona)
                  {
                      case 1: g.FillPie(Brushes.Black, coord.X, coord.Y, 50, 50, -130, 80);
                          g.FillEllipse(Brushes.Black, coord.X+35, coord.Y+15, 10, 10);
                          break;
                      case 2: g.FillPie(Brushes.Black, coord.X, coord.Y, 50, 50, 50, 80);
                          g.FillEllipse(Brushes.Black, coord.X + 35, coord.Y + 25, 10, 10);
                          break;
                      case 3: g.FillPie(Brushes.Black, coord.X, coord.Y, 50, 50, -40, 80);
                          g.FillEllipse(Brushes.Black, coord.X + 25, coord.Y + 5, 10, 10);
                          break;
                      case 4: g.FillPie(Brushes.Black, coord.X, coord.Y, 50, 50, 140, 80);
                          g.FillEllipse(Brushes.Black, coord.X + 15, coord.Y + 5, 10, 10);
                          break;
                      default:
                          break;
                  }
              
               

                 Eat = true;
            }
            g.FillEllipse(Brushes.Indigo, EvilCoord.X, EvilCoord.Y, 50, 50);
        }

        

       
    }
}
