using System;
using System.Collections.Generic;
using System.Text;

namespace GeometricPrimitives
{
    public abstract class Shape2D
    {
        public abstract bool Equals(Shape2D shape, double epsilon = 1e-5);

        public virtual Shape2D IntersectingShape(Shape2D shape)
        {
            //TODO: we actually need to throw an exception here
            return new EmptyShape2D();
        }

        public virtual double XMin { get; }
        public virtual double XMax { get; }
        public virtual double YMin { get; }
        public virtual double YMax { get; }        
    }
}
