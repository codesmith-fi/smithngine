// ***************************************************************************
// ** SmithNgine.Input.InputManager                                         **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************

#region Using statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace Codesmith.SmithNgine.Input
{
    #region Types and Enums
    public enum MouseButton
    {
        Left = 0,
        Middle,
        Right
    }
    #endregion

    public class InputManager : IMouseEventSource
    {
        #region Fields/Attributes
        private const int MaxPlayers = 4;

        private List<KeyboardState> keyboardStates = new List<KeyboardState>(MaxPlayers);
        private List<GamePadState> gamepadStates = new List<GamePadState>(MaxPlayers);
        private List<KeyboardState> previousKeyboardStates = new List<KeyboardState>(MaxPlayers);
        private List<GamePadState> previousGamepadStates = new List<GamePadState>(MaxPlayers);
        private MouseState mouseState;
        private MouseState previousMouseState;
        #endregion

        #region Properties
        public int ScrollWheelValue
        {
            get { return mouseState.ScrollWheelValue; }
        }

        public int MouseX
        {
            get { return mouseState.X; }
        }

        public int MouseY
        {
            get { return mouseState.Y; }
        }
        #endregion

        #region Events
        public event EventHandler<MousePositionEventArgs> MousePositionChanged;
        public event EventHandler<MouseWheelEventArgs> MouseWheelChanged;
        public event EventHandler<MouseButtonEventArgs> MouseButtonPressed;
        #endregion

        #region Constructors
        public InputManager()
        {
            for (int i = 0; i < MaxPlayers; i++)
            {
                keyboardStates.Add(Keyboard.GetState((PlayerIndex)i));
                gamepadStates.Add(GamePad.GetState((PlayerIndex)i));
            }
            mouseState = Mouse.GetState();
        }
        #endregion 

        #region New methods
        public void Update()
        {
            // copy previous states so we know what is being done in current frame
            previousKeyboardStates = new List<KeyboardState>(keyboardStates);
            previousGamepadStates = new List<GamePadState>(gamepadStates);
            previousMouseState = mouseState;
            
            for (int i = 0; i < MaxPlayers; i++)
            {
                keyboardStates[i] = Keyboard.GetState((PlayerIndex)i);
                gamepadStates[i] = GamePad.GetState((PlayerIndex)i);
            }
            mouseState = Mouse.GetState();

            HandleEvents();
        }

        public bool IsKeyPressed(Keys key, PlayerIndex? playerInControl, out PlayerIndex keySource)
        {
            keySource = PlayerIndex.One;
            if (playerInControl.HasValue)
            {
                keySource = playerInControl.Value;
                int index = (int)playerInControl;
                return (keyboardStates[index].IsKeyDown(key) && previousKeyboardStates[index].IsKeyUp(key));
            }
            else
            {
                for (int i = 0; i < MaxPlayers; i++)
                {
                    if (keyboardStates[i].IsKeyDown(key) && previousKeyboardStates[i].IsKeyUp(key))
                    {
                        keySource = (PlayerIndex)i;
                        return true;
                    }
                }
                return false;
            }
        }

        public bool IsGamePadButtonPressed(Buttons button, PlayerIndex? playerInControl, out PlayerIndex buttonSource)
        {
            buttonSource = PlayerIndex.One;
            if (playerInControl.HasValue)
            {
                buttonSource = playerInControl.Value;
                int index = (int)playerInControl;
                return (gamepadStates[index].IsButtonDown(button) && previousGamepadStates[index].IsButtonUp(button));
            }
            else
            {
                for (int i = 0; i < MaxPlayers; i++)
                {
                    if (gamepadStates[i].IsButtonDown(button) && previousGamepadStates[i].IsButtonUp(button))
                    {
                        buttonSource = (PlayerIndex)i; 
                        return true;
                    }
                }
                return false;
            }
        }

        // Checks if mouse button was being pressed
        // If param "checkAgainst" is set to ButtonState.Pressed checks if the
        // button is being held down. 
        public bool IsMouseButtonPressed(MouseButton button, ButtonState checkAgainst = ButtonState.Released )
        {
            // Left button was pressed after previous check
            if (button == MouseButton.Left &&
                previousMouseState.LeftButton == checkAgainst &&
                mouseState.LeftButton == ButtonState.Pressed )
            {
                return true;
            }
            // Right button was pressed after previous check
            else if (button == MouseButton.Right &&
                previousMouseState.RightButton == checkAgainst &&
                mouseState.RightButton == ButtonState.Pressed)
            {
                return true;
            }
            // Middle button was pressed
            else if (button == MouseButton.Middle &&
                previousMouseState.MiddleButton == checkAgainst &&
                mouseState.MiddleButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        // Helper which checks if button is being pressed between consecutive checks
        public bool IsMouseButtonHeld(MouseButton button)
        {
            return IsMouseButtonPressed(button, ButtonState.Pressed);
        }

        // Check if the mouse wheel was rotated
        public bool IsMouseWheelRotated()
        {
            return (mouseState.ScrollWheelValue != previousMouseState.ScrollWheelValue);
        }
        #endregion 

        #region Private new methods
        private void HandleEvents()
        {
            // Notify mouse button listeners
            if (MouseButtonPressed != null)
            {
                MouseButtonEventArgs args = new MouseButtonEventArgs();
                args.left = IsMouseButtonPressed(MouseButton.Left);
                args.right = IsMouseButtonPressed(MouseButton.Right);
                args.middle = IsMouseButtonPressed(MouseButton.Middle);
                if (args.left || args.middle || args.right)
                {
                    args.X = mouseState.X;
                    args.Y = mouseState.Y;
                    MouseButtonPressed(this, args);
                }
            }

            // Notify mouse position listeners
            if (MousePositionChanged != null)
            {
                if (!(previousMouseState.X == mouseState.X && previousMouseState.Y == mouseState.Y))
                {
                    MousePositionEventArgs args = new MousePositionEventArgs(
                        previousMouseState.X, previousMouseState.Y,
                        mouseState.X, mouseState.Y);
                    MousePositionChanged(this, args);
                }
            }

            // Notify mouse wheel rotation listeners
            if (MouseWheelChanged != null)
            {
                if (previousMouseState.ScrollWheelValue != mouseState.ScrollWheelValue)
                {
                    MouseWheelEventArgs args = new MouseWheelEventArgs(
                        previousMouseState.ScrollWheelValue, mouseState.ScrollWheelValue);
                    MouseWheelChanged(this, args);
                }
            }

        }
        #endregion
    }
}
