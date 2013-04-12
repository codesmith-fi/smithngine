using Microsoft.Xna.Framework;
using System;

namespace Codesmith.SmithNgine.Input
{
    public class GamepadEventArgs : EventArgs
    {
        public PlayerIndex? Player;

        public GamepadEventArgs(PlayerIndex player)
        {
            Player = player;
        }
    }
}
