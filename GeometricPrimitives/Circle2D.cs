using System;
using System.Collections.Generic;
using System.Text;

namespace GeometricPrimitives
{
    public class Circle2D : Shape2D
    {
        public readonly Point2D Center;
        public readonly double Radius;
        public override double XMin
        {
            get
            {
                return Center.X - Radius;
            }
        }
        public override double XMax
        {
            get
            {
                return Center.X + Radius;
            }
        }
        public override double YMin
        {
            get
            {
                return Center.Y - Radius;
            }
        }
        public override double YMax
        {
            get
            {
                return Center.Y + Radius;
            }
        }


        public Circle2D(Point2D center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public override bool Equals(Shape2D circle, double epsilon = 1e-5)
        {
            if (circle is Circle2D compare)
            {
                return (Center.Equals(compare.Center) && Utilities.SameValue(Radius, compare.Radius, epsilon));
            }
            else
            {
                return false;
            }
        }
    }
}
