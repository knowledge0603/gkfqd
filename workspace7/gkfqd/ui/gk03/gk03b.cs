using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SuperMap.Data;

namespace gkfqd.ui.gk03
{
    public partial class gk03b : Form
    {
        #region 定义区
        private Datasource importDatasource;
        //工矿废弃地临时图层DatasetVector
        DatasetVector importResultShp = null;
        #endregion

        public gk03b()
        {
            InitializeComponent();
            workspace1 = new SuperMap.Data.Workspace();
           
            workspace1.Open(gkfqd.Common.Tool.GetConnectionInfo());
            importDatasource = workspace1.Datasources[gkfqd.Common.Tool.GetWorkspaceDataDatasources()];
            importResultShp = importDatasource.Datasets["IntersectTemp"] as DatasetVector;
            mapControl1.Map.Workspace = workspace1;
            mapControl1.Map.Layers.Clear();
            mapControl1.Map.Layers.Add(importResultShp, true);
            mapControl1.Map.ViewEntire();
            mapControl1.Map.Refresh();
        }
    }
}
