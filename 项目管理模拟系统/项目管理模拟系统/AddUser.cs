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
    public delegate void AddUserDelegate(Personnel toAdd);
    public partial class AddUser : Form
    {
        Personnel np;
        public event AddUserDelegate AdUser;
        public AddUser()
        {
            InitializeComponent();
            int i = All.AllUser.Count;
            maskedTextBox1.Text=(All.AllUser.ElementAt(i-1).getUID()+1).ToString();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            np = new Personnel();
            //判断用户名是否合法
            np.setUID(int.Parse(maskedTextBox1.Text));
            if (Check.CheckUserName(maskedTextBox2.Text) == 0)
            {
                MessageBox.Show("未输入用户名", "未输入", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (Check.CheckUserName(maskedTextBox2.Text) == -1)
            {
                MessageBox.Show("用户名重复！请重新输入", "重复", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                np.setUserName(maskedTextBox2.Text);
            //判断密码是否合法
            if (maskedTextBox3.Text == "")
            {
                MessageBox.Show("密码不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                maskedTextBox3.Text = "";
                maskedTextBox4.Text = "";
                return;
            }
            else if (!Check.CheckPassword(maskedTextBox3.Text , maskedTextBox4.Text))
            {
                MessageBox.Show("密码输入不一致！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                maskedTextBox3.Text="";
                maskedTextBox4.Text="";
                return;
            }
            else
                np.setPassword(maskedTextBox3.Text);
            //判断真实姓名是否合法
            if(maskedTextBox5.Text=="")
            {
                MessageBox.Show("未输入真实姓名！", "未输入", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                np.setRealName(maskedTextBox5.Text);
            }
            //判断生日是否合法
            if(maskedTextBox6.Text=="")
            {
                MessageBox.Show("请选择生日日期！", "未选择", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                np.setBirthday(dateTimePicker1.Value.Year,dateTimePicker1.Value.Month,dateTimePicker1.Value.Day);
            //判断工作日期是否合法
            if(maskedTextBox7.Text=="")
            {
                MessageBox.Show("请选择工作日期！", "未选择", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                np.setWorkday(dateTimePicker2.Value.Year,dateTimePicker2.Value.Month,dateTimePicker2.Value.Day);
            //判断权限是否合法
            if(checkBox1.Checked==false&&checkBox2.Checked==false&&checkBox3.Checked==false&&checkBox4.Checked==false)
            {
                MessageBox.Show("请选择用户权限！", "未选择", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if(Check.CheckRight(checkBox1.Checked,checkBox2.Checked,checkBox3.Checked,checkBox4.Checked)==2)
            {
                MessageBox.Show("已经设置过总经理！最多只能设置一名总经理", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if(Check.CheckRight(checkBox1.Checked,checkBox2.Checked,checkBox3.Checked,checkBox4.Checked)==3)
            {
                MessageBox.Show("已经设置过项目管理员！最多只能设置一名项目管理员", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                np.setRight(checkBox1.Checked,checkBox2.Checked,checkBox3.Checked,checkBox4.Checked,false);
            }
            np.setLevel(comboBox1.SelectedIndex+1);
            Card newCard=new Card(np);
            newCard.Confirm += new ConfirmDelegate(Card_Confirm);
            newCard.Show();
        }

        void Card_Confirm()
        {
            AdUser(np);
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            maskedTextBox6.Text = dateTimePicker1.Value.Year.ToString()+'-'+dateTimePicker1.Value.Month.ToString()+"-"+ dateTimePicker1.Value.Day.ToString();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            maskedTextBox7.Text = dateTimePicker2.Value.Year.ToString() + '-' + dateTimePicker2.Value.Month.ToString() + "-" + dateTimePicker2.Value.Day.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
