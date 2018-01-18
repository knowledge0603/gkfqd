using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using SuperMap.UI;
using SuperMap.Data;
using SuperMap.Layout;

namespace gkfqd.ui.gk06
{
    public partial class gk065 : WinFormsUI.Docking.DockContent
    {
        private Int32 m_mapID;
        Boolean isLock = false;
        public gk065()
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
            mapLayoutControl1.MapLayout.Workspace = workspace1;

            mapLayoutControl1.IsHorizontalScrollbarVisible = true;
            mapLayoutControl1.IsVerticalScrollbarVisible = true;

            try
            {
                LayoutElements elements = mapLayoutControl1.MapLayout.Elements;
                //构造GeoMap
                GeoMap geoMap = new GeoMap();
                geoMap.MapName = "temp";

                //设置GeoMap对象的外切矩形
                Rectangle2D rect = new Rectangle2D(new Point2D(850, 1300), new Size2D(
                        1500, 1500));
                GeoRectangle geoRect = new GeoRectangle(rect, 0);
                geoMap.Shape = geoRect;
                elements.AddNew(geoMap);
                m_mapID = elements.GetID();

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            mapLayoutControl1.TrackMode = TrackMode.Edit;
            mapLayoutControl1.MapLayout.Zoom(4);
        }
    }
}
