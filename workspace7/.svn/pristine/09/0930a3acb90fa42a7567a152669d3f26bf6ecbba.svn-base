using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SuperMap.Data;
using SuperMap.UI;
using SuperMap.Mapping;

namespace SuperMap.SampleCode.Mapping
{
    public class SampleRun
    {
        private Workspace m_workspace;
        private MapControl mapControl1;
        private DatasetVector m_dataset;
        DatasetVector importResultShp = null;
        private Datasource importDatasource;


        /// <summary>
        /// 根据workspace和map构造 SampleRun对象
        /// </summary>
        public SampleRun(Workspace workspace, MapControl mapControl)
        {
            try
            {
                m_workspace = workspace;
                mapControl1 = mapControl;

                mapControl1.Map.Workspace = workspace;
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
               // WorkspaceConnectionInfo conInfo = new WorkspaceConnectionInfo(@"..\..\SampleData\City\Changchun.smwu");
                //WorkspaceConnectionInfo conInfo = new WorkspaceConnectionInfo() { Type = WorkspaceType.Oracle, Server = "ORCL", Database = "", Name = "workspace", User = "gkfqd", Password = "123456" };
                //m_workspace.Open(conInfo);
                //m_dataset = m_workspace.Datasources[0].Datasets["ClipResult"] as DatasetVector;

                //mapControl1.Map.Layers.Add(m_dataset, true);
                //mapControl1.Map.Refresh();


                Workspace x_workspace = new Workspace();
                WorkspaceConnectionInfo wci = new WorkspaceConnectionInfo() { Type = WorkspaceType.Oracle, Server = "ORCL", Database = "", Name = "workspace", User = "gkfqd", Password = "123456" };
                x_workspace.Open(wci);
                importDatasource = x_workspace.Datasources["ORCL_gkfqd"];
                importResultShp = importDatasource.Datasets["ClipResult"] as DatasetVector;
                mapControl1.Map.Workspace = x_workspace;
                mapControl1.Map.Layers.Clear();
                mapControl1.Map.Layers.Add(importResultShp, true);
                mapControl1.Map.ViewEntire();
                mapControl1.Map.Refresh();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 获取选中的记录集
        /// </summary>
        public Recordset GetSelectedRecordset(Boolean isAllRecord)
        {
            Recordset recordset = null;

            try
            {
                if (isAllRecord)
                {
                    recordset = m_dataset.GetRecordset(false, CursorType.Static);
                }
                else
                {
                    Selection[] selection = mapControl1.Map.FindSelection(true);
                    //判断选择集是否为空
                    if (selection != null && selection.Length != 0)
                    {
                        recordset = selection[0].ToRecordset();
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }

            return recordset;

        }


        /// <summary>
        /// 按照ID选中对象
        /// </summary>
        /// <param name="idArray"></param>
        public void SetSelectionByIDs(List<Int32> idArray)
        {
            Recordset recordset = null;
            try
            {
                recordset = m_dataset.Query(idArray.ToArray(), CursorType.Static);

                Selection selection = mapControl1.Map.Layers[0].Selection;

                selection.FromRecordset(recordset);

                mapControl1.Map.Refresh();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            finally
            {
                recordset.Dispose();
            }
        }

        /// <summary>
        /// 是否按照自定义风格显示
        /// </summary>
        /// <param name="isCustom"></param>
        public void ShowCustomStyle(Boolean isCustom)
        {
            try
            {
                Map map = mapControl1.Map;
                Selection selection = map.Layers[0].Selection;

                selection.SetStyleOptions(StyleOptions.FillBackColor, true);
                selection.SetStyleOptions(StyleOptions.FillBackOpaque, true);
                selection.SetStyleOptions(StyleOptions.FillForeColor, true);
                selection.SetStyleOptions(StyleOptions.FillGradientAngle, true);
                selection.SetStyleOptions(StyleOptions.FillGradientMode, true);
                selection.SetStyleOptions(StyleOptions.FillGradientOffsetRatioX, true);
                selection.SetStyleOptions(StyleOptions.FillGradientOffsetRatioY, true);
                selection.SetStyleOptions(StyleOptions.FillOpaqueRate, true);
                selection.SetStyleOptions(StyleOptions.FillSymbolID, true);
                selection.SetStyleOptions(StyleOptions.LineColor, true);
                selection.SetStyleOptions(StyleOptions.LineSymbolID, true);
                selection.SetStyleOptions(StyleOptions.LineWidth, true);
                selection.SetStyleOptions(StyleOptions.MarkerAngle, true);
                selection.SetStyleOptions(StyleOptions.MarkerSize, true);
                selection.SetStyleOptions(StyleOptions.MarkerSymbolID, true);

                selection.IsDefaultStyleEnabled = !isCustom;

                GeoStyle geoStyle = selection.Style;
                if (isCustom)
                {
                    geoStyle.FillForeColor = Color.Yellow;
                    geoStyle.FillBackOpaque = false;
                    geoStyle.FillBackColor = Color.Blue;
                    geoStyle.FillSymbolID = 0;
                    geoStyle.FillOpaqueRate = 25;
                    geoStyle.LineColor = Color.Red;
                    geoStyle.LineWidth = 1.0;
                }

                map.Refresh();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
    }
}
