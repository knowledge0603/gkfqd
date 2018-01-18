using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace gkfqd.ui.gk07
{
    public partial class gk072 :  WinFormsUI.Docking.DockContent
    {
        public gk072()
        {
            InitializeComponent();
            string str = System.IO.Directory.GetCurrentDirectory();
            //复制选择文件到临时文件夹，目的是重命名文件，导入到数据库指定文件中
            String targetPath = str + "\\template\\工矿废弃地管理系统开发项目帮助文档.mht";
            webBrowser1.Navigate(targetPath);
            //Process.Start("template\\工矿废弃地管理系统开发项目帮助文档.mht");
          //  System.Diagnostics.Process.Start("explorer.exe", "template\\工矿废弃地管理系统开发项目帮助文档.mht");
        }
    }
}
