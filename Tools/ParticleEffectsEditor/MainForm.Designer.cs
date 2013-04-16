namespace Codesmith.smithNgine.ParticleEffectsEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uiVerticalSplitter = new System.Windows.Forms.SplitContainer();
            this.uiHorizSplitter = new System.Windows.Forms.SplitContainer();
            this.uiMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uiEffectPreview = new EffectPreviewControl();
            ((System.ComponentModel.ISupportInitialize)(this.uiVerticalSplitter)).BeginInit();
            this.uiVerticalSplitter.Panel2.SuspendLayout();
            this.uiVerticalSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiHorizSplitter)).BeginInit();
            this.uiHorizSplitter.SuspendLayout();
            this.uiMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // verticalSplitter
            // 
            this.uiVerticalSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiVerticalSplitter.Location = new System.Drawing.Point(0, 28);
            this.uiVerticalSplitter.Name = "verticalSplitter";
            // 
            // verticalSplitter.Panel2
            // 
            this.uiVerticalSplitter.Panel2.Controls.Add(this.uiHorizSplitter);
            this.uiVerticalSplitter.Size = new System.Drawing.Size(647, 458);
            this.uiVerticalSplitter.SplitterDistance = 215;
            this.uiVerticalSplitter.TabIndex = 0;
            // 
            // horizSplitter
            // 
            this.uiHorizSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiHorizSplitter.Location = new System.Drawing.Point(0, 0);
            this.uiHorizSplitter.Name = "horizSplitter";
            this.uiHorizSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.uiHorizSplitter.Size = new System.Drawing.Size(428, 458);
            this.uiHorizSplitter.SplitterDistance = 247;
            this.uiHorizSplitter.TabIndex = 0;
            // 
            // horizSplitter.Panel1
            //
            this.uiHorizSplitter.Panel1.Controls.Add(this.uiEffectPreview);

            // 
            // menuStrip1
            // 
            this.uiMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.uiMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.uiMenuStrip.Name = "menuStrip1";
            this.uiMenuStrip.Size = new System.Drawing.Size(647, 28);
            this.uiMenuStrip.TabIndex = 1;
            this.uiMenuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.exitToolStripMenuItem.Text = "Load...";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.saveToolStripMenuItem.Text = "Save...";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.loadToolStripMenuItem.Text = "Load...";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.newToolStripMenuItem.Text = "New";
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(152, 24);
            this.exitToolStripMenuItem1.Text = "Exit";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.aboutToolStripMenuItem.Text = "About";

            // Effect review control
            this.uiEffectPreview.Cursor = System.Windows.Forms.Cursors.Cross;
            this.uiEffectPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiEffectPreview.Location = new System.Drawing.Point(0, 0);
            this.uiEffectPreview.Name = "uiEffectPreview";
            this.uiEffectPreview.Effect = null;
            this.uiEffectPreview.Size = new System.Drawing.Size(554, 306);
            this.uiEffectPreview.TabIndex = 0;
            this.uiEffectPreview.Text = "Particle Preview";
            this.uiEffectPreview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.uiEffectPreview_MouseDown);
            this.uiEffectPreview.MouseMove += new System.Windows.Forms.MouseEventHandler(this.uiEffectPreview_MouseMove);
            this.uiEffectPreview.MouseUp += new System.Windows.Forms.MouseEventHandler(this.uiEffectPreview_MouseUp);
   
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 486);
            this.Controls.Add(this.uiVerticalSplitter);
            this.Controls.Add(this.uiMenuStrip);
            this.Name = "MainForm";
            this.Text = "Particle Effect Editor - smithNgine";
            this.uiVerticalSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiVerticalSplitter)).EndInit();
            this.uiVerticalSplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiHorizSplitter)).EndInit();
            this.uiHorizSplitter.ResumeLayout(false);
            this.uiMenuStrip.ResumeLayout(false);
            this.uiMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.SplitContainer uiVerticalSplitter;
        private System.Windows.Forms.SplitContainer uiHorizSplitter;
        private System.Windows.Forms.MenuStrip uiMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private EffectPreviewControl uiEffectPreview;

    }
}

