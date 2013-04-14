using Microsoft.Xna.Framework;
using System;

namespace Codesmith.SmithNgine.Primitives
{
    public class Circle : IEquatable<Circle>
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

        /// <summary>
        /// Linearly interpolate along the circle perimeter. Get a point along the circle
        /// </summary>
        /// <param name="amount">Amount to interpolate, from 0 (=-PI) to 1.0f (PI)</param>
        /// <returns></returns>
        public Vector2 Lerp(float amount)
        {
            float angle = MathHelper.Lerp(-MathHelper.Pi, MathHelper.Pi, amount);
            return new Vector2((float)(Math.Sin(angle) * Radius) + Position.X, (float)(Math.Cos(angle) * Radius) + Position.Y);
        }

        /// <summary>
        /// Get a point from the circle perimeter
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public Vector2 GetPoint(float angle)
        {
            return new Vector2((float)(Math.Sin(angle) * Radius) + Position.X, (float)(Math.Cos(angle) * Radius)+Position.Y); 
        }

        public Vector2 GetRandomContainedPoint(Random r)
        {
            double angle = r.NextDouble() * MathHelper.TwoPi;
            double radius = r.NextDouble() * Radius;
            return new Vector2((float)(Math.Sin(angle) * radius) + Position.X, (float)(-Math.Cos(angle) * radius)+Position.Y);
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
