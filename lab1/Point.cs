using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application1
{
    public class Point
    {
        public double x { get; set; }
        public double y { get; set; }

        public Point() { }

        public Point(double x1, double y1)
        {
            x = x1;
            y = y1;
        }

        public override string ToString()
        {
            return String.Format("x={0:f2}\ny={1:f2}", x, y);
        }

        public override bool Equals(object obj)
        {
            Point p = obj as Point;

            if (x == p.x && y == p.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
