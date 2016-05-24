namespace EmotivEngine
{
    partial class MainWindow
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
            this.toggleStartStop = new System.Windows.Forms.Button();
            this.log = new System.Windows.Forms.TextBox();
            this.resetLog = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.comboBoxSelectController = new System.Windows.Forms.ComboBox();
            this.comboBoxSelectControllable = new System.Windows.Forms.ComboBox();
            this.labelController = new System.Windows.Forms.Label();
            this.labelDevice = new System.Windows.Forms.Label();
            this.labelMapping = new System.Windows.Forms.Label();
            this.comboBoxSelectMap = new System.Windows.Forms.ComboBox();
            this.btnAddMapping = new System.Windows.Forms.Button();
            this.btnDelMapping = new System.Windows.Forms.Button();
            this.btnEditMapping = new System.Windows.Forms.Button();
            this.btnBind = new System.Windows.Forms.Button();
            this.listViewCurrentBindings = new System.Windows.Forms.ListView();
            this.columnController = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDevice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMap = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // toggleStartStop
            // 
            this.toggleStartStop.Location = new System.Drawing.Point(15, 78);
            this.toggleStartStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.toggleStartStop.Name = "toggleStartStop";
            this.toggleStartStop.Size = new System.Drawing.Size(267, 62);
            this.toggleStartStop.TabIndex = 1;
            this.toggleStartStop.Text = "Start Engine";
            this.toggleStartStop.UseVisualStyleBackColor = true;
            this.toggleStartStop.Click += new System.EventHandler(this.toggleStartStop_Click);
            // 
            // log
            // 
            this.log.AcceptsReturn = true;
            this.log.Location = new System.Drawing.Point(297, 15);
            this.log.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.log.Size = new System.Drawing.Size(321, 310);
            this.log.TabIndex = 4;
            this.log.Text = "No Events";
            // 
            // resetLog
            // 
            this.resetLog.Location = new System.Drawing.Point(297, 334);
            this.resetLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.resetLog.Name = "resetLog";
            this.resetLog.Size = new System.Drawing.Size(323, 62);
            this.resetLog.TabIndex = 5;
            this.resetLog.Text = "Reset Log";
            this.resetLog.UseVisualStyleBackColor = true;
            this.resetLog.Click += new System.EventHandler(this.resetLog_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.BackColor = System.Drawing.Color.Red;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(16, 15);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(265, 59);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "Stopped";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxSelectController
            // 
            this.comboBoxSelectController.FormattingEnabled = true;
            this.comboBoxSelectController.Location = new System.Drawing.Point(15, 181);
            this.comboBoxSelectController.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxSelectController.Name = "comboBoxSelectController";
            this.comboBoxSelectController.Size = new System.Drawing.Size(265, 24);
            this.comboBoxSelectController.TabIndex = 6;
            // 
            // comboBoxSelectControllable
            // 
            this.comboBoxSelectControllable.FormattingEnabled = true;
            this.comboBoxSelectControllable.Location = new System.Drawing.Point(15, 246);
            this.comboBoxSelectControllable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxSelectControllable.Name = "comboBoxSelectControllable";
            this.comboBoxSelectControllable.Size = new System.Drawing.Size(265, 24);
            this.comboBoxSelectControllable.TabIndex = 7;
            // 
            // labelController
            // 
            this.labelController.AutoSize = true;
            this.labelController.Location = new System.Drawing.Point(16, 161);
            this.labelController.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelController.Name = "labelController";
            this.labelController.Size = new System.Drawing.Size(69, 17);
            this.labelController.TabIndex = 8;
            this.labelController.Text = "Controller";
            // 
            // labelDevice
            // 
            this.labelDevice.AutoSize = true;
            this.labelDevice.Location = new System.Drawing.Point(17, 226);
            this.labelDevice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDevice.Name = "labelDevice";
            this.labelDevice.Size = new System.Drawing.Size(51, 17);
            this.labelDevice.TabIndex = 9;
            this.labelDevice.Text = "Device";
            // 
            // labelMapping
            // 
            this.labelMapping.AutoSize = true;
            this.labelMapping.Location = new System.Drawing.Point(17, 287);
            this.labelMapping.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMapping.Name = "labelMapping";
            this.labelMapping.Size = new System.Drawing.Size(62, 17);
            this.labelMapping.TabIndex = 11;
            this.labelMapping.Text = "Mapping";
            // 
            // comboBoxSelectMap
            // 
            this.comboBoxSelectMap.FormattingEnabled = true;
            this.comboBoxSelectMap.Location = new System.Drawing.Point(15, 306);
            this.comboBoxSelectMap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxSelectMap.Name = "comboBoxSelectMap";
            this.comboBoxSelectMap.Size = new System.Drawing.Size(265, 24);
            this.comboBoxSelectMap.TabIndex = 10;
            // 
            // btnAddMapping
            // 
            this.btnAddMapping.Location = new System.Drawing.Point(93, 350);
            this.btnAddMapping.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddMapping.Name = "btnAddMapping";
            this.btnAddMapping.Size = new System.Drawing.Size(48, 46);
            this.btnAddMapping.TabIndex = 12;
            this.btnAddMapping.Text = "Add";
            this.btnAddMapping.UseVisualStyleBackColor = true;
            this.btnAddMapping.Click += new System.EventHandler(this.btnAddMapping_Click);
            // 
            // btnDelMapping
            // 
            this.btnDelMapping.Location = new System.Drawing.Point(208, 350);
            this.btnDelMapping.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelMapping.Name = "btnDelMapping";
            this.btnDelMapping.Size = new System.Drawing.Size(73, 46);
            this.btnDelMapping.TabIndex = 13;
            this.btnDelMapping.Text = "Delete";
            this.btnDelMapping.UseVisualStyleBackColor = true;
            this.btnDelMapping.Click += new System.EventHandler(this.btnDelMapping_Click);
            // 
            // btnEditMapping
            // 
            this.btnEditMapping.Location = new System.Drawing.Point(149, 350);
            this.btnEditMapping.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEditMapping.Name = "btnEditMapping";
            this.btnEditMapping.Size = new System.Drawing.Size(51, 46);
            this.btnEditMapping.TabIndex = 14;
            this.btnEditMapping.Text = "Edit";
            this.btnEditMapping.UseVisualStyleBackColor = true;
            this.btnEditMapping.Click += new System.EventHandler(this.btnEditMapping_Click);
            // 
            // btnBind
            // 
            this.btnBind.Location = new System.Drawing.Point(15, 350);
            this.btnBind.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBind.Name = "btnBind";
            this.btnBind.Size = new System.Drawing.Size(65, 46);
            this.btnBind.TabIndex = 15;
            this.btnBind.Text = "Bind";
            this.btnBind.UseVisualStyleBackColor = true;
            this.btnBind.Click += new System.EventHandler(this.button1_Click);
            // 
            // listViewCurrentBindings
            // 
            this.listViewCurrentBindings.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewCurrentBindings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnController,
            this.columnDevice,
            this.columnMap});
            this.listViewCurrentBindings.HoverSelection = true;
            this.listViewCurrentBindings.Location = new System.Drawing.Point(15, 402);
            this.listViewCurrentBindings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listViewCurrentBindings.Name = "listViewCurrentBindings";
            this.listViewCurrentBindings.Size = new System.Drawing.Size(604, 242);
            this.listViewCurrentBindings.TabIndex = 16;
            this.listViewCurrentBindings.UseCompatibleStateImageBehavior = false;
            this.listViewCurrentBindings.View = System.Windows.Forms.View.Details;
            // 
            // columnController
            // 
            this.columnController.Text = "Controller";
            this.columnController.Width = 200;
            // 
            // columnDevice
            // 
            this.columnDevice.Text = "Device";
            this.columnDevice.Width = 200;
            // 
            // columnMap
            // 
            this.columnMap.Text = "Map";
            this.columnMap.Width = 204;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 660);
            this.Controls.Add(this.listViewCurrentBindings);
            this.Controls.Add(this.btnBind);
            this.Controls.Add(this.btnEditMapping);
            this.Controls.Add(this.btnDelMapping);
            this.Controls.Add(this.btnAddMapping);
            this.Controls.Add(this.labelMapping);
            this.Controls.Add(this.comboBoxSelectMap);
            this.Controls.Add(this.labelDevice);
            this.Controls.Add(this.labelController);
            this.Controls.Add(this.comboBoxSelectControllable);
            this.Controls.Add(this.comboBoxSelectController);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.resetLog);
            this.Controls.Add(this.log);
            this.Controls.Add(this.toggleStartStop);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button toggleStartStop;
        private System.Windows.Forms.TextBox log;
        private System.Windows.Forms.Button resetLog;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ComboBox comboBoxSelectController;
        private System.Windows.Forms.ComboBox comboBoxSelectControllable;
        private System.Windows.Forms.Label labelController;
        private System.Windows.Forms.Label labelDevice;
        private System.Windows.Forms.Label labelMapping;
        private System.Windows.Forms.ComboBox comboBoxSelectMap;
        private System.Windows.Forms.Button btnAddMapping;
        private System.Windows.Forms.Button btnDelMapping;
        private System.Windows.Forms.Button btnEditMapping;
        private System.Windows.Forms.Button btnBind;
        private System.Windows.Forms.ListView listViewCurrentBindings;
        private System.Windows.Forms.ColumnHeader columnDevice;
        private System.Windows.Forms.ColumnHeader columnMap;
        private System.Windows.Forms.ColumnHeader columnController;
    }
}