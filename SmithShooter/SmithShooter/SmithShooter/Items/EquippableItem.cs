using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codesmith.SmithShooter.Entities;

namespace Codesmith.SmithShooter.Items
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class EquippableItem
    {
        public Entity Owner
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public EquippableItem(String name, Entity owner = null )
        {
            Name = name;
            Owner= owner;
        }

        public virtual bool UpdateItem(GameTime gameTime)
        {
            return true;
        }
    }
}
