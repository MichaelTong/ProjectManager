using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 项目管理模拟系统
{
    public delegate void ChLeveDelegate(int level);
    public partial class ChangeLevelcs : Form
    {
        public ChLeveDelegate ChLeve;
        public ChangeLevelcs()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChLeve(comboBox1.SelectedIndex + 1);
            this.Close();
        }
    }
}
