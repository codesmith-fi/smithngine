using Microsoft.Xna.Framework;
using System;

namespace Codesmith.SmithNgine.Gfx
{
    public class DragEventArgs : EventArgs
    {
        public Vector2 PositionDelta
        {
            get;
            set;
        }

        public DragEventArgs(Vector2 delta)
        {
            PositionDelta = delta;
        }
    }
}
