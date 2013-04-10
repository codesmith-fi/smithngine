using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithNgine.Gfx
{
    public class MenuEntryEventArgs : EventArgs
    {
        public int Id
        {
            get;
            internal set;
        }

        public MenuEntryEventArgs( int id )
        {
            Id = id;
        }
    }
}
