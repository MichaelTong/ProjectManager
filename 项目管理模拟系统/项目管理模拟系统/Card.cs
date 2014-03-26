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
    public delegate void ConfirmDelegate();
    public partial class Card : Form
    {
        public event ConfirmDelegate Confirm;
        public Card(Personnel np)
        {
            InitializeComponent();
            maskedTextBox3.Text = np.getUID().ToString();
            maskedTextBox4.Text = np.getUserName();
            maskedTextBox5.Text = np.getRealName();
            maskedTextBox6.Text = np.getBirthday();
            maskedTextBox7.Text = np.getWorkday();
            maskedTextBox8.Text = np.getLevel().ToString();
            maskedTextBox9.Text = np.getRightText();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Confirm();
            this.Close();
        }
    }
}
