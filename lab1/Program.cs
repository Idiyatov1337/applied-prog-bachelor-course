using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application1
{
    class Program
    {
        static void Main(string[] args)
        {
            Line first = new Line();
            Line second = new Line();

            Console.WriteLine("Введите координаты для линии 1:");
            first.init();
            Console.WriteLine("Введите координаты для линии 2:");
            second.init();
            Console.WriteLine("Точка пересечения линий 1 и 2:");
            Point result = Line.Intersection(first, second);

            if (result != null)
            {
                Console.WriteLine(result);
            }
            Console.ReadKey();
        }
    }
}
