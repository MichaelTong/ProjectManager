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
    public delegate void MakWPDelegate();
    public partial class MakeWeekPlan : Form
    {
        public event MakWPDelegate MakWP;
        ProVsUse MWPVU;
        public MakeWeekPlan(ProVsUse MWPVU)
        {
            InitializeComponent();
            this.MWPVU = MWPVU;
            maskedTextBox1.Text = MWPVU.getProj().getProName() + "(" + MWPVU.getProj().getPID() + ")";
            maskedTextBox2.Text = MWPVU.getProj().getBeginTime();
            maskedTextBox3.Text = MWPVU.getProj().getEndTime();
            maskedTextBox4.Text = MWPVU.getProjPlan().getPlan();
            maskedTextBox5.Text = MWPVU.getProjPlan().getBTime();
            maskedTextBox6.Text = MWPVU.getProjPlan().getETime();
            maskedTextBox7.Text = MWPVU.getProc().ToString();
            maskedTextBox8.Text = MWPVU.getWeight().ToString();
            maskedTextBox9.Text = "("+MWPVU.getWeekPlan().getBTime()+")"+MWPVU.getWeekPlan().getPlan();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox10.Text == "")
            {
                MessageBox.Show("计划不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DateTime now = DateTime.Now;
            Plan temp = new Plan();
            temp.setPlan(maskedTextBox10.Text);
            temp.setBTime(now.Year, now.Month, now.Day);
            MWPVU.setWeekPlan(temp);
            MessageBox.Show("计划提交成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MakWP();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
