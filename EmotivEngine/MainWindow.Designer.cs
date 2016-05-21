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
            this.openMappingDialog = new System.Windows.Forms.Button();
            this.log = new System.Windows.Forms.TextBox();
            this.resetLog = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // toggleStartStop
            // 
            this.toggleStartStop.Location = new System.Drawing.Point(11, 63);
            this.toggleStartStop.Name = "toggleStartStop";
            this.toggleStartStop.Size = new System.Drawing.Size(200, 50);
            this.toggleStartStop.TabIndex = 1;
            this.toggleStartStop.Text = "Start Engine";
            this.toggleStartStop.UseVisualStyleBackColor = true;
            this.toggleStartStop.Click += new System.EventHandler(this.toggleStartStop_Click);
            // 
            // openMappingDialog
            // 
            this.openMappingDialog.Location = new System.Drawing.Point(11, 118);
            this.openMappingDialog.Name = "openMappingDialog";
            this.openMappingDialog.Size = new System.Drawing.Size(200, 50);
            this.openMappingDialog.TabIndex = 2;
            this.openMappingDialog.Text = "Open Mapping Configurator";
            this.openMappingDialog.UseVisualStyleBackColor = true;
            this.openMappingDialog.Click += new System.EventHandler(this.openMappingDialog_Click);
            // 
            // log
            // 
            this.log.AcceptsReturn = true;
            this.log.Location = new System.Drawing.Point(223, 12);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.log.Size = new System.Drawing.Size(242, 101);
            this.log.TabIndex = 4;
            this.log.Text = "No Events";
            // 
            // resetLog
            // 
            this.resetLog.Location = new System.Drawing.Point(223, 118);
            this.resetLog.Name = "resetLog";
            this.resetLog.Size = new System.Drawing.Size(242, 50);
            this.resetLog.TabIndex = 5;
            this.resetLog.Text = "Reset Log";
            this.resetLog.UseVisualStyleBackColor = true;
            this.resetLog.Click += new System.EventHandler(this.resetLog_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.BackColor = System.Drawing.Color.Red;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(12, 12);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(199, 48);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "Stopped";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 179);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.resetLog);
            this.Controls.Add(this.log);
            this.Controls.Add(this.openMappingDialog);
            this.Controls.Add(this.toggleStartStop);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button toggleStartStop;
        private System.Windows.Forms.Button openMappingDialog;
        private System.Windows.Forms.TextBox log;
        private System.Windows.Forms.Button resetLog;
        private System.Windows.Forms.Label statusLabel;
    }
}