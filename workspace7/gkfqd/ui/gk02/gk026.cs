using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SuperMap.Data;
using SuperMap.Mapping;
using SuperMap.UI;

namespace gkfqd.ui.gk02
{
    public partial class gk026 : WinFormsUI.Docking.DockContent
    {
        public gk026()
        {
            InitializeComponent();
        }
        //打开地图
        private void gk026_Load(object sender, EventArgs e)
        {
            Workspace m_workspace = new Workspace();
           /* WorkspaceConnectionInfo workspaceConnectionInfo = new WorkspaceConnectionInfo();
            workspaceConnectionInfo.Type = WorkspaceType.Oracle;
            workspaceConnectionInfo.Server = "ORCL";
            workspaceConnectionInfo.Database = "";
            workspaceConnectionInfo.Name = "workspace";
            workspaceConnectionInfo.User = "gkfqd";
            workspaceConnectionInfo.Password = "123456";
            m_workspace.Open(workspaceConnectionInfo);*/
            m_workspace.Open(gkfqd.Common.Tool.GetConnectionInfo());
            this.mapControl1.Map.Workspace = m_workspace;
            mapControl1.Map.Open(m_workspace.Maps[0]);
        }
        //释放对象
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mapControl1.Dispose();
            workspace1.Dispose();
        }
        //全屏显示
        private void FullScreen_Click(object sender, EventArgs e)
        {
            mapControl1.Map.ViewEntire();
        }
        //缩小
        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomOut;
        }
        //漫游
        private void btnPan_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.Pan;
        }
        //放大
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomIn;
        }
    }
}
