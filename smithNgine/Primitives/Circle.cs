using Microsoft.Xna.Framework;
using System;

namespace Codesmith.SmithNgine.Primitives
{
    class Circle : IEquatable<Circle>
    {
        public Vector2 Position;
        public float Radius;

        public int X
        {
            get { return (int)this.Position.X; }
        }

        public int Y
        {
            get { return (int)this.Position.Y; }
        }

        public Circle(float radius, Vector2 position)
        {
            this.Radius = radius;
            this.Position = position;
        }

        public Circle(float radius, int x, int y)
        {
            this.Radius = radius;
            this.Position = new Vector2(x, y);
        }

        public bool Equals(Circle other)
        {
            return (Radius == other.Radius && Position == other.Position);
        }

        public bool Intersects(Circle other)
        {
            float distance = Vector2.Distance(other.Position, this.Position);
            return (distance < (other.Radius + this.Radius));
        }

    }
}
