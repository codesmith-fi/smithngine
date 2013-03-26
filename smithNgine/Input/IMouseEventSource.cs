using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithNgine.Input
{
    public interface IMouseEventSource
    {
        event EventHandler<MousePositionEventArgs> MousePositionChanged;
        event EventHandler<MouseWheelEventArgs> MouseWheelChanged;
        event EventHandler<MouseButtonEventArgs> MouseButtonPressed;
    }
}
