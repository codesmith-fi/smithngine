using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithNgine
{
    public interface IHoverableObject
    {
        bool IsHovered
        {
            get;
        }

        event EventHandler<HoverEventArgs> BeingHovered;
    }
}
