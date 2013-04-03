using System;

namespace Codesmith.SmithNgine.General
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
