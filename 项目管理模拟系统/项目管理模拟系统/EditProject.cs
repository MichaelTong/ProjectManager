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
    public delegate void EditProjDelegate();
    public partial class EditProject : Form
    {
        public event EditProjDelegate EdProj;
        private Project toEdit;
        private SortedSet<Personnel> previous;
        private Personnel Man;
        public EditProject()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }
        public EditProject(Project toEdit)
        {
            InitializeComponent();
            if (toEdit != null)
            {
                this.toEdit = toEdit;
                maskedTextBox1.Text = toEdit.getPID().ToString();
                maskedTextBox2.Text = toEdit.getProName();

                String[] date = toEdit.getBeginTime().Split('-');
                dateTimePicker1.Value = dateTimePicker1.Value.AddYears(int.Parse(date[0]) - dateTimePicker1.Value.Year);
                dateTimePicker1.Value = dateTimePicker1.Value.AddMonths(int.Parse(date[1]) - dateTimePicker1.Value.Month);
                dateTimePicker1.Value = dateTimePicker1.Value.AddDays(int.Parse(date[2]) - dateTimePicker1.Value.Day);

                date = toEdit.getEndTime().Split('-');
                dateTimePicker2.Value = dateTimePicker2.Value.AddYears(int.Parse(date[0]) - dateTimePicker2.Value.Year);
                dateTimePicker2.Value = dateTimePicker2.Value.AddMonths(int.Parse(date[1]) - dateTimePicker2.Value.Month);
                dateTimePicker2.Value = dateTimePicker2.Value.AddDays(int.Parse(date[2]) - dateTimePicker2.Value.Day);

                previous = new SortedSet<Personnel>();
                System.Windows.Forms.DataGridViewRowCollection rows1 = dataGridView1.Rows;
                System.Windows.Forms.DataGridViewRowCollection rows2 = dataGridView2.Rows;
                rows1.Clear();
                rows2.Clear();
                foreach (ProVsUse pvu in toEdit.VsUse)
                {
                    String[] useProj = Check.GetUseProj(pvu.getPers());
                    if (pvu.getType() == 0)
                        Man = pvu.getPers();
                    else
                        rows2.Add(false, true, pvu.getPers().getUID().ToString(), pvu.getPers().getUserName(), pvu.getProc(), useProj[0], useProj[1]);
                    previous.Add(pvu.getPers());
                }
                foreach (Personnel temp in All.AllUser)
                {
                    String[] useProj = Check.GetUseProj(temp);
                    bool flag = previous.Add(temp);
                    if (flag)
                        previous.Remove(temp);
                    if (!temp.getRight(2) && !temp.getRight(3) && !temp.getRight(4) && flag)
                        rows1.Add(false, false, temp.getUID().ToString(), temp.getUserName(), "0", useProj[0], useProj[1]);
                }
                maskedTextBox6.Text = Man.getUserName();
                comboBox1.Enabled = false;
                maskedTextBox5.Enabled = false;
                button5.Enabled = false;
            }
            else
                comboBox1.SelectedIndex = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Queue<DataGridViewRow> toCh = new Queue<DataGridViewRow>();
            DataGridViewRow move;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1[0, i].Value.Equals(true))
                {
                    toCh.Enqueue(dataGridView1.Rows[i]);
                }
            }
            while (toCh.Count != 0)
            {
                move = toCh.Dequeue();
                dataGridView1.Rows.Remove(move);
                move.Cells[0].Value = false;
                dataGridView2.Rows.Add(move.Cells[0].Value, move.Cells[1].Value, move.Cells[2].Value, move.Cells[3].Value, move.Cells[4].Value,move.Cells[5].Value,move.Cells[6].Value);
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
                dataGridView1.Rows.Add(move.Cells[0].Value, move.Cells[1].Value, move.Cells[2].Value, move.Cells[3].Value, move.Cells[4].Value, move.Cells[5].Value, move.Cells[6].Value);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (maskedTextBox5.Text == "")
            {
                MessageBox.Show("请输入检索内容！", "未输入", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (comboBox1.SelectedIndex == 0)
                    toEdit = Check.SearchProj(0, maskedTextBox5.Text);
                else
                    toEdit = Check.SearchProj(1, maskedTextBox5.Text);
                if (toEdit != null)
                {
                    maskedTextBox1.Text = toEdit.getPID().ToString();
                    maskedTextBox2.Text = toEdit.getProName();

                    String[] date = toEdit.getBeginTime().Split('-');
                    dateTimePicker1.Value = dateTimePicker1.Value.AddYears(int.Parse(date[0]) - dateTimePicker1.Value.Year);
                    dateTimePicker1.Value = dateTimePicker1.Value.AddMonths(int.Parse(date[1]) - dateTimePicker1.Value.Month);
                    dateTimePicker1.Value = dateTimePicker1.Value.AddDays(int.Parse(date[2]) - dateTimePicker1.Value.Day);

                    date = toEdit.getEndTime().Split('-');
                    dateTimePicker2.Value = dateTimePicker2.Value.AddYears(int.Parse(date[0]) - dateTimePicker2.Value.Year);
                    dateTimePicker2.Value = dateTimePicker2.Value.AddMonths(int.Parse(date[1]) - dateTimePicker2.Value.Month);
                    dateTimePicker2.Value = dateTimePicker2.Value.AddDays(int.Parse(date[2]) - dateTimePicker2.Value.Day);

                    previous=new SortedSet<Personnel>();
                    System.Windows.Forms.DataGridViewRowCollection rows1 = dataGridView1.Rows;
                    System.Windows.Forms.DataGridViewRowCollection rows2 = dataGridView2.Rows;
                    rows1.Clear();
                    rows2.Clear();
                    foreach (ProVsUse pvu in toEdit.VsUse)
                    {
                        String[] useProj = Check.GetUseProj(pvu.getPers());
                        if (pvu.getType() == 0)
                            Man = pvu.getPers();
                        else
                            rows2.Add(false, true, pvu.getPers().getUID().ToString(), pvu.getPers().getUserName(), pvu.getProc(), useProj[0], useProj[1]);
                        previous.Add(pvu.getPers());
                    }
                    foreach (Personnel temp in All.AllUser) 
                    {
                        String[] useProj = Check.GetUseProj(temp);
                        bool flag=previous.Add(temp);
                        if(flag)
                            previous.Remove(temp);
                        if (!temp.getRight(2) && !temp.getRight(3) && !temp.getRight(4)&& flag)
                            rows1.Add(false, false, temp.getUID().ToString(), temp.getUserName(),"0" ,useProj[0], useProj[1]);
                    }
                    maskedTextBox6.Text = Man.getUserName();
                }
                else
                    MessageBox.Show("未找到此项目！请检查输入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(maskedTextBox1.Text=="")
            {
                MessageBox.Show("未选择项目！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("项目起始时间不能晚于结束时间！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("参与人员不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (maskedTextBox2.Text != "")
                    toEdit.setProName(maskedTextBox2.Text);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[1].Value.Equals(true))
                    {
                        Personnel toCan = Check.SearchUser(0, row.Cells[2].Value.ToString());
                        foreach (ProVsUse pvu in toCan.VsPro)
                            if (pvu.getProj() == toEdit)
                            {
                                toEdit.VsUse.Remove(pvu);
                                toCan.VsPro.Remove(pvu);
                                All.AllPVU.Remove(pvu);
                                break;
                            }
                    }
                }
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells[1].Value.Equals(false))
                    {
                        Personnel toAdd = Check.SearchUser(0, row.Cells[2].Value.ToString());
                        ProVsUse npvu = new ProVsUse(toAdd, toEdit);
                        toAdd.setRight(1);
                        npvu.setType(1);
                        if (toAdd.VsPro == null)
                            toAdd.VsPro = new HashSet<ProVsUse>();
                        toAdd.VsPro.Add(npvu);
                        toEdit.VsUse.Add(npvu);
                        All.AllPVU.Add(npvu);
                    }
                }
                RefreshFile.RefreshProcess(toEdit);
                MessageBox.Show("修改项目成功！请通知项目经理重新配置任务计划", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EdProj();
                this.Close();
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
    }
}
