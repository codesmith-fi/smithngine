using System;
using Microsoft.Xna.Framework;

namespace Codesmith.SmithNgine.General
{
    public interface IActivatableObject
    {
        bool ObjectIsActive
        {
            get;
        }

        void ActivateObject();
        void DeactivateObject();
        void Dismiss();
        void Update(GameTime gameTime);

        event EventHandler<EventArgs> ObjectActivated;
        event EventHandler<EventArgs> ObjectDeactivated;
    }
}
