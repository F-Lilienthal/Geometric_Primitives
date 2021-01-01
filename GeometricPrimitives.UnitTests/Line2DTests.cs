using System;
using System.Collections.Generic;
using System.Text;
using GeometricPrimitives;
using NUnit.Framework;

namespace GeometricPrimitives.UnitTests
{
    [TestFixture]
    class Line2DTests
    {
        [TestCase(5, 0)]
        [TestCase(-5, 0)]
        [TestCase(20, 0)]
        public void HasInside_PointInside_ExpectedPositive(double pointX, double pointY)
        {
            Line2D line = new Line2D(new Point2D(0, 0), new Point2D(10, 0));
            Point2D point = new Point2D(pointX, pointY);

            Assert.That(line.HasInside(point) == Signum.Positive);
        }

        [TestCase(5, 5)]
        [TestCase(-5, 5)]
        [TestCase(20, 1)]
        public void HasInside_PointOutside_ExpectedNegative(double pointX, double pointY)
        {
            Line2D line = new Line2D(new Point2D(0, 0), new Point2D(10, 0));
            Point2D point = new Point2D(pointX, pointY);

            Assert.That(line.HasInside(point) == Signum.Negative);
        }

    }
}
