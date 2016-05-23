using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace EmotivEngine
{
    public partial class MainWindow : Form
    {
        CentralControlEngine cce;
        static private string xmlMapPath = "./Data/Maps/";
        static private string xmlCategoryPath = "./Data/Categories/";

        public MainWindow()
        {
            //TODO: Liste von Controller und Devices initialisieren.
            InitializeComponent();
            cce = CentralControlEngine.Instance;
            cce.registerController(EmoController.getInstance(cce));
            cce.loggerUpdated += new EventHandler<LoggerEventArgs>(updateLog);
            //cce.registerMap(new Map(Texts.ControllerTypes.CT_EmotivEPOC, "test", new int[] { 1, 2, 3 }, "Map 1", EmoController.getInstance(cce).getCommands(), new string[] { "asd", "asd2" }));

            log.Text = cce.getLogText();
            log.AppendText("");
            if (Directory.Exists(xmlMapPath))
                foreach (string path in Directory.GetFiles(xmlMapPath))
                    cce.registerMap(Map.ReadXml(path));

            List<DeviceCategory> categories = new List<DeviceCategory>();
            if (Directory.Exists(xmlCategoryPath))
                foreach (string path in Directory.GetFiles(xmlCategoryPath))
                    categories.Add(DeviceCategory.ReadXml(XmlReader.Create(path)));
            if (categories.Count > 0)
                cce.registerCategories(categories);
            comboBoxSelectController.DataSource = cce.getControllers();
            comboBoxSelectControllable.DataSource = cce.getControllableDevices();
            comboBoxSelectMap.DataSource = cce.getMaps();
            comboBoxSelectMap.DisplayMember = "name";


        }

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

        private void updateLog(object sender, LoggerEventArgs e)
        {
            if (this.log.InvokeRequired)
                this.Invoke(new Action(() => log.AppendText(e.getAddedLog())));
            else
                log.AppendText(e.getAddedLog());
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
            //TODO: Serialisieren igentlich unnötig, kann gelöscht werden, wenn neu erstellt/geänderte Maps
            //immer gleich serialisiert werden.

            if (!Directory.Exists(xmlMapPath))
                Directory.CreateDirectory(xmlMapPath);
            foreach (Map m in cce.getMaps())
            {
                m.WriteXml(XmlWriter.Create(new StringBuilder().Append(xmlMapPath).Append(m.name).Append(".xml").ToString()));
            }
            Application.Exit();
        }

        private void btnAddMapping_Click(object sender, EventArgs e)
        {
            //TODO
            //Setzen der beiden Werte für ComboBoxen in MapperGui
            //comboBoxSelectController.SelectedItem
            //comboBoxSelectControllable.SelectedItem
            new MapperGUI(cce.getControllableDevices(), cce.getControllers()).Show();
            //anlegen der persistierten map
            //cce.Map(createdMap) -> Wo bekomme ich created Map her? Muss das möglicherweise aus deiner MapEditor Klasse ausgerufen werden?

        }

        private void btnDelMapping_Click(object sender, EventArgs e)
        {
            //TODO
            //löschen der persistierten Version der Map
            cce.unregisterMap((Map)comboBoxSelectMap.SelectedItem);
        }

        private void btnEditMapping_Click(object sender, EventArgs e)
        {
            //comboBoxSelectController.SelectedItem
            //comboBoxSelectControllable.SelectedItem
            //(Map)comboBoxSelectMap.SelectedItem
        }
    }
}
