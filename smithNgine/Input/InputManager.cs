// ***************************************************************************
// ** GameStateManagement.InputManager                                      **
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
    public class InputManager
    {
        #region Fields/Attributes
        private const int MaxPlayers = 4;

        private List<KeyboardState> keyboardStates = new List<KeyboardState>(MaxPlayers);
        private List<GamePadState> gamepadStates = new List<GamePadState>(MaxPlayers);
        private List<KeyboardState> previousKeyboardStates = new List<KeyboardState>(MaxPlayers);
        private List<GamePadState> previousGamepadStates = new List<GamePadState>(MaxPlayers);
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public InputManager()
        {
            for (int i = 0; i < MaxPlayers; i++)
            {
                keyboardStates.Add(Keyboard.GetState((PlayerIndex)i));
                gamepadStates.Add(GamePad.GetState((PlayerIndex)i));
            }
        }
        #endregion 

        #region New methods
        public void Update()
        {
            // copy previous states so we know what is being done in current frame
            previousKeyboardStates = new List<KeyboardState>(keyboardStates);
            previousGamepadStates = new List<GamePadState>(gamepadStates);

            for (int i = 0; i < MaxPlayers; i++)
            {
                keyboardStates[i] = Keyboard.GetState((PlayerIndex)i);
                gamepadStates[i] = GamePad.GetState((PlayerIndex)i);
            }
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

        public bool IsButtonPressed(Buttons button, PlayerIndex? playerInControl, out PlayerIndex buttonSource)
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
        #endregion 
    }
}
