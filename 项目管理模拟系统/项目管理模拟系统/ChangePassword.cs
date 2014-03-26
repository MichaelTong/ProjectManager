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

    public delegate void ChPassDelegate(String pass);
    public partial class ChangePassword : Form
    {
        public event ChPassDelegate ChPass;
        Personnel toCh;
        public ChangePassword(Personnel toCh)
        {
            InitializeComponent();
            this.toCh = toCh;
            if (toCh != All.curUser)
                maskedTextBox1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (toCh == All.curUser)
            {
                if (maskedTextBox1.Text != toCh.getPassword())
                    MessageBox.Show("原密码错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (maskedTextBox2.Text == "")
                {
                    MessageBox.Show("密码不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return ;
                }
                else if (!Check.CheckPassword(maskedTextBox2.Text, maskedTextBox3.Text))
                    MessageBox.Show("新密码输入不一致！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    ChPass(maskedTextBox2.Text);
                    this.Close();
                }
            }
            else
            {
                if (maskedTextBox2.Text == "")
                {
                    MessageBox.Show("密码不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (!Check.CheckPassword(maskedTextBox2.Text, maskedTextBox3.Text))
                    MessageBox.Show("新密码输入不一致！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    ChPass(maskedTextBox2.Text);
                    this.Close();
                }
            }
        }
    }
}
