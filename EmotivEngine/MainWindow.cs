using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using System.Text;

namespace EmotivEngine
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class MainWindow : Form
    {
        /// <summary>
        /// The Central Control engine reference
        /// </summary>
        CentralControlEngine cce;
        /// <summary>
        /// The XML map path
        /// </summary>
        static public string xmlMapPath = "./Data/Maps/";
        /// <summary>
        /// The XML category path
        /// </summary>
        static public string xmlCategoryPath = "./Data/Categories/";

        static public string loggerPath = "./Data/log.txt";

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {

            //Initialize window, cce, emocontroller, logger
            InitializeComponent();
            cce = CentralControlEngine.Instance;
            cce.registerController(EmoController.getInstance(cce));
            cce.loggerUpdated += new EventHandler<LoggerEventArgs>(updateLog);
            log.Text = cce.getLogText();

            //load maps
            if (Directory.Exists(xmlMapPath))
                foreach (string path in Directory.GetFiles(xmlMapPath))
                    cce.registerMap(Map.ReadXml(path));

            //change GUI on bindings changed
            cce.bindingsChanged += new EventHandler<BindingsEventArgs>((sender, argument) =>
           {
               this.Invoke(new Action<BindingsEventArgs>((binding) =>
               {
                   this.listViewCurrentBindings.Items.Clear();
                   foreach (string[] test in binding.bindings)
                   {
                       ListViewItem newItem = new ListViewItem(test[0]);
                       newItem.SubItems.Add(test[1]);
                       newItem.SubItems.Add(test[2]);
                       this.listViewCurrentBindings.Items.Add(newItem);
                   }
               }), argument);
           });

            //change GUI on mpas changed
            cce.mapsChanged += new EventHandler((sender, arguments) =>
            {
                this.Invoke(new Action(() =>
                {
                    Map currentSelectedMap = (Map)comboBoxSelectMap.SelectedItem;
                    comboBoxSelectMap.DataSource = cce.getMaps();
                    comboBoxSelectMap.SelectedItem = currentSelectedMap;
                }));
            });
            for (int i = 0; i < listViewCurrentBindings.Columns.Count; i++)
                listViewCurrentBindings.Columns[i].Width = listViewCurrentBindings.Width / listViewCurrentBindings.Columns.Count;

            //change GUI on controllables changed
            cce.controllablesChanged += new EventHandler((sender, arguments) =>
            {
                if (comboBoxSelectControllable.InvokeRequired)
                    comboBoxSelectControllable.Invoke(new Action(() => { comboBoxSelectControllable.DataSource = cce.getControllableDevices(); }));
                else
                    comboBoxSelectControllable.DataSource = cce.getControllableDevices();
            });

            //Read categories
            List<DeviceCategory> categories = new List<DeviceCategory>();
            if (Directory.Exists(xmlCategoryPath))
                foreach (string path in Directory.GetFiles(xmlCategoryPath))
                    categories.Add(DeviceCategory.ReadXml(XmlReader.Create(path)));
            if (categories.Count > 0)
                cce.registerCategories(categories);

            //set data for GUI elements
            comboBoxSelectController.DataSource = cce.getControllers();
            comboBoxSelectController.DisplayMember = "Name";
            comboBoxSelectControllable.DataSource = cce.getControllableDevices();
            comboBoxSelectControllable.DisplayMember = "Name";
            comboBoxSelectMap.DataSource = cce.getMaps();
            comboBoxSelectMap.DisplayMember = "name";

            comboBoxSelectLogLevel.Items.AddRange(Enum.GetNames(typeof(Logger.loggingLevel)));
            comboBoxSelectLogLevel.SelectedItem = comboBoxSelectLogLevel.Items[0];


            try {
                //local java client Tim
                //cce.registerControllableDevice(new TcpDevice(cce, "127.0.0.1", 23232));
                //UE Simulation
                TcpDevice tcp = new TcpDevice(cce, "127.0.0.1", 8890);
                cce.registerControllableDevice(tcp);
                //cce.registerControllableDevice(new TcpDevice(cce, "7.237.127.223", 8890));
            } catch (Exception e)
            {
            }

        }

        /// <summary>
        /// Handles the Click event of the toggleStartStop control. Start/Stop the <see cref="CentralControlEngine"/>
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void toggleStartStop_Click(object sender, EventArgs e)
        {
            if (!cce.getIsRunning())
            {
                cce.start();
                statusLabel.BackColor = Color.Green;
                statusLabel.Text = "Running";
                toggleStartStop.Text = "Stop Engine";

            }
            else
            {
                cce.stop();
                statusLabel.BackColor = Color.Red;
                statusLabel.Text = "Stopped";
                toggleStartStop.Text = "Start Engine";
            }
        }

        /// <summary>
        /// Updates the log. Add a log via <see cref="CentralControlEngine"/> to <see cref="Logger"/>
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="LoggerEventArgs"/> instance containing the event data.</param>
        private void updateLog(object sender, LoggerEventArgs e)
        {
            if (this.log.InvokeRequired)
                this.Invoke(new Action(() => log.AppendText(e.getAddedLog())));
            else
                log.AppendText(e.getAddedLog());
        }

        /// <summary>
        /// Handles the Click event of the resetLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void resetLog_Click(object sender, EventArgs e)
        {
            cce.resetLog();
            log.Text = cce.getLogText();
        }

        /// <summary>
        /// Handles the FormClosed event of the MainWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosedEventArgs"/> instance containing the event data.</param>
        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(File.Exists(loggerPath + ".old"))
                File.Delete(loggerPath + ".old");
            if (File.Exists(loggerPath))
                File.Move(loggerPath, loggerPath + ".old");
            FileStream fs = File.Open(loggerPath, FileMode.CreateNew);
            fs.Write(ASCIIEncoding.ASCII.GetBytes(cce.getLogText()), 0, cce.getLogText().Length);
            fs.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnAddMapping control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddMapping_Click(object sender, EventArgs e)
        {
            new MapperGUI(cce.getControllableDevices(), cce.getControllers(), cce).Show();
        }

        /// <summary>
        /// Handles the Click event of the btnDelMapping control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDelMapping_Click(object sender, EventArgs e)
        {
            //TODO
            //löschen der persistierten Version der Map
            cce.unregisterMap((Map)comboBoxSelectMap.SelectedItem);
            MapEditor.loadMap((Map)comboBoxSelectMap.SelectedItem).deleteMapping(xmlMapPath);
        }

        /// <summary>
        /// Handles the Click event of the btnEditMapping control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnEditMapping_Click(object sender, EventArgs e)
        {
            if (cce.getIsRunning())
                MessageBox.Show(this, Texts.GUITexts.cannotUnbindEngineRunning, Texts.GUITexts.cannotEditMapEngineRunning, MessageBoxButtons.OK, MessageBoxIcon.Stop); 
            new MapperGUI(MapEditor.loadMap((Map)comboBoxSelectMap.SelectedItem), cce);

        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxSelectControllable.SelectedItem != null && comboBoxSelectController.SelectedItem != null && comboBoxSelectMap.SelectedItem != null)
                cce.bindControllerDeviceMap((IController)comboBoxSelectController.SelectedItem, (IControllableDevice)comboBoxSelectControllable.SelectedItem, (Map)comboBoxSelectMap.SelectedItem);
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBoxSelectLogLevel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void comboBoxSelectLogLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            cce.setLogLevel((Logger.loggingLevel)Enum.Parse(typeof(Logger.loggingLevel), comboBoxSelectLogLevel.SelectedItem.ToString()));
        }

        /// <summary>
        /// Handles the Click event of the btnUnbind control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnUnbind_Click(object sender, EventArgs e)
        {
            if (cce.getIsRunning())
                MessageBox.Show(this, Texts.GUITexts.cannotUnbindEngineRunning, Texts.GUITexts.errorUnbindCaption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else
            {
                foreach (ListViewItem lvi in listViewCurrentBindings.Items)
                {
                    if (lvi.Selected == true)
                    {  
                        cce.unbindControllerDeviceMap(new List<IController>(cce.getControllers()).Find((c) => c.Name == lvi.SubItems[0].Text));
                    }
                }

            }
        }
    }
}
