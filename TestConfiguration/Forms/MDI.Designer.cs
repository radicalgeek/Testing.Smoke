﻿namespace TestConfiguration.Forms
{
    partial class MDI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDI));
            this.imgLstMenuIcons = new System.Windows.Forms.ImageList(this.components);
            this.tspMainTools = new System.Windows.Forms.ToolStrip();
            this.tsbNewConfiguration = new System.Windows.Forms.ToolStripButton();
            this.tsbNewConfigurationWithTemplate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOpenConfigurationFile = new System.Windows.Forms.ToolStripButton();
            this.mnuMainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewConfigurationWithTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOpenConfigurationFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tspMainTools.SuspendLayout();
            this.mnuMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgLstMenuIcons
            // 
            this.imgLstMenuIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLstMenuIcons.ImageStream")));
            this.imgLstMenuIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLstMenuIcons.Images.SetKeyName(0, "09.png");
            this.imgLstMenuIcons.Images.SetKeyName(1, "10.png");
            this.imgLstMenuIcons.Images.SetKeyName(2, "11.png");
            // 
            // tspMainTools
            // 
            this.tspMainTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tspMainTools.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.tspMainTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewConfiguration,
            this.tsbNewConfigurationWithTemplate,
            this.toolStripSeparator2,
            this.tsbOpenConfigurationFile});
            this.tspMainTools.Location = new System.Drawing.Point(0, 24);
            this.tspMainTools.Name = "tspMainTools";
            this.tspMainTools.Size = new System.Drawing.Size(691, 55);
            this.tspMainTools.TabIndex = 3;
            this.tspMainTools.Text = "toolStrip1";
            // 
            // tsbNewConfiguration
            // 
            this.tsbNewConfiguration.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNewConfiguration.Image = global::TestConfiguration.Properties.Resources._10;
            this.tsbNewConfiguration.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewConfiguration.Name = "tsbNewConfiguration";
            this.tsbNewConfiguration.Size = new System.Drawing.Size(52, 52);
            this.tsbNewConfiguration.Text = "New Configuration...";
            this.tsbNewConfiguration.Click += new System.EventHandler(this.mnuNewConfiguration_Click);
            // 
            // tsbNewConfigurationWithTemplate
            // 
            this.tsbNewConfigurationWithTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNewConfigurationWithTemplate.Image = global::TestConfiguration.Properties.Resources._09;
            this.tsbNewConfigurationWithTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewConfigurationWithTemplate.Name = "tsbNewConfigurationWithTemplate";
            this.tsbNewConfigurationWithTemplate.Size = new System.Drawing.Size(52, 52);
            this.tsbNewConfigurationWithTemplate.Text = "New Configuration With Template...";
            this.tsbNewConfigurationWithTemplate.Click += new System.EventHandler(this.mnuNewConfigurationWithTemplate_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 55);
            // 
            // tsbOpenConfigurationFile
            // 
            this.tsbOpenConfigurationFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpenConfigurationFile.Image = global::TestConfiguration.Properties.Resources._11;
            this.tsbOpenConfigurationFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenConfigurationFile.Name = "tsbOpenConfigurationFile";
            this.tsbOpenConfigurationFile.Size = new System.Drawing.Size(52, 52);
            this.tsbOpenConfigurationFile.Text = "Open Configuration File...";
            this.tsbOpenConfigurationFile.Click += new System.EventHandler(this.mnuOpenConfigurationFile_Click);
            // 
            // mnuMainMenu
            // 
            this.mnuMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuHelp});
            this.mnuMainMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMainMenu.Name = "mnuMainMenu";
            this.mnuMainMenu.Size = new System.Drawing.Size(691, 24);
            this.mnuMainMenu.TabIndex = 4;
            this.mnuMainMenu.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewConfiguration,
            this.mnuNewConfigurationWithTemplate,
            this.toolStripSeparator1,
            this.mnuOpenConfigurationFile,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuNewConfiguration
            // 
            this.mnuNewConfiguration.Image = global::TestConfiguration.Properties.Resources._10;
            this.mnuNewConfiguration.Name = "mnuNewConfiguration";
            this.mnuNewConfiguration.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuNewConfiguration.Size = new System.Drawing.Size(340, 22);
            this.mnuNewConfiguration.Text = "New Configuration...";
            this.mnuNewConfiguration.Click += new System.EventHandler(this.mnuNewConfiguration_Click);
            // 
            // mnuNewConfigurationWithTemplate
            // 
            this.mnuNewConfigurationWithTemplate.Image = global::TestConfiguration.Properties.Resources._09;
            this.mnuNewConfigurationWithTemplate.Name = "mnuNewConfigurationWithTemplate";
            this.mnuNewConfigurationWithTemplate.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.mnuNewConfigurationWithTemplate.Size = new System.Drawing.Size(340, 22);
            this.mnuNewConfigurationWithTemplate.Text = "New Configuration With Template...";
            this.mnuNewConfigurationWithTemplate.Click += new System.EventHandler(this.mnuNewConfigurationWithTemplate_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(262, 6);
            // 
            // mnuOpenConfigurationFile
            // 
            this.mnuOpenConfigurationFile.Image = global::TestConfiguration.Properties.Resources._11;
            this.mnuOpenConfigurationFile.Name = "mnuOpenConfigurationFile";
            this.mnuOpenConfigurationFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuOpenConfigurationFile.Size = new System.Drawing.Size(340, 22);
            this.mnuOpenConfigurationFile.Text = "Open Configuration File...";
            this.mnuOpenConfigurationFile.Click += new System.EventHandler(this.mnuOpenConfigurationFile_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "Help";
            this.mnuHelp.Visible = false;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(337, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(340, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // MDI
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(691, 658);
            this.Controls.Add(this.tspMainTools);
            this.Controls.Add(this.mnuMainMenu);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuMainMenu;
            this.Name = "MDI";
            this.Text = "Test Configuration Tool";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MDI_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MDI_DragEnter);
            this.tspMainTools.ResumeLayout(false);
            this.tspMainTools.PerformLayout();
            this.mnuMainMenu.ResumeLayout(false);
            this.mnuMainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imgLstMenuIcons;
        private System.Windows.Forms.ToolStrip tspMainTools;
        private System.Windows.Forms.MenuStrip mnuMainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuNewConfiguration;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenConfigurationFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuNewConfigurationWithTemplate;
        private System.Windows.Forms.ToolStripButton tsbNewConfiguration;
        private System.Windows.Forms.ToolStripButton tsbNewConfigurationWithTemplate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbOpenConfigurationFile;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}