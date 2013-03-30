using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithNgine.Input
{
    public class MouseParameters
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }
        public bool LeftButton { get; internal set; }
        public bool RightButton { get; internal set; }
        public bool MiddleButton { get; internal set; }
        public int WheelValue { get; internal set; }
        public Vector2 Position
        {
            get
            {
                return new Vector2(X, Y);
            }
        }
    }

    public class MouseEventArgs : EventArgs
    {
        public MouseParameters PreviousState;
        public MouseParameters State;

        public MouseEventArgs(MouseState previous, MouseState current)
        {
            PreviousState = FromMouseState(previous);
            State = FromMouseState(current);
        }

        private MouseParameters FromMouseState(MouseState state)
        {
            MouseParameters param = new MouseParameters();
            param.X = state.X;
            param.Y = state.Y;
            param.LeftButton = (state.LeftButton == ButtonState.Pressed);
            param.RightButton = (state.RightButton == ButtonState.Pressed);
            param.MiddleButton = (state.MiddleButton == ButtonState.Pressed);
            param.WheelValue = state.ScrollWheelValue;
            return param;
        }
    }
}
