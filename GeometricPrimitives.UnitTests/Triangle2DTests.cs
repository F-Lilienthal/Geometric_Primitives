using System;
using System.Collections.Generic;
using System.Text;
using GeometricPrimitives;
using NUnit.Framework;

namespace GeometricPrimitives.UnitTests
{
    [TestFixture]
    class Triangle2DTests
    {
        [Test]
        public void Orientation_Positive_sgnPositive()
        {
            Triangle2D triangle = new Triangle2D(new Point2D(0, 0),
                                                 new Point2D(1, 0),
                                                 new Point2D(0, 1));

            Assert.That(triangle.Orientation == Signum.Positive);
        }

        [Test]
        public void Orientation_Negative_sgnNegative()
        {
            Triangle2D triangle = new Triangle2D(new Point2D(0, 0),
                                                 new Point2D(0, 1),
                                                 new Point2D(1, 0));

            Assert.That(triangle.Orientation == Signum.Negative);
        }

        [Test]
        public void Orientation_Degenerate_sgnZero()
        {
            Triangle2D triangle = new Triangle2D(new Point2D(0, 0),
                                                 new Point2D(1, 0),
                                                 new Point2D(2, 0));

            Assert.That(triangle.Orientation == Signum.Zero);
        }

        [Test]
        public void GetCentroid()
        {
            Triangle2D triangle = new Triangle2D(new Point2D(0, 0),
                                                 new Point2D(1, 0),
                                                 new Point2D(0, 1));

            Point2D centroidExpected = new Point2D(0.3333333, 0.3333333);
            Point2D centroidActual = triangle.Centroid;

            Assert.That(centroidExpected.Equals(centroidActual));
        }

        [TestCase(0, 0, 10, 0, 0, 10, 50)]
        public void Area(double p1x, double p1y, double p2x, double p2y, double p3x, double p3y, double expectedArea)
        {
            Triangle2D triangle = new Triangle2D(new Point2D(p1x, p1y), new Point2D(p2x, p2y), new Point2D(p3x, p3y));

            Assert.That(Utilities.SameValue(triangle.Area, expectedArea, expectedArea * 1e-9));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void CircCenter(int exampleID)
        {
            Triangle2D triangle = InitExample_CircCenter(exampleID);
            Point2D expectedPoint = InitExpectedPoint_CircCenter(exampleID);

            Assert.That(expectedPoint.Equals(triangle.CircCenter));
        }

        private Triangle2D InitExample_CircCenter(int exampleID)
        {
            return exampleID switch
            {
                0 => new Triangle2D(new Point2D(0, 0), new Point2D(1, 0), new Point2D(0, 1)),
                1 => new Triangle2D(new Point2D(1, 1), new Point2D(1, 0), new Point2D(0, 1)),
                2 => new Triangle2D(new Point2D(0, 0), new Point2D(-5, 0), new Point2D(0, -3)),
                _ => InitExample_CircCenter(0),
            };
        }

        private Point2D InitExpectedPoint_CircCenter(int exampleID)
        {
            return exampleID switch
            {
                0 => new Point2D(0.5, 0.5),
                1 => new Point2D(0.5, 0.5),
                2 => new Point2D(-2.5, -1.5),
                _ => InitExpectedPoint_CircCenter(0),
            };
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Qaulity(int exampleID)
        {
            Triangle2D triangle = InitExample_Quality(exampleID);
            double qualityActual = triangle.Quality;
            double qualityExpected = InitExpectedQuality(exampleID);

            Assert.That(Utilities.SameValue(qualityActual, qualityExpected));
        }

        private Triangle2D InitExample_Quality(int exampleID)
        {
            return exampleID switch
            {
                0 => new Triangle2D(new Point2D(0, 0), new Point2D(0.5, 0.8660254), new Point2D(1, 0)),
                1 => new Triangle2D(new Point2D(0, 0), new Point2D(1, 0), new Point2D(0, 1)),
                2 => new Triangle2D(new Point2D(0, 0), new Point2D(1, 0), new Point2D(0, 2 * 0.8660254)),
                _ => InitExample_CircCenter(0),
            };
        }

        private double InitExpectedQuality(int exampleID)
        {
            return exampleID switch
            {
                0 => 0.5773502,
                1 => 0.7071068,
                2 => 1,
                _ => InitExpectedQuality(0),
            };
        }

        [Test]
        public void SwitchOrientation_Positive_SwitchedToNegativ()
        {
            Triangle2D triangle = new Triangle2D(new Point2D(0, 0),
                                                 new Point2D(1, 0),
                                                 new Point2D(0, 1));

            triangle.SwitchOrientation();

            Assert.That(triangle.Orientation == Signum.Negative);
        }

        [Test]
        public void SwitchOrientation_Negative_SwitchedToPositive()
        {
            Triangle2D triangle = new Triangle2D(new Point2D(0, 0),
                                                 new Point2D(0, 1),
                                                 new Point2D(1, 0));

            triangle.SwitchOrientation();

            Assert.That(triangle.Orientation == Signum.Positive);
        }

        [Test]
        public void SwitchOrientation_Degenerate_NoChange()
        {
            Triangle2D triangle = new Triangle2D(new Point2D(0, 0),
                                                 new Point2D(1, 0),
                                                 new Point2D(2, 0));

            triangle.SwitchOrientation();

            Assert.That(triangle.Orientation == Signum.Zero);
        }

        [Test]
        public void HasInCircle_Inside_ExpectedPositive()
        {
            Triangle2D triangle = new Triangle2D(new Point2D(0, 0),
                                                 new Point2D(1, 0),
                                                 new Point2D(0, 1));

            Point2D point = new Point2D(0.5, 0.5);

            Assert.That(triangle.HasInCircle(point) == Signum.Positive);
        }

        [Test]
        public void HasInCircle_Outside_ExpectedNegative()
        {
            Triangle2D triangle = new Triangle2D(new Point2D(0, 0),
                                                 new Point2D(1, 0),
                                                 new Point2D(0, 1));

            Point2D point = new Point2D(1.5, 1.5);

            Assert.That(triangle.HasInCircle(point) == Signum.Negative);
        }

        [Test]
        public void HasInCircle_OnBoundary_ExpectedZero()
        {
            Triangle2D triangle = new Triangle2D(new Point2D(0, 0),
                                                 new Point2D(1, 0),
                                                 new Point2D(0, 1));

            Point2D point = new Point2D(1, 1);

            Assert.That(triangle.HasInCircle(point) == Signum.Zero);
        }

        [Test]
        public void HasInCircle_OnBoundrayDegenerateTriangle_ExpectedZero()
        {
            Triangle2D triangle = new Triangle2D(new Point2D(0, 0),
                                                 new Point2D(1, 0),
                                                 new Point2D(2, 0));

            Point2D point = new Point2D(3, 0);

            Assert.That(triangle.HasInCircle(point) == Signum.Zero);
        }

        [Test]
        public void HasInCircle_InsideDegenerateTriangle_ExpectedNegative()
        {
            Triangle2D triangle = new Triangle2D(new Point2D(0, 0),
                                                 new Point2D(1, 0),
                                                 new Point2D(2, 0));

            Point2D point = new Point2D(3, 1);

            Assert.That(triangle.HasInCircle(point) == Signum.Positive);
        }

        [Test]
        public void HasInCircle_OutsideDegenerateTriangle_ExpectedNegative()
        {
            Triangle2D triangle = new Triangle2D(new Point2D(0, 0),
                                                 new Point2D(1, 0),
                                                 new Point2D(2, 0));

            Point2D point = new Point2D(3, -1);

            Assert.That(triangle.HasInCircle(point) == Signum.Negative);
        }

        [Test]
        public void HasInside_PointInside_ExpectedPositive()
        {
            Triangle2D triangle = new Triangle2D(new Point2D(0, 0),
                                                 new Point2D(1, 0),
                                                 new Point2D(0, 1));

            Point2D point = new Point2D(0.25, 0.25);

            Assert.That(triangle.HasInside(point) == Signum.Positive);
        }

        [Test]
        public void HasInside_PointOutside_ExpectedNegative()
        {
            Triangle2D triangle = new Triangle2D(new Point2D(0, 0),
                                                 new Point2D(1, 0),
                                                 new Point2D(0, 1));

            Point2D point = new Point2D(1, 1);

            Assert.That(triangle.HasInside(point) == Signum.Negative);
        }

        [TestCase(0.5, 0.5)]
        [TestCase(0.5, 0)]
        [TestCase(0, 0.5)]
        [TestCase(0, 0)]
        [TestCase(1, 0)]
        [TestCase(0, 1)]
        public void HasInside_PointOnBoundary_ExpectedZero(double x, double y)
        {
            Triangle2D triangle = new Triangle2D(new Point2D(0, 0),
                                                 new Point2D(1, 0),
                                                 new Point2D(0, 1));

            Point2D point = new Point2D(x, y);

            Assert.That(triangle.HasInside(point) == Signum.Zero);
        }

        [Test]
        public void HasOverlap_Overlap_ExpectedPositive()
        {
            Triangle2D triangle1 = new Triangle2D(new Point2D(0, 0), new Point2D(10, 0), new Point2D(0, 10));
            Triangle2D triangle2 = new Triangle2D(new Point2D(1, 1), new Point2D(11, 1), new Point2D(1, 11));

            Assert.That(triangle1.HasOverlap(triangle2) == Signum.Positive);
        }

        [Test]
        public void HasOverlap_NoOverlap_ExpectedNegative()
        {
            Triangle2D triangle1 = new Triangle2D(new Point2D(0, 0), new Point2D(10, 0), new Point2D(0, 10));
            Triangle2D triangle2 = new Triangle2D(new Point2D(20, 20), new Point2D(30, 20), new Point2D(20, 30));

            Assert.That(triangle1.HasOverlap(triangle2) == Signum.Negative);
        }

        [Test]
        public void HasOverlap_Touching_ExpectedZero()
        {
            Triangle2D triangle1 = new Triangle2D(new Point2D(0, 0), new Point2D(10, 0), new Point2D(0, 10));
            Triangle2D triangle2 = new Triangle2D(new Point2D(5, 5), new Point2D(15, 5), new Point2D(5, 15));

            Assert.That(triangle1.HasOverlap(triangle2) == Signum.Zero);
        }
    }
}
