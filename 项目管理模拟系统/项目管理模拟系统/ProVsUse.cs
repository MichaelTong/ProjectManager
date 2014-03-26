using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 项目管理模拟系统
{
    public class ProVsUse : IComparable
    {
        int type;
        Personnel pers;
        Project proj;
        int graS;
        int graM;
        int proc;
        int toProc;
        int weight;
        public int rank;
        Plan projPlan;
        Plan weekPlan;
        String report;
        public bool warned;
        public bool setProjPlan(Plan projPlan)
        {
            this.projPlan=projPlan;
            return true;
        }
        public Plan getProjPlan()
        {
            return projPlan;
        }
        public bool setWeekPlan(Plan weekPlan)
        {
            this.weekPlan = weekPlan;
            return true;
        }
        public Plan getWeekPlan()
        {
            return weekPlan;
        }
        public int CompareTo(object obj)
        {
            ProVsUse otherProVsUse = obj as ProVsUse;
            if (otherProVsUse != null)
                return this.pers.getUID().CompareTo(otherProVsUse.pers.getUID());
            else
                throw new ArgumentException("Object is not a ProVsUse");
        }
        public ProVsUse()
        {
            projPlan = new Plan(0);
            weekPlan = new Plan(1);
            report="#";
        }
        public ProVsUse(Personnel pers, Project proj)
        {
            projPlan = new Plan(0);
            weekPlan = new Plan(1);
            report="#";
            this.pers = pers;
            this.proj = proj;
        }
        public bool setReport(String report)
        {
            this.report = report;
            return true;
        }
        public String getReport()
        {
            return report;
        }
        public bool setType(int type)
        {
            this.type = type;
            return true;
        }
        public int getType()
        {
            return type;
        }
        public bool setPers(Personnel pers)
        {
            this.pers = pers;
            return true;
        }
        public bool setProj(Project proj)
        {
            this.proj = proj;
            return true;
        }
        public Personnel getPers()
        {
            return pers;
        }
        public Project getProj()
        {
            return proj;
        }
        public bool setGraS(int graS)
        {
            this.graS = graS;
            return true;
        }
        public bool setGraM(int graM)
        {
            this.graM = graM;
            return true;
        }
        public int getGraS()
        {
            return graS;
        }
        public int getGraM()
        {
            return graM;
        }
        public bool setProc(int proc)
        {
            this.proc = proc;
            return true;
        }
        public int getProc()
        {
            return proc;
        }
        public bool setToProc(int toProc)
        {
            this.toProc = toProc;
            return true;
        }
        public int getToProc()
        {
            return toProc;
        }
        public bool setWeight(int weight)
        {
            this.weight = weight;
            return true;
        }
        public int getWeight()
        {
            return weight;
        }
    }
}
