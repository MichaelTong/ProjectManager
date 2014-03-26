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
    public delegate void ChBirthDelegate(int year,int month,int day);
    public partial class ChangeBirthday : Form
    {
        public event ChBirthDelegate ChBirth;
        public ChangeBirthday()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            maskedTextBox1.Text = dateTimePicker1.Value.Year.ToString();
            maskedTextBox2.Text = dateTimePicker1.Value.Month.ToString();
            maskedTextBox3.Text = dateTimePicker1.Value.Day.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "" || maskedTextBox2.Text == "" || maskedTextBox3.Text == "")
                MessageBox.Show("请选择日期！", "未选择", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                int year = int.Parse(maskedTextBox1.Text);
                int month = int.Parse(maskedTextBox2.Text);
                int day = int.Parse(maskedTextBox3.Text);
                ChBirth(year, month, day);
                this.Close();
            }
        }
    }
}
