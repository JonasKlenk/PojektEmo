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
        private ICollection<IControllableDevice> listControllableDevices;
        private ICollection<IController> listController;
        private Mapping mapping;
        private string[] displayMap;


        private void setAvailiableControllers(ICollection<IController> availiableControllers)
        {
            List<string> types = new List<string>();
            foreach (var item in availiableControllers)
            {
                types.Add(item.getType());
            }
            ComboControllerID.DataSource = types;
        }
        private void setAvailiableControllabelDevices(ICollection<IControllableDevice> availiableDevices)
        {
            List<string> types = new List<string>();
            foreach (var item in availiableDevices)
            {
                types.Add(item.getType());
            }
            ComboControllableDeviceID.DataSource = types;
        }

        internal MapperGUI(ICollection<IControllableDevice> availiableDevices, ICollection<IController> availiableControllers)
        {
            InitializeComponent();
            setAvailiableControllers(availiableControllers);
            setAvailiableControllabelDevices(availiableDevices);
            mapping = new Mapping(availiableControllers, availiableDevices);
            
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
            mapping.bind(selectedAction, selectedCommand);
            listMapping.DataSource = mapping.getTextCommandMapping();
        }

        private void buttonDeleteBind_Click(object sender, EventArgs e)
        {
            mapping.unbind(selectedMapping);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }


    }
}
