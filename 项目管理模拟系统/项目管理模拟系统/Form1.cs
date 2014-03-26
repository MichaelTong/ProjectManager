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
    public partial class Form1 : Form
    {
        Login loginSys;
        bool loginflag;
        public Form1()
        {
            InitializeComponent();
            this.comboBox1.Items.Add("项目组员");
            this.comboBox1.Items.Add("项目经理");
            this.comboBox1.Items.Add("总经理");
            this.comboBox1.Items.Add("项目管理员");
            this.comboBox1.Items.Add("系统管理员");
            this.comboBox1.SelectedIndex = 0;
            this.loginSys = new Login();
        }

        public void initiate()
        {
            setMasked1();
            setMasked2();
            setCombo();
        }
        private void setMasked1()
        {
            maskedTextBox1.Text = "";
        }
        private void setMasked2()
        {
            maskedTextBox2.Text = "";
        }
        private void setCombo()
        {
            comboBox1.SelectedIndex=0;
        }
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginSys.setLogin(maskedTextBox1.Text, maskedTextBox2.Text, comboBox1.SelectedIndex);
            loginflag = loginSys.check();
            if (loginflag)
            {
                this.Hide();
                Main mainForm = new Main();
                mainForm.Logout += new LogoutDelegate(Main_Logout);
                mainForm.Show();
            }
            else
                setMasked2();
        }

        void Main_Logout()
        {
            setMasked2();
            All.curUser = null;
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshFile.RefreshAllFiles();
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshFile.RefreshAllFiles();
            System.Environment.Exit(0);
        }
    }
}
