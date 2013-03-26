using System;
using Microsoft.Xna.Framework;

namespace Codesmith.SmithNgine.Input
{
    public class MouseButtonEventArgs : EventArgs
    {
        public bool left;
        public bool right;
        public bool middle;
        public int X;
        public int Y;

        public MouseButtonEventArgs()
        {
            this.left = false;
            this.right = false;
            this.middle = false;
        }

        public MouseButtonEventArgs(bool left, bool middle, bool right)
        {
            this.left = left;
            this.middle = middle;
            this.right = right;
        }
    }
}
