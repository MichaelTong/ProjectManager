using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace 项目管理模拟系统
{
    static class LoadFile
    {
        public static void loadUser()
        {
            Match m;
            StreamReader sr = new StreamReader(@"data\user.dat");
            String nextLine;
            Personnel temp;
            All.AllUser = new SortedSet<Personnel>();
            while (!sr.EndOfStream)
            {
                nextLine = sr.ReadLine();
                m = Regex.Match(nextLine, @"(\d+)\s%(.+)%\s(\S+)\s%(.+)%\s(\-*\d+)\s(\-*\d+)\s(\-*\d+)\s(\-*\d+)\s(\-*\d+)\s(\-*\d+)\s(\-*\d+)\s(\-*\d+)\s(\-*\d+)\s(\-*\d+)\s(\-*\d+)\s(\-*\d+)\s");
                if (m.Success)
                {
                    temp = new Personnel(int.Parse(m.Groups[1].Value), m.Groups[2].Value, m.Groups[3].Value, int.Parse(m.Groups[11].Value));
                    temp.setBirthday(int.Parse(m.Groups[5].Value), int.Parse(m.Groups[6].Value), int.Parse(m.Groups[7].Value));
                    temp.setWorkday(int.Parse(m.Groups[8].Value), int.Parse(m.Groups[9].Value), int.Parse(m.Groups[10].Value));
                    temp.setRight(int.Parse(m.Groups[12].Value), int.Parse(m.Groups[13].Value), int.Parse(m.Groups[14].Value), int.Parse(m.Groups[15].Value), int.Parse(m.Groups[16].Value));
                    temp.setRealName(m.Groups[4].Value);
                    All.AllUser.Add(temp);
                }
            }
            sr.Close();
        }
        public static void loadProj()
        {
            Match m;
            StreamReader sr = new StreamReader(@"data\proj.dat");
            String nextLine;
            Project temp;
            All.AllProj = new SortedSet<Project>();
            while (!sr.EndOfStream)
            {
                nextLine = sr.ReadLine();
                m = Regex.Match(nextLine, @"(\d+)\s%(.+)%\s(\-*\d+)\s(\-*\d+)\s(\-*\d+)\s(\-*\d+)\s(\-*\d+)\s(\-*\d+)\[(.+)\]");
                if (m.Success)
                {
                    temp = new Project();
                    temp.setPID(int.Parse(m.Groups[1].Value));
                    temp.setProName(m.Groups[2].Value);
                    temp.setBeginTime(int.Parse(m.Groups[3].Value), int.Parse(m.Groups[4].Value), int.Parse(m.Groups[5].Value));
                    temp.setEndTime(int.Parse(m.Groups[6].Value), int.Parse(m.Groups[7].Value), int.Parse(m.Groups[8].Value));
                    temp.setBossReport(m.Groups[9].Value);
                    All.AllProj.Add(temp);
                }
            }
            sr.Close();
        }
        public static void loadPVU()
        {
            StreamReader sr = new StreamReader(@"data\projvsuser.dat");
            String nextLine;
            
            ProVsUse temp;
            Personnel tempPer;
            Project tempPro;
            Plan tempPlan;
            All.AllPVU = new HashSet<ProVsUse>();
            Match match;
            //**UID%[type]PID GraSelf GraManager Process toProcess@[ProjPlan] by bm bd ey em ed@[WeekPlan] by bm bd[report]warned...**type=0->manager type=1->fellow
            while (!sr.EndOfStream)
            {
                nextLine = sr.ReadLine();
                String[] m = nextLine.Split('&');
                tempPer = Check.SearchUser(0, m[0]);
                for(int i=1;i<m.Count();i++)
                {
                    match = Regex.Match(m[i], @"\[(\d+)\](\d+)\s(\d+)\s(\d+)\s(\d+)\s(\d+)\s(\d+)\@\[(.+)\]\s(\d+)\s(\d+)\s(\d+)\s(\d+)\s(\d+)\s(\d+)\@\[(.+)\]\s(\d+)\s(\d+)\s(\d+)\[(.+)\](\d)");
                    if (match.Success)
                    {
                        temp = new ProVsUse();
                        temp.setPers(tempPer);
                        temp.setType(int.Parse(match.Groups[1].Value));
                        tempPro = Check.SearchProj(0, match.Groups[2].Value);
                        temp.setProj(tempPro);
                        temp.setGraS(int.Parse(match.Groups[3].Value));
                        temp.setGraM(int.Parse(match.Groups[4].Value));
                        temp.setProc(int.Parse(match.Groups[5].Value));
                        temp.setToProc(int.Parse(match.Groups[6].Value));
                        temp.setWeight(int.Parse(match.Groups[7].Value));

                        tempPlan = new Plan(0);
                        tempPlan.setPlan(match.Groups[8].Value);
                        tempPlan.setBTime(int.Parse(match.Groups[9].Value), int.Parse(match.Groups[10].Value), int.Parse(match.Groups[11].Value));
                        tempPlan.setETime(int.Parse(match.Groups[12].Value), int.Parse(match.Groups[13].Value), int.Parse(match.Groups[14].Value));
                        temp.setProjPlan(tempPlan);

                        tempPlan = new Plan(1);
                        tempPlan.setPlan(match.Groups[15].Value);
                        tempPlan.setBTime(int.Parse(match.Groups[16].Value), int.Parse(match.Groups[17].Value), int.Parse(match.Groups[18].Value));
                        temp.setWeekPlan(tempPlan);
                        temp.setReport(match.Groups[19].Value);
                        temp.warned = int.Parse(match.Groups[20].Value) == 1 ? true : false;
                        if(tempPer.VsPro==null)
                            tempPer.VsPro=new HashSet<ProVsUse>();
                        if (tempPro.VsUse == null)
                            tempPro.VsUse = new SortedSet<ProVsUse>();
                        tempPer.VsPro.Add(temp);
                        tempPro.VsUse.Add(temp);
                        All.AllPVU.Add(temp);
                    }
                }
            }
            sr.Close();
        }
        public static void loadNotice()
        {
            StreamReader sr = new StreamReader(@"data\notice.dat");
            String nextLine;
            All.AllNotice = new SortedSet<Notice>();
            while (!sr.EndOfStream)
            {
                nextLine = sr.ReadLine();
                if (nextLine != "" || nextLine != "\n")
                {
                    Notice nN = new Notice();
                    nN.setNotice(nextLine);
                    All.AllNotice.Add(nN);
                }
            }
            sr.Close();
        }
        public static void loadAll()
        {
            loadUser();
            loadProj();
            loadPVU();
            loadNotice();
            RefreshFile.RefreshAllProcess();
            RefreshFile.RefreshAllRank();
        }
    }
}
