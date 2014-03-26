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

    public delegate void LogoutDelegate();
    public partial class Main : Form
    {
        bool perInfoShow;
        bool proInfoShow;
        bool useInfoShow;
        bool perProjShow;
        bool comLogShow;
        public event LogoutDelegate Logout;
        public Main()
        {
            InitializeComponent();
            maskedTextBox1.Text = All.curUser.getUserName();
            maskedTextBox2.Text = All.curUser.getRightText(); 
            perInfoShow = false;
            proInfoShow = false;
            useInfoShow = false;
            perProjShow = false;
            comLogShow = false;
            maskedTextBox3.Text = All.curUser.getUID().ToString();
            maskedTextBox4.Text = All.curUser.getUserName();
            maskedTextBox5.Text = All.curUser.getRealName();
            maskedTextBox6.Text = All.curUser.getBirthday();
            maskedTextBox7.Text = All.curUser.getWorkday();
            RefreshFile.RefreshNoticeBox(listBox1);
            if (All.curUser.getLevel() != -1)
                maskedTextBox8.Text = All.curUser.getLevel().ToString();
            else
                maskedTextBox8.Text = "null";
            maskedTextBox9.Text = All.curUser.getRightText();
            this.button2.Enabled = false;
            this.button3.Enabled = false;
            this.button4.Enabled = false;
            this.button17.Enabled = false;
            this.button25.Hide();
            if (All.curUser.getRight(0)||All.curUser.getRight(1))
            {
                RefreshFile.RefreshJoinedProjects(dataGridView3);
                RefreshFile.RefreshManProjects(dataGridView4);
                DateTime today = DateTime.Now;
                DateTime bt;
                DateTime et;
                String temp;
                TimeSpan t;
                int days;
                if(All.curUser.VsPro!=null)
                    foreach (ProVsUse pvu in All.curUser.VsPro)
                    {
                        bt = new DateTime();
                        et = new DateTime();
                        temp = "";
                        bt = bt.AddYears(pvu.getProj().getBYear() - 1);
                        bt = bt.AddMonths(pvu.getProj().getBMonth() - 1);
                        bt = bt.AddDays(pvu.getProj().getBDay() - 1);

                        et = et.AddYears(pvu.getProj().getEYear() - 1);
                        et = et.AddMonths(pvu.getProj().getEMonth() - 1);
                        et = et.AddDays(pvu.getProj().getEDay() - 1);

                        if (et.Date.CompareTo(today.Date.Date) == 1)
                        {
                            listBox2.Items.Add("=============================================================");
                            t = et - today;
                            days = (int)t.TotalDays;
                            if (pvu.getType() == 1)
                                temp = "您参与的项目 ";
                            else
                                temp = "您管理的项目 ";
                            temp += pvu.getProj().getProName() + "(" + pvu.getProj().getPID().ToString() + ") ";
                            temp += "将在 " + days.ToString() + " 天后" + "于" + et.ToLongDateString() + "结束";
                            listBox2.Items.Add(temp);
                            t = today - bt;
                            days = (int)t.TotalDays;
                            if (pvu.getType() == 0 && days % 30 <= 5)
                            {
                                temp = "请在" + (5 - days % 30).ToString() + "天内为组员制定任务计划。";
                                listBox2.Items.Add(temp);
                                temp = "若已制定或当前任务计划未完成请忽略。";
                                listBox2.Items.Add(temp);
                            }
                            else if (pvu.getType() == 1 && days % 7 <= 2)
                            {
                                temp = "请在" + (2 - days % 7).ToString() + "天内汇报进度并提交周计划。";
                                listBox2.Items.Add(temp);
                                temp = "若已完成请忽略。";
                            }
                            if (pvu.warned)
                            {
                                temp = "您在本项目 " + pvu.getProj().getProName() + "(" + pvu.getProj().getPID().ToString() + ") " + "由于一些原因被总经理警告！！！";
                                listBox2.Items.Add(temp);
                                listBox2.Items.Add("——若有疑议请尽快联系项目经理或总经理！");
                            }
                            if (pvu.getType() == 1 && pvu.getProj().VsUse.Count >= 4 && pvu.getProj().VsUse.Count - 1 - pvu.rank <= 1)
                            {
                                temp = "警告！！！您在本项目 " + pvu.getProj().getProName() + "(" + pvu.getProj().getPID().ToString() + ") " + "排名过低";
                                listBox2.Items.Add(temp);
                            }
                            listBox2.Items.Add("=============================================================");
                        }
                    }
                if (dataGridView4.Rows.Count == 0)
                {
                    button21.Enabled = false;
                    button22.Enabled = false;
                    button23.Enabled = false;
                    button24.Enabled = false;
                }
                if (dataGridView3.Rows.Count == 0)
                {
                    button18.Enabled = false;
                    button19.Enabled = false;
                    button20.Enabled = false;
                }
                this.button4.Enabled = true;
            }
            if (All.curUser.getRight(4))
            {
                this.button3.Enabled = true;
                this.button2.Enabled = false;
                this.button7.Enabled = false;
                this.button8.Enabled = false;
                this.button9.Enabled = false;
                RefreshFile.RefreshUserTable(dataGridView1);
                this.button25.Show();
            }
            if (All.curUser.getRight(2))
            {
                DateTime bt;
                DateTime et;
                String temp;
                int days;
                TimeSpan t;
                foreach (Project proj in All.AllProj)
                {
                    temp = "";
                    bt = new DateTime();
                    et = new DateTime();
                    bt = bt.AddYears(proj.getBYear() - 1);
                    bt = bt.AddMonths(proj.getBMonth() - 1);
                    bt = bt.AddDays(proj.getBDay() - 1);

                    et = et.AddYears(proj.getEYear() - 1);
                    et = et.AddMonths(proj.getEMonth() - 1);
                    et = et.AddDays(proj.getEDay() - 1);
                    if (et.Date.CompareTo(DateTime.Now.Date) == 1)
                    {
                        listBox2.Items.Add("=============================================================");
                        t = et - DateTime.Now;
                        days = (int)t.TotalDays;
                        temp += "项目 " + proj.getProName() + "(" + proj.getPID().ToString() + ") ";
                        temp += "将在 " + days.ToString() + " 天后" + "于" + et.ToLongDateString() + "结束";
                        listBox2.Items.Add(temp);
                        listBox2.Items.Add("=============================================================");
                    }
                }
                this.button17.Enabled = true;
                RefreshFile.RefreshAllProjTalbe(dataGridView7);
            }
            if (All.curUser.getRight(3)) 
            {
                this.button2.Enabled = true;
                RefreshFile.RefreshProjTable(dataGridView2);
            }
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
            tabControl1.TabPages.Remove(tabPage4);
            tabControl1.TabPages.Remove(tabPage5);
            tabControl1.TabPages.Remove(tabPage6);
            comboBox1.SelectedIndex = 0;
            groupBox7.Hide();
            groupBox8.Hide();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            Logout();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!perInfoShow)
            {
                tabControl1.TabPages.Add(tabPage2);
                tabControl1.SelectedTab = tabPage2;
                perInfoShow = true;
                button1.Text = "关闭个人信息页";
                pictureBox1.BackColor = System.Drawing.Color.FromName("Lime");
            }
            else
            {
                perInfoShow = false;
                if (tabControl1.SelectedTab == tabPage2)
                    tabControl1.SelectedIndex = 0;
                tabControl1.TabPages.Remove(tabPage2);
                pictureBox1.BackColor = System.Drawing.Color.FromName("Control");
                button1.Text = "打开个人信息页";
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (!proInfoShow)
            {
                tabControl1.TabPages.Add(tabPage3);
                tabControl1.SelectedTab = tabPage3;
                proInfoShow = true;
                button2.Text = "关闭项目管理页";
                pictureBox2.BackColor = System.Drawing.Color.FromName("Lime");
            }
            else
            {
                proInfoShow = false;
                if (tabControl1.SelectedTab == tabPage3)
                    tabControl1.SelectedIndex = 0;
                tabControl1.TabPages.Remove(tabPage3);
                pictureBox2.BackColor = System.Drawing.Color.FromName("Control");
                button2.Text = "打开项目管理页";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!useInfoShow)
            {
                tabControl1.TabPages.Add(tabPage4);
                tabControl1.SelectedTab = tabPage4;
                useInfoShow = true;
                button3.Text = "关闭用户管理页";
                pictureBox3.BackColor = System.Drawing.Color.FromName("Lime");
            }
            else
            {
                useInfoShow = false;
                if (tabControl1.SelectedTab == tabPage4)
                    tabControl1.SelectedIndex = 0;
                tabControl1.TabPages.Remove(tabPage4);
                pictureBox3.BackColor = System.Drawing.Color.FromName("Control");
                button3.Text = "打开用户管理页";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!perProjShow)
            {
                tabControl1.TabPages.Add(tabPage5);
                tabControl1.SelectedTab = tabPage5;
                perProjShow = true;
                button4.Text = "关闭本人项目";
                pictureBox4.BackColor = System.Drawing.Color.FromName("Lime");
            }
            else
            {
                perProjShow = false;
                if (tabControl1.SelectedTab == tabPage5)
                    tabControl1.SelectedIndex = 0;
                tabControl1.TabPages.Remove(tabPage5);
                pictureBox4.BackColor = System.Drawing.Color.FromName("Control");
                button4.Text = "查看本人项目";
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (!comLogShow)
            {
                tabControl1.TabPages.Add(tabPage6);
                tabControl1.SelectedTab = tabPage6;
                comLogShow = true;
                button17.Text = "关闭公司报告";
                pictureBox5.BackColor = System.Drawing.Color.FromName("Lime");
            }
            else
            {
                comLogShow = false;
                if (tabControl1.SelectedTab == tabPage6)
                    tabControl1.SelectedIndex = 0;
                tabControl1.TabPages.Remove(tabPage6);
                pictureBox5.BackColor = System.Drawing.Color.FromName("Control");
                button17.Text = "查看公司报告";
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            ChangeBirthday CB = new ChangeBirthday();
            CB.ChBirth += new ChBirthDelegate(ChangeBirthday_ChBirth);
            CB.Show();
        }

        void ChangeBirthday_ChBirth(int year, int month, int day)
        {
            All.curUser.setBirthday(year, month, day);
            MessageBox.Show("生日修改成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            maskedTextBox6.Text = All.curUser.getBirthday();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChangeWorkday CW = new ChangeWorkday();
            CW.ChWork += new ChWorkDelegate(ChangeWorkday_ChWork);
            CW.Show();
        }

        void ChangeWorkday_ChWork(int year, int month, int day)
        {
            All.curUser.setWorkday(year, month, day);
            MessageBox.Show("工作日期修改成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            maskedTextBox7.Text = All.curUser.getBirthday();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ChangeRealName CR = new ChangeRealName();
            CR.ChReal += new ChRealDelegate(ChangeRealName_ChReal);
            CR.Show();
        }

        void ChangeRealName_ChReal(String real)
        {
            All.curUser.setRealName(real);
            MessageBox.Show("真实姓名修改成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            maskedTextBox5.Text = All.curUser.getRealName();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ChangePassword CP = new ChangePassword(All.curUser);
            CP.ChPass += new ChPassDelegate(ChangePassword_ChPass);
            CP.Show();
        }

        void ChangePassword_ChPass(String pass)
        {
            All.curUser.setPassword(pass);
            MessageBox.Show("密码修改成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChangeUserName CU = new ChangeUserName(All.curUser);
            CU.ChUser += new ChUserDelegate(ChangeUserName_ChUser);
            CU.Show();
        }

        void ChangeUserName_ChUser(String user)
        {
            All.curUser.setUserName(user);
            MessageBox.Show("用户名修改成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            maskedTextBox4.Text = All.curUser.getUserName();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            AddUser AU = new AddUser();
            AU.AdUser += new AddUserDelegate(AddUser_AdUser);
            AU.Show();
        }

        void AddUser_AdUser(Personnel toAdd)
        {
            All.AllUser.Add(toAdd);
            RefreshFile.RefreshUserTable(dataGridView1);
            MessageBox.Show("添加用户成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("用户可能与项目关联，请谨慎操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DeleteUser DU = new DeleteUser();
            DU.DelUser+=new DelUserDelegate(DeleteUser_DelUser);
            DU.Show();
        }

        void DeleteUser_DelUser(Personnel toDelete)
        {
            All.AllUser.Remove(toDelete);
            if(toDelete.VsPro!=null)
                foreach (ProVsUse pvu in toDelete.VsPro)
                {
                    pvu.getProj().VsUse.Remove(pvu);
                    All.AllPVU.Remove(pvu);
                }
            RefreshFile.RefreshUserTable(dataGridView1);
            MessageBox.Show("删除用户成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            EditUser EU = new EditUser();
            EU.EdUser += new EdUserDelegate(EditUser_EdUser);
            EU.Show();
        }

        void EditUser_EdUser()
        {
            RefreshFile.RefreshUserTable(dataGridView1);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            AddProject AP = new AddProject();
            AP.AdProj += new AddProJectDelegate(AddProject_AdProj);
            AP.Show();
        }

        void AddProject_AdProj()
        {
            RefreshFile.RefreshProjTable(dataGridView2);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            DeleteProject DP = new DeleteProject();
            DP.DelProj+=new DelProjDelegate(DeleteProject_DelProj);
            DP.Show();
        }

        void DeleteProject_DelProj()
        {
            RefreshFile.RefreshProjTable(dataGridView2);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            EditProject EP = new EditProject();
            EP.EdProj += new EditProjDelegate(EditProject_EdProj);
            EP.Show();
        }

        void EditProject_EdProj()
        {
            RefreshFile.RefreshAllRank();
            RefreshFile.RefreshAllProcess();
            RefreshFile.RefreshProjTable(dataGridView2);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                groupBox8.Hide();
                groupBox7.Hide();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                groupBox8.Hide();
                groupBox7.Show();
            }
            else
            {
                groupBox8.Show();
                groupBox7.Hide();
            }
        }

        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            RefreshFile.RefreshFellowTable(Check.SearchProj(0,dataGridView4.CurrentRow.Cells[0].Value.ToString()), dataGridView5);
        }

        private void dataGridView5_SelectionChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            Project temp=Check.SearchProj(0,dataGridView4.CurrentRow.Cells[0].Value.ToString());
            if(Check.CompareDate(temp.getEYear(),temp.getEMonth(),temp.getEDay(),now.Year,now.Month,now.Day)==-1)
            {
                button21.Enabled=false;
                button22.Enabled=false;
                button23.Enabled=false;
                button24.Enabled=false;
            }
            else
            {
                button21.Enabled=true;
                button22.Enabled=true;
                button23.Enabled=true;
                button24.Enabled=true;
                if (dataGridView5.CurrentRow.Cells[3].Value.Equals(dataGridView5.CurrentRow.Cells[4].Value))
                    button21.Enabled = false;
                else
                    button21.Enabled = true;
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Project toCFProj = Check.SearchProj(0, dataGridView4.CurrentRow.Cells[0].Value.ToString());
            ProVsUse toCFPVU = null;
            foreach (ProVsUse temp in toCFProj.VsUse)
            {
                if (temp.getPers().getUID().ToString() == dataGridView5.CurrentRow.Cells[0].Value.ToString())
                {
                    toCFPVU = temp;
                    break;
                }
            }
            ConfirmProcess CP = new ConfirmProcess(toCFProj, toCFPVU);
            CP.ConProc += new ConProcDelegate(ConfirmProcess_ConProc);
            CP.Show();
        }

        void ConfirmProcess_ConProc(String nProc, Project toCFProj, ProVsUse toCFPVU)
        {
            int temp = dataGridView4.CurrentRow.Index;
            toCFPVU.setProc(int.Parse(nProc));
            toCFPVU.setToProc(int.Parse(nProc));
            RefreshFile.RefreshProcess(toCFProj);
            RefreshFile.RefreshManProjects(dataGridView4);
            dataGridView4.Rows[0].Selected = false; 
            dataGridView4.Rows[temp].Selected = true;
            dataGridView4.CurrentCell = dataGridView4.Rows[temp].Cells[0];
            MessageBox.Show("进度更新成功！请继续给组员评分！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Project toMProj = Check.SearchProj(0, dataGridView4.CurrentRow.Cells[0].Value.ToString());
            ProVsUse toMPVU = null;
            foreach (ProVsUse temp in toMProj.VsUse)
            {
                if (temp.getPers().getUID().ToString() == dataGridView5.CurrentRow.Cells[0].Value.ToString())
                {
                    toMPVU = temp;
                    break;
                }
            }
            MakeProjPlan MPP = new MakeProjPlan(toMProj, toMPVU);
            MPP.MakePP += new MakePPDelegate(MakeProjPlan_MakePP);
            MPP.Show();
        }

        void MakeProjPlan_MakePP(Project toMProj,ProVsUse toMPVU)
        {
            int temp = dataGridView4.CurrentRow.Index;
            RefreshFile.RefreshProcess(toMProj);
            RefreshFile.RefreshManProjects(dataGridView4);
            dataGridView4.Rows[0].Selected = false;
            dataGridView4.Rows[temp].Selected = true;
            dataGridView4.CurrentCell = dataGridView4.Rows[temp].Cells[0];
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Project upPro = Check.SearchProj(0, dataGridView4.CurrentRow.Cells[0].Value.ToString());
            ProVsUse upPVU = null;
            foreach (ProVsUse temp in upPro.VsUse)
            {
                if (temp.getPers().getUID().ToString() == dataGridView5.CurrentRow.Cells[0].Value.ToString())
                {
                    upPVU = temp;
                    break;
                }
            }
            Remark Rem = new Remark(upPVU.getPers(),upPVU);
            Rem.RM+=new RemDelegate(Remark_RM);
            Rem.Show();
        }

        void Remark_RM(Personnel upPer, ProVsUse upPVU)
        {
            int temp = dataGridView4.CurrentRow.Index;
            RefreshFile.RefreshRank(upPVU);
            RefreshFile.RefreshManProjects(dataGridView4);
            dataGridView4.Rows[0].Selected = false;
            dataGridView4.Rows[temp].Selected = true;
            dataGridView4.CurrentCell = dataGridView4.Rows[temp].Cells[0];
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Project RepPro = Check.SearchProj(0, dataGridView3.CurrentRow.Cells[0].Value.ToString());
            ProVsUse RepPVU = null;
            Personnel Manager = null;
            foreach (ProVsUse temp in RepPro.VsUse)
            {
                if (temp.getPers()==All.curUser)
                    RepPVU = temp;
                if (temp.getType() == 0)
                    Manager = temp.getPers();
            }
            MyReport MyR = new MyReport(RepPro, RepPVU, Manager);
            MyR.MyRep+=new MyReportDelegate(MyR_MyRep);
            MyR.Show();
        }
        void MyR_MyRep(Project RepPro, ProVsUse RepPVU)
        {
            RefreshFile.RefreshJoinedProjects(dataGridView3);
        }
        private void button19_Click(object sender, EventArgs e)
        {
            Project RepPro = Check.SearchProj(0, dataGridView3.CurrentRow.Cells[0].Value.ToString());
            ProVsUse RepPVU = null;
            foreach (ProVsUse temp in RepPro.VsUse)
            {
                if (temp.getPers() == All.curUser)
                {
                    RepPVU = temp;
                    break;
                }
            }
            SelfRemark SR = new SelfRemark(RepPVU);
            SR.SelfRem += new SelfRe(SelfRemark_SelfRem);
            SR.Show();
        }
        void SelfRemark_SelfRem(ProVsUse RepPVU)
        {
            MessageBox.Show("自我评价成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshFile.RefreshJoinedProjects(dataGridView3);
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            Project temp = Check.SearchProj(0, dataGridView3.CurrentRow.Cells[0].Value.ToString());
            DateTime now = DateTime.Now;
            if (Check.CompareDate(temp.getEYear(), temp.getEMonth(), temp.getEDay(), now.Year, now.Month, now.Day) == -1)
            {
                button18.Enabled = false;
                button19.Enabled = false;
                button20.Enabled = false;
            }
            else
            {
                button18.Enabled = true;
                button19.Enabled = true;
                button20.Enabled = true;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Project MWProj = Check.SearchProj(0, dataGridView3.CurrentRow.Cells[0].Value.ToString());
            ProVsUse MWPVU = null;
            foreach (ProVsUse temp in MWProj.VsUse)
            {
                if (temp.getPers() == All.curUser)
                {
                    MWPVU = temp;
                    break;
                }
            }
            MakeWeekPlan MWP = new MakeWeekPlan(MWPVU);
            MWP.MakWP += new MakWPDelegate(MakeWeekPlan_MakWP);
            MWP.Show();
        }
        void MakeWeekPlan_MakWP()
        {
            RefreshFile.RefreshJoinedProjects(dataGridView3);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Project toBossPro = Check.SearchProj(0, dataGridView4.CurrentRow.Cells[0].Value.ToString());
            BossReport BR = new BossReport(toBossPro);
            BR.BosRep += new BosRepDelegate(BossReport_BosRep);
            BR.Show();
        }

        void BossReport_BosRep()
        {
            MessageBox.Show("报告提交成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            MessageBox.Show("用户可能与项目关联，请谨慎操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Personnel toDelete = Check.SearchUser(0, dataGridView1.CurrentRow.Cells[0].Value.ToString());
            DeleteUser DU = new DeleteUser(toDelete);
            DU.DelUser += new DelUserDelegate(DeleteUser_DelUser);
            DU.Show();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            Personnel toEdit = Check.SearchUser(0, dataGridView1.CurrentRow.Cells[0].Value.ToString());
            EditUser EU = new EditUser(toEdit);
            EU.EdUser += new EdUserDelegate(EditUser_EdUser);
            EU.Show();
        }

        private void dataGridView7_SelectionChanged(object sender, EventArgs e)
        {
            Project temp = Check.SearchProj(0, dataGridView7.CurrentRow.Cells[0].Value.ToString());
            RefreshFile.RefreshBossFellowTable(temp, dataGridView6);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            Project toDelete = Check.SearchProj(0, dataGridView2.CurrentRow.Cells[0].Value.ToString());
            DeleteProject DU = new DeleteProject(toDelete);
            DU.DelProj += new DelProjDelegate(DeleteProject_DelProj);
            DU.Show();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            Project toEdit = Check.SearchProj(0, dataGridView2.CurrentRow.Cells[0].Value.ToString());
            EditProject EP = new EditProject(toEdit);
            EP.EdProj += new EditProjDelegate(EditProject_EdProj);
            EP.Show();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Project temp = Check.SearchProj(0, dataGridView7.CurrentRow.Cells[0].Value.ToString());
            Personnel toWarn = Check.SearchUser(0, dataGridView6.CurrentRow.Cells[0].Value.ToString());
            int i = dataGridView6.CurrentRow.Index;
            foreach(ProVsUse pvn in toWarn.VsPro)
                if (pvn.getProj() == temp)
                {
                    pvn.warned = !pvn.warned;
                }
            MessageBox.Show(button26.Text+"成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshFile.RefreshBossFellowTable(temp, dataGridView6);
            dataGridView6.Rows[0].Selected = false;
            dataGridView6.Rows[i].Selected = true;
            dataGridView6.CurrentCell = dataGridView6.Rows[i].Cells[0];
            if (dataGridView6.Rows[i].Cells[6].Value.Equals("是"))
                button26.Text = "取消警告";
            else
                button26.Text = "发送警告";
        }

        private void dataGridView6_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView6.CurrentRow.Cells[6].Value.Equals("是"))
                button26.Text = "取消警告";
            else
                button26.Text = "发送警告";
        }

        private void button30_Click(object sender, EventArgs e)
        {
            RealeaseNotice RN = new RealeaseNotice();
            RN.reNo+=new reNoDelegate(RN_reNo);
            RN.Show();
        }
        void RN_reNo()
        {
            RefreshFile.RefreshNoticeBox(listBox1);
            RefreshFile.RefreshNotice();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            RefreshFile.Backup();
            MessageBox.Show("数据备份成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshFile.RefreshAllFiles();
            Logout();
        }
    }
}
