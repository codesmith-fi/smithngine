using Microsoft.Xna.Framework;
using System;

namespace Codesmith.SmithNgine
{
    public interface IMovableObject2D
    {
        // Movable 2D object has a position
        Vector2 Position
        {
            get;
            set;
        }

        // Event triggered when the position changes
        event EventHandler<PositionEventArgs> PositionChanged;
    }
}
