using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperMap.Data;
using SuperMap.UI;
using System.Diagnostics;

namespace gkfqd.ui.gk04
{
    class gk04
    {
        private Workspace m_workspace;
        private MapControl mapControl;
        /// <summary>
        /// 根据workspace和map构造 gk04对象
        /// </summary>
        public gk04(Workspace workspace, MapControl mapControl)
        {
            try
            {
                m_workspace = workspace;
                mapControl = mapControl;
                mapControl.Map.Workspace = workspace;
                Initialize();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 打开需要的数据库及工作空间
        /// </summary>
        private void Initialize()
        {
            try
            {
                WorkspaceConnectionInfo wci = new WorkspaceConnectionInfo() { Type = WorkspaceType.Oracle, Server = "ORCL", Database = "", Name = "workspace", User = "gkfqd", Password = "123456" };
                m_workspace.Open(wci);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
    }
}
