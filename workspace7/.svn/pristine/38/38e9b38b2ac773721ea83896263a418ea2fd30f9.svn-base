using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SuperMap.Data;
using System.Data.OleDb;

namespace gkfqd.ui.gk01
{
    public partial class gk019 : Form
    {
        #region 变量区
        private Datasource gk019Datasource;
      
        private DataGridViewRow gk019DataGridViewRow;

        DataSet dataSet = new DataSet();
  
        //插入更新标记
        string insertUpdateFlag;
        //共同用  sqlQuery
        StringBuilder sqlQuery = new StringBuilder();
        #endregion

        #region 初始化
        public gk019(Datasource datasource, DataGridViewRow dataGridViewRow, String projectName, String inputStatus, DataTable dt,string townName)
        {
            InitializeComponent();

            gk019Datasource = datasource;
            gk019DataGridViewRow = dataGridViewRow;
            textBox3.Text = projectName;
            textBox3.ReadOnly = true;
            textBox4.Text = townName;
            textBox4.ReadOnly = true;
            if (inputStatus == "已录入")
            {
                button1.Text = "更新";
                textBox1.Text = dt.Rows[0]["地块编号"].ToString();
                textBox2.Text = dt.Rows[0]["地块位置"].ToString();

                textBox26.Text = dt.Rows[0]["地块名称"].ToString();
                textBox27.Text = dt.Rows[0]["地块面积"].ToString();
                textBox28.Text = dt.Rows[0]["坐标系"].ToString();
                textBox29.Text = dt.Rows[0]["界址点数"].ToString();
                textBox30.Text = dt.Rows[0]["几度分带"].ToString();
                textBox31.Text = dt.Rows[0]["精度"].ToString();
                textBox32.Text = dt.Rows[0]["计量单位"].ToString();
                textBox33.Text = dt.Rows[0]["记录图形属性"].ToString();
                textBox34.Text = dt.Rows[0]["投影类型"].ToString();
                
               //地块编号设为自读 防止错误更新 没有主键更新 多条
                textBox1.ReadOnly = true;
            }
            insertUpdateFlag = inputStatus;
        }
        #endregion

        #region 数据插入或更新
        private void button1_Click(object sender, EventArgs e)
        {
            if (insertUpdateFlag == "已录入")
            {
                UpdateData();
            }
            else
            {
                insertData();

            }
        }
        #endregion 

        #region 数据插入处理
        private void insertData()
        {
            #region 属性记录补登
            //文件上传成功后，文件表插入成功后，更新属性表记录
            //获得用于操作的两个数据集
            DatasetVector gkfqdDatasetVector = (DatasetVector)gk019Datasource.Datasets["JX"+gkfqd.Common.DbUse.GetTownCode(textBox4.Text)];
            DatasetVector tempDatasetVector = (DatasetVector)gk019Datasource.Datasets["temp_gkfqd"];

            string strSmid = gk019DataGridViewRow.Cells["SMID"].Value.ToString();//获取smid值
            // 构造一个查询参数对象，查询选中的记录
            QueryParameter para = new QueryParameter();
            para.AttributeFilter = "SMID = " + strSmid;
            para.CursorType = CursorType.Dynamic;


            Recordset tempRecordset = tempDatasetVector.Query(para);
            Recordset gkfqdRecordset = gkfqdDatasetVector.GetRecordset(false, CursorType.Dynamic);
            //判断添加地块在目标图层是否存在 标记
            Boolean flag = false;

            //临时图层到正式图层用smid
            string getSmid = "";
            if (gkfqdRecordset==null) {
                flag = false;
            }
            else if (gkfqdRecordset.RecordCount > 0)
            {
                for (int i = 0; i <= gkfqdRecordset.RecordCount; i++)
                {
                    gkfqdRecordset.MoveTo(i);
                    if (gkfqdRecordset.GetGeometry().Bounds.Equals(tempRecordset.GetGeometry().Bounds))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            if (!flag)
            {
                //非面数据不能导入
                if (tempRecordset.GetGeometry().GetType().Name.ToString() != "GeoRegion")
                {

                    MessageBox.Show("导入图斑不是面数据，不能导入，请重新选择！");
                    return;
                }
                //------以下为属性字段添加
                //地块编号
                if (textBox1.Text == "")
                {

                    MessageBox.Show("请输入地块编号！");
                    return;
                }
             
                //地块位置
                if (textBox2.Text == "")
                {

                    MessageBox.Show("请输入地块位置！");
                    return;
                }

                if (button1.Text != "更新")
                {
                    //查询重复项目名称
                    sqlQuery.Clear();
                    sqlQuery.Append(" SELECT DKBH  FROM  "+ "JX"+gkfqd.Common.DbUse.GetTownCode(textBox4.Text));
                    sqlQuery.Append(" WHERE DKBH ='" + textBox1.Text + "'");

                    gkfqd.Common.DbUse.GetOleDbconnection().Close();
                    gkfqd.Common.DbUse.GetOleDbconnection().Open();
                    dataSet.Clear();
                    OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                    MyAdapter.Fill(dataSet);
                    gkfqd.Common.DbUse.GetOleDbconnection().Close();
                    if (dataSet.Tables[0].Rows.Count != 0)
                    {
                        MessageBox.Show("该地块编号已经存在,请重新录入！");
                        return;
                    }
                }
                //临时图层到正式图层赋值
                gkfqdRecordset.AddNew(tempRecordset.GetGeometry());
                //所属复垦项目名称 主键
                gkfqdRecordset.SetFieldValue("FKXMMC", textBox3.Text);
                //地块编号
                gkfqdRecordset.SetFieldValue("DKBH", textBox1.Text);
                //地块位置
                gkfqdRecordset.SetFieldValue("DKWZ", textBox2.Text);
                //地块名称	
                gkfqdRecordset.SetFieldValue("DKMC", textBox26.Text);
                //地块面积	
                gkfqdRecordset.SetFieldValue("DKMJ", textBox27.Text);
                //坐标系	
                gkfqdRecordset.SetFieldValue("ZBX", textBox28.Text);
                //界址点数	
                gkfqdRecordset.SetFieldValue("JZDS", textBox29.Text);
                //几度分带	
                gkfqdRecordset.SetFieldValue("JDFD", textBox30.Text);
                //精度	
                gkfqdRecordset.SetFieldValue("JD", textBox31.Text);
                //计量单位     
                gkfqdRecordset.SetFieldValue("JLDW", textBox32.Text);
                //记录图形属性	
                gkfqdRecordset.SetFieldValue("JLTCSX", textBox33.Text);
                //投影类型
                gkfqdRecordset.SetFieldValue("TYLX", textBox34.Text);
                gkfqdRecordset.Update();
                //插入记录成功后防止再次点击插入设置按钮不可用
                button1.Enabled = false;
            }
            else
            {

                MessageBox.Show("该图斑已经存在，请确认或重新选择！");
                tempRecordset.Dispose();
                gkfqdRecordset.Dispose();
                return;
            }

            tempRecordset.Dispose();
            gkfqdRecordset.Dispose();
            gk011 frmGk011;
            frmGk011 = (gk011)this.Owner;
            frmGk011.RefreshDataGridView();
            MessageBox.Show("导入成功！");


            #endregion
        }
        #endregion

        #region 数据更新处理
        public void UpdateData()
        {
            sqlQuery.Clear();
            //工矿废弃地建新图层属性表   地块编号	
            sqlQuery.Append(" UPDATE  " + "JX" + gkfqd.Common.DbUse.GetTownCode(textBox4.Text) + " SET");
            
            //地块位置   DKWZ
            if (textBox2.Text!="")
            {
                sqlQuery.Append(" DKWZ='" + textBox2.Text + "',");
            }
            //地块名称	
            if (textBox26.Text != "")
            {
                sqlQuery.Append("DKMC='" + textBox26.Text + "',");
            }
            //地块面积	
            if (textBox27.Text != "")
            {
                sqlQuery.Append("DKMJ=" + textBox27.Text + ",");
            }
            //坐标系
            if (textBox28.Text != "")
            {
                sqlQuery.Append("ZBX='" + textBox28.Text + "',");
            }
            //界址点数
            if (textBox29.Text != "")
            {
                sqlQuery.Append("JZDS=" + textBox29.Text + ",");
            }
            //几度分带	
            if (textBox30.Text != "")
            {
                sqlQuery.Append("JDFD='" + textBox30.Text + "',");
            }
            //精度	
            if (textBox31.Text != "")
            {
                sqlQuery.Append("JD=" + textBox31.Text + ",");
            }
            //计量单位 
            if (textBox32.Text != "")
            {
                sqlQuery.Append("JLDW='" + textBox32.Text + "',");
            }
            //记录图形属性
            if (textBox33.Text != "")
            {
                sqlQuery.Append("JLTCSX='" + textBox33.Text + "',");
            }
            //投影类型
            if (textBox34.Text != "")
            {
                sqlQuery.Append("TYLX='" + textBox34.Text + "',");
            }
            //地块编号
            sqlQuery.Append("DKBH='" + textBox1.Text + "'");
            //项目名称	FKXMMC
            sqlQuery.Append("   WHERE FKXMMC = '" + textBox3.Text + "'");
            //地块编号 DKBH
            sqlQuery.Append("   AND   DKBH = '" + textBox1.Text + "'");
            gkfqd.Common.DbUse.conn.Close();
            gkfqd.Common.DbUse.conn.Open();
            OleDbCommand updateCommand = new OleDbCommand(sqlQuery.ToString(), gkfqd.Common.DbUse.conn);
            updateCommand.ExecuteNonQuery();
            gkfqd.Common.DbUse.conn.Close();
            MessageBox.Show("建新地块表更新成功");
            //更新成功后，不能再频繁点击更新按钮
            button1.Enabled = false;


        }
        #endregion

        #region 数字检查
        private void textBox27_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e,sender);
        }

        private void textBox29_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox30_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox31_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }
        #endregion
    }
}
