using System;

namespace Codesmith.SmithNgine
{
    public class RotationEventArgs : EventArgs
    {
        float oldRotation = 0.0f;
        float rotation = 0.0f;
        public RotationEventArgs(float oldRotation, float newRotation)
        {
            this.oldRotation = oldRotation;
            this.rotation = newRotation;
        }

        private RotationEventArgs() { }
    }
}
