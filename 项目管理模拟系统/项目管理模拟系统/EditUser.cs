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
    public delegate void EdUserDelegate();
    public partial class EditUser : Form
    {
        Personnel toEdit;
        public event EdUserDelegate EdUser;
        public EditUser()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }
        public EditUser(Personnel toEdit)
        {
            InitializeComponent();
            if (toEdit != null)
            {
                comboBox1.Enabled = false;
                maskedTextBox2.Enabled = false;
                button1.Enabled = false;
                this.toEdit = toEdit;
                maskedTextBox3.Text = toEdit.getUID().ToString();
                maskedTextBox4.Text = toEdit.getUserName();
                maskedTextBox5.Text = toEdit.getRealName();
                maskedTextBox6.Text = toEdit.getBirthday();
                maskedTextBox7.Text = toEdit.getWorkday();
                maskedTextBox8.Text = toEdit.getLevel().ToString();
                maskedTextBox9.Text = toEdit.getRightText();
                if (toEdit == All.curUser)
                {
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button7.Enabled = false;
                    button8.Enabled = false;
                    button9.Enabled = false;
                }
                else
                {

                    button2.Enabled = true;
                    button3.Enabled = true;
                    button7.Enabled = true;
                    button8.Enabled = true;
                    button9.Enabled = true;
                }
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
                if (comboBox1.SelectedIndex == 0)
                    toEdit = Check.SearchUser(0, maskedTextBox2.Text);
                else
                    toEdit = Check.SearchUser(1, maskedTextBox2.Text);
                if (toEdit != null)
                {
                    maskedTextBox3.Text = toEdit.getUID().ToString();
                    maskedTextBox4.Text = toEdit.getUserName();
                    maskedTextBox5.Text = toEdit.getRealName();
                    maskedTextBox6.Text = toEdit.getBirthday();
                    maskedTextBox7.Text = toEdit.getWorkday();
                    maskedTextBox8.Text = toEdit.getLevel().ToString();
                    maskedTextBox9.Text = toEdit.getRightText();
                    if (toEdit == All.curUser)
                    {
                        button2.Enabled = false;
                        button3.Enabled = false;
                        button7.Enabled = false;
                        button8.Enabled = false;
                        button9.Enabled = false;
                    }
                    else
                    {

                        button2.Enabled = true;
                        button3.Enabled = true;
                        button7.Enabled = true;
                        button8.Enabled = true;
                        button9.Enabled = true;
                    }
                }
                else
                    MessageBox.Show("未找到此用户！请检查输入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (toEdit == null)
                MessageBox.Show("未选择用户！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                ChangePassword CP = new ChangePassword(toEdit);
                CP.ChPass += new ChPassDelegate(ChangePassword_ChPass);
                CP.Show();
            }
        }

        void ChangePassword_ChPass(String pass)
        {
            toEdit.setPassword(pass);
            MessageBox.Show("密码修改成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (toEdit == null)
                MessageBox.Show("未选择用户！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                ChangeRealName CR = new ChangeRealName();
                CR.ChReal += new ChRealDelegate(ChangeRealName_ChReal);
                CR.Show();
            }
        }

        void ChangeRealName_ChReal(String real)
        {
            toEdit.setRealName(real);
            MessageBox.Show("真实姓名修改成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refresh();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (toEdit == null)
                MessageBox.Show("未选择用户！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                ChangeBirthday CB = new ChangeBirthday();
                CB.ChBirth += new ChBirthDelegate(ChangeBirthday_ChBirth);
                CB.Show();
            }
        }

        void ChangeBirthday_ChBirth(int year, int month, int day)
        {
            toEdit.setBirthday(year, month, day);
            MessageBox.Show("生日修改成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refresh();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (toEdit == null)
                MessageBox.Show("未选择用户！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                ChangeWorkday CW = new ChangeWorkday();
                CW.ChWork += new ChWorkDelegate(ChangeWorkday_ChWork);
                CW.Show();
            }
        }

        void ChangeWorkday_ChWork(int year, int month, int day)
        {
            toEdit.setWorkday(year, month, day);
            MessageBox.Show("工作日期修改成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (toEdit == null)
                MessageBox.Show("未选择用户！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                ChangeLevelcs CL = new ChangeLevelcs();
                CL.ChLeve += new ChLeveDelegate(ChangeLevelcs_ChLeve);
                CL.Show();
            }
        }

        void ChangeLevelcs_ChLeve(int Leve)
        {
            toEdit.setLevel(Leve);
            MessageBox.Show("技术级别修改成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (toEdit == null)
                MessageBox.Show("未选择用户！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                ChangeRight CRI = new ChangeRight(toEdit);
                CRI.ChRight+=new ChRightDelegate(CHangeRight_ChRight);
                CRI.Show();
            }
        }

        void CHangeRight_ChRight()
        {
            refresh();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            EdUser();
            this.Close();
        }

        private void refresh()
        {
            if (toEdit != null)
            {
                maskedTextBox3.Text = toEdit.getUID().ToString();
                maskedTextBox4.Text = toEdit.getUserName();
                maskedTextBox5.Text = toEdit.getRealName();
                maskedTextBox6.Text = toEdit.getBirthday();
                maskedTextBox7.Text = toEdit.getWorkday();
                maskedTextBox8.Text = toEdit.getLevel().ToString();
                maskedTextBox9.Text = toEdit.getRightText();
            }
        }
    }
}
