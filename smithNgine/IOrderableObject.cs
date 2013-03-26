using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithNgine
{
    public interface IOrderableObject
    {
        // Drawing order
        float Order
        {
            get;
            set;
        }

        // Event triggered when the order changes
        event EventHandler<OrderEventArgs> OrderChanged;
    }
}
