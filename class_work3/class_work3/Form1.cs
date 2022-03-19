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
        List<stripe> _allStrips;
        private int _interval = 1000, _basestripe = 10;
        public Form1()
        {
            InitializeComponent();

            _allStrips = new List<stripe>();
            _genran = new Random();

            AddStripes(_basestripe);
            timer1.Interval = _interval;
            timer1.Start();
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

        private void end() 
        {
            timer1.Stop();
            TextBox text1 = new TextBox();
            text1.Text = "You Win!!!";
            text1.PointToClient(new Point(0,0));
            text1.Size = ClientSize;
            text1.AutoSize = false;
            text1.Font = new Font(text1.Font.Name, 99, text1.Font.Style, text1.Font.Unit);
            this.Controls.Add(text1);
            text1.Show();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_allStrips.Count == 0) 
            {
                AddStripes(_basestripe);
                _interval = (int)(_interval * 0.8);
                timer1.Interval = _interval;

                if (_interval < 600) 
                {
                    end();
                }
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

        }
    }
}
