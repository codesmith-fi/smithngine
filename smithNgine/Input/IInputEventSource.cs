using System;

namespace Codesmith.SmithNgine.Input
{
    public interface IInputEventSource
    {
        event EventHandler<MousePositionEventArgs> MousePositionChanged;
        event EventHandler<MouseWheelEventArgs> MouseWheelChanged;
        event EventHandler<MouseButtonEventArgs> MouseButtonPressed;
        event EventHandler<KeyboardEventArgs> KeysPressed;
    }
}
