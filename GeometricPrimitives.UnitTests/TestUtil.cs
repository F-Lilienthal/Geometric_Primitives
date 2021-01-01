using System;
using System.Collections.Generic;
using System.Text;

namespace GeometricPrimitives.UnitTests
{
    class TestUtil
    {
        public static bool IsSameValue(double value1, double value2, double epsilon = 1e-5)
        {
            return (Math.Abs(value1 - value2) <= epsilon);
        }
    }
}
