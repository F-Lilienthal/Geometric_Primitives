using System;
using System.Collections.Generic;
using System.Text;

namespace GeometricPrimitives
{
    public class Utilities
    {
        public static bool SameValue(double a, double b, double epsilon = 1e-5)
        {
            return (Math.Abs(a - b) <= epsilon);
        }

        public static bool LessThanValue(double a, double b, double epsilon = 1e-5)
        {
            return (a < b - epsilon);
        }

        public static bool GreaterThanValue(double a, double b, double epsilon = 1e-5)
        {
            return (a > b + epsilon);
        }

        public static void SwapValues(ref double a, ref double b)
        {
            double temp = a;
            a = b;
            b = temp;
        }
    }
}
