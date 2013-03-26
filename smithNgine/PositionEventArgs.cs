using System;
using Microsoft.Xna.Framework;

namespace Codesmith.SmithNgine
{
    public class PositionEventArgs : EventArgs
    {
        public Vector2 oldPosition = Vector2.Zero;
        public Vector2 newPosition = Vector2.Zero;
        public PositionEventArgs(Vector2 oldPosition, Vector2 newPosition)
        {
            this.oldPosition = oldPosition;
            this.newPosition = newPosition;
        }

        private PositionEventArgs() { }
    }
}
