using Microsoft.Xna.Framework;
using System;

namespace Codesmith.SmithNgine.Gfx
{
    public class MenuEntryEventArgs : EventArgs
    {
        public PlayerIndex? ControllingPlayer
        {
            get;
            internal set;
        }

        public MenuEntryEventArgs(PlayerIndex? index)
        {
            ControllingPlayer = index;
        }
    }
}
