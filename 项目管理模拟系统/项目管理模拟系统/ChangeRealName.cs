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
    public delegate void ChRealDelegate(String real);
    public partial class ChangeRealName : Form
    {
        public event ChRealDelegate ChReal;
        public ChangeRealName()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "" )
                MessageBox.Show("输入不能为空！", "未输入", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                ChReal(maskedTextBox1.Text);
                this.Close();
            }
        }
    }
}
