using Codesmith.SmithNgine.Particles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Codesmith.smithNgine.ParticleEffectsEditor.Nodes
{
    class ParticleEffectTreeNode : TreeNode
    {
        public ParticleEffectTreeNode(ParticleEffect effect)
        {
            if (effect == null)
            {
                throw new ArgumentException("Particle effect is null");
            }

            Text = effect.Name;
            Tag = effect;

            // Add each emitter as a new subnode
            foreach (ParticleEmitter em in effect.Emitters)
            {
                Nodes.Add(new ParticleEmitterTreeNode(em));
            }
        }
    }
}
