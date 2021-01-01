using System;
using System.Collections.Generic;
using System.Text;
using GeometricPrimitives;
using NUnit.Framework;

namespace GeometricPrimitives.UnitTests
{
    [TestFixture]
    class EmptyShape2DTests
    {
        [Test]
        public void Equals_CompareToEmptyShape_ExpectedTrue()
        {
            EmptyShape2D emptyShape = new EmptyShape2D();
            EmptyShape2D comparingShape = new EmptyShape2D();

            Assert.That(emptyShape.Equals(comparingShape));
        }

        [Test]
        public void Equals_CompareToPoint2D_ExpectedTrue()
        {
            EmptyShape2D emptyShape = new EmptyShape2D();
            Point2D point = new Point2D(0, 0);

            Assert.That(!emptyShape.Equals(point));
        }

    }
}
