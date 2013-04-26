/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.Input
{
    using System;

    public interface IInputEventSource
    {
        event EventHandler<MouseEventArgs> MousePositionChanged;
        event EventHandler<MouseEventArgs> MouseWheelChanged;
        event EventHandler<MouseEventArgs> MouseButtonPressed;
        event EventHandler<MouseEventArgs> MouseButtonReleased;
        event EventHandler<KeyboardEventArgs> KeysPressed;
    }
}
