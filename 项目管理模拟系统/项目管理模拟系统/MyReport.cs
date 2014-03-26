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
    public delegate void MyReportDelegate(Project RepPro,ProVsUse RepPVU);
    public partial class MyReport : Form
    {
        public event MyReportDelegate MyRep;
        Project RepPro;
        ProVsUse RepPVU;
        public MyReport(Project RepPro,ProVsUse RepPVU,Personnel Manager)
        {
            InitializeComponent();
            this.RepPro = RepPro;
            this.RepPVU = RepPVU;
            maskedTextBox1.Text = RepPro.getProName() + "(" + RepPro.getPID().ToString() + ")";
            maskedTextBox2.Text = Manager.getUserName() + "(" + Manager.getUID().ToString() + ")";
            maskedTextBox3.Text = RepPVU.getPers().getUserName() + "(" + RepPVU.getPers().getUID().ToString() + ")";
            maskedTextBox4.Text = RepPVU.getProjPlan().getPlan();
            maskedTextBox5.Text = RepPVU.getProc().ToString();
            maskedTextBox8.Text = RepPVU.getWeight().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox6.Text == "")
            {
                MessageBox.Show("汇报进度不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(int.Parse(maskedTextBox6.Text)>100)
            {
                MessageBox.Show("汇报进度不能超过100！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(int.Parse(maskedTextBox6.Text)<int.Parse(maskedTextBox5.Text))
            {
                MessageBox.Show("汇报进度不能小于当前进度！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(maskedTextBox7.Text=="")
            {
                MessageBox.Show("本周报告不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            RepPVU.setToProc(int.Parse(maskedTextBox6.Text));
            RepPVU.setReport(maskedTextBox7.Text);
            MyRep(RepPro, RepPVU);
            MessageBox.Show("报告成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
