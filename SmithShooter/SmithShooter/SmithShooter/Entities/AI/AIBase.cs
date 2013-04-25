using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace Codesmith.SmithShooter.Entities.AI
{
    class HateItem : IEquatable<HateItem>
    {
        public Entity Target { get; set; }
        public float Hate { get; set; }

        public static int Comparer(HateItem a, HateItem b)
        {
            if (a.Hate > b.Hate)
            {
                return 1;
            }
            else if (a.Hate < b.Hate)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public HateItem(Entity e, float h)
        {
            Target = e;
            Hate = h;
        }

        public bool Equals(HateItem other)
        {
            return other.Target == this.Target;
        }
    }

    public abstract class AIBase
    {
        private List<HateItem> hateList;

        public float ActionAmount { get; set; }

        public Ship Owner
        {
            get;
            internal set;
        }

        public AIBase()
        {
            hateList = new List<HateItem>();
        }

        public void Hate(Entity entity, float amount)
        {
            Debug.Assert(entity != null, "Trying to hate null, it's not possible!:)");

            if(hateList.Contains( new HateItem(entity, amount) ))
            {
                HateItem existing = hateList.Find(e => e.Target == entity);
                existing.Hate += amount;
            }
            else
            {
                HateItem hate = new HateItem(entity, amount);
                hateList.Add(hate);
                hateList.Sort(HateItem.Comparer);
            }
        }

        public Entity MostHatedEntity()
        {
            Entity target = null;
            if (hateList.Count > 0)
            {
                target = hateList[0].Target;
            }
            return target;
        }

        public virtual void UpdateAI(GameTime gameTime)
        {
        }
    }
}
