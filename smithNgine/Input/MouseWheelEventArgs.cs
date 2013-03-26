using System;

namespace Codesmith.SmithNgine.Input
{
    public class MouseWheelEventArgs : EventArgs
    {
        public int oldValue;
        public int newValue;

        public MouseWheelEventArgs(int oldValue, int newValue)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        private MouseWheelEventArgs() { }
    }
}
