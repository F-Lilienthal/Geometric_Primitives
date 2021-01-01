﻿using System;
using System.Collections.Generic;
using System.Text;
using GeometricPrimitives;
using NUnit.Framework;

namespace GeometricPrimitives.UnitTests
{
    [TestFixture]
    class LineSegment2DTests
    {
        [Test]
        public void Equals_SameVertexPositions_ExpectedTrue()
        {
            LineSegment2D lineSegment1 = new LineSegment2D(new Point2D(10, 0), new Point2D(67, 76.21));
            LineSegment2D lineSegment2 = new LineSegment2D(new Point2D(10, 0), new Point2D(67, 76.21));

            Assert.That(lineSegment1.Equals(lineSegment2));
        }

        [Test]
        public void Equals_DifferentVertexOrder_ExpectedTrue()
        {
            LineSegment2D lineSegment1 = new LineSegment2D(new Point2D(10, 0), new Point2D(67, 76.21));
            LineSegment2D lineSegment2 = new LineSegment2D(new Point2D(67, 76.21), new Point2D(10, 0));

            Assert.That(lineSegment1.Equals(lineSegment2));
        }

        [Test]
        public void Equals_NotEqual_ExpectedFalse()
        {
            LineSegment2D lineSegment1 = new LineSegment2D(new Point2D(10, 0), new Point2D(67, 76.21));
            LineSegment2D lineSegment2 = new LineSegment2D(new Point2D(-41, 0), new Point2D(0, 98));

            Assert.IsFalse(lineSegment1.Equals(lineSegment2));
        }

        [Test]
        public void GetCenter()
        {
            LineSegment2D lineSegment = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 0));

            Point2D centerExpected = new Point2D(5, 0);
            Point2D centerActual = lineSegment.Center;

            Assert.That(centerExpected.Equals(centerActual));
        }

        [Test]
        public void GetLength()
        {
            LineSegment2D lineSegment = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 20));

            Assert.That(TestUtil.IsSameValue(lineSegment.Length, 22.3606798));
        }

        [Test]
        public void HasInside_PointInside_ExpectedPositve()
        {
            LineSegment2D lineSegment = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 0));
            Point2D point = new Point2D(5, 0);

            Assert.That(lineSegment.HasInside(point) == Signum.Positive);
        }

        [TestCase(5, 5)]
        [TestCase(-5, 0)]
        [TestCase(15, 0)]
        public void HasInside_PointOutside_ExpectedNegative(double px, double py)
        {
            LineSegment2D lineSegment = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 0));
            Point2D point = new Point2D(px, py);

            Assert.That(lineSegment.HasInside(point) == Signum.Negative);
        }

        [TestCase(0, 0)]
        [TestCase(10, 0)]
        public void HasInside_PointOnSegmentPoint_ExpectedZero(double px, double py)
        {
            LineSegment2D lineSegment = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 0));
            Point2D point = new Point2D(px, py);

            Assert.That(lineSegment.HasInside(point) == Signum.Zero);
        }

        [Test]
        public void IsOneSameLine_SameLineWithRay_ExpectedTrue()
        {
            LineSegment2D lineSegment = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 0));
            Ray2D ray = new Ray2D(new Point2D(20, 0), new Point2D(-10, 0));

            Assert.That(lineSegment.IsOnSameLine(ray));
        }

        [Test]
        public void IsOneSameLine_SameLineWithRay_ExpectedFalse()
        {
            LineSegment2D lineSegment = new LineSegment2D(new Point2D(41, 62), new Point2D(1, -2));
            Ray2D ray = new Ray2D(new Point2D(4, 10), new Point2D(10, 99));

            Assert.IsFalse(lineSegment.IsOnSameLine(ray));
        }

        [Test]
        public void IsOneSameLine_SameLineWithLineSegment_ExpectedTrue()
        {
            LineSegment2D lineSegment1 = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 0));
            LineSegment2D lineSegment2 = new LineSegment2D(new Point2D(20, 0), new Point2D(30, 0));

            Assert.That(lineSegment1.IsOnSameLine(lineSegment2));
        }

        [Test]
        public void IsOneSameLine_SameLineWithLineSegment_ExpectedFalse()
        {
            LineSegment2D lineSegment1 = new LineSegment2D(new Point2D(41, 62), new Point2D(1, -2));
            LineSegment2D lineSegment2 = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 0));

            Assert.IsFalse(lineSegment1.IsOnSameLine(lineSegment2));
        }

        [Test]
        public void IntersectingShapeWithRay_NoIntersection_ExpectedEmptyShape()
        {
            LineSegment2D lineSegment = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 0));
            Ray2D ray = new Ray2D(new Point2D(0, 10), new Point2D(10, 0));
            Shape2D expectedIntersectingShape = new EmptyShape2D();

            Shape2D actualIntersectingShape = lineSegment.IntersectingShape(ray);

            Assert.That(expectedIntersectingShape.Equals(actualIntersectingShape));
        }

        [Test]
        public void IntersectingShapeWithRay_LineSegmentFullyInsideRay_ExpectedLineSegment()
        {
            LineSegment2D lineSegment = new LineSegment2D(new Point2D(15, 15), new Point2D(20, 20));
            Ray2D ray = new Ray2D(new Point2D(0, 0), new Point2D(10, 10));
            Shape2D expectedIntersectingShape = new LineSegment2D(new Point2D(15, 15), new Point2D(20, 20));

            Shape2D actualIntersectingShape = lineSegment.IntersectingShape(ray);

            Assert.That(expectedIntersectingShape.Equals(actualIntersectingShape));
        }

        [TestCase(-5, -5, 5, 5)]
        [TestCase(5, 5, -5, -5)]
        public void IntersectingShapeWithRay_LineSegmentPartiallyInsideRay_ExpectedShortLineSegment(double p1x, double p1y, double p2x, double p2y)
        {
            LineSegment2D lineSegment = new LineSegment2D(new Point2D(p1x, p1y), new Point2D(p2x, p2y));
            Ray2D ray = new Ray2D(new Point2D(0, 0), new Point2D(10, 10));
            Shape2D expectedIntersectingShape = new LineSegment2D(new Point2D(0, 0), new Point2D(5, 5));

            Shape2D actualIntersectingShape = lineSegment.IntersectingShape(ray);

            Assert.That(expectedIntersectingShape.Equals(actualIntersectingShape));
        }

        [Test]
        public void IntersectingShapeWithRay_NormalIntersection_ExpectedPoint()
        {
            LineSegment2D lineSegment = new LineSegment2D(new Point2D(5, -10), new Point2D(5, 10));
            Ray2D ray = new Ray2D(new Point2D(-5, 0), new Point2D(10, 0));
            Shape2D expectedIntersectingShape = new Point2D(5, 0);

            Shape2D actualIntersectingShape = lineSegment.IntersectingShape(ray);

            Assert.That(expectedIntersectingShape.Equals(actualIntersectingShape));
        }

        [Test]
        public void IntersectingShapeWithRay_TouchingPoint_ExpectedPoint()
        {
            LineSegment2D lineSegment = new LineSegment2D(new Point2D(5, 0), new Point2D(5, 10));
            Ray2D ray = new Ray2D(new Point2D(-5, 0), new Point2D(10, 0));
            Shape2D expectedIntersectingShape = new Point2D(5, 0);

            Shape2D actualIntersectingShape = lineSegment.IntersectingShape(ray);

            Assert.That(expectedIntersectingShape.Equals(actualIntersectingShape));
        }
    }
}
