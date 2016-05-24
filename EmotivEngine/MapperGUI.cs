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
    public partial class MapperGUI : Form
    {
        private int selectedCommand;
        private int selectedAction;
        private int selectedMapping;
        private MapEditor mapEditor;
        private IController[] availableControllers;
        private IControllableDevice[] availableDevices;


        private void setAvailableControllers(IController[] availiableControllers)
        {
            comboControllerID.DataSource = availiableControllers;
        }
        private void setAvailableControllabelDevices(IControllableDevice[] availiableDevices)
        {
            comboControllableDeviceID.DataSource = availiableDevices;
        }

        internal MapperGUI(IControllableDevice[] availableDevices, IController[] availableControllers)
        {
            InitializeComponent();
            mapEditor = new MapEditor(availableControllers, availableDevices);
            this.availableControllers = availableControllers;
            this.availableDevices = availableDevices;
            setAvailableControllers(availableControllers);
            setAvailableControllabelDevices(availableDevices);
        }

        internal MapperGUI(MapEditor mapEditor)
        {
            this.mapEditor = mapEditor;
            this.InitializeComponent();
            this.Show();
            this.upateGUI(mapEditor);
        }

        private void upateGUI(MapEditor mapEditor)
        {
            try
            {
                listCommandTypes.DataSource = mapEditor.getCommandList();
                listActionTypes.DataSource = mapEditor.getActionList();
                listMapping.DataSource = mapEditor.getTextCommandMapping();
                name.Text = mapEditor.name;
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show(Texts.GUITexts.exception);
                this.Close();
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
            selectedMapping = listMapping.SelectedIndex;
        }

        private void buttonBind_Click(object sender, EventArgs e)
        {
            mapEditor.bind(selectedCommand, selectedAction);
            listMapping.DataSource = mapEditor.getTextCommandMapping();
            listMapping.Show();
        }

        private void buttonDeleteBind_Click(object sender, EventArgs e)
        {
            mapEditor.unbind(selectedMapping);
            listMapping.DataSource = mapEditor.getTextCommandMapping();
            listMapping.Show();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (SaveMappingDialog.ShowDialog() == DialogResult.OK)
            {
                mapEditor.saveMapping(SaveMappingDialog.OpenFile());
            }
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
                    listMapping.DataSource = mapEditor.getTextCommandMapping();
                }
                catch (Exception)
                {
                    System.Windows.Forms.MessageBox.Show(Texts.GUITexts.exception);
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
