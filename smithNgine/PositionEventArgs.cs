using System;
using Microsoft.Xna.Framework;

namespace Codesmith.SmithNgine
{
    public class PositionEventArgs : EventArgs
    {
        public Vector2 oldPosition;
        public Vector2 newPosition;
        public PositionEventArgs(Vector2 oldPosition, Vector2 newPosition)
        {
            this.oldPosition = oldPosition;
            this.newPosition = newPosition;
        }
    }
}
