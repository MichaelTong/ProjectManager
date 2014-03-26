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
    public delegate void DelProjDelegate();
    public partial class DeleteProject : Form
    {
        public event DelProjDelegate DelProj;
        Project toDelete;
        public DeleteProject()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }
        public DeleteProject(Project toDelete)
        {
            InitializeComponent();
            this.toDelete = toDelete;
            if (toDelete != null)
            {
                String[] temp = Check.GetUseProj(toDelete);
                maskedTextBox1.Text = temp[0];
                maskedTextBox7.Text = temp[1];
                maskedTextBox3.Text = toDelete.getPID().ToString();
                maskedTextBox4.Text = toDelete.getProName();
                maskedTextBox5.Text = toDelete.getBeginTime();
                maskedTextBox6.Text = toDelete.getEndTime();
                comboBox1.Enabled = false;
                button1.Enabled = false;
                maskedTextBox2.Enabled = false;
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
                    toDelete = Check.SearchProj(0, maskedTextBox2.Text);
                else
                    toDelete = Check.SearchProj(1, maskedTextBox2.Text);
                if (toDelete != null)
                {
                    String[] temp = Check.GetUseProj(toDelete);
                    maskedTextBox1.Text = temp[0];
                    maskedTextBox7.Text = temp[1];
                    maskedTextBox3.Text = toDelete.getPID().ToString();
                    maskedTextBox4.Text = toDelete.getProName();
                    maskedTextBox5.Text = toDelete.getBeginTime();
                    maskedTextBox6.Text = toDelete.getEndTime();
                }
                else
                    MessageBox.Show("未找到此项目！请检查输入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (toDelete == null)
            {
                MessageBox.Show("未输入删除条目", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (toDelete.VsUse != null)
            {
                foreach (ProVsUse pvu in toDelete.VsUse)
                {
                    pvu.getPers().VsPro.Remove(pvu);
                    All.AllPVU.Remove(pvu);
                }
            }
            All.AllProj.Remove(toDelete);
            DelProj();
            MessageBox.Show("删除项目成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
