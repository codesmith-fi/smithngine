// ***************************************************************************
// ** SmithNgine.Input.KeyboardEventArgs                                    **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************
using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Codesmith.SmithNgine.Input
{
    public class KeyboardEventArgs : EventArgs
    {
        // Array of pressed keys
        public Keys[] keys;
        public PlayerIndex? player;

        public KeyboardEventArgs(Keys[] keys, PlayerIndex? player)
        {
            this.keys = keys;
            this.player = player;
        }
    }
}
