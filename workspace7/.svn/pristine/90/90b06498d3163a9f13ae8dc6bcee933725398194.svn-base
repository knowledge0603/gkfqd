using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperMap.Data;
using SuperMap.UI;
using System.Diagnostics;
using SuperMap.Mapping;
using System.Windows.Forms;
using System.IO;

namespace gkfqd.Common
{
    public class ImpImg
    {
        private Workspace workspace1;
        private MapControl m_mapControl;
        private Datasource m_datasource;
        private readonly String m_datasetImage = "temp_gkfqd01";


        public ImpImg(Workspace workspace, MapControl mapControl)
        {
            try
            {
                workspace1 = workspace;
                m_mapControl = mapControl;

                m_mapControl.Map.Workspace = workspace;
                Initialize();

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 打开需要的工作空间文件及地图
        /// </summary>
        private void Initialize()
        {
            try
            {
                //打开工作空间及地图
                WorkspaceConnectionInfo conInfo = new WorkspaceConnectionInfo(gkfqd.Common.Tool.GetConnectionInfo());

                workspace1.Open(conInfo);
                m_datasource = workspace1.Datasources[0];

                // 调整mapControl的状态
                m_mapControl.Action = SuperMap.UI.Action.Pan;
                m_mapControl.Map.ViewEntire();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        public void AddImage(Boolean isWithStyle)
        {
            try
            {

                DatasetImage dataset = m_datasource.Datasets[m_datasetImage] as DatasetImage;
                //设置风格并添加数据集
                Layer layer = null;
                if (isWithStyle)
                {
                    LayerSettingImage setting = new LayerSettingImage();
                    setting.OpaqueRate = 50;
                    layer = m_mapControl.Map.Layers.Add(dataset, setting, true);
                }
                else
                {
                    layer = m_mapControl.Map.Layers.Add(dataset, true);
                }
                //全幅显示添加的图层
                m_mapControl.Map.EnsureVisible(layer);
                m_mapControl.Map.Refresh();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 创建影像金字塔
        /// </summary>
        //public void BuildPyramid()
        //{
        //    try
        //    {
        //        if (m_datasetImage.BuildPyramid())
        //        {
        //            MessageBox.Show("创建影像金字塔成功");
        //        }
        //        else
        //        {
        //            MessageBox.Show("创建影像金字塔失败");

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.WriteLine(ex.Message);
        //    }
        //}
        /// <summary>
        /// 删除影像金字塔
        /// </summary>
        //public void RemovePyramid()
        //{
        //    try
        //    {
        //        if (m_datasetImage.RemovePyramid())
        //        {
        //            MessageBox.Show("删除影像金字塔成功");
        //        }
        //        else
        //        {
        //            MessageBox.Show("删除影像金字塔失败");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.WriteLine(ex.Message);
        //    }
        //}
    }
}
