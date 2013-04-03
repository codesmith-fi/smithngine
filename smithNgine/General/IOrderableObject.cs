// ***************************************************************************
// ** SmithNgine.IOrderableObject                                           **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************
using System;

namespace Codesmith.SmithNgine.General
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
