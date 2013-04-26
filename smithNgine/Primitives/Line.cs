/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
namespace Codesmith.SmithNgine.Primitives
{
    using Microsoft.Xna.Framework;
    using System;

    /// <summary>
    /// Implements a line primitive
    /// Line has two vectors for start and end point
    /// </summary>
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
