using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application1;

namespace UnitTestCalc
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MethodForEquals()
        {
            Point f1 = new Point(0, 0);
            Point f2 = new Point(0, 0);

            Assert.AreEqual(f1.Equals(f2), true);
        }


        [TestMethod]
        public void MethodForIntersection()
        {
            Line line1 = new Line(new Point(0, 4), new Point(4, 0));
            Line line2 = new Line(new Point(0, 0), new Point(4, 4));

            Assert.AreEqual(Line.Intersection(line1, line2), new Point(2, 2));
        }
    }
}
