using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace 项目管理模拟系统
{
    static class RefreshFile
    {
        public static bool RefreshAllFiles()
        {
            RefreshAllProcess();
            RefreshUser();
            RefreshProject();
            RefreshProjVsUser();
            RefreshNotice();
            return true;
        }
        public static bool RefreshUser()
        {
            String temp;
            StreamWriter sw = new StreamWriter(@"data\user.dat");
            temp = "**UID userName password realName birthyear birthmonth birthday  workYear workMonth workDay level right[] %Projects**" + "\n";
            sw.Write(temp);
            foreach (Personnel per in All.AllUser)
            {
                temp = per.getUID().ToString() + " %" + per.getUserName() + "% " + per.getPassword() + " %" + per.getRealName() + "% ";
                temp = temp + per.getbirthyear().ToString() + " " + per.getbirthmonth().ToString() + " " + per.getbirthday().ToString() + " ";
                temp = temp + per.getworkyear().ToString() + " " + per.getworkmonth().ToString() + " " + per.getworkday().ToString() + " ";
                temp = temp + per.getLevel().ToString() + " ";
                for (int i = 0; i < 5; i++)
                    temp = temp + (per.getRight(i) ? 1 : 0).ToString() + " ";
                temp = temp + "\n";
                sw.Write(temp);
            }
            sw.Close();
            return true;
        }
        public static bool RefreshProject()
        {
            String temp;
            StreamWriter sw = new StreamWriter(@"data\proj.dat");
            temp = "**PID proName bYear bMonth bDay eYear eMonth eDay[BossReport]**" + "\n";
            sw.Write(temp);
            foreach (Project proj in All.AllProj)
            {
                temp = proj.getPID().ToString() + " %" + proj.getProName() + "% ";
                temp = temp + proj.getBYear().ToString() + " " + proj.getBMonth().ToString() + " " + proj.getBDay().ToString() + " ";
                temp = temp + proj.getEYear().ToString() + " " + proj.getEMonth().ToString() + " " + proj.getEDay().ToString() + "["+proj.getBossReport()+"]\n";
                sw.Write(temp);
            }
            sw.Close();
            return true;
        }
        public static bool RefreshProjVsUser()
        {
            Lookup<int, ProVsUse> lookup = (Lookup<int, ProVsUse>)All.AllPVU.ToLookup(p => p.getPers().getUID(), p => p);
            StreamWriter sw = new StreamWriter(@"data\projvsuser.dat");
            String temp;
            //**UID%[type]PID GraSelf GraManager Process toProcess@[ProjPlan]by bm bd ey em ed@[WeekPlan]by bm bd...**type=0->manager type=1->fellow
            foreach(IGrouping<int,ProVsUse> PVUGroup in lookup)
            {
                sw.Write(PVUGroup.Key.ToString());
                    foreach(ProVsUse pvu in PVUGroup)
                    {
                        temp = "&[" + pvu.getType().ToString() + "]";
                        temp = temp + pvu.getProj().getPID().ToString() + " ";
                        temp = temp + pvu.getGraS().ToString() + " " + pvu.getGraM().ToString() + " ";
                        temp = temp + pvu.getProc().ToString() + " " + pvu.getToProc().ToString() + " ";
                        temp = temp + pvu.getWeight().ToString();
                        temp = temp + "@[" + pvu.getProjPlan().getPlan() + "] " + pvu.getProjPlan().getBY().ToString() + " " + pvu.getProjPlan().getBM().ToString() + " " + pvu.getProjPlan().getBD().ToString() + " " + pvu.getProjPlan().getEY().ToString() + " " + pvu.getProjPlan().getEM().ToString() + " " + pvu.getProjPlan().getED().ToString();
                        temp = temp + "@[" + pvu.getWeekPlan().getPlan() + "] " + pvu.getWeekPlan().getBY().ToString() + " " + pvu.getWeekPlan().getBM().ToString() + " " + pvu.getWeekPlan().getBD().ToString();
                        temp = temp + "[" + pvu.getReport() + "]";
                        temp = temp + (pvu.warned == true ? 1 : 0).ToString();
                        sw.Write(temp);
                    }
                    temp = "";
                    sw.Write("\n");
            }
            sw.Close();
            return true;
        }
        public static bool RefreshNotice()
        {
            StreamWriter sw = new StreamWriter(@"data\notice.dat");
            if (All.AllNotice != null)
                foreach (Notice n in All.AllNotice)
                    sw.WriteLine(n.getNotice());
            sw.Close();
            return true;
        }
        public static bool RefreshUserTable(System.Windows.Forms.DataGridView dataGridView1)
        {
            dataGridView1.Rows.Clear();
            System.Windows.Forms.DataGridViewRowCollection rows = dataGridView1.Rows;
            foreach (Personnel per in All.AllUser)
            {
                rows.Add(per.getUID(), per.getUserName(), per.getPassword(), per.getRealName(), per.getBirthday(), per.getWorkday(),per.getLevel().ToString(), per.getRight(0), per.getRight(1), per.getRight(2), per.getRight(3), per.getRight(4));
            }
            return true;
        }
        public static bool RefreshProjTable(System.Windows.Forms.DataGridView dataGridView2)
        {
            dataGridView2.Rows.Clear();
            System.Windows.Forms.DataGridViewRowCollection rows = dataGridView2.Rows;
            String manager;
            String fellow;
            foreach (Project proj in All.AllProj)
            {
                manager="";
                fellow="";
                if(proj.VsUse!=null)
                    foreach (ProVsUse pvu in proj.VsUse)
                    {
                        if (pvu.getType() == 0)
                            manager += pvu.getPers().getUserName() + "(" + pvu.getPers().getUID().ToString() + ")";
                        else if (pvu.getType() == 1)
                            fellow += pvu.getPers().getUserName() + "(" + pvu.getPers().getUID().ToString() + ") ";
                    }
                rows.Add(proj.getPID().ToString(), proj.getProName(), proj.getBeginTime(), proj.getEndTime(),proj.getProc(), manager, fellow);
            }
            return true;
        }
        public static bool RefreshAddProjTable(System.Windows.Forms.DataGridView dataGridView1)
        {
            dataGridView1.Rows.Clear();
            System.Windows.Forms.DataGridViewRowCollection rows = dataGridView1.Rows;
            foreach (Personnel per in All.AllUser)
            {
                String[] useProj = Check.GetUseProj(per);
                if(!per.getRight(2)&&!per.getRight(3)&&!per.getRight(4))
                    rows.Add(false, per.getUID().ToString(), per.getUserName(), useProj[0], useProj[1]);
            }
            return true;
        }
        public static bool RefreshConnection(Personnel pers, ProVsUse pvu, Project proj)
        {
            pvu.setProj(proj);
            pvu.setPers(pers);
            if (proj.VsUse == null)
                proj.VsUse = new SortedSet<ProVsUse>();
            if (pers.VsPro == null)
                pers.VsPro = new HashSet<ProVsUse>();
            proj.VsUse.Add(pvu);
            pers.VsPro.Add(pvu);
            return true;
        }
        public static bool RefreshJoinedProjects(System.Windows.Forms.DataGridView dataGridView3)
        {
            dataGridView3.Rows.Clear();
            System.Windows.Forms.DataGridViewRowCollection rows = dataGridView3.Rows;
            if (All.curUser.VsPro != null)
            {
                foreach (ProVsUse pvu in All.curUser.VsPro)
                    if (pvu.getType() != 0)
                        rows.Add(pvu.getProj().getPID().ToString(), pvu.getProj().getProName(), Check.GetUseProj(pvu.getProj())[0], pvu.getProj().getBeginTime(), pvu.getProj().getEndTime(), pvu.getProjPlan().getPlan(), pvu.getWeight(), pvu.getProjPlan().getBTime(), pvu.getProjPlan().getETime(), "(" + pvu.getWeekPlan().getBTime() + ")" + pvu.getWeekPlan().getPlan(), pvu.getProc(), pvu.getToProc(), pvu.getGraS(), pvu.getGraM(), pvu.rank);
            }
                return true;
        }
        public static bool RefreshManProjects(System.Windows.Forms.DataGridView dataGridView4)
        {
            dataGridView4.Rows.Clear();
            System.Windows.Forms.DataGridViewRowCollection rows = dataGridView4.Rows;
            if (All.curUser.VsPro != null)
            {
                foreach (ProVsUse pvu in All.curUser.VsPro)
                    if (pvu.getType() == 0)
                        rows.Add(pvu.getProj().getPID().ToString(), pvu.getProj().getProName(), pvu.getProj().getProc(), pvu.getProj().getBeginTime(), pvu.getProj().getEndTime(), Check.GetUseProj(pvu.getProj())[1]);
            }
            return true;
        }
        public static bool RefreshAllProcess()
        {
            foreach(Project proj in All.AllProj)
            {
                double x = 0, y = 0;
                if(proj.VsUse!=null)
                    foreach (ProVsUse pvu in proj.VsUse)
                    {
                        x += pvu.getWeight() * pvu.getProc();
                        y += pvu.getWeight();
                    }
                if (y != 0)
                    proj.setProc(x / y);
            }
            return true;
        }
        public static bool RefreshProcess(Project proj)
        {
            foreach (ProVsUse pvu in proj.VsUse)
            {
                if (pvu.getType() != 1)
                {
                    double x = 0, y = 0;
                    foreach (ProVsUse pvu1 in pvu.getProj().VsUse)
                    {
                        x += pvu1.getWeight() * pvu1.getProc();
                        y += pvu1.getWeight();
                    }
                    if (y != 0)
                        pvu.getProj().setProc(x / y);
                }
            }
            return true;
        }
        public static bool RefreshAllRank()
        {
            int rank;
            foreach (ProVsUse pvu in All.AllPVU)
            {
                rank = 0;
                if (pvu.getType() == 1)
                    foreach (ProVsUse pvu1 in pvu.getProj().VsUse)
                        if (pvu1.getType() == 1 && pvu1 != pvu && pvu1.getGraM() < pvu.getGraM())
                            rank++;
                pvu.rank = pvu.getProj().VsUse.Count - 1 - rank;
            }
            return true;
        }
        public static bool RefreshRank(ProVsUse pvu)
        {
            if (pvu.getType() != 0)
            {
                int rank = 0;
                foreach (ProVsUse pvu1 in pvu.getProj().VsUse)
                    if (pvu1.getType() == 1 && pvu1 != pvu && pvu1.getGraM() < pvu.getGraM())
                        rank++;
                pvu.rank = pvu.getProj().VsUse.Count - 1 - rank;
                return true;
            }
            return false;
        }
        public static bool RefreshFellowTable(Project proj,System.Windows.Forms.DataGridView dataGridView5)
        {
            dataGridView5.Rows.Clear();
            System.Windows.Forms.DataGridViewRowCollection rows = dataGridView5.Rows;
            foreach (ProVsUse pvu in proj.VsUse)
                if (pvu.getType() != 0)
                    rows.Add(pvu.getPers().getUID().ToString(), pvu.getPers().getUserName(), pvu.getWeight(), pvu.getProc(), pvu.getToProc(), pvu.getProjPlan().getPlan(), pvu.getProjPlan().getBTime(), pvu.getProjPlan().getETime(), pvu.getGraS(), pvu.getGraM(), "(" + pvu.getWeekPlan().getBTime() + ")" + pvu.getWeekPlan().getPlan());
            return true;
        }
        public static bool RefreshAllProjTalbe(System.Windows.Forms.DataGridView dataGridView7)
        {
            dataGridView7.Rows.Clear();
            System.Windows.Forms.DataGridViewRowCollection rows = dataGridView7.Rows;
            foreach (Project proj in All.AllProj)
            {
                String[] temp = Check.GetUseProj(proj);
                rows.Add(proj.getPID().ToString(), proj.getProName(), proj.getProc(), proj.getBeginTime(), proj.getEndTime(), temp[0], temp[1], proj.getBossReport());
            }
            return true;
        }
        public static bool RefreshBossFellowTable(Project proj, System.Windows.Forms.DataGridView dataGridView6)
        {
            dataGridView6.Rows.Clear();
            System.Windows.Forms.DataGridViewRowCollection rows = dataGridView6.Rows;
            foreach (ProVsUse pvu in proj.VsUse)
                if (pvu.getType() != 0)
                    rows.Add(pvu.getPers().getUID().ToString(), pvu.getPers().getUserName(), pvu.getWeight(), pvu.getProc(), pvu.getGraS(), pvu.getGraM(),pvu.warned==true?"是":"否");
            return true;
        }
        public static bool RefreshNoticeBox(System.Windows.Forms.ListBox listBox)
        {
            listBox.Items.Clear();
            for (int i = All.AllNotice.Count - 1; i >= 0; i--)
            {
                listBox.Items.Add(All.AllNotice.ElementAt(i).getNotice());
            }
            return true;
        }
        public static bool Backup()
        {
            RefreshAllProcess();
            BackUser();
            BackProject();
            BackProjVsUser();
            BackNotice();
            return true;
        }
        public static bool BackUser()
        {
            String temp;
            StreamWriter sw = new StreamWriter(@"Backup\user.dat");
            temp = "**UID userName password realName birthyear birthmonth birthday  workYear workMonth workDay level right[] %Projects**" + "\n";
            sw.Write(temp);
            foreach (Personnel per in All.AllUser)
            {
                temp = per.getUID().ToString() + " %" + per.getUserName() + "% " + per.getPassword() + " %" + per.getRealName() + "% ";
                temp = temp + per.getbirthyear().ToString() + " " + per.getbirthmonth().ToString() + " " + per.getbirthday().ToString() + " ";
                temp = temp + per.getworkyear().ToString() + " " + per.getworkmonth().ToString() + " " + per.getworkday().ToString() + " ";
                temp = temp + per.getLevel().ToString() + " ";
                for (int i = 0; i < 5; i++)
                    temp = temp + (per.getRight(i) ? 1 : 0).ToString() + " ";
                temp = temp + "\n";
                sw.Write(temp);
            }
            sw.Close();
            return true;
        }
        public static bool BackProject()
        {
            String temp;
            StreamWriter sw = new StreamWriter(@"Backup\proj.dat");
            temp = "**PID proName bYear bMonth bDay eYear eMonth eDay[BossReport]**" + "\n";
            sw.Write(temp);
            foreach (Project proj in All.AllProj)
            {
                temp = proj.getPID().ToString() + " %" + proj.getProName() + "% ";
                temp = temp + proj.getBYear().ToString() + " " + proj.getBMonth().ToString() + " " + proj.getBDay().ToString() + " ";
                temp = temp + proj.getEYear().ToString() + " " + proj.getEMonth().ToString() + " " + proj.getEDay().ToString() + "[" + proj.getBossReport() + "]\n";
                sw.Write(temp);
            }
            sw.Close();
            return true;
        }
        public static bool BackProjVsUser()
        {
            Lookup<int, ProVsUse> lookup = (Lookup<int, ProVsUse>)All.AllPVU.ToLookup(p => p.getPers().getUID(), p => p);
            StreamWriter sw = new StreamWriter(@"Backup\projvsuser.dat");
            String temp;
            //**UID%[type]PID GraSelf GraManager Process toProcess@[ProjPlan]by bm bd ey em ed@[WeekPlan]by bm bd...**type=0->manager type=1->fellow
            foreach (IGrouping<int, ProVsUse> PVUGroup in lookup)
            {
                sw.Write(PVUGroup.Key.ToString());
                foreach (ProVsUse pvu in PVUGroup)
                {
                    temp = "&[" + pvu.getType().ToString() + "]";
                    temp = temp + pvu.getProj().getPID().ToString() + " ";
                    temp = temp + pvu.getGraS().ToString() + " " + pvu.getGraM().ToString() + " ";
                    temp = temp + pvu.getProc().ToString() + " " + pvu.getToProc().ToString() + " ";
                    temp = temp + pvu.getWeight().ToString();
                    temp = temp + "@[" + pvu.getProjPlan().getPlan() + "] " + pvu.getProjPlan().getBY().ToString() + " " + pvu.getProjPlan().getBM().ToString() + " " + pvu.getProjPlan().getBD().ToString() + " " + pvu.getProjPlan().getEY().ToString() + " " + pvu.getProjPlan().getEM().ToString() + " " + pvu.getProjPlan().getED().ToString();
                    temp = temp + "@[" + pvu.getWeekPlan().getPlan() + "] " + pvu.getWeekPlan().getBY().ToString() + " " + pvu.getWeekPlan().getBM().ToString() + " " + pvu.getWeekPlan().getBD().ToString();
                    temp = temp + "[" + pvu.getReport() + "]";
                    temp = temp + (pvu.warned == true ? 1 : 0).ToString();
                    sw.Write(temp);
                }
                temp = "";
                sw.Write("\n");
            }
            sw.Close();
            return true;
        }
        public static bool BackNotice()
        {
            StreamWriter sw = new StreamWriter(@"Backup\notice.dat");
            if (All.AllNotice != null)
                foreach (Notice n in All.AllNotice)
                    sw.WriteLine(n.getNotice());
            sw.Close();
            return true;
        }
    }
}
