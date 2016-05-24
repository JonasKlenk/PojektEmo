using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;

namespace EmotivEngine
{
    public partial class MainWindow : Form
    {
        CentralControlEngine cce;
        static public string xmlMapPath = "./Data/Maps/";
        static public string xmlCategoryPath = "./Data/Categories/";

        public MainWindow()
        {
            //TODO: Liste von Controller und Devices initialisieren.
            InitializeComponent();
            cce = CentralControlEngine.Instance;
            cce.registerController(EmoController.getInstance(cce));
            //HACK Test
            cce.registerControllableDevice(DummyDevice.getInstance(cce));
            //HACK Testende
            cce.loggerUpdated += new EventHandler<LoggerEventArgs>(updateLog);
            //cce.registerMap(new Map(Texts.ControllerTypes.CT_EmotivEPOC, "test", new int[] { 0, 1, -1 }, "Map 1", EmoController.getInstance(cce).getCommands(), new string[] { "asd", "asd2" }));
            log.Text = cce.getLogText();
            log.AppendText("");
            if (Directory.Exists(xmlMapPath))
                foreach (string path in Directory.GetFiles(xmlMapPath))
                    cce.registerMap(Map.ReadXml(path));
            cce.bindingsChanged += new EventHandler<BindingsEventArgs>((sender, argument) =>
           {
               this.Invoke(new Action<BindingsEventArgs>( (binding) => {
                   this.listViewCurrentBindings.Items.Clear();
               foreach (string[] test in binding.bindings) {
                   ListViewItem newItem = new ListViewItem(test[0]);
                   newItem.SubItems.Add(test[1]);
                   newItem.SubItems.Add(test[2]);
                       this.listViewCurrentBindings.Items.Add(newItem);
                   } }), argument);
           });
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
                listViewCurrentBindings.Columns[i].Width = this.Width / listViewCurrentBindings.Columns.Count;
                
            List<DeviceCategory> categories = new List<DeviceCategory>();
            if (Directory.Exists(xmlCategoryPath))
                foreach (string path in Directory.GetFiles(xmlCategoryPath))
                    categories.Add(DeviceCategory.ReadXml(XmlReader.Create(path)));
            if (categories.Count > 0)
                cce.registerCategories(categories);
            comboBoxSelectController.DataSource = cce.getControllers();
            comboBoxSelectController.DisplayMember = "Name";
            comboBoxSelectControllable.DataSource = cce.getControllableDevices();
            comboBoxSelectControllable.DisplayMember = "Name";
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
            MapperGUI mapperGui = new MapperGUI(cce.getControllableDevices(), cce.getControllers(), cce);
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
               //TODO Serialisierung läuft nicht so: m.WriteXml(XmlWriter.Create(new StringBuilder().Append(xmlMapPath).Append(m.name).Append(".xml").ToString()));
            }
            Application.Exit();
        }

        private void btnAddMapping_Click(object sender, EventArgs e)
        {
            //
            //Setzen der beiden Werte für ComboBoxen in MapperGui
            //comboBoxSelectController.SelectedItem
            //comboBoxSelectControllable.SelectedItem
            new MapperGUI(cce.getControllableDevices(), cce.getControllers(), cce).Show();
            //anlegen der persistierten map
            //cce.Map(createdMap) -> Wo bekomme ich created Map her? Muss das möglicherweise aus deiner MapEditor Klasse ausgerufen werden?
            //--> Passiert im Konstruktor der Gui...

        }

        private void btnDelMapping_Click(object sender, EventArgs e)
        {
            //TODO
            //löschen der persistierten Version der Map
            cce.unregisterMap((Map)comboBoxSelectMap.SelectedItem);
            MapEditor.loadMap((Map)comboBoxSelectMap.SelectedItem).deleteMapping(xmlMapPath);
        }

        private void btnEditMapping_Click(object sender, EventArgs e)
        {
            new MapperGUI(MapEditor.loadMap((Map)comboBoxSelectMap.SelectedItem), cce);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxSelectControllable.SelectedItem != null && comboBoxSelectController.SelectedItem != null && comboBoxSelectMap.SelectedItem != null)
                cce.bindControllerDeviceMap((IController)comboBoxSelectController.SelectedItem, (IControllableDevice)comboBoxSelectControllable.SelectedItem, (Map)comboBoxSelectMap.SelectedItem); 
        }
    }
}
