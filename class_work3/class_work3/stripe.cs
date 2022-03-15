using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace class_work3
{
    class stripe
    {

        private Rectangle _rect;
        Color _color;

        public stripe(int x,int y, int width,int hight) 
        {
            _rect = new Rectangle(x, y, width, hight);
            _color = Color.Aqua;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(_color), _rect);
            g.DrawRectangle(new Pen(Color.Black), _rect);
        }

        public bool Inside(int x, int y)
        {
            if (x < _rect.Left || x > _rect.Right) return false;
            if (y < _rect.Top || y > _rect.Bottom) return false;

            return true;
        }

        public bool Intersect(stripe stripe)
        {
            if (_rect.IntersectsWith(stripe._rect)) 
            {
                return true;
            }

            return false;
        }

        internal void SetColor(Color color)
        {
            _color = color;
        }
    }
}
