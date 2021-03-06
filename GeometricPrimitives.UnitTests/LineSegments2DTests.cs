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

        [TestCase(0, 0, 10, 0, 5, 0)]
        [TestCase(4, 8, 19, 37, 11.5, 22.5)]
        public void GetCenter(double x1, double y1, double x2, double y2, double centerX, double centerY)
        {
            LineSegment2D lineSegment = new LineSegment2D(new Point2D(x1, y1), new Point2D(x2, y2));

            Point2D centerExpected = new Point2D(centerX, centerY);
            Point2D centerActual = lineSegment.Center;

            Assert.That(centerExpected.Equals(centerActual));
        }

        [Test]
        public void GetLength()
        {
            LineSegment2D lineSegment = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 20));

            Assert.That(TestUtil.IsSameValue(lineSegment.Length, 22.3606798));
        }

        [TestCase(0)]
        [TestCase(1)]
        public void HasInside_PointInside_ExpectedPositve(int exampleID)
        {
            (LineSegment2D, Point2D) values = InitValues_HasInside_PointInside(exampleID);

            LineSegment2D lineSegment = values.Item1;
            Point2D point = values.Item2;

            Assert.That(lineSegment.HasInside(point) == Signum.Positive);
        }

        private (LineSegment2D, Point2D) InitValues_HasInside_PointInside(int exampleID)
        {
            LineSegment2D lineSegment;
            Point2D point;

            switch (exampleID)
            {
                case 0:
                    lineSegment = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 0));
                    point = new Point2D(5, 0);
                    return (lineSegment, point);
                case 1:
                    lineSegment = new LineSegment2D(new Point2D(15, 15), new Point2D(5, 5));
                    point = new Point2D(10, 10);
                    return (lineSegment, point);
                default:
                    return InitValues_HasInside_PointInside(0);
            }
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

        [Test]
        public void IntersectingShapeWithLineSegment_NormalIntersection_ExpectedPoint()
        {
            LineSegment2D lineSegment1 = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 10));
            LineSegment2D lineSegment2 = new LineSegment2D(new Point2D(0, 10), new Point2D(10, 0));
            Shape2D expectedIntersectingShape = new Point2D(5,5);

            Shape2D actualIntersectingShape = lineSegment1.IntersectingShape(lineSegment2);

            Assert.That(expectedIntersectingShape.Equals(actualIntersectingShape));
        }

        [Test]
        public void IntersectingShapeWithLineSegment_TouchingPoint_ExpectedPoint()
        {
            LineSegment2D lineSegment1 = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 10));
            LineSegment2D lineSegment2 = new LineSegment2D(new Point2D(0, 10), new Point2D(5, 5));
            Shape2D expectedIntersectingShape = new Point2D(5, 5);

            Shape2D actualIntersectingShape = lineSegment1.IntersectingShape(lineSegment2);

            Assert.That(expectedIntersectingShape.Equals(actualIntersectingShape));
        }

        [Test]
        public void IntersectingShapeWithLineSegment_OneIdenticalEndPoint_ExpectedPoint()
        {
            LineSegment2D lineSegment1 = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 10));
            LineSegment2D lineSegment2 = new LineSegment2D(new Point2D(10, 10), new Point2D(20, 0));
            Shape2D expectedIntersectingShape = new Point2D(10, 10);

            Shape2D actualIntersectingShape = lineSegment1.IntersectingShape(lineSegment2);

            Assert.That(expectedIntersectingShape.Equals(actualIntersectingShape));
        }

        [TestCase(0)]
        [TestCase(1)]
        public void IntersectingShapeWithLineSegment_OverlapCompletlyInside_ExpectedLineSegment(int exampleID)
        {
            (LineSegment2D, LineSegment2D, LineSegment2D) values = InitValues_IntersectingShapeWithLineSegment_OverlapCompletlyInside(exampleID);
            LineSegment2D lineSegment1 = values.Item1;
            LineSegment2D lineSegment2 = values.Item2;
            Shape2D expectedIntersectingShape = values.Item3;

            Shape2D actualIntersectingShape = lineSegment1.IntersectingShape(lineSegment2);

            Assert.That(expectedIntersectingShape.Equals(actualIntersectingShape));
        }

        private (LineSegment2D, LineSegment2D, LineSegment2D) InitValues_IntersectingShapeWithLineSegment_OverlapCompletlyInside(int exampleID)
        {
            LineSegment2D lineSegment1;
            LineSegment2D lineSegment2;
            LineSegment2D expectedIntersectingShape;

            switch (exampleID)
            {
                case 0:
                    lineSegment1 = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 10));
                    lineSegment2 = new LineSegment2D(new Point2D(2, 2), new Point2D(5, 5));
                    expectedIntersectingShape = new LineSegment2D(new Point2D(2, 2), new Point2D(5, 5));
                    return (lineSegment1, lineSegment2, expectedIntersectingShape);
                case 1:
                    lineSegment1 = new LineSegment2D(new Point2D(4, 6), new Point2D(12, 18));
                    lineSegment2 = new LineSegment2D(new Point2D(-2, -3), new Point2D(16, 24));
                    expectedIntersectingShape = new LineSegment2D(new Point2D(4, 6), new Point2D(12, 18));
                    return (lineSegment1, lineSegment2, expectedIntersectingShape);
                default:
                    return InitValues_IntersectingShapeWithLineSegment_OverlapCompletlyInside(0);                    
            }
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void IntersectingShapeWithLineSegment_PartialOverlap_ExpectedLineSegment(int exampleID)
        {
            (LineSegment2D, LineSegment2D, LineSegment2D) values = InitValues_IntersectingShapeWithLineSegment_PartialOverlap(exampleID);
            LineSegment2D lineSegment1 = values.Item1;
            LineSegment2D lineSegment2 = values.Item2;
            Shape2D expectedIntersectingShape = values.Item3;

            Shape2D actualIntersectingShape = lineSegment1.IntersectingShape(lineSegment2);

            Assert.That(expectedIntersectingShape.Equals(actualIntersectingShape));
        }

        private (LineSegment2D, LineSegment2D, LineSegment2D) InitValues_IntersectingShapeWithLineSegment_PartialOverlap(int exampleID)
        {
            LineSegment2D lineSegment1;
            LineSegment2D lineSegment2;
            LineSegment2D expectedIntersectingShape;

            switch (exampleID)
            {
                case 0:
                    lineSegment1 = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 10));
                    lineSegment2 = new LineSegment2D(new Point2D(5, 5), new Point2D(15, 15));
                    expectedIntersectingShape = new LineSegment2D(new Point2D(5, 5), new Point2D(10, 10));
                    return (lineSegment1, lineSegment2, expectedIntersectingShape);
                case 1:
                    lineSegment1 = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 10));
                    lineSegment2 = new LineSegment2D(new Point2D(15, 15), new Point2D(5, 5));
                    expectedIntersectingShape = new LineSegment2D(new Point2D(10, 10), new Point2D(5, 5));
                    return (lineSegment1, lineSegment2, expectedIntersectingShape);
                case 2:
                    lineSegment1 = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 10));
                    lineSegment2 = new LineSegment2D(new Point2D(-5, -5), new Point2D(5, 5));
                    expectedIntersectingShape = new LineSegment2D(new Point2D(0, 0), new Point2D(5, 5));
                    return (lineSegment1, lineSegment2, expectedIntersectingShape);
                case 3:
                    lineSegment1 = new LineSegment2D(new Point2D(10, 10), new Point2D(0, 0));
                    lineSegment2 = new LineSegment2D(new Point2D(-5, -5), new Point2D(5, 5));
                    expectedIntersectingShape = new LineSegment2D(new Point2D(0, 0), new Point2D(5, 5));
                    return (lineSegment1, lineSegment2, expectedIntersectingShape);
                default:
                    return InitValues_IntersectingShapeWithLineSegment_OverlapCompletlyInside(0);
            }
        }

        [Test]
        public void IntersectingShapeWithLineSegment_IdenticalSegments_ExpectedLineSegment()
        {
            LineSegment2D lineSegment1 = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 10));
            LineSegment2D lineSegment2 = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 10));
            Shape2D expectedIntersectingShape = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 10));

            Shape2D actualIntersectingShape = lineSegment1.IntersectingShape(lineSegment2);

            Assert.That(expectedIntersectingShape.Equals(actualIntersectingShape));
        }

        [Test]
        public void IntersectingShapeWithLineSegment_NoIntersection_ExpectedEmptyShape()
        {
            LineSegment2D lineSegment1 = new LineSegment2D(new Point2D(0, 0), new Point2D(10, 10));
            LineSegment2D lineSegment2 = new LineSegment2D(new Point2D(40, 40), new Point2D(30, 50));
            Shape2D expectedIntersectingShape = new EmptyShape2D();

            Shape2D actualIntersectingShape = lineSegment1.IntersectingShape(lineSegment2);

            Assert.That(expectedIntersectingShape.Equals(actualIntersectingShape));
        }
    }
}
