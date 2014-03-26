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
    public delegate void MakePPDelegate(Project toMProj, ProVsUse toMPVU);
    public partial class MakeProjPlan : Form
    {
        public event MakePPDelegate MakePP;
        Project toMProj;
        ProVsUse toMPVU;
        Plan nPlan;
        public MakeProjPlan(Project toMProj, ProVsUse toMPVU)
        {
            InitializeComponent();
            this.toMProj = toMProj;
            this.toMPVU = toMPVU;
            textBox1.Text = toMProj.getProName() + "(" + toMProj.getPID().ToString() + ")";
            textBox2.Text = toMPVU.getPers().getUserName() + "(" + toMPVU.getPers().getUID().ToString() + ")";
            textBox10.Text = toMProj.getBeginTime();
            textBox11.Text = toMProj.getEndTime();
            textBox3.Text = toMPVU.getProjPlan().getBTime();
            textBox4.Text = toMPVU.getProjPlan().getETime();
            textBox5.Text = toMPVU.getWeight().ToString();
            textBox6.Text = toMPVU.getProjPlan().getPlan();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox9.Text == "")
            {
                MessageBox.Show("计划说明不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return ;
            }
            if(textBox8.Text==""||textBox7.Text=="")
            {
                MessageBox.Show("请选择计划时间！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(dateTimePicker1.Value.CompareTo(dateTimePicker2.Value)==1)
            {
                MessageBox.Show("计划起始时间不能晚于结束时间！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(Check.CompareDate(toMProj.getBYear(),toMProj.getBMonth(),toMProj.getBDay(),dateTimePicker1.Value.Year,dateTimePicker1.Value.Month,dateTimePicker1.Value.Day)==1||Check.CompareDate(toMProj.getEYear(),toMProj.getEMonth(),toMProj.getEDay(),dateTimePicker2.Value.Year,dateTimePicker2.Value.Month,dateTimePicker2.Value.Day)==-1)
            {
                MessageBox.Show("计划时间不能超出项目时间范围！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            nPlan = new Plan();
            nPlan.setBTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);
            nPlan.setETime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day);
            nPlan.setType(0);
            nPlan.setPlan(textBox9.Text);
            toMPVU.setProc(toMPVU.getProc()*toMPVU.getWeight()/(toMPVU.getWeight() + comboBox1.SelectedIndex + 1));
            toMPVU.setToProc(toMPVU.getProc());
            toMPVU.setWeight(toMPVU.getWeight()+comboBox1.SelectedIndex + 1);
            toMPVU.setProjPlan(nPlan);
            MessageBox.Show("新计划制定成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MakePP(toMProj, toMPVU);
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox8.Text = dateTimePicker1.Value.Year.ToString() + '-' + dateTimePicker1.Value.Month.ToString() + '-' + dateTimePicker1.Value.Day.ToString();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox7.Text = dateTimePicker2.Value.Year.ToString() + '-' + dateTimePicker2.Value.Month.ToString() + '-' + dateTimePicker2.Value.Day.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
