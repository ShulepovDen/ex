using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace WindowsFormsApp1
{
    public partial class ControlAgent : UserControl
    {
        public ControlAgent()
        {
            InitializeComponent();
        }
        public ControlAgent(int id, List<globalClass.agent> agent)
        {
            InitializeComponent();
            label1.Text = agent[id].name;
            label2.Text = agent[id].type;
            pictureBox1.Image = Image.FromFile(agent[id].file);
            this.BackColor = ColorTranslator.FromHtml("#FF9540");
        }
        private void ControlAgent_Load(object sender, EventArgs e)
        {

        }
        public static void getAll()
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ControlAgent_Click(object sender, EventArgs e)
        {
            MySqlCommand getId = new MySqlCommand("SELECT id_agent from agents where company_name='" + label1.Text + "'", globalClass.connection);
            MySqlDataReader idRead = getId.ExecuteReader();
            idRead.Read();
            
            if (checkBox1.Checked)
            {
                checkBox1.Checked = false;
                globalClass.listSelected.Remove(idRead.GetInt32("id_agent"));
            }
            else
            {
                globalClass.listSelected.Add(idRead.GetInt32("id_agent"));
                checkBox1.Checked = true;
            }
            
            idRead.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            CheckBox chbx = sender as CheckBox;
            chbx.Checked = !chbx.Checked;
        }
    }
}
