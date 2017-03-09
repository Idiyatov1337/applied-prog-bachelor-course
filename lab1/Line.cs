using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application1
{
    public class Line
    {
        public Point a;
        public Point b;

        public Line()
        {
            a = new Point();
            b = new Point();
        }

        public Line(Point one, Point two)
        {
            this.a = one;
            this.b = two;
        }

        public void init()
        {
            Console.WriteLine("Координаты первой точки: ");
            Console.Write("x=");
            a.x = Convert.ToDouble(Console.ReadLine());
            Console.Write("y=");
            a.y = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Координаты второй точки:");
            Console.Write("x=");
            b.x = Convert.ToDouble(Console.ReadLine());
            Console.Write("y=");
            b.y = Convert.ToDouble(Console.ReadLine());
        }

        

        public static Point Intersection(Line first, Line second)
        {
            if (!first.Equals(second))
            {

                double x, y;
                double a1, b1, c1, a2, b2, c2;

                a1 = first.a.y - first.b.y;
                b1 = first.b.x - first.a.x;
                c1 = first.a.x*first.b.y - first.b.x*first.a.y;
                a2 = second.a.y - second.b.y;
                b2 = second.b.x - second.a.x;
                c2 = second.a.x*second.b.y - second.b.x*second.a.y;

                if (a1 * b2 - a2 * b1 == 0)
                {
                    Console.WriteLine("Прямые не пересекаются");
                    return null;
                }
                else
                {
                    y = (a2*c1 - a1*c2)/(a1*b2 - a2*b1);
                    x = (-b1*y - c1)/a1;
                    return new Point(x, y);
                }
            }
            else
            {
                Console.WriteLine("Прямые совпадают");
                return null;
            }
        }
    }
}
