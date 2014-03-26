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
    public delegate void ChUserDelegate(String user);
    public partial class ChangeUserName : Form
    {
        public event ChUserDelegate ChUser;
        public Personnel toChange;
        public ChangeUserName(Personnel toChange)
        {
            this.toChange = toChange;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (All.curUser.getUserName() == maskedTextBox1.Text)
            {
                MessageBox.Show("未更改！请重新输入", "未更改", MessageBoxButtons.OK, MessageBoxIcon.Information);
                maskedTextBox1.Text = "";
                return;
            }
            else
            {
                if (Check.CheckUserName(maskedTextBox1.Text) == 0)
                {
                    MessageBox.Show("未输入用户名", "未输入", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (Check.CheckUserName(maskedTextBox1.Text) == -1)
                {
                    MessageBox.Show("用户名重复！请重新输入", "重复", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    ChUser(maskedTextBox1.Text);
                    this.Close();
                }
            }
        }
    }
}
