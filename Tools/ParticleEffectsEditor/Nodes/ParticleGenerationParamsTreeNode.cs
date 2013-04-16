using Codesmith.SmithNgine.Particles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Codesmith.smithNgine.ParticleEffectsEditor.Nodes
{
    class ParticleGenerationParamsTreeNode : TreeNode
    {
        public ParticleGenerationParamsTreeNode(ParticleGenerationParams config)
        {
            Text = "Parameters";
            Tag = config;
        }
    }
}
