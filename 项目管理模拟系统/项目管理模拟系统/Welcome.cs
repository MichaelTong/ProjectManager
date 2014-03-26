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
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }
        public void load()
        {
            System.Threading.Thread.Sleep(2000); 
            LoadFile.loadAll();
        }
    }
}
