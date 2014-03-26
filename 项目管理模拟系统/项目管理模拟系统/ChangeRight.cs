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
    public delegate void ChRightDelegate();
    public partial class ChangeRight : Form
    {
        public event ChRightDelegate ChRight;
        Personnel toChange;
        public ChangeRight(Personnel toChange)
        {
            InitializeComponent();
            this.toChange = toChange;
            checkBox1.Checked = this.toChange.getRight(0);
            checkBox2.Checked = this.toChange.getRight(1);
            checkBox3.Checked = this.toChange.getRight(2);
            checkBox4.Checked = this.toChange.getRight(3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false)
            {
                MessageBox.Show("请选择用户权限！", "未选择", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (Check.CheckRight(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked) == 2 && !toChange.getRight(2))
            {
                MessageBox.Show("已经设置过总经理！最多只能设置一名总经理", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (Check.CheckRight(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked) == 3 && !toChange.getRight(3))
            {
                MessageBox.Show("已经设置过项目管理员！最多只能设置一名项目管理员", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                this.toChange.setRight(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked, false);
                MessageBox.Show("修改成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChRight();
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
