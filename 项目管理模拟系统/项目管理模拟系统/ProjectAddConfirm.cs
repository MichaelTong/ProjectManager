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
    public delegate void ProjectAddConfirmDelegate();
    public partial class ProjectAddConfirm : Form
    {
        public event ProjectAddConfirmDelegate ProAC;
        Personnel manager;
        SortedSet<Personnel> PerSet;
        Project np;
        public ProjectAddConfirm(Personnel manager, SortedSet<Personnel> PerSet, Project np)
        {
            InitializeComponent();
            this.manager = manager;
            this.PerSet = PerSet;
            this.np = np;
            maskedTextBox1.Text = this.np.getPID().ToString();
            maskedTextBox2.Text = this.np.getProName();
            maskedTextBox3.Text = this.np.getBeginTime();
            maskedTextBox4.Text = this.np.getEndTime();
            dataGridView1.Rows.Add(this.manager.getUID().ToString(), this.manager.getUserName(), Check.GetUseProj(manager)[0], Check.GetUseProj(manager)[1]);
            foreach (Personnel per in PerSet)
            {
                dataGridView2.Rows.Add(per.getUID().ToString(), per.getUserName(), Check.GetUseProj(per)[0], Check.GetUseProj(per)[1]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            ProVsUse addPVU = new ProVsUse();
            All.AllProj.Add(np);
            addPVU.setType(0);
            addPVU.setProj(np);
            manager.setRight(1);
            RefreshFile.RefreshConnection(manager, addPVU, np);
            All.AllPVU.Add(addPVU);
            for (i = 0; i < dataGridView2.Rows.Count; i++)
            {
                addPVU = new ProVsUse();
                addPVU.setType(1);
                addPVU.setProj(np);
                RefreshFile.RefreshConnection(PerSet.ElementAt(i), addPVU, np);
                PerSet.ElementAt(i).setRight(0);
                RefreshFile.RefreshRank(addPVU);
                All.AllPVU.Add(addPVU);
            }
            ProAC();
            this.Close();
        }
    }
}
