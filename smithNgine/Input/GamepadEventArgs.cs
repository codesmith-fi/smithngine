/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.Input
{
    using Microsoft.Xna.Framework;
    using System;

    public class GamepadEventArgs : EventArgs
    {
        public PlayerIndex? Player;

        public GamepadEventArgs(PlayerIndex player)
        {
            Player = player;
        }
    }
}
