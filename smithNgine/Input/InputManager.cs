/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
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

    /// <summary>
    /// The input manager of the framework. Owned usually by the GameStateManager
    /// Listens to Keyboard, Mouse(touch) and GamePad changes.
    /// 
    /// Offers several events and methods to access the input states.
    /// </summary>
    public class InputManager : IInputEventSource
    {
        #region Fields
        private List<KeyboardState> keyboardStates;
        private List<GamePadState> gamepadStates;
        private List<KeyboardState> previousKeyboardStates;
        private List<GamePadState> previousGamepadStates;
        private MouseState mouseState;
        private MouseState previousMouseState;
        #endregion

        #region Properties
        /// <summary>
        /// Set or get the number of max players managed by the input manager
        /// XNA, don't use more than 4.
        /// </summary>
        public int MaxPlayers
        {
            set;
            get;
        }

        /// <summary>
        /// Get the current mouse scrollwheelvalue
        /// </summary>
        public int ScrollWheelValue
        {
            get { return mouseState.ScrollWheelValue; }
        }

        /// <summary>
        /// Get the current X coordinate of the mouse pointer
        /// </summary>
        public int MouseX
        {
            get { return mouseState.X; }
        }

        /// <summary>
        /// Get the current Y coordinate of the mouse pointer
        /// </summary>
        public int MouseY
        {
            get { return mouseState.Y; }
        }
        #endregion

        #region Events
        public event EventHandler<GamepadEventArgs> GamepadConnected;
        public event EventHandler<GamepadEventArgs> GamepadDisconnected;
//        public event EventHandler<GamepadEventArgs> GamepadButtonPressed;
        public event EventHandler<MouseEventArgs> MousePositionChanged;
        public event EventHandler<MouseEventArgs> MouseWheelChanged;
        public event EventHandler<MouseEventArgs> MouseButtonPressed;
        public event EventHandler<MouseEventArgs> MouseButtonReleased;
        public event EventHandler<KeyboardEventArgs> KeysPressed;
        #endregion

        #region Constructors
        public InputManager()
        {
            MaxPlayers = 4;
            keyboardStates = new List<KeyboardState>(MaxPlayers);
            previousKeyboardStates = new List<KeyboardState>(MaxPlayers);
            gamepadStates = new List<GamePadState>(MaxPlayers);
            previousGamepadStates = new List<GamePadState>(MaxPlayers);
            for (int i = 0; i < MaxPlayers; i++)
            {
                keyboardStates.Add(Keyboard.GetState((PlayerIndex)i));
                gamepadStates.Add(GamePad.GetState((PlayerIndex)i));
            }
            mouseState = Mouse.GetState();
        }
        #endregion 

        #region New methods
        public PlayerIndex? GetConnectedGamePad()
        {
            if (gamepadStates[0].IsConnected)
            {
                return PlayerIndex.One;
            }
            else if (gamepadStates[1].IsConnected)
            {
                return PlayerIndex.Two;
            }
            else if (gamepadStates[2].IsConnected)
            {
                return PlayerIndex.Three;
            }
            else if (gamepadStates[3].IsConnected)
            {
                return PlayerIndex.Four;
            }
            else
            {
                return null;
            }
        }

        public GamePadState GetGamePadState(PlayerIndex? index, bool current = true)
        {
            if (index.HasValue)
            {
                int i = (int)index;
                if (i >= MaxPlayers)
                {
                    throw new ArgumentOutOfRangeException("GetGamePadState() player index " + i + " out of range");
                }
                return current ? gamepadStates[i] : previousGamepadStates[i];
            }
            else
            {
                throw new ArgumentNullException("Invalid argument");
            }
        }

        public void Update()
        {
            // copy previous states so we know what is being done in current frame
            previousKeyboardStates = new List<KeyboardState>(keyboardStates);
            previousGamepadStates = new List<GamePadState>(gamepadStates);
            previousMouseState = mouseState;

            for (int i = 0; i < MaxPlayers; i++)
            {
                keyboardStates[i] = Keyboard.GetState((PlayerIndex)i);
                if (KeysPressed != null)
                {
                    Keys[] keys = keyboardStates[i].GetPressedKeys();
                    if (keys.Length > 0)
                    {
                        KeyboardEventArgs args = new KeyboardEventArgs(keys, (PlayerIndex)i);
                        KeysPressed(this, args);
                    }
                }

                gamepadStates[i] = GamePad.GetState((PlayerIndex)i);
                // Report gamepad connect to observers
                if( gamepadStates[i].IsConnected && !previousGamepadStates[i].IsConnected)
                {
                    GamepadEventArgs args = new GamepadEventArgs((PlayerIndex)i);
                    GamepadConnected(this, args);
                }
                // Report gamepad disconnect to observers
                else if (!gamepadStates[i].IsConnected && previousGamepadStates[i].IsConnected)
                {
                    GamepadEventArgs args = new GamepadEventArgs((PlayerIndex)i);
                    GamepadDisconnected(this, args);
                }
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

        public bool IsKeyHeld(Keys key, PlayerIndex? playerInControl, out PlayerIndex keySource)
        {
            keySource = PlayerIndex.One;
            if (playerInControl.HasValue)
            {
                keySource = playerInControl.Value;
                int index = (int)playerInControl;
                return (keyboardStates[index].IsKeyDown(key) && previousKeyboardStates[index].IsKeyDown(key));
            }
            else
            {
                for (int i = 0; i < MaxPlayers; i++)
                {
                    if (keyboardStates[i].IsKeyDown(key) && previousKeyboardStates[i].IsKeyDown(key))
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

        public bool IsMouseButtonReleased(MouseButton button, ButtonState checkAgainst = ButtonState.Pressed)
        {
            // Left button was pressed after previous check
            if (button == MouseButton.Left &&
                previousMouseState.LeftButton == checkAgainst &&
                mouseState.LeftButton == ButtonState.Released)
            {
                return true;
            }
            // Right button was pressed after previous check
            else if (button == MouseButton.Right &&
                previousMouseState.RightButton == checkAgainst &&
                mouseState.RightButton == ButtonState.Released)
            {
                return true;
            }
            // Middle button was pressed
            else if (button == MouseButton.Middle &&
                previousMouseState.MiddleButton == checkAgainst &&
                mouseState.MiddleButton == ButtonState.Released)
            {
                return true;
            }
            return false;
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
            // Notify mouse position listeners
            if (MousePositionChanged != null)
            {
                if (!(previousMouseState.X == mouseState.X && previousMouseState.Y == mouseState.Y))
                {
                    MouseEventArgs args = new MouseEventArgs(previousMouseState, mouseState);
                    MousePositionChanged(this, args);
                }
            }

            if (MouseButtonReleased != null)
            {
                if (IsMouseButtonReleased(MouseButton.Left) ||
                    IsMouseButtonReleased(MouseButton.Middle) ||
                    IsMouseButtonReleased(MouseButton.Right))
                {
                    MouseEventArgs args = new MouseEventArgs(previousMouseState, mouseState);
                    MouseButtonReleased(this, args);
                }
            }

            // Notify mouse button listeners
            if (MouseButtonPressed != null)
            {
                if (IsMouseButtonPressed(MouseButton.Left) || 
                    IsMouseButtonPressed(MouseButton.Middle) || 
                    IsMouseButtonPressed(MouseButton.Right))
                {
                    MouseEventArgs args = new MouseEventArgs(previousMouseState, mouseState);
                    MouseButtonPressed(this, args);
                }
            }

            // Notify mouse wheel rotation listeners
            if (MouseWheelChanged != null)
            {
                if (previousMouseState.ScrollWheelValue != mouseState.ScrollWheelValue)
                {
                    MouseEventArgs args = new MouseEventArgs(previousMouseState, mouseState);
                    MouseWheelChanged(this, args);
                }
            }

        }
        #endregion

    }
}
