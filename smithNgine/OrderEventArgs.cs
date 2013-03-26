// ***************************************************************************
// ** SmithNgine.OrderEventArgs                                             **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************
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
