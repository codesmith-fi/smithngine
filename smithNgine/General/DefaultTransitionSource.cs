/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.General
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
