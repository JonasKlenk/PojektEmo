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

namespace EmotivEngine
{
    public partial class MapperGUI : Form
    {
        private int selectedCommand;
        private int selectedAction;
        private int selectedMapping;
        private MapEditor mapEditor;
        private IController[] availableControllers;
        private IControllableDevice[] availableDevices;
        private CentralControlEngine controlEngine;

        private void initializeList()
        {
            for(int i = 0; i < listViewMapping.Columns.Count; i++)
                listViewMapping.Columns[i].Width = (listViewMapping.Width - 25)/ listViewMapping.Columns.Count;
            setMappingList();
        }

        private void setAvailableControllers(IController[] availiableControllers)
        {
            comboControllerID.DataSource = availiableControllers;
        }
        private void setAvailableControllabelDevices(IControllableDevice[] availiableDevices)
        {
            comboControllableDeviceID.DataSource = availiableDevices;
        }

        internal MapperGUI(IControllableDevice[] availableDevices, IController[] availableControllers, CentralControlEngine cce)
        {
            InitializeComponent();
            mapEditor = new MapEditor(availableControllers, availableDevices);
            this.availableControllers = availableControllers;
            this.availableDevices = availableDevices;
            setAvailableControllers(availableControllers);
            setAvailableControllabelDevices(availableDevices);
            controlEngine = cce;
            initializeList();
        }

        internal MapperGUI(MapEditor mapEditor, CentralControlEngine cce)
        {
            this.mapEditor = mapEditor;
            this.InitializeComponent();
            this.Show();
            this.upateGUI(mapEditor);
            controlEngine = cce;
            initializeList();
        }

        private void upateGUI(MapEditor mapEditor)
        {
            try
            {
                listCommandTypes.DataSource = mapEditor.getCommandList();
                listCommandTypes.DisplayMember = "Name";
                listActionTypes.DataSource = mapEditor.getActionList();
                listActionTypes.DisplayMember = "Name";
                setMappingList();
                name.Text = mapEditor.name;
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show(Texts.GUITexts.MapException);
                this.Close();
            }
        }

        private void setMappingList()
        {
            string[][] textList = mapEditor.getTextCommandMapping();
            if (listViewMapping.InvokeRequired)
                this.Invoke(new Action(() => { listViewMapping.Items.Clear(); }));
            else
                listViewMapping.Items.Clear();
            for(int i = 0; i < textList.Length; i++ )
            {
                ListViewItem li = new ListViewItem(textList[i][0]);
                li.SubItems.Add(textList[i][1]);
                if (listViewMapping.InvokeRequired)
                    this.Invoke(new Action<ListViewItem>((lclLi) => { listViewMapping.Items.Add(lclLi); }), li);
                else
                    listViewMapping.Items.Add(li);
            }

        }

        private void ComboControllerID_SelectedIndexChanged(object sender, EventArgs e)
        {
            mapEditor.setActiveController(comboControllerID.SelectedIndex);
            listCommandTypes.DataSource = mapEditor.getCommandList();

        }

        private void ComboControllableDeviceID_SelectedIndexChanged(object sender, EventArgs e)
        {
            mapEditor.setActiveDevice(comboControllableDeviceID.SelectedIndex);
            listActionTypes.DataSource = mapEditor.getActionList();

        }

        private void listCommandTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCommand = listCommandTypes.SelectedIndex;
        }

        private void listActionTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedAction = listActionTypes.SelectedIndex;
        }

        private void listMapping_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewMapping.SelectedIndices.Count == 1)
                selectedMapping = listViewMapping.SelectedIndices[0];
            else
                selectedMapping = -1;
        }

        private void buttonBind_Click(object sender, EventArgs e)
        {

            mapEditor.bind(selectedCommand, selectedAction);
                setMappingList();
                listViewMapping.Show();

        }

        private void buttonDeleteBind_Click(object sender, EventArgs e)
        {
            if (selectedMapping >= 0)
            {
            mapEditor.unbind(selectedMapping);
                setMappingList();
                listViewMapping.Show();
        }
            else
                MessageBox.Show("Please select a single binding");
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(MainWindow.xmlMapPath))
                Directory.CreateDirectory(MainWindow.xmlMapPath);
            controlEngine.registerMap(mapEditor.saveMapping(File.Open(MainWindow.xmlMapPath + mapEditor.name + ".xml", FileMode.OpenOrCreate)));
            this.Close();
        }

        private void name_TextChanged(object sender, EventArgs e)
        {
            mapEditor.setName(name.Text);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadMappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openMapDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                mapEditor = MapEditor.loadMap(openMapDialog.OpenFile());
                name.Text = mapEditor.name;
                comboControllerID.Text = mapEditor.controllerType;
                comboControllerID.Enabled = false;
                comboControllableDeviceID.Text = mapEditor.deviceType;
                comboControllableDeviceID.Enabled = false;
                listCommandTypes.DataSource = mapEditor.getCommandList();
                listActionTypes.DataSource = mapEditor.getActionList();
                //listMapping.DataSource = mapEditor.getTextCommandMapping();
            }
                catch (Exception)
                {
                    System.Windows.Forms.MessageBox.Show(Texts.GUITexts.MapException);
                    this.Close();
                }

            }
            this.Close();
        }

        private void newMappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapEditor = new MapEditor(availableControllers, availableDevices);
            setAvailableControllers(availableControllers);
            setAvailableControllabelDevices(availableDevices);
        }
    }
}
