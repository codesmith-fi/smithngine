using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithNgine
{
    public abstract class ObjectBase : IActivatableObject
    {
        #region Fields
        private bool objectIsActive;
        #endregion

        #region Properties
        public ITransitionSource TransitionSource
        {
            get;
            set;
        }

        public bool ObjectIsActive
        {
            get { return objectIsActive; }
        }

        #endregion

        #region Events
        public event EventHandler<EventArgs> ObjectActivated;
        public event EventHandler<EventArgs> ObjectDeactivated;
        #endregion

        public ObjectBase()
        {
            TransitionSource = new DefaultTransitionSource();
        }

        public virtual void ActivateObject()
        {
            if (!ObjectIsActive && ObjectActivated != null)
            {
                ObjectActivated(this, EventArgs.Empty);
            }
            objectIsActive = true;
        }

        public virtual void DeactivateObject()
        {
            if (ObjectIsActive && ObjectDeactivated != null)
            {
                ObjectDeactivated(this, EventArgs.Empty);
            }
            objectIsActive = false;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Dismiss()
        {
        }
    }
}
