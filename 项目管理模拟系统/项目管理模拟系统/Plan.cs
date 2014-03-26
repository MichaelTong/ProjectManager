using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 项目管理模拟系统
{
    public class Plan
    {
        String plan;
        int type;
        int by, bm, bd;
        int ey, em, ed;
        public Plan()
        {
            plan = "#";
        }
        public Plan(int type)
        {
            this.type = type;
            plan = "#";
        }
        public bool setPlan(String str)
        {
            plan = str;
            return true;
        }
        public bool setType(int type)
        {
            this.type = type;
            return true;
        }
        public bool setBTime(int y, int m, int d)
        {
            by = y;
            bm = m;
            bd = d;
            return true;
        }
        public bool setETime(int y, int m, int d)
        {
            ey = y;
            em = m;
            ed = d;
            return true;
        }
        public String getPlan()
        {
            return plan;
        }
        public int getType()
        {
            return type;
        }
        public String getBTime()
        {
            return by.ToString() + "-" + bm.ToString() + "-" + bd.ToString();
        }
        public String getETime()
        {
            if (type == 1)
                return "";
            else
                return ey.ToString() + "-" + em.ToString() + "-" + ed.ToString();
        }
        public int getBY()
        {
            return by;
        }
        public int getBM()
        {
            return bm;
        }
        public int getBD()
        {
            return bd;
        }
        public int getEY()
        {
            return ey;
        }
        public int getEM()
        {
            return em;
        }
        public int getED()
        {
            return ed;
        }
    }
}
