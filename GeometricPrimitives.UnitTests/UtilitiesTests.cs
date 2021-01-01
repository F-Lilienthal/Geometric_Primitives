using System;
using System.Collections.Generic;
using System.Text;
using GeometricPrimitives;
using NUnit.Framework;

namespace GeometricPrimitives.UnitTests
{
    [TestFixture]
    class UtilitiesTests
    {
        [Test]
        public void SwapValues_TwoDoubles()
        {
            double a = 5;
            double b = 3;

            Utilities.SwapValues(ref a, ref b);

            Assert.That(a == 3 && b == 5);
        }
    }
}
