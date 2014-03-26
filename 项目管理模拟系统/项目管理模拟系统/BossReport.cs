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
    public delegate void BosRepDelegate();
    public partial class BossReport : Form
    {
        public event BosRepDelegate BosRep;
        Project toBossPro;
        public BossReport(Project toBossPro)
        {
            InitializeComponent();
            this.toBossPro = toBossPro;
            maskedTextBox1.Text = toBossPro.getPID().ToString();
            maskedTextBox2.Text = toBossPro.getProName();
            maskedTextBox3.Text = toBossPro.getBeginTime();
            maskedTextBox4.Text = toBossPro.getEndTime();
            maskedTextBox5.Text = toBossPro.getProc().ToString();
            String[] temp = Check.GetUseProj(toBossPro);
            maskedTextBox8.Text = temp[0];
            maskedTextBox6.Text = temp[1];
            maskedTextBox9.Text = toBossPro.getBossReport();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox7.Text == "")
            {
                MessageBox.Show("报告不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            toBossPro.setBossReport(maskedTextBox7.Text);
            BosRep();
            this.Close();
        }
    }
}
