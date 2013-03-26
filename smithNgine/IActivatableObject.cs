using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithNgine
{
    public interface IActivatableObject
    {
        bool ObjectIsActive
        {
            get;
        }

        void ActivateObject();
        void DeactivateObject();

        event EventHandler<EventArgs> ObjectActivated;
        event EventHandler<EventArgs> ObjectDeactivated;
    }
}
