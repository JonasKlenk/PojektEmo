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
        private IControllableDevice[] listControllableDevices;
        private IController[] listController;
        private MapEditor mapEditor;
        private string[] displayMap;


        private void setAvailableControllers(ICollection<IController> availiableControllers)
        {
            List<string> types = new List<string>();
            foreach (var item in availiableControllers)
            {
                types.Add(item.getType());
            }
            ComboControllerID.DataSource = types;
        }
        private void setAvailableControllabelDevices(ICollection<IControllableDevice> availiableDevices)
        {
            List<string> types = new List<string>();
            foreach (var item in availiableDevices)
            {
                types.Add(item.getType().getDeviceKategory());
            }
            ComboControllableDeviceID.DataSource = types;
        }

        internal MapperGUI(IControllableDevice[] availableDevices, IController[] availableControllers)
        {
            InitializeComponent();
            mapEditor = new MapEditor(availableControllers, availableDevices);
            setAvailableControllers(availableControllers);
            setAvailableControllabelDevices(availableDevices);
        }
        private void ComboControllerID_SelectedIndexChanged(object sender, EventArgs e)
        {
            mapEditor.setActiveController(ComboControllerID.SelectedIndex);
            listCommandTypes.DataSource = mapEditor.getCommandList();
            listCommandTypes.Enabled = true;
        }

        private void ComboControllableDeviceID_SelectedIndexChanged(object sender, EventArgs e)
        {
            mapEditor.setActiveDevice(ComboControllableDeviceID.SelectedIndex);
            listActionTypes.DataSource = mapEditor.getActionList();
            listActionTypes.Enabled = true;
        }

        private void listCommandTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCommand = listCommandTypes.SelectedIndex;
        }

        private void listActionTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedAction = listCommandTypes.SelectedIndex;
        }

        private void listMapping_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedMapping = listMapping.SelectedIndex;
        }

        private void buttonBind_Click(object sender, EventArgs e)
        {
            mapEditor.bind(selectedCommand, selectedAction);
            listMapping.DataSource = mapEditor.getTextCommandMapping();
        }

        private void buttonDeleteBind_Click(object sender, EventArgs e)
        {
            mapEditor.unbind(selectedMapping);
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
                //Map a = mapEditor.loadMap(openMapDialog.OpenFile());
            }
            this.Close();
        }
    }
}
