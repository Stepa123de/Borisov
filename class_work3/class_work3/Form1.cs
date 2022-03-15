using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace class_work3
{
    public partial class Form1 : Form
    {
        private stripe _mystripe;
        private Random _genran;
        private const int _StripeWidth = 100, _StripeHight = 25;
        List<stripe> All_Stripes;
        public Form1()
        {
            InitializeComponent();

            All_Stripes = new List<stripe>();
            _genran = new Random();
            int x = _genran.Next(this.ClientSize.Width - _StripeWidth);
            int y = _genran.Next(this.ClientSize.Height - _StripeHight);

            AddStripes(10);
            timer1.Start();
            //_mystripe = new stripe(x, y, _StripeWidth, _StripeHight);

            //All_Stripes.Add(_mystripe);
        }

        private void AddStripes(int v)
        {
            while (v > 0)
            {
                int x = _genran.Next(this.ClientSize.Width - _StripeWidth);
                int y = _genran.Next(this.ClientSize.Height - _StripeHight);
                _mystripe = new stripe(x, y, _StripeWidth, _StripeHight);
                int r = _genran.Next(256);
                int g = _genran.Next(256);
                int b = _genran.Next(256);

                _mystripe.SetColor(Color.FromArgb(r, g, b));

                All_Stripes.Add(_mystripe);
                v--;
            }
        }
            private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = All_Stripes.Count-1; i >= 0 ; i--) 
            {
                if (All_Stripes[i].Inside(e.X, e.Y)) 
                {
                    bool key = true;

                    for (int j = i + 1; j < All_Stripes.Count; j++) 
                    {
                        if (All_Stripes[j].Intersect(All_Stripes[i])) 
                        {
                            key = false;
                            break; 
                        }
                    }
                    if (key) 
                    {
                        All_Stripes.RemoveAt(i);
                        Invalidate();
                    }
                    
                    break;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            AddStripes(1);
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();

            foreach(stripe x in All_Stripes) x.Draw(g);
            
        }
    }
}
