using System;

namespace Codesmith.SmithNgine
{
    public class OrderEventArgs : EventArgs
    {
        float order = 0.0f;
        float oldOrder = 0.0f;
        public OrderEventArgs(float oldOrder, float newOrder)
        {
            this.order = newOrder;
            this.oldOrder = oldOrder;
        }

        private OrderEventArgs() { }
    }
}
