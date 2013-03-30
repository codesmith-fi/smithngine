using System;

namespace Codesmith.SmithNgine.Input
{
    public interface IInputEventSource
    {
        event EventHandler<MouseEventArgs> MousePositionChanged;
        event EventHandler<MouseEventArgs> MouseWheelChanged;
        event EventHandler<MouseEventArgs> MouseButtonPressed;
        event EventHandler<MouseEventArgs> MouseButtonHeld; 
        event EventHandler<KeyboardEventArgs> KeysPressed;
    }
}
