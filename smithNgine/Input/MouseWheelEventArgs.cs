// ***************************************************************************
// ** SmithNgine.Input.MouseWheelEventArgs                                  **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************

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
