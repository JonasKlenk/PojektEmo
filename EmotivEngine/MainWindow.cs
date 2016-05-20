using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmotivEngine
{
    public partial class MainWindow : Form
    {
        CentralControlEngine cce;

        public MainWindow()
        {
            InitializeComponent();
            cce = CentralControlEngine.Instance;
            cce.registerController(EmoController.getInstance(cce));
            cce.loggerUpdated += new EventHandler(updateLog);
        }

        private void toggleStartStop_Click(object sender, EventArgs e)
        {
            if (!cce.isRunning())
            {
                cce.start();
                resetLog_Click(null, null);
                statusLabel.BackColor = Color.Green;
                statusLabel.Text = "Running";
                toggleStartStop.Text = "Stop Engine";          

            }
            else { 
                cce.stop();
                statusLabel.BackColor = Color.Red;
                statusLabel.Text = "Stopped";
                toggleStartStop.Text = "Start Engine";

            }
        }

        private void updateLog(object sender, EventArgs e)
        {
            if (this.log.InvokeRequired)
            {
                this.Invoke(new Action(() => { log.Text = cce.getLogText(); }));
            }
            else
            {
                log.Text = cce.getLogText();
            }
        }

        private void resetLog_Click(object sender, EventArgs e)
        {
            cce.resetLog();
            log.Text = cce.getLogText();
        }

        private void openMappingDialog_Click(object sender, EventArgs e)
        {
            MapperGUI mapperGui = new MapperGUI(cce.getControllableDevices(), cce.getControllers());
            mapperGui.Show();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
