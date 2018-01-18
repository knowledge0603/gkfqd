using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using SuperMap.Data;

namespace gkfqd.ui.gk01
{
    public partial class gk01d : Form
    {
        #region 变量区
        private Datasource gk01dDatasource;

        private DataGridViewRow gk01dDataGridViewRow;

        DataSet dataSet = new DataSet();

        //插入更新标记
        string insertUpdateFlag;
        //共同用  sqlQuery
        StringBuilder sqlQuery = new StringBuilder();
        #endregion

        #region 初始化

        public gk01d(Datasource datasource, DataGridViewRow dataGridViewRow, String projectName, String inputStatus, DataTable dt,string townName)
        {
            InitializeComponent();
            gk01dDatasource = datasource;
            gk01dDataGridViewRow = dataGridViewRow;
            textBox2.Text = projectName;
            textBox2.ReadOnly = true;
            textBox4.Text = townName;
            textBox4.ReadOnly = true;
            if (inputStatus == "已录入")
            {
                button1.Text = "更新";
                textBox1.Text = dt.Rows[0]["备注"].ToString();
                textBox2.Text = dt.Rows[0]["所属复垦项目名称"].ToString();
                textBox3.Text = dt.Rows[0]["地块编号"].ToString();
                textBox4.Text = dt.Rows[0]["项目所在县名称"].ToString();
                //地块编号设为自读 防止错误更新 没有主键更新 多条
                textBox3.ReadOnly = true;
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
            DatasetVector gkfqdDatasetVector = (DatasetVector)gk01dDatasource.Datasets["BG" + gkfqd.Common.DbUse.GetTownCode(textBox4.Text)];
            DatasetVector tempDatasetVector = (DatasetVector)gk01dDatasource.Datasets["temp_gkfqd"];

            string strSmid = gk01dDataGridViewRow.Cells["SMID"].Value.ToString();//获取smid值
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
            if (gkfqdRecordset == null)
            {
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
                //项目所在县
                if (textBox4.Text == "")
                {
                    MessageBox.Show("请输入项目所在县！");
                    return;
                }

                //项目名称
                if (textBox2.Text == "")
                {
                    MessageBox.Show("请输入项目名称！");
                    return;
                }
                //备注
                if (textBox1.Text == "")
                {
                    MessageBox.Show("请输入备注！");
                    return;
                }
                //地块编号
                if (textBox3.Text == "")
                {
                    MessageBox.Show("请输入地块编号！");
                    return;
                }

                if (button1.Text != "更新")
                {
                    //查询重复项目名称
                    sqlQuery.Clear();
                    sqlQuery.Append(" SELECT DKBH  FROM  " + "BG" + gkfqd.Common.DbUse.GetTownCode(textBox4.Text));
                    sqlQuery.Append(" WHERE DKBH ='" + textBox3.Text + "'");

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
                gkfqdRecordset.SetFieldValue("FKXMMC", textBox2.Text);
                //地块编号
                gkfqdRecordset.SetFieldValue("DKBH", textBox3.Text);
                //备注
                gkfqdRecordset.SetFieldValue("BZ", textBox1.Text);
                //项目所在县名
                gkfqdRecordset.SetFieldValue("XMSZXM", textBox4.Text);
             
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
            sqlQuery.Append(" UPDATE  " + "BG" + gkfqd.Common.DbUse.GetTownCode(textBox4.Text));
            //所属复垦项目名称  FKXMMC
            sqlQuery.Append(" SET  FKXMMC='" + textBox2.Text + "',");
            //项目所在县名称	
            sqlQuery.Append("XMSZXM='" + textBox4.Text + "',");
            //备注	
            sqlQuery.Append("BZ='" + textBox1.Text + "',");
            //地块编号
            sqlQuery.Append("DKBH='" + textBox3.Text + "'");
            
            //项目名称	FKXMMC
            sqlQuery.Append("   WHERE FKXMMC = '" + textBox2.Text + "'");
            //地块编号 DKBH
            sqlQuery.Append("   AND   DKBH = '" + textBox3.Text + "'");
            gkfqd.Common.DbUse.conn.Close();
            gkfqd.Common.DbUse.conn.Open();
            OleDbCommand updateCommand = new OleDbCommand(sqlQuery.ToString(), gkfqd.Common.DbUse.conn);
            updateCommand.ExecuteNonQuery();
            gkfqd.Common.DbUse.conn.Close();
            MessageBox.Show("补耕地块表更新成功");


        }
        #endregion
    }
}
