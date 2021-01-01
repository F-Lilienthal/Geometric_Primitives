using System;
using System.Collections.Generic;
using System.Text;

namespace GeometricPrimitives
{
    class TypeDef
    {
        public enum Signum { Positive, Negative, Zero, None } //"None" is for testing purposes, to make test fail that require return value but have not been implemented yet

        public enum RelativePosition { Inside, Outside, Overlap, OnPoint, OnLine }

        static class TypeDef
        {
        }
    }
}
