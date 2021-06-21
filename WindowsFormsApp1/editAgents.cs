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
    public partial class editAgents : Form
    {
        public editAgents()
        {
            InitializeComponent();
        }
        public int _passId;
        private void editAgents_Load(object sender, EventArgs e)
        {
            globalClass.agent agent =  globalClass.listAgent.Find(item => item.id == _passId);
            pictureBox1.Image = Image.FromFile( agent.file);
            nameBox.Text = agent.name;
        }
    }
}
