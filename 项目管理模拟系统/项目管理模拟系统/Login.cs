using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 项目管理模拟系统
{
    class Login
    {
        private String userName;
        private String password;
        private int rightnum;
        public Login(String user, String pass, int righ)
        {
            setUserName(user);
            setPassword(pass);
            setRightNum(righ);
        }

        public Login()
        {
            // TODO: Complete member initialization
        }
        
        public void setLogin(String user, String pass, int righ)
        {
            setUserName(user);
            setPassword(pass);
            setRightNum(righ);
        }

        public bool setUserName(String user)
        {
            userName = user;
            return true;
        }
        public bool setPassword(String pass)
        {
            password = pass;
            return true;
        }
        public bool setRightNum(int righ)
        {
            rightnum = righ;
            return true;
        }
        public bool check()
        {
            Lookup<String, Personnel> lookup = (Lookup<String, Personnel>)All.AllUser.ToLookup(p => p.getUserName(), p => p);
            if (userName == "" || password == "" || rightnum == 0)
            {
                MessageBox.Show("未选择登录角色或未输入用户名或密码！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            foreach(IGrouping<String,Personnel> userGroup in lookup)
            {
                if(userName==userGroup.Key)
                {
                    foreach(Personnel per in userGroup)
                    {
                        if (password == per.getPassword() && per.getRight(rightnum - 1))
                        {
                            All.curUser = per;
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("登录角色、用户名或密码错误！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }
                }
            }
            MessageBox.Show("用户不存在！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
    }
}
