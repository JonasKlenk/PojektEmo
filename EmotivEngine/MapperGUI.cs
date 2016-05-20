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
        private Mapping mapping;
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
                types.Add(item.getType());
            }
            ComboControllableDeviceID.DataSource = types;
        }

        internal MapperGUI(IControllableDevice[] availableDevices, IController[] availableControllers)
        {
            InitializeComponent();
            mapping = new Mapping(availableControllers, availableDevices);
            setAvailableControllers(availableControllers);
            setAvailableControllabelDevices(availableDevices);
        }
        private void ComboControllerID_SelectedIndexChanged(object sender, EventArgs e)
        {
            mapping.setActiveController(ComboControllerID.SelectedIndex);
            listCommandTypes.DataSource = mapping.getCommandList();
            listCommandTypes.Enabled = true;
        }

        private void ComboControllableDeviceID_SelectedIndexChanged(object sender, EventArgs e)
        {
            mapping.setActiveDevice(ComboControllableDeviceID.SelectedIndex);
            listActionTypes.DataSource = mapping.getActionList();
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
            mapping.bind(selectedCommand, selectedAction);
            listMapping.DataSource = mapping.getTextCommandMapping();
        }

        private void buttonDeleteBind_Click(object sender, EventArgs e)
        {
            mapping.unbind(selectedMapping);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (SaveMappingDialog.ShowDialog() == DialogResult.OK)
            {
                mapping.saveMapping(SaveMappingDialog.OpenFile());
            }
             
            
        }

        private void name_TextChanged(object sender, EventArgs e)
        {
            mapping.setName(name.Text);
        }
    }
}
