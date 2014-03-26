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
    public delegate void reNoDelegate();
    public partial class RealeaseNotice : Form
    {
        public event reNoDelegate reNo;
        public RealeaseNotice()
        {
            InitializeComponent();
            RefreshFile.RefreshNoticeBox(listBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox2.Text == "")
            {
                MessageBox.Show("请输入通知内容！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Notice nN = new Notice();
            nN.setNotice(DateTime.Now.ToLongDateString()+" "+DateTime.Now.ToLongTimeString() + ":" + maskedTextBox2.Text);
            All.AllNotice.Add(nN);
            MessageBox.Show("新通知发布成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            reNo();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
