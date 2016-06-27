using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mynuolr;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        
        net_tools s = new net_tools();
        private Dictionary<string, string[]> d;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            net_tools s = new net_tools();
            net_tools.ShowNetworkInterfaceMessage();
            

        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            d = net_tools.ShowNetworkInterfaceMessage();
            foreach (string key in d.Keys)

            {
                comboBox1.Items.Add(key);
                comboBox1.SelectedIndex = 0;
            
            }
          
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string c = comboBox1.Text;
            textBox4.Text = d[c][0];
            textBox5.Text = d[c][1];
        }
    }
}
