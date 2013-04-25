/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using Microsoft.Xna.Framework;
using System;

namespace Codesmith.SmithNgine.Primitives
{
    /// <summary>
    /// Implements a Circle class which has a Radius and Position
    /// </summary>
    public class Circle : IEquatable<Circle>
    {
        #region Properties
        /// <summary>
        /// Get or set the position of this circle
        /// </summary>
        public Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set the radius of this circle
        /// </summary>
        public float Radius
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set the X coordinate of the position
        /// </summary>
        public float X
        {
            get { return Position.X; }
        }

        /// <summary>
        /// Get or set the Y coordinate of the position
        /// </summary>
        public float Y
        {
            get { return Position.Y; }
        }
        #endregion 

        #region Constructors
        /// <summary>
        /// Construct a circle with radius and position
        /// </summary>
        /// <param name="radius">Radius</param>
        /// <param name="position">Position</param>
        public Circle(float radius, Vector2 position)
        {
            this.Radius = radius;
            this.Position = position;
        }

        /// <summary>
        /// Construct a circle with radius, and x and y coordinates for position
        /// </summary>
        /// <param name="radius">Radius</param>
        /// <param name="x">X coordinate for the position</param>
        /// <param name="y">Y coordinate for the position</param>
        public Circle(float radius, float x, float y)
        {
            this.Radius = radius;
            this.Position = new Vector2(x, y);
        }
        #endregion

        #region New methods
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
        /// Inflate the circle (modify radius by given amount)
        /// </summary>
        /// <param name="amount">Amount to inflate</param>
        public void Inflate(float amount)
        {
            Radius += amount;
        }

        /// <summary>
        /// Get a point from the circle perimeter
        /// </summary>
        /// <param name="angle"></param>
        /// <returns>A random point (as a Vector2) from the circle perimeter</returns>
        public Vector2 GetPoint(float angle)
        {
            return new Vector2((float)(Math.Sin(angle) * Radius) + Position.X, (float)(Math.Cos(angle) * Radius)+Position.Y); 
        }

        /// <summary>
        /// Get a random point (as a Vector2) within the circle
        /// </summary>
        /// <param name="r">Random</param>
        /// <returns>Random point (as a Vector2) contained by this circle</returns>
        public Vector2 GetRandomContainedPoint(Random r)
        {
            double angle = r.NextDouble() * MathHelper.TwoPi;
            double radius = r.NextDouble() * Radius;
            return new Vector2((float)(Math.Sin(angle) * radius) + Position.X, (float)(-Math.Cos(angle) * radius)+Position.Y);
        }

        /// <summary>
        /// Equality comparison
        /// </summary>
        /// <param name="other">The other circle to compare</param>
        /// <returns>True if circle is equal to the other, otherwise false</returns>
        public bool Equals(Circle other)
        {
            return (Radius == other.Radius && Position == other.Position);
        }

        /// <summary>
        /// Check if this circle intersects (overlaps) other circle
        /// </summary>
        /// <param name="other">The other circle</param>
        /// <returns>true if the two circles intersect, otherwise false</returns>
        public bool Intersects(Circle other)
        {
            float distance = Vector2.Distance(other.Position, this.Position);
            return (distance < (other.Radius + this.Radius));
        }
        #endregion
    }
}
