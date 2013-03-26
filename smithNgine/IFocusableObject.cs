using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithNgine
{
    interface IFocusableObject
    {
        bool HasFocus
        {
            get;
        }

        void GainFocus();
        void LooseFocus();
    }
}
