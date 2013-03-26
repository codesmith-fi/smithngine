using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithNgine
{
    public class DefaultTransitionSource : ITransitionSource
    {
        float value = 1.0f;
        public float TransitionValue
        {
            get { return 1.0f; }
        }

        public DefaultTransitionSource(float value = 1.0f)
        {
            this.value = value;
        }
    }
}
