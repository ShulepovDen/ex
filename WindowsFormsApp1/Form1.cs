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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            globalClass.connection.Open();
            MySqlCommand typeRead = new MySqlCommand("SELECT * from agent_type_dictionary", globalClass.connection);
            MySqlDataReader typeReader = typeRead.ExecuteReader();
            if (typeReader.HasRows)
            {
                while (typeReader.Read())
                {
                    comboBox1.Items.Add(typeReader.GetString("agent_type"));
                }
            }
            typeReader.Close();
            comboBox2.Items.Add("Имя");
            comboBox2.Items.Add("Переворот");
            comboBox2.Items.Add("ИмяОбратно");
            comboBox1.SelectedIndex = 0;
            MySqlCommand agentRead = new MySqlCommand("SELECT * from agents", globalClass.connection);
            MySqlDataReader agentReader = agentRead.ExecuteReader();
            if (agentReader.HasRows)
            {
                while (agentReader.Read())
                {
                    globalClass.listAgent.Add(new globalClass.agent
                    {
                        name = agentReader.GetString("company_name"),
                        id = agentReader.GetInt32("id_agent"),
                        type = agentReader.GetString("agent_type")
                    }) ;
                    if (agentReader.GetString("logo") == "нет")
                    {
                        globalClass.listAgent.Last().file = "./agents/picture.png";
                    }
                    else
                    {
                        globalClass.listAgent.Last().file =".."+ agentReader.GetString("logo");
                       
                    }
                }
            }
            if (globalClass.listAgent.Count > 0) 
            {
                for(int i = 0; i < globalClass.listAgent.Count; i++)
                {
                    ControlAgent controlAgent = new ControlAgent(i, globalClass.listAgent);
                    panelAgent.Controls.Add(controlAgent);
                }

            }
            agentReader.Close();
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            globalClass.connection.Close();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (globalClass.listSelected.Count == 0)
            {

            }
            else
            {
                editAgents editAgents = new editAgents();
                editAgents._passId = globalClass.listSelected.Last();
                editAgents.ShowDialog();
                MessageBox.Show("asd");
            }
            
        }

        private void searchBox_MouseEnter(object sender, EventArgs e)
        {
            TextBox TB = (TextBox)sender;
            int VisibleTime = 1000;  //in milliseconds

            ToolTip tt = new ToolTip();
            tt.Show("Test ToolTip", TB, 0, -25, VisibleTime);
        }
        public void searchList()
        {
            List<globalClass.agent> agents = new List<globalClass.agent>();
            List<globalClass.agent> agentsType = new List<globalClass.agent>();
            List<globalClass.agent> agentsName = new List<globalClass.agent>();
            if (comboBox1.SelectedIndex == 0)
            {
                agentsType = globalClass.listAgent;
            }
            else
            {
                agentsType = globalClass.listAgent.FindAll(item => item.type == comboBox1.Text);
            }
            if (searchBox.Text == "")
            {
                agentsName = globalClass.listAgent;
            }
            else
            {
                agentsName = globalClass.listAgent.FindAll(item => item.name.ToLower().StartsWith(searchBox.Text.ToLower()));
            }
            
            var ss = agentsName.Select(s1=>s1.id).ToList().Intersect(agentsType.Select(s2=>s2.id).ToList());
            foreach(int i in ss)
            {
                agents.AddRange(globalClass.listAgent.FindAll(item => item.id == i));
            }
            if (agents.Count > 0)
            {
                panelAgent.Controls.Clear();
                panelAgent.RowCount = 0;
                for (int i = 0; i < agents.Count; i++)
                {
                    ControlAgent controlAgent = new ControlAgent(i,agents);
                    panelAgent.Controls.Add(controlAgent);
                }
            }
            else
            {
                /*panelAgent.Controls.Clear();
                panelAgent.RowCount = 0;
                for (int i = 0; i < globalClass.listAgent.Count; i++)
                {
                    ControlAgent controlAgent = new ControlAgent(i, globalClass.listAgent);
                    panelAgent.Controls.Add(controlAgent);
                }*/
                panelAgent.Controls.Clear();
                panelAgent.RowCount = 0;
                Empty empty = new Empty();
                panelAgent.Controls.Add(empty);
            }
            if (comboBox1.SelectedIndex == 0 && searchBox.Text == "")
            {
                panelAgent.Controls.Clear();
                panelAgent.RowCount = 0;
                for (int i = 0; i < globalClass.listAgent.Count; i++)
                {
                    ControlAgent controlAgent = new ControlAgent(i, globalClass.listAgent);
                    panelAgent.Controls.Add(controlAgent);
                }
            }
        }
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            searchList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchList();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            sort();
            searchList();
        }
        private void sort()
        {
            if (comboBox2.SelectedItem.ToString() == "Имя")
            {
                globalClass.listAgent = globalClass.listAgent.OrderBy(item => item.name).ToList();

            }
            if (comboBox2.SelectedItem.ToString() == "Переворот")
            {
                globalClass.listAgent.Reverse(0, globalClass.listAgent.Count());

            }
            if (comboBox2.SelectedItem.ToString() == "ИмяОбратно")
            {
                globalClass.listAgent = globalClass.listAgent.OrderByDescending(item => item.name).ToList();

            }
        }
    }
}
