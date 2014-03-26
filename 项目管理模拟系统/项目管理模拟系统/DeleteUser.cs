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
    public delegate void DelUserDelegate(Personnel toDelete);
    public partial class DeleteUser : Form
    {
        public event DelUserDelegate DelUser;
        Personnel toDelete;
        public DeleteUser()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }
        public DeleteUser(Personnel toDelete)
        {
            InitializeComponent();
            if (toDelete != null)
            {
                comboBox1.Enabled = false;
                maskedTextBox2.Enabled = false;
                button1.Enabled = false;
                this.toDelete = toDelete;
                maskedTextBox3.Text = toDelete.getUID().ToString();
                maskedTextBox4.Text = toDelete.getUserName();
                maskedTextBox5.Text = toDelete.getRealName();
                maskedTextBox6.Text = toDelete.getBirthday();
                maskedTextBox7.Text = toDelete.getWorkday();
                maskedTextBox8.Text = toDelete.getLevel().ToString();
                maskedTextBox9.Text = toDelete.getRightText();
            }
            else
                comboBox1.SelectedIndex = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox2.Text == "")
            {
                MessageBox.Show("请输入检索内容！", "未输入", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (comboBox1.SelectedIndex==0)
                    toDelete=Check.SearchUser(0,maskedTextBox2.Text);
                else
                    toDelete=Check.SearchUser(1,maskedTextBox2.Text);
                if(toDelete!=null)
                {
                    maskedTextBox3.Text = toDelete.getUID().ToString();
                    maskedTextBox4.Text = toDelete.getUserName();
                    maskedTextBox5.Text = toDelete.getRealName();
                    maskedTextBox6.Text = toDelete.getBirthday();
                    maskedTextBox7.Text = toDelete.getWorkday();
                    maskedTextBox8.Text = toDelete.getLevel().ToString();
                    maskedTextBox9.Text = toDelete.getRightText();
                }
                else
                    MessageBox.Show("未找到此用户！请检查输入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (toDelete == null)
                MessageBox.Show("请选择要删除的用户！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (toDelete == All.curUser)
                MessageBox.Show("不可删除系统管理员！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                DelUser(toDelete);
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
