using Codesmith.SmithNgine.Particles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Codesmith.smithNgine.ParticleEffectsEditor.Nodes
{
    class ParticleEmitterTreeNode : TreeNode
    {
        public ParticleEmitterTreeNode(ParticleEmitter emitter)
        {
            Text = emitter.Name;
            Tag = emitter;
            Nodes.Add(new ParticleGenerationParamsTreeNode(emitter.Configuration));
        }
    }
}
