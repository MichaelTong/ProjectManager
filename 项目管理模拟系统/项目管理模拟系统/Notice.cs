using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 项目管理模拟系统
{
    class Notice:IComparable
    {
        String notice;
        public Notice()
        {
        }
        public int CompareTo(object obj)
        {
            Notice otherNotice = obj as Notice;
            if (otherNotice != null)
                return this.notice.CompareTo(otherNotice.notice);
            else
                throw new ArgumentException("Object is not a Notice");
        }
        public String getNotice()
        {
            return notice;
        }
        public bool setNotice(String str)
        {
            notice = str;
            return true;
        }
    }
}
