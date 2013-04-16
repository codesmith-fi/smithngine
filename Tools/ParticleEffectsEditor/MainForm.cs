using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Codesmith.smithNgine.ParticleEffectsEditor.Nodes;


namespace Codesmith.smithNgine.ParticleEffectsEditor
{
    public partial class MainForm : Form
    {
        private TreeNode rootEffectTreeNode;

        public MainForm()
        {
            InitializeComponent();
        }

        private void uiEffectPreview_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
        }

        private void uiEffectPreview_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                uiEffectPreview.Effect.Position = new Vector2(e.Location.X, e.Location.Y);
            }
        }

        private void uiEffectPreview_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
        }

        private void uiEffectPreview_Click(object sender, EventArgs e)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            uiEffectConfigurationTreeView.Nodes.Clear();
            rootEffectTreeNode = new ParticleEffectTreeNode(uiEffectPreview.Effect);
            uiEffectConfigurationTreeView.Nodes.Add(rootEffectTreeNode);
        }

        private void uiEffectConfigurationTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            uiEffectPropertyGrid.SelectedObject = e.Node.Tag;
        }

        private void addEmitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
