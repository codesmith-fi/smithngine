using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithNgine
{
    public abstract class ObjectBase
    {
        #region Properties
        public ITransitionSource TransitionSource
        {
            get;
            set;
        }
        #endregion

        public ObjectBase()
        {
            TransitionSource = new DefaultTransitionSource();
        }
    }
}
