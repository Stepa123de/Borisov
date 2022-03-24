using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace class_work3
{
    public partial class Form1 : Form
    {
        private stripe _mystripe;
        private Random _genran;
        private const int _StripeWidth = 100, _StripeHight = 25;
        private List<stripe> _allStrips;
        private int _interval = 2000, _basestripe = 10, _lvl = 1;
        private String FormText = "Start";
        public Form1()
        {
            InitializeComponent();

            _allStrips = new List<stripe>();
            _genran = new Random();

            MainButton("Play",75);
            
        }

        private void MainButton(string text,int fontSize)
        {
            int myWidth = 300,myHeight = 100;
            Button btn = new Button();
            btn.Size = new Size(300, 120);
            btn.Location = new Point((this.ClientSize.Width - myWidth) / 2, (this.ClientSize.Height - myHeight) / 2);
            btn.Click += MainButtonClick;
            btn.Text = text;
            btn.Font = new Font("arial", fontSize);
            btn.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(btn);
        }

        private void MainButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            this.Controls.Remove(btn);
            GameStart();
        }

        private void GameStart() 
        {
            AddStripes(_basestripe);
            timer1.Interval = _interval;
            timer1.Start();
            FormText = "Level " + _lvl;
        }

        private void AddStripes(int v)
        {
            while (v > 0)
            {
                
                int flag = _genran.Next(2),x,y;

                if (flag == 0)
                {
                    x = _genran.Next(this.ClientSize.Width - _StripeWidth);
                    y = _genran.Next(this.ClientSize.Height - _StripeHight);
                    _mystripe = new stripe(x, y, _StripeWidth, _StripeHight);
                }
                else
                {
                    x = _genran.Next(this.ClientSize.Width - _StripeHight);
                    y = _genran.Next(this.ClientSize.Height - _StripeWidth);
                    _mystripe = new stripe(x, y, _StripeHight, _StripeWidth);
                }

                int r = _genran.Next(256);
                int g = _genran.Next(256);
                int b = _genran.Next(256);


                _mystripe.SetColor(Color.FromArgb(r, g, b));

                _allStrips.Add(_mystripe);
                v--;
            }

           
           
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = _allStrips.Count - 1; i >= 0; i--)
            {
                if (_allStrips[i].Inside(e.X, e.Y))
                {
                    bool key = true;

                    for (int j = i + 1; j < _allStrips.Count; j++)
                    {
                        if (_allStrips[j].Intersect(_allStrips[i]))
                        {
                            key = false;
                            break;
                        }
                    }
                    if (key)
                    {
                        _allStrips.RemoveAt(i);
                        Invalidate();
                    }

                    break;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_allStrips.Count == 0)
            {
                if (_lvl == 1)
                {
                    AddStripes(10);
                    timer1.Interval = 1500;
                    _lvl++;
                    FormText = "Level " + _lvl;
                }
                else if (_lvl == 2)
                {
                    AddStripes(10);
                    timer1.Interval = 1100;
                    _lvl++;
                    FormText = "Level " + _lvl;
                }
                else if (_lvl == 3)
                {
                    AddStripes(10);
                    timer1.Interval = 700;
                    _lvl++;
                    FormText = "Level " + _lvl;
                }
                else
                {
                    _lvl = 1;
                    timer1.Stop();
                    MainButton("You Win!", 42);
                    FormText = "Replay";
                }
            }
            else if(_allStrips.Count == 30)
            {
                _allStrips = new List<stripe>();
                timer1.Stop();
                MainButton("You Lose!",42);
                FormText = "Replay ";
            }
            else 
            {
                AddStripes(1); 
            }

            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();

            foreach (stripe x in _allStrips) x.Draw(g);

            Text = FormText;

        }
    }
}
