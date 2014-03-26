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
    public delegate void ConProcDelegate(String nProc, Project toCFProj, ProVsUse toCFPVU);
    public partial class ConfirmProcess : Form
    {
        public event  ConProcDelegate ConProc;
        Project toCFProj;
        ProVsUse toCFPVU;
        public ConfirmProcess(Project toCFProj, ProVsUse toCFPVU)
        {
            InitializeComponent();
            this.toCFProj = toCFProj;
            this.toCFPVU = toCFPVU;
            maskedTextBox1.Text = toCFProj.getPID().ToString();
            maskedTextBox2.Text = toCFProj.getProName();
            maskedTextBox3.Text = toCFPVU.getPers().getUserName()+"("+toCFPVU.getPers().getUID().ToString()+")";
            maskedTextBox4.Text = toCFPVU.getProc().ToString();
            maskedTextBox5.Text = toCFPVU.getToProc().ToString();
            maskedTextBox7.Text = toCFPVU.getReport();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox6.Text == "")
                MessageBox.Show("请填写确认进度！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (int.Parse(maskedTextBox6.Text) > 100)
                MessageBox.Show("进度不可大于100！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                ConProc(maskedTextBox6.Text, toCFProj, toCFPVU);
                this.Close();
            }
        }
    }
}
