using System;
using System.Collections.Generic;
using System.Text;

namespace GeometricPrimitives
{
    public class Ray2D : Shape2D
    {
        public readonly Point2D Position;
        public readonly Point2D Direction;

        public Ray2D(Point2D position, Point2D direction)
        {
            Position = position;
            Direction = direction;
        }

        public override bool Equals(Shape2D ray, double epsilon = 1e-5)
        {
            if (ray is Ray2D)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
