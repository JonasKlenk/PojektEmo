namespace EmotivEngine
{
    partial class MapperGUI
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
            this.buttonBind = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.ComboControllerID = new System.Windows.Forms.ComboBox();
            this.ComboControllableDeviceID = new System.Windows.Forms.ComboBox();
            this.listCommandTypes = new System.Windows.Forms.ListBox();
            this.listActionTypes = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMappingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonDeleteBind = new System.Windows.Forms.Button();
            this.listMapping = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonBind
            // 
            this.buttonBind.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonBind.Location = new System.Drawing.Point(318, 80);
            this.buttonBind.Name = "buttonBind";
            this.buttonBind.Size = new System.Drawing.Size(75, 23);
            this.buttonBind.TabIndex = 0;
            this.buttonBind.Text = "Bind";
            this.buttonBind.UseVisualStyleBackColor = true;
            this.buttonBind.Click += new System.EventHandler(this.buttonBind_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(552, 465);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(635, 465);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // ComboControllerID
            // 
            this.ComboControllerID.FormattingEnabled = true;
            this.ComboControllerID.Items.AddRange(new object[] {
            "a",
            "b",
            "c",
            "d",
            "e",
            "f",
            "g",
            "h",
            "i"});
            this.ComboControllerID.Location = new System.Drawing.Point(12, 40);
            this.ComboControllerID.Name = "ComboControllerID";
            this.ComboControllerID.Size = new System.Drawing.Size(300, 21);
            this.ComboControllerID.TabIndex = 3;
            // 
            // ComboControllableDeviceID
            // 
            this.ComboControllableDeviceID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboControllableDeviceID.FormattingEnabled = true;
            this.ComboControllableDeviceID.Location = new System.Drawing.Point(399, 40);
            this.ComboControllableDeviceID.Name = "ComboControllableDeviceID";
            this.ComboControllableDeviceID.Size = new System.Drawing.Size(309, 21);
            this.ComboControllableDeviceID.TabIndex = 5;
            // 
            // listCommandTypes
            // 
            this.listCommandTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listCommandTypes.FormattingEnabled = true;
            this.listCommandTypes.Location = new System.Drawing.Point(12, 80);
            this.listCommandTypes.Name = "listCommandTypes";
            this.listCommandTypes.Size = new System.Drawing.Size(300, 173);
            this.listCommandTypes.TabIndex = 6;
            this.listCommandTypes.SelectedIndexChanged += new System.EventHandler(this.listCommandTypes_SelectedIndexChanged);
            // 
            // listActionTypes
            // 
            this.listActionTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listActionTypes.FormattingEnabled = true;
            this.listActionTypes.Location = new System.Drawing.Point(399, 80);
            this.listActionTypes.Name = "listActionTypes";
            this.listActionTypes.Size = new System.Drawing.Size(309, 173);
            this.listActionTypes.TabIndex = 7;
            this.listActionTypes.SelectedIndexChanged += new System.EventHandler(this.listActionTypes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Controller";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(396, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Controllabel Device";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Commands";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(396, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Actions";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(720, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadMappingToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadMappingToolStripMenuItem
            // 
            this.loadMappingToolStripMenuItem.Name = "loadMappingToolStripMenuItem";
            this.loadMappingToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.loadMappingToolStripMenuItem.Text = "Load Mapping";
            // 
            // buttonDeleteBind
            // 
            this.buttonDeleteBind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteBind.Location = new System.Drawing.Point(12, 465);
            this.buttonDeleteBind.Name = "buttonDeleteBind";
            this.buttonDeleteBind.Size = new System.Drawing.Size(158, 23);
            this.buttonDeleteBind.TabIndex = 13;
            this.buttonDeleteBind.Text = "Delete Binding";
            this.buttonDeleteBind.UseVisualStyleBackColor = true;
            this.buttonDeleteBind.Click += new System.EventHandler(this.buttonDeleteBind_Click);
            // 
            // listMapping
            // 
            this.listMapping.FormattingEnabled = true;
            this.listMapping.Location = new System.Drawing.Point(12, 259);
            this.listMapping.Name = "listMapping";
            this.listMapping.Size = new System.Drawing.Size(696, 199);
            this.listMapping.TabIndex = 14;
            this.listMapping.SelectedIndexChanged += new System.EventHandler(this.listMapping_SelectedIndexChanged);
            // 
            // MapperGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 500);
            this.Controls.Add(this.listMapping);
            this.Controls.Add(this.buttonDeleteBind);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listActionTypes);
            this.Controls.Add(this.listCommandTypes);
            this.Controls.Add(this.ComboControllableDeviceID);
            this.Controls.Add(this.ComboControllerID);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonBind);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MapperGUI";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBind;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox ComboControllerID;
        private System.Windows.Forms.ComboBox ComboControllableDeviceID;
        private System.Windows.Forms.ListBox listCommandTypes;
        private System.Windows.Forms.ListBox listActionTypes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMappingToolStripMenuItem;
        private System.Windows.Forms.Button buttonDeleteBind;
        private System.Windows.Forms.ListBox listMapping;
    }
}

