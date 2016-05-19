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


        public MapperGUI()
        {
            InitializeComponent();
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

        }

        private void buttonDeleteBind_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }
    }
}
