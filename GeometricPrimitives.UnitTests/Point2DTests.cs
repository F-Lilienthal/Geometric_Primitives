using System;
using System.Collections.Generic;
using System.Text;
using GeometricPrimitives;
using NUnit.Framework;

namespace GeometricPrimitives.UnitTests
{
    [TestFixture]
    class Point2DTests
    {
        [Test]
        public void Equals_ExactlyEqual_ExpectedTrue()
        {
            Point2D point1 = new Point2D(5, 5);
            Point2D point2 = new Point2D(5, 5);

            Assert.IsTrue(point1.Equals(point2));
        }

        [Test]
        public void Equals_EqualWithinEpsilon_ExpectedTrue()
        {
            Point2D point1 = new Point2D(5, 5);
            Point2D point2 = new Point2D(4.999999, 5.000001);

            Assert.IsTrue(point1.Equals(point2));
        }

        [Test]
        public void Equals_Unequal_ExpectedFalse()
        {
            Point2D point1 = new Point2D(5, 5);
            Point2D point2 = new Point2D(5, 5.001);

            Assert.IsFalse(point1.Equals(point2));
        }

        [TestCase(0, 0, 10, 0, 10)]
        [TestCase(0, 0, 10, 10, 14.142135)]
        public void Distance(double x1, double y1, double x2, double y2, double expectedDistance)
        {
            Point2D point1 = new Point2D(x1, y1);
            Point2D point2 = new Point2D(x2, y2);

            double actualDistance = point1.DistanceTo(point2);

            Assert.That(TestUtil.IsSameValue(actualDistance, expectedDistance));
        }

        [TestCase(10, 0, 90, 0, 10)]
        [TestCase(10, 0, -90, 0, -10)]
        [TestCase(10, 0, 180, -10, 0)]
        [TestCase(10, 0, -180, -10, 0)]
        [TestCase(-5, 5, 45, -7.07106781, 0)]
        public void Rotate(double xStart, double yStart, double angleInDeg, double xExpected, double yExpected)
        {
            Point2D point = new Point2D(xStart, yStart);
            point.Rotate(angleInDeg);

            Assert.That(Utilities.SameValue(point.X, xExpected) && Utilities.SameValue(point.Y, yExpected));
        }
    }
}