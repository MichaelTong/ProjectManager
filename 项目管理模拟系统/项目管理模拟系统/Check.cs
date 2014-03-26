using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 项目管理模拟系统
{
    static class Check
    {
        public static int CheckUserName(String str)
        {
            if (str == "")
                return 0;
            else
            {
                foreach (Personnel per in All.AllUser)
                    if (per.getUserName() == str)
                        return -1;
                return 1;
            }
        }
        public static bool CheckPassword(String str1, String str2)
        {
            if (str1 != str2)
                return false;
            else
                return true;
        }
        public static int CheckRight(bool r0, bool r1, bool r2, bool r3)
        {
            foreach (Personnel per in All.AllUser)
                if (r2 && per.getRight(2))
                    return 2;
                else if (r3 && per.getRight(3))
                    return 3;
            return 1;
        }
        public static Personnel SearchUser(int type, String key)
        {
            foreach (Personnel per in All.AllUser)
            {
                if (type == 0 && key == per.getUID().ToString())
                    return per;
                else if (type == 1 && key == per.getUserName())
                    return per;
            }
            return null;
        }
        public static Project SearchProj(int type, String key)
        {
            foreach (Project proj in All.AllProj)
            {
                if (type == 0 && key == proj.getPID().ToString())
                    return proj;
                else if (type == 1 && key == proj.getProName())
                    return proj;
            }
            return null;
        }
        public static String[] GetUseProj(Personnel per)
        {
            String[] useProj = {"",""};
            if (per.VsPro != null)
            {
                foreach (ProVsUse pvu in per.VsPro)
                    if (pvu.getType() == 0)
                        useProj[0] += (pvu.getProj().getProName() + "(" + pvu.getProj().getPID() + ") ");
                    else if (pvu.getType() == 1)
                        useProj[1] += (pvu.getProj().getProName() + "(" + pvu.getProj().getPID() + ") ");
            }
            return useProj;
        }
        public static String[] GetUseProj(Project proj)
        {
            String[] useProj = { "", "" };
            if (proj.VsUse != null)
            {
                foreach (ProVsUse pvu in proj.VsUse)
                    if (pvu.getType() == 0)
                        useProj[0] += (pvu.getPers().getUserName() + "(" + pvu.getPers().getUID() + ") ");
                    else if (pvu.getType() == 1)
                        useProj[1] += (pvu.getPers().getUserName() + "(" + pvu.getPers().getUID() + ") ");
            }
            return useProj;
        }
        public static int CompareDate(int y1, int m1, int d1, int y2, int m2, int d2)
        {
            if (y1 < y2)
                return -1;
            else if (y1 > y2)
                return 1;
            else
            {
                if (m1 < m2)
                    return -1;
                else if (m1 > m2)
                    return 1;
                else
                {
                    if (d1 < d2)
                        return -1;
                    else if (d1 > d2)
                        return 1;
                    else
                        return 0;
                }
            }
        }
    }
}
