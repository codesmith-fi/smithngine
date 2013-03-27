using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithNgine
{
    public interface IFocusableObject
    {
        bool HasFocus
        {
            get;
        }

        void GainFocus();
        void LooseFocus();

        event EventHandler<EventArgs> FocusGained;
        event EventHandler<EventArgs> FocusLost;
    }
}
