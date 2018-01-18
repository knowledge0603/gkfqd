using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using WinFormsUI.Docking;


using gkfqd.ui;
using gkfqd.ui.gk01;
using gkfqd.ui.gk03;
using gkfqd.ui.gk06;
using gkfqd.ui.gk05;
using gkfqd.ui.gk07;
using System.Data.OleDb;

namespace gkfqd
{
    public partial class Menu : DevExpress.XtraEditors.XtraForm
    {
        #region 变量区

        StringBuilder sqlQuery = new StringBuilder();
        DataSet dataSet1 = new DataSet();
        

        #endregion

        #region 初始化
        string userId = "";
        public static string userRole = "";
        public static string userRoleStatic = "test1";
        public Menu(string userIdPara,string userRolePara)
        {
            InitializeComponent();
            userId = userIdPara;
            userRole = userRolePara;
        
            //当用户角色是2 ，浏览用户，只能看不能改删，看到所有用户的记录
            if (userRole=="2")
            {
                //复垦录入不可用
                barButtonItem34.Enabled = false;
                barButtonItem1.Enabled = false;
                barButtonItem26.Enabled = false;
               
            }
            this.dockPanel1_AutoSize();
        }
        private void dockPanel1_AutoSize()
        {
            this.dockPanel1.Width = this.ClientSize.Width;
            this.dockPanel1.Height = this.ClientSize.Height - 45;
        }
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gk032 gk032 = new gk032();
            this.ShowWindows_Click(gk032);
        }
        //问题报告提示
        private void Menu_Load(object sender, EventArgs e)
        {
                AlertWindows alertwindows = new AlertWindows();
                sqlQuery.Append(" SELECT * FROM wtbg ");
                sqlQuery.Append(" WHERE   ");
                sqlQuery.Append("  yh = '" + login.userId + "'");
                sqlQuery.Append(" and wtzt = '未解决'");
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gkfqd.Common.DbUse.GetOleDbconnection().Open();
                dataSet1.Clear();
                OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                MyAdapter1.Fill(dataSet1);
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                if (dataSet1.Tables[0].Rows.Count != 0)
                {
                    //取得包含标识
                    String strBhbs = dataSet1.Tables[0].Rows[0][0].ToString();
                    alertwindows.Show(strBhbs);
                }
                
        }
        #endregion

        #region 打开选项卡窗体..

        /// <summary>打开窗体</summary>
        /// <param name="form">窗体实例</param>
        private void ShowWindows_Click(DockContent form)
        {
            string strText = form.Text;
            if (this.FindTab(strText) == null)
            {
                form.Show(this.dockPanel1);
            }
            else
            {
                this.FindTab(strText).DockHandler.Show();
            }
        }

        #endregion

        #region 检查选项卡是否存在...

        /// <summary>检查选项卡是否存在。</summary>
        /// <param name="text">选项卡名称</param>
        /// <returns></returns>
        private IDockContent FindTab(string text)
        {
            if (this.dockPanel1.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                {
                    if (form.Text == text)
                    {
                        return form as IDockContent;
                    }
                }
            }
            else
            {
                foreach (IDockContent content in this.dockPanel1.Documents)
                {
                    if (content.DockHandler.TabText == text)
                    {
                        return content;
                    }
                }
            }
            return null;
        }

        #endregion
        
        #region 地图操作
        private void barButtonItem35_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gkfqd.ui.gk02.gk026 dktd = new gkfqd.ui.gk02.gk026();
            this.ShowWindows_Click(dktd);
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //gkfqd.ui.gk02.gk026.mapControl1.Map.ViewEntire();


        }
        #endregion

        #region 分析
        //占用地类分析
        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gkfqd.ui.gk04.gk041 dlfx = new gkfqd.ui.gk04.gk041();
            this.ShowWindows_Click(dlfx);
        }
        //规划符合分析
        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gkfqd.ui.gk04.gk042 ghfh = new gkfqd.ui.gk04.gk042();
            this.ShowWindows_Click(ghfh);
        }
        //新建土地供应
        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gkfqd.ui.gk04.gk043 tdgy = new gkfqd.ui.gk04.gk043();
            this.ShowWindows_Click(tdgy);
        }
        //基本农田数据分析
        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gkfqd.ui.gk04.gk044 jbnt = new gkfqd.ui.gk04.gk044();
            this.ShowWindows_Click(jbnt);
        }
        //土地复垦后地类分析
        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gkfqd.ui.gk04.gk045 tdfk = new gkfqd.ui.gk04.gk045();
            this.ShowWindows_Click(tdfk);
        }
        #endregion

        #region 菜单操作
        private void barButtonItem34_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gk014 gk014 = new gk014();
            this.ShowWindows_Click(gk014);
        }

        private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gk061 gk061 = new gk061();
            this.ShowWindows_Click(gk061);
        }

        private void barButtonItem36_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gk015 gk015 = new gk015();
            this.ShowWindows_Click(gk015);
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //复垦验收
            gk034 gk034 = new gk034();
            this.ShowWindows_Click(gk034);
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //指标使用
            gk035 gk035 = new gk035();
            this.ShowWindows_Click(gk035);
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //资金安排
            gk036 gk036 = new gk036();
            this.ShowWindows_Click(gk036);
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //指标下达
            gk037 gk037 = new gk037();
            this.ShowWindows_Click(gk037);
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //权属调整
            gk038 gk038 = new gk038();
            this.ShowWindows_Click(gk038);
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //验收考核
            gk039 gk039 = new gk039();
            this.ShowWindows_Click(gk039);
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //复垦建新
            gk03a gk03a = new gk03a();
            this.ShowWindows_Click(gk03a);
        }

        private void barButtonItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //历史项目监管
            gk051 gk051 = new gk051();
            this.ShowWindows_Click(gk051);
        }
          private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //重叠面积输出
            gk053 gk053 = new gk053();
            this.ShowWindows_Click(gk053);
        }

        private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //新建项目监管
            gk054 gk054 = new gk054();
            this.ShowWindows_Click(gk054);
        }

        private void barButtonItem29_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //影像图输出
            gk062 gk062 = new gk062();
            this.ShowWindows_Click(gk062);
        }

        private void barButtonItem31_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //专题图输出
            gk063 gk063 = new gk063();
            this.ShowWindows_Click(gk063);
        }

        private void barButtonItem32_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             //业务调查图输出
            gk064 gk064 = new gk064();
            this.ShowWindows_Click(gk064);
        }

        private void barButtonItem33_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //业务成果图输出
            gk065 gk065 = new gk065();
            this.ShowWindows_Click(gk065);

        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //地块查找
            gk016 gk016 = new gk016();
            this.ShowWindows_Click(gk016);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //建新项目添加
            gk018 gk018 = new gk018();
            this.ShowWindows_Click(gk018);
        }

        private void barButtonItem27_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //建新项目操作
            gk01a gk01a = new gk01a();
            this.ShowWindows_Click(gk01a);
        }

        private void barButtonItem37_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //建新地块操作
            gk01b gk01b = new gk01b();
            this.ShowWindows_Click(gk01b);
        }

        private void barButtonItem25_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // System.Diagnostics.Process.Start("explorer.exe", "template\\工矿废弃地管理系统开发项目帮助文档.docx");
            //建新地块操作
            gk072 gk072 = new gk072();
            this.ShowWindows_Click(gk072);
        }

        private void barButtonItem26_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //导入投影
            gk01g gk01g = new gk01g();
            this.ShowWindows_Click(gk01g);
        }

        private void barButtonItem29_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //导入投影
            gk01e gk01e = new gk01e();
            this.ShowWindows_Click(gk01e);
        }

        private void barButtonItem39_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //复垦进度
            gk033 gk033 = new gk033();
            this.ShowWindows_Click(gk033);
        }
            //问题报告
        private void barButtonItem42_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gk071 gk071 = new gk071();
            this.ShowWindows_Click(gk071);
        }

        private void 专题图_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gk063 gk063 = new gk063();
            this.ShowWindows_Click(gk063);
        }
        #endregion

        

        
    }
}