using System;

namespace Codesmith.SmithNgine.General
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
