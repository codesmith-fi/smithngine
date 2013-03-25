using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithNgine
{
    interface IRotatableObject
    {
        float Rotation
        {
            get;
            set;
        }

        event EventHandler<EventArgs> RotationChanged;
    }
}
