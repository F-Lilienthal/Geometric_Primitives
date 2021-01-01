using System;
using System.Collections.Generic;
using System.Text;

namespace GeometricPrimitives
{
    public class Point2D : Shape2D
    {
        public double X { get; protected set; }
        public double Y { get; protected set; }

        public Point2D(double xPosition, double yPosition)
        {
            X = xPosition;
            Y = yPosition;
        }

        public override bool Equals(Shape2D point, double epsilon = 1e-5)
        {
            if (point is Point2D compare)
            {
                return ((Math.Abs(X - compare.X) < epsilon) && (Math.Abs(Y - compare.Y) < epsilon));
            }
            else
            {
                return false;
            }
        }

        public Point2D Clone()
        {
            return new Point2D(X, Y);
        }

        public void Shift(double shiftX, double shiftY)
        {
            X += shiftX;
            Y += shiftY;
        }

        public void ShiftTo(double newX, double newY)
        {
            X = newX;
            Y = newY;
        }

        public void Rotate(double AngleInDeg)
        {
            double sin = Math.Sin(AngleInDeg * Math.PI / 180);
            double cos = Math.Cos(AngleInDeg * Math.PI / 180);

            double newX = cos * X - sin * Y;
            double newY = sin * X + cos * Y;

            X = newX;
            Y = newY;
        }

        public void Stretch(double stretchFactor)
        {
            X *= stretchFactor;
            Y *= stretchFactor;
        }

        public double DistanceTo(Point2D point)
        {
            return Math.Sqrt((X - point.X) * (X - point.X) + (Y - point.Y) * (Y - point.Y));
        }

        public double Dot(Point2D point)
        {
            return (X * point.X + Y * point.Y);
        }

        public double VectorLength()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public static Point2D operator +(Point2D point1, Point2D point2)
        {
            return new Point2D(point1.X + point2.X, point1.Y + point2.Y);
        }

        public static Point2D operator -(Point2D point1, Point2D point2)
        {
            return new Point2D(point1.X - point2.X, point1.Y - point2.Y);
        }

    }
}
