using System;
using System.Collections.Generic;
using System.Text;

namespace GeometricPrimitives
{
    public class Line2D : Shape2D
    {
        public readonly Point2D Position;
        public readonly Point2D Direction;

        public Line2D(Point2D position, Point2D direction)
        {
            Position = position;
            Direction = direction;
        }

        public override bool Equals(Shape2D line, double epsilon = 1e-5)
        {
            return false;
        }

        public Signum HasInside(Point2D point)
        {
            Point2D pointVector = new Point2D(point.X - Position.X, point.Y - Position.Y);

            if (Math.Abs(Direction.Dot(Direction) * pointVector.Dot(pointVector) - Direction.Dot(pointVector) * (Direction.Dot(pointVector))) <= 1e-5)
            {
                return Signum.Positive;
            }
            else
            {
                return Signum.Negative;
            }
        }
    }

}
