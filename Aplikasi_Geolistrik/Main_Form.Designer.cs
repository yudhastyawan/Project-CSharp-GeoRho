namespace Aplikasi_Geolistrik
{
    partial class Main_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Form));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vESSchlumbergerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mappingWennerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wennerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dipoleDipoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(716, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vESSchlumbergerToolStripMenuItem,
            this.mappingWennerToolStripMenuItem,
            this.imagingToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // vESSchlumbergerToolStripMenuItem
            // 
            this.vESSchlumbergerToolStripMenuItem.Name = "vESSchlumbergerToolStripMenuItem";
            this.vESSchlumbergerToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.vESSchlumbergerToolStripMenuItem.Text = "VES (Schlumberger)";
            this.vESSchlumbergerToolStripMenuItem.Click += new System.EventHandler(this.vESSchlumbergerToolStripMenuItem_Click);
            // 
            // mappingWennerToolStripMenuItem
            // 
            this.mappingWennerToolStripMenuItem.Name = "mappingWennerToolStripMenuItem";
            this.mappingWennerToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.mappingWennerToolStripMenuItem.Text = "Mapping (Wenner)";
            this.mappingWennerToolStripMenuItem.Click += new System.EventHandler(this.mappingWennerToolStripMenuItem_Click);
            // 
            // imagingToolStripMenuItem
            // 
            this.imagingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wennerToolStripMenuItem,
            this.dipoleDipoleToolStripMenuItem});
            this.imagingToolStripMenuItem.Name = "imagingToolStripMenuItem";
            this.imagingToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.imagingToolStripMenuItem.Text = "Imaging";
            // 
            // wennerToolStripMenuItem
            // 
            this.wennerToolStripMenuItem.Name = "wennerToolStripMenuItem";
            this.wennerToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.wennerToolStripMenuItem.Text = "Wenner";
            this.wennerToolStripMenuItem.Click += new System.EventHandler(this.wennerToolStripMenuItem_Click);
            // 
            // dipoleDipoleToolStripMenuItem
            // 
            this.dipoleDipoleToolStripMenuItem.Name = "dipoleDipoleToolStripMenuItem";
            this.dipoleDipoleToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.dipoleDipoleToolStripMenuItem.Text = "Dipole-Dipole";
            this.dipoleDipoleToolStripMenuItem.Click += new System.EventHandler(this.dipoleDipoleToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 313);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(732, 352);
            this.Name = "Main_Form";
            this.Text = "GeoRho 1.0 Beta";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_Form_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vESSchlumbergerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mappingWennerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wennerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dipoleDipoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

