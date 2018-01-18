using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SuperMap.Data;
using DevExpress.XtraBars.Docking;
using SuperMap.Analyst.SpatialAnalyst;
using SuperMap.Mapping;

namespace gkfqd.ui.gk03
{
    public partial class gk031 : WinFormsUI.Docking.DockContent
    {
        #region 定义区
        private Datasource importDatasource;
        //工矿废弃地临时图层DatasetVector
        DatasetVector importResultShp = null;
        #endregion

        #region 初始化
        public gk031()
        {
            InitializeComponent();
            workspace1 = new SuperMap.Data.Workspace();
           /* WorkspaceConnectionInfo workspaceConnectionInfo = new WorkspaceConnectionInfo();
            workspaceConnectionInfo.Type = WorkspaceType.Oracle;
            workspaceConnectionInfo.Server = "ORCL";
            workspaceConnectionInfo.Database = "";
            workspaceConnectionInfo.Name = "workspace";
            workspaceConnectionInfo.User = "gkfqd";
            workspaceConnectionInfo.Password = "123456";
            workspace1.Open(workspaceConnectionInfo);*/
            workspace1.Open(gkfqd.Common.Tool.GetConnectionInfo());
            importDatasource = workspace1.Datasources[gkfqd.Common.Tool.GetWorkspaceDataDatasources()];
            importResultShp = importDatasource.Datasets["gkfqd"] as DatasetVector;
            mapControl1.Map.Workspace = workspace1;
            mapControl1.Map.Layers.Clear();
            mapControl1.Map.Layers.Add(importResultShp, true);
            mapControl1.Map.ViewEntire();
            mapControl1.Map.Refresh();
        }
        #endregion 

        #region 菜单操作

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            DockPanel panelX = new DockPanel();

            panelX = this.dockManager1.AddPanel(DockingStyle.Right);

            System.Drawing.Size mSize = SystemInformation.WorkingArea.Size;

            panelX.Width = mSize.Width / 2;
            gk032 frmgk032 = new gk032(importDatasource, mapControl1);
            frmgk032.Dock = DockStyle.Fill;
            frmgk032.TopLevel = false;
            frmgk032.FormBorderStyle = FormBorderStyle.None;
            panelX.Controls.Add(frmgk032);
            frmgk032.Show();
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            mapControl1.Map.Refresh();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            mapControl1.Map.Refresh();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.Select;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.Pan;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomIn;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomOut;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomFree;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            mapControl1.Map.ViewEntire();
        }
        #endregion

    }
}
