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
    public delegate void SelfRe(ProVsUse SelfPVU);
    public partial class SelfRemark : Form
    {
        public event SelfRe SelfRem;
        ProVsUse SelfPVU;
        public SelfRemark(ProVsUse SelfPVU)
        {
            InitializeComponent();
            this.SelfPVU = SelfPVU;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelfPVU.setGraS(comboBox1.SelectedIndex + 1);
            SelfRem(SelfPVU);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
