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
    public delegate void AddProJectDelegate();
    public partial class AddProject : Form
    {
        Project np;
        Personnel manager;
        SortedSet<Personnel> PerSet;
        public event AddProJectDelegate AdProj;
        public AddProject()
        {
            InitializeComponent();
            RefreshFile.RefreshAddProjTable(dataGridView1);
            PerSet = new SortedSet<Personnel>();
            int i = All.AllProj.Count;
            if (i != 0)
                maskedTextBox1.Text = (All.AllProj.ElementAt(i - 1).getPID() + 1).ToString();
            else
                maskedTextBox1.Text = "1";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Queue<DataGridViewRow> toCh=new Queue<DataGridViewRow>();
            DataGridViewRow move;
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                if (dataGridView1[0, i].Value.Equals(true))
                {
                    toCh.Enqueue(dataGridView1.Rows[i]);
                }
            }
            while(toCh.Count!=0)
            {
                move = toCh.Dequeue();
                dataGridView1.Rows.Remove(move);
                move.Cells[0].Value = false;
                dataGridView2.Rows.Add(move.Cells[0].Value, move.Cells[1].Value, move.Cells[2].Value, false, move.Cells[3].Value, move.Cells[4].Value);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Queue<DataGridViewRow> toCh = new Queue<DataGridViewRow>();
            DataGridViewRow move;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2[0, i].Value.Equals(true))
                {
                    toCh.Enqueue(dataGridView2.Rows[i]);
                }
            }
            while (toCh.Count != 0)
            {
                move = toCh.Dequeue();
                dataGridView2.Rows.Remove(move);
                move.Cells[0].Value = false;
                dataGridView1.Rows.Add(move.Cells[0].Value, move.Cells[1].Value, move.Cells[2].Value, move.Cells[4].Value, move.Cells[5].Value);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            maskedTextBox3.Text = dateTimePicker1.Value.Year.ToString() + "-" + dateTimePicker1.Value.Month.ToString() + "-" + dateTimePicker1.Value.Day.ToString();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            maskedTextBox4.Text = dateTimePicker2.Value.Year.ToString() + "-" + dateTimePicker2.Value.Month.ToString() + "-" + dateTimePicker2.Value.Day.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (maskedTextBox2.Text == "")
            {
                MessageBox.Show("未输入项目名称！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(maskedTextBox3.Text==""||maskedTextBox4.Text=="")
            {
                MessageBox.Show("未设置时间！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("项目起始时间不能晚于结束时间！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("参与项目的用户不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (dataGridView2.Rows.Count == 1)
            {
                MessageBox.Show("参与项目用户过少！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                int k = 0;
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    if (dataGridView2.Rows[i].Cells[3].Value != null && dataGridView2.Rows[i].Cells[3].Value.Equals(true))
                    {
                        manager = Check.SearchUser(0, dataGridView2.Rows[i].Cells[1].Value.ToString());
                        k++;
                    }
                    else
                        PerSet.Add(Check.SearchUser(0, dataGridView2.Rows[i].Cells[1].Value.ToString()));
                if (k == 0)
                {
                    MessageBox.Show("未设定项目经理！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (k > 1)
                {
                    MessageBox.Show("项目经理只能设定一个！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    np = new Project();
                    np.setPID(int.Parse(maskedTextBox1.Text));
                    np.setProName(maskedTextBox2.Text);
                    np.setBeginTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);
                    np.setEndTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day);
                    ProjectAddConfirm PAC = new ProjectAddConfirm(manager,PerSet,np);
                    PAC.ProAC+=new ProjectAddConfirmDelegate(ProjectAddConfirm_ProAC);
                    PAC.Show();
                }
            }
        }

        void ProjectAddConfirm_ProAC()
        {
            MessageBox.Show("项目添加成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AdProj();
            this.Close(); ;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
