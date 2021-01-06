using System;
using System.Collections.Generic;
using System.Text;

namespace GeometricPrimitives
{
    public class LineSegment2D : Shape2D
    {
        public readonly List<Point2D> PointList;
        public Point2D Center { get; }
        public double Length { get; }

        public override double XMin
        {
            get
            {
                return Math.Min(PointList[0].X, PointList[1].X);
            }
        }
        public override double XMax
        {
            get
            {
                return Math.Max(PointList[0].X, PointList[1].X);
            }
        }
        public override double YMin
        {
            get
            {
                return Math.Min(PointList[0].Y, PointList[1].Y);
            }
        }
        public override double YMax
        {
            get
            {
                return Math.Max(PointList[0].Y, PointList[1].Y);
            }
        }

        public LineSegment2D(Point2D point1, Point2D point2)
        {
            PointList = new List<Point2D>
            {
                point1,
                point2
            };

            Center = CalcCenter();
            Length = CalcLength();
        }

        public LineSegment2D Clone()
        {
            return new LineSegment2D(PointList[0], PointList[1]);
        }

        public override bool Equals(Shape2D lineSegment, double epsilon = 1e-5)
        {
            if (lineSegment is LineSegment2D compare)
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

                return (equalCount == 2);
            }
            else
            {
                return false;
            }
        }

        public Signum HasInside(Point2D point)
        {
            double r1x = PointList[1].X - PointList[0].X;
            double r1y = PointList[1].Y - PointList[0].Y;
            double px = point.X - PointList[0].X;
            double py = point.Y - PointList[0].Y;

            if (Utilities.SameValue(r1x * py, r1y * px))
            {
                if ((Utilities.SameValue(px, 0) && Utilities.SameValue(py, 0)) ||
                    (Utilities.SameValue(px, r1x) && Utilities.SameValue(py, r1y)))
                {
                    return Signum.Zero;
                }
                else if (Utilities.LessThanValue(px, 0) || Utilities.LessThanValue(py, 0) ||
                         Utilities.GreaterThanValue(px, r1x) || Utilities.GreaterThanValue(py, r1y))
                {
                    return Signum.Negative;
                }
                else
                {
                    return Signum.Positive;
                }
            }
            else
            {
                return Signum.Negative;
            }
        }

        public bool IsOnSameLine(Ray2D ray)
        {
            Line2D line = new Line2D(ray.Position, ray.Direction);

            return (line.HasInside(PointList[0]) == Signum.Positive) &&
                   (line.HasInside(PointList[1]) == Signum.Positive);
        }

        public bool IsOnSameLine(LineSegment2D lineSegment)
        {
            Line2D line = new Line2D(lineSegment.PointList[0], lineSegment.PointList[1] - lineSegment.PointList[0]);

            return (line.HasInside(PointList[0]) == Signum.Positive) &&
                   (line.HasInside(PointList[1]) == Signum.Positive);
        }

        public Shape2D IntersectingShape(LineSegment2D lineSegment)
        {
            return new EmptyShape2D();
        }

        public Shape2D IntersectingShape(Ray2D ray)
        {
            if (IsOnSameLine(ray))
            {
                return IntersectingShapeOnLine(ray);
            }
            else
            {
                double r0x = PointList[0].X;
                double r0y = PointList[0].Y;
                double r1x = PointList[1].X - PointList[0].X;
                double r1y = PointList[1].Y - PointList[0].Y;
                double s0x = ray.Position.X;
                double s0y = ray.Position.Y;
                double s1x = ray.Direction.X;
                double s1y = ray.Direction.Y;

                double u = (s1x * (s0y - r0y) - s0x * s1y + r0x * s1y) / (r1y * s1x - r1x * s1y);
                double v = (r1x * (s0y - r0y) - s0x * r1y + r0x * r1y) / (r1y * s1x - r1x * s1y);

                if (u >= -1e-5 && u <= 1 + 1e-5 && v >= -1e-5)
                {
                    return new Point2D(r0x + u * r1x, r0y + u * r1y);
                }
                else
                {
                    return new EmptyShape2D();
                }
            }
        }

        private Shape2D IntersectingShapeOnLine(Ray2D ray)
        {
            double Umin = PointList[0].Dot(ray.Direction);
            double Umax = PointList[1].Dot(ray.Direction);
            bool switched = false;

            if (Umin > Umax)
            {
                Utilities.SwapValues(ref Umin, ref Umax);
                switched = true;
            }

            if (Umax > 0 && Umin >= 0)
            {
                return this;
            }
            else if (Umax > 0 && Umin < 0)
            {
                if (switched)
                {
                    return new LineSegment2D(ray.Position, PointList[0]);
                }
                else
                {
                    return new LineSegment2D(ray.Position, PointList[1]);
                }
            }
            else
            {
                return new EmptyShape2D();
            }
        }

        private Point2D CalcCenter()
        {
            return new Point2D((PointList[0].X + PointList[1].X) / 2, (PointList[0].Y + PointList[1].Y) / 2);
        }

        private double CalcLength()
        {
            return PointList[0].DistanceTo(PointList[1]);
        }
    }
}
