using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 项目管理模拟系统
{
    public  class Personnel:IComparable
    {
        private int UID;
        private String userName;
        private String password;
        private String realName;
        private int birthyear;
        private int birthmonth;
        private int birthday;
        private int workYear;
        private int workMonth;
        private int workDay;
        private int level;
        private bool[] right = new bool[5];
        public HashSet<ProVsUse> VsPro;
        public int CompareTo(object obj)
        {
            Personnel otherPersonnel = obj as Personnel;
            if (otherPersonnel != null)
                return this.UID.CompareTo(otherPersonnel.UID);
            else
                throw new ArgumentException("Object is not a Personnel");
        }
        public Personnel()
        {
        }
        public Personnel(String user, String pass, int leve, bool[] righ)
        {
            setUserName(user);
            setPassword(pass);
            setRight(righ);
            setLevel(leve);
        }
        public Personnel(int UID,String user, String pass, int leve)
        {
            this.UID = UID;
            setUserName(user);
            setPassword(pass);
            setLevel(leve);
        }
        public int getUID()
        {
            return UID;
        }
        public bool setUID(int UID)
        {
            this.UID = UID;
            return true;
        }
        public int getbirthyear()
        {
            return birthyear;
        }
        public int getbirthmonth()
        {
            return birthmonth;
        }
        public int getbirthday()
        {
            return birthday;
        }
        public int getworkyear()
        {
            return workYear;
        }
        public int getworkmonth()
        {
            return workMonth;
        }
        public int getworkday()
        {
            return workDay;
        }
        public bool setUserName(String str)
        {
            if(str!=null)
            {
                userName=str;
                return true;
            }
            else
                return false;
        }
        public bool setPassword(String str)
        {
            if(str!=null)
            {
                password=str;
                return true;
            }
            else
                return false;
        }
        public bool setRealName(String str)
        {
            if(str!=null)
            {
                realName=str;
                return true;
            }
            else
                return false;
        }
        public bool setBirthday(int year, int month, int day)
        {
            if(year!=0&&month!=0&&day!=0)
            {
                birthyear=year;
                birthmonth=month;
                birthday=day;
                return true;
            }
            else
                return false;
        }
        public bool setWorkday(int year, int month, int day)
        {
            if(year!=0&&month!=0&&day!=0)
            {
                workYear=year;
                workMonth=month;
                workDay=day;
                return true;
            }
            else
                return false;
        }
        public bool setLevel(int n)
        {
            level=n;
            return true;
        }
        public bool setRight(bool[] a)
        {
            for(int i=0;i<5;i++)
                right[i]=a[i];
            return true;
        }
        public bool setRight(int a1, int a2, int a3, int a4, int a5)
        {
            if (a1 == 1)
                right[0] = true;
            else
                right[0] = false;
            if (a2 == 1)
                right[1] = true;
            else
                right[1] = false;
            if (a3 == 1)
                right[2] = true;
            else
                right[2] = false;
            if (a4 == 1)
                right[3] = true;
            else
                right[3] = false;
            if (a5 == 1)
                right[4] = true;
            else
                right[4] = false;
            return true;
        }
        public bool setRight(bool r0, bool r1, bool r2, bool r3, bool r4)
        {
            right[0] = r0;
            right[1] = r1;
            right[2] = r2;
            right[3] = r3;
            right[4] = r4;
            return true;
        }
        public bool setRight(int i)
        {
            right[i] = true;
            return true;
        }
        public bool desetRight(int i)
        {
            right[i] = false;
            return true;
        }
        public String getUserName()
        {
            return userName;
        }
        public String getRealName()
        {
            if (realName == "_")
                return "null";
            else
                return realName;
        }
        public String getBirthday()
        {
            if (birthyear == -1 || birthmonth == -1 || birthday == -1)
                return "null";
            else
                return (birthyear.ToString() + "-" + birthmonth.ToString() + "-" + birthday.ToString());
        }
        public String getWorkday()
        {
            if (workYear == -1 || workMonth == -1 || workDay == -1)
                return "null";
            else
                return (workYear.ToString() + "-" + workMonth.ToString() + "-" + workDay.ToString());
        }
        public String getPassword()
        {
            return password;
        }
        public int getLevel()
        {
            return level;
        }
        public String getRightText()
        {
            String temp="";
            if (right[0])
                temp += "项目组员 ";
            if (right[1])
                temp += "项目经理 ";
            if (right[2])
                temp += "总经理 ";
            if (right[3])
                temp += "项目管理员 ";
            if (right[4])
                temp += "系统管理员";
            return temp;
        }
        public bool getRight(int i)
        {
            return right[i];
        }
    }
}
