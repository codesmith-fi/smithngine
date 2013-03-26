using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithNgine.Input
{
    public class MouseButtonEventArgs : EventArgs
    {
        public bool left;
        public bool right;
        public bool middle;

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
