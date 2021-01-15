using System;
using System.Collections.Generic;
using System.Text;
using GeometricPrimitives;
using NUnit.Framework;

namespace GeometricPrimitives.UnitTests
{
    [TestFixture]
    class Circle2DTests
    {
        [TestCase(1, 0, 0, 3.14159265)]
        [TestCase(1, 5, -8, 3.14159265)]
        [TestCase(2.34, 0, 0, 17.2021047)]
        public void Area(double radius, double centerX, double centerY, double expectedArea)
        {
            Circle2D circle = new Circle2D(new Point2D(centerX, centerY), radius);

            Assert.That(TestUtil.IsSameValue(circle.Area, expectedArea));
        }
    }
}
