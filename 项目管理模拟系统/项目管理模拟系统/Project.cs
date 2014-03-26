using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 项目管理模拟系统
{
    public class Project:IComparable
    {
        private int PID;
        private String proName;
        private int bYear, bMonth, bDay;
        private int eYear, eMonth, eDay;
        private double proc;
        private String bossReport;
        public SortedSet<ProVsUse> VsUse;
        public int CompareTo(object obj)
        {
            Project otherProject = obj as Project;
            if (otherProject != null)
                return this.PID.CompareTo(otherProject.PID);
            else
                throw new ArgumentException("Object is not a Project");
        }
        public Project()
        {
            bossReport = "#";
            proc = 0;
        }
        public bool setBossReport(String bossReport)
        {
            this.bossReport = bossReport;
            return true;
        }
        public String getBossReport()
        {
            return bossReport;
        }
        public int getBYear()
        {
            return bYear;
        }
        public int getBMonth()
        {
            return bMonth;
        }
        public int getBDay()
        {
            return bDay;
        }
        public int getEYear()
        {
            return eYear;
        }
        public int getEMonth()
        {
            return eMonth;
        }
        public int getEDay()
        {
            return eDay;
        }
        public bool setPID(int PID)
        {
            this.PID = PID;
            return true;
        }
        public bool setProc(double proc)
        {
            this.proc = proc;
            return true;
        }
        public bool setProName(String proName)
        {
            this.proName = proName;
            return true;
        }
        public bool setBYear(int bYear)
        {
            this.bYear = bYear;
            return true;
        }
        public bool setBMonth(int bMonth)
        {
            this.bMonth = bMonth;
            return true;
        }
        public bool setBDay(int bDay)
        {
            this.bDay = bDay;
            return true;
        }
        public bool setEYear(int eYear)
        {
            this.eYear = eYear;
            return true;
        }
        public bool setEMonth(int eMonth)
        {
            this.eMonth = eMonth;
            return true;
        }
        public bool setEDay(int eDay)
        {
            this.eDay = eDay;
            return true;
        }
        public bool setBeginTime(int y, int m, int d)
        {
            return (setBYear(y) && setBMonth(m) && setBDay(d));
        }
        public bool setEndTime(int y, int m, int d)
        {
            return (setEYear(y) && setEMonth(m) && setEDay(d));
        }
        public int getPID()
        {
            return PID;
        }
        public String getProName()
        {
            return proName;
        }
        public String getBeginTime()
        {
            return (bYear.ToString()+"-"+bMonth.ToString()+"-"+bDay.ToString());
        }
        public String getEndTime()
        {
            return (eYear.ToString() + "-" + eMonth.ToString() + "-" + eDay.ToString());
        }
        public double getProc()
        {
            return proc;
        }
    }
}
