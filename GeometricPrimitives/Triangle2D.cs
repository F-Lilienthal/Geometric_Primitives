using System;
using System.Collections.Generic;
using System.Text;

namespace GeometricPrimitives
{
    public class Triangle2D : Shape2D
    {
        public readonly List<Point2D> PointList;
        public readonly Point2D Centroid;
        public readonly Point2D CircCenter;
        public readonly double Area;
        public readonly double Quality;

        public Signum Orientation { get; protected set; }

        public Triangle2D(Point2D point1, Point2D point2, Point2D point3)
        {
            //TODO_FL: check for degenerate edge (i.e. vertex1 has different position from vertex2)
            PointList = new List<Point2D>
            {
                point1,
                point2,
                point3
            };

            Orientation = CalcOrientation();
            Centroid = CalcCentroid();
            CircCenter = CalcCircCenter();
            Area = CalcArea();
            Quality = CalcQuality();
        }

        public override bool Equals(Shape2D triangle, double epsilon = 1e-5)
        {
            if (triangle is Triangle2D compare)
            {
                int equalCount = 0;

                for (int i = 0; i < PointList.Count; i++)
                {
                    for (int j = 0; j < compare.PointList.Count; j++)
                    {
                        if (PointList[i].Equals(compare.PointList[j], epsilon))
                        {
                            equalCount++;
                            break;
                        }
                    }
                }

                return (equalCount == 3);
            }
            else
            {
                return false;
            }
        }

        public Triangle2D Clone()
        {
            return new Triangle2D(PointList[0], PointList[1], PointList[2]);
        }

        private Signum CalcOrientation()
        {
            double value = (PointList[0].X - PointList[2].X) * (PointList[1].Y - PointList[2].Y) -
                           (PointList[0].Y - PointList[2].Y) * (PointList[1].X - PointList[2].X);

            if (value > 1e-5)
            {
                return Signum.Positive;
            }
            else if (value < -1e-5)
            {
                return Signum.Negative;
            }
            else
            {
                return Signum.Zero;
            }
        }

        private Point2D CalcCentroid()
        {
            Point2D centroid = new Point2D((PointList[0].X + PointList[1].X + PointList[2].X) / 3,
                                           (PointList[0].Y + PointList[1].Y + PointList[2].Y) / 3);

            return centroid;
        }

        private Point2D CalcCircCenter()
        {
            double x1 = PointList[0].X;
            double x2 = PointList[1].X;
            double x3 = PointList[2].X;
            double y1 = PointList[0].Y;
            double y2 = PointList[1].Y;
            double y3 = PointList[2].Y;

            double u = (y2 - y3) * (x1 * x1 + y1 * y1) + (y3 - y1) * (x2 * x2 + y2 * y2) + (y1 - y2) * (x3 * x3 + y3 * y3);
            double v = (x3 - x2) * (x1 * x1 + y1 * y1) + (x1 - x3) * (x2 * x2 + y2 * y2) + (x2 - x1) * (x3 * x3 + y3 * y3);
            double d = x1 * y2 + x2 * y3 + x3 * y1 - x1 * y3 - x2 * y1 - x3 * y2;

            return new Point2D(u / (2 * d), v / (2 * d));
        }

        private double CalcArea()
        {
            //Heron's formula
            double a = PointList[1].DistanceTo(PointList[0]);
            double b = PointList[2].DistanceTo(PointList[1]);
            double c = PointList[0].DistanceTo(PointList[2]);
            double s = (a + b + c) / 2;

            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }

        private double CalcQuality()
        {
            double circRadius = PointList[0].DistanceTo(CircCenter);

            double minEdgeLength = PointList[0].DistanceTo(PointList[1]);
            minEdgeLength = Math.Min(minEdgeLength, PointList[1].DistanceTo(PointList[2]));
            minEdgeLength = Math.Min(minEdgeLength, PointList[2].DistanceTo(PointList[0]));

            return circRadius / minEdgeLength;
        }

        public void SwitchOrientation()
        {
            Signum orientation = Orientation;

            if (orientation == Signum.Negative || orientation == Signum.Positive)
            {
                PointList.Insert(0, PointList[1]);
                PointList.RemoveAt(2);
            }

            Orientation = CalcOrientation();
        }

        public Signum HasInCircle(Point2D point)
        {
            Point2D A = PointList[0];
            Point2D B = PointList[1];
            Point2D C = PointList[2];
            Point2D D = point;

            double value = (A.X - D.X) * (B.Y - D.Y) * ((C.X - D.X) * (C.X - D.X) + (C.Y - D.Y) * (C.Y - D.Y)) +
                           (B.X - D.X) * (C.Y - D.Y) * ((A.X - D.X) * (A.X - D.X) + (A.Y - D.Y) * (A.Y - D.Y)) +
                           (C.X - D.X) * (A.Y - D.Y) * ((B.X - D.X) * (B.X - D.X) + (B.Y - D.Y) * (B.Y - D.Y)) -
                           (C.X - D.X) * (B.Y - D.Y) * ((A.X - D.X) * (A.X - D.X) + (A.Y - D.Y) * (A.Y - D.Y)) -
                           (A.X - D.X) * (C.Y - D.Y) * ((B.X - D.X) * (B.X - D.X) + (B.Y - D.Y) * (B.Y - D.Y)) -
                           (B.X - D.X) * (A.Y - D.Y) * ((C.X - D.X) * (C.X - D.X) + (C.Y - D.Y) * (C.Y - D.Y));

            //TODO_FL: What is a meaningful epsilon here??? => Probably requires quite a bit of testing to get an idea for the range of values
            if (value > 1e-5)
            {
                return Signum.Positive;
            }
            else if (value < -1e-5)
            {
                return Signum.Negative;
            }
            else
            {
                return Signum.Zero;
            }
        }

        public Signum HasInside(Point2D point)
        {
            double epsilon = 1e-5;

            double r1x = PointList[1].X - PointList[0].X;
            double r1y = PointList[1].Y - PointList[0].Y;
            double r2x = PointList[2].X - PointList[0].X;
            double r2y = PointList[2].Y - PointList[0].Y;
            double px = point.X - PointList[0].X;
            double py = point.Y - PointList[0].Y;

            double u = (py * r2x - px * r2y) / (r1y * r2x - r1x * r2y);
            double v = (px * r1y - py * r1x) / (r1y * r2x - r1x * r2y);

            if ((u > epsilon) && (u < 1 - epsilon) && (v > epsilon) && (v < 1 - epsilon) && (u + v < 1 - epsilon))
            {
                return Signum.Positive;
            }
            else if ((Math.Abs(u) < epsilon && (v > -epsilon) && (v < 1 + epsilon)) ||
                     (Math.Abs(v) < epsilon && (u > -epsilon) && (u < 1 + epsilon)) ||
                     (Math.Abs(u + v - 1) < epsilon))
            {
                return Signum.Zero;
            }
            else
            {
                return Signum.Negative;
            }
        }

        public Signum HasOverlap(Triangle2D triangle)
        {
            bool touchingIntervals = false;

            for (int i = 0; i < 3; i++)
            {
                Point2D edgeVector = PointList[(i + 1) % 3] - PointList[i];
                Point2D direction = new Point2D(-edgeVector.Y, edgeVector.X);

                double Umin = double.MaxValue;
                double Umax = -double.MaxValue;
                double Vmin = double.MaxValue;
                double Vmax = -double.MaxValue;

                for (int j = 0; j < 3; j++)
                {
                    Umin = Math.Min(Umin, direction.Dot(PointList[j]));
                    Umax = Math.Max(Umax, direction.Dot(PointList[j]));
                    Vmin = Math.Min(Vmin, direction.Dot(triangle.PointList[j]));
                    Vmax = Math.Max(Vmax, direction.Dot(triangle.PointList[j]));
                }

                if (Utilities.GreaterThanValue(Umin, Vmax) || Utilities.LessThanValue(Umax, Vmin))
                {
                    return Signum.Negative;
                }
                else if (Utilities.SameValue(Umin, Vmax) || Utilities.SameValue(Umax, Vmin))
                {
                    touchingIntervals = true;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                Point2D edgeVector = triangle.PointList[(i + 1) % 3] - PointList[i];
                Point2D direction = new Point2D(-edgeVector.Y, edgeVector.X);

                double Umin = double.MaxValue;
                double Umax = -double.MaxValue;
                double Vmin = double.MaxValue;
                double Vmax = -double.MaxValue;

                for (int j = 0; j < 3; j++)
                {
                    Umin = Math.Min(Umin, direction.Dot(triangle.PointList[j]));
                    Umax = Math.Max(Umax, direction.Dot(triangle.PointList[j]));
                    Vmin = Math.Min(Vmin, direction.Dot(PointList[j]));
                    Vmax = Math.Max(Vmax, direction.Dot(PointList[j]));
                }

                if (Utilities.GreaterThanValue(Umin, Vmax) || Utilities.LessThanValue(Umax, Vmin))
                {
                    return Signum.Negative;
                }
                else if (Utilities.SameValue(Umin, Vmax) || Utilities.SameValue(Umax, Vmin))
                {
                    touchingIntervals = true;
                }
            }

            if (touchingIntervals)
            {
                return Signum.Zero;
            }
            else
            {
                return Signum.Positive;
            }
        }
    }
}
