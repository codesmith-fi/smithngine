using Microsoft.Xna.Framework;
using System;

namespace Codesmith.SmithNgine.Primitives
{
    public class Line : IEquatable<Line>
    {
        public Vector2 Start
        {
            set;
            get;
        }
        public Vector2 End
        {
            set;
            get;
        }

        public Line(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
        }

        public virtual Vector2 Lerp(float amount)
        {
            return Vector2.Lerp(Start, End, amount);
        }

        public bool Equals(Line other)
        {
            return (other.Start == this.Start && other.End == this.End);
        }
    }
}
