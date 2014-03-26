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
    public delegate void RemDelegate(Personnel upPer,ProVsUse upPVU);
    public partial class Remark : Form
    {
        public event RemDelegate RM;
        Personnel upPer;
        ProVsUse upPVU;
        public Remark(Personnel upPer,ProVsUse upPVU)
        {
            InitializeComponent();
            this.upPer = upPer;
            this.upPVU = upPVU;
            comboBox1.SelectedIndex = 0;
            maskedTextBox1.Text = upPer.getUserName() + "(" + upPer.getUID().ToString() + ")";
            maskedTextBox2.Text = upPVU.getProj().getProName() + "(" + upPVU.getProj().getPID() + ")";
            maskedTextBox3.Text = upPVU.getProc().ToString() + "/" + upPVU.getWeight().ToString();
            maskedTextBox4.Text = upPVU.getGraS().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            upPVU.setGraM(comboBox1.SelectedIndex + 1);
            RefreshFile.RefreshRank(upPVU);
            MessageBox.Show("评分成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RM(upPer, upPVU);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
