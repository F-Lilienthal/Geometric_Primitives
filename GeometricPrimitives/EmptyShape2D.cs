using System;
using System.Collections.Generic;
using System.Text;

namespace GeometricPrimitives
{
    public class EmptyShape2D : Shape2D
    {
        public override bool Equals(Shape2D emptyShape, double epsilon = 1e-5)
        {
            return this.GetType().Equals(emptyShape.GetType());
        }
    }
}
