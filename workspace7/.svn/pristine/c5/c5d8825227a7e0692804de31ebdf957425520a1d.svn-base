 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SuperMap.Data;
using System.IO;

using System.Data.OleDb;

namespace gkfqd.ui.gk01
{
    public partial class gk013 : Form
    {
        #region 变量定义区域
        private Datasource gk013Datasource;
        //选择上传文件路径
        String filePath = null;
        private DataGridViewRow gk013DataGridViewRow;
        //选择上传文件名
        string fileName = null;
        //文件长度
        long fileLength;
        //传输文件返回状态
        string transFileFlag;
        //插入更新标记
        string insertUpdateFlag;
        //共同用  sqlQuery
        StringBuilder sqlQuery = new StringBuilder();
        //查询地块编号用Dataset
        DataSet DataSetDkbh = new DataSet();
        //行政区名
        DataSet dataSetTownName = new DataSet();

        //oracle 连接 操作
        public static OleDbConnection conn = new OleDbConnection("Provider=MSDAORA.1;User ID=gkfqd;Password=123456;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST =192.168.1.103)(PORT = 1521))) (connECT_DATA = (SERVICE_NAME = orcl)))");

        #endregion

        #region 初始化
        public gk013(Datasource datasource, DataGridViewRow dataGridViewRow, String projectName, String inputStatus,DataTable dt,string townName)
        {
            InitializeComponent();
            gk013Datasource = datasource;
            gk013DataGridViewRow = dataGridViewRow;
            textBox10.Text = projectName;
            textBox10.ReadOnly = true;
            textBox35.Text = townName;
            textBox35.ReadOnly = true;
            if (inputStatus == "已录入")
            {
                button2.Text = "更新";
                textBox1.Text = dt.Rows[0]["地块编号"].ToString();
                textBox2.Text = dt.Rows[0]["地块中的国有面积"].ToString();
                textBox4.Text = dt.Rows[0]["地块位置"].ToString();
                textBox5.Text = dt.Rows[0]["地块中的集体面积"].ToString();
                comboBox9.Text = dt.Rows[0]["土源保障情况"].ToString();
                comboBox10.Text = dt.Rows[0]["现状地形坡度"].ToString();
                comboBox4.Text = dt.Rows[0]["水源保障情况"].ToString();
                textBox14.Text = dt.Rows[0]["预期复垦耕地等级"].ToString();
                comboBox16.Text = dt.Rows[0]["预期地形坡度"].ToString();
                textBox6.Text = dt.Rows[0]["其他规划名称"].ToString();
                comboBox1.Text = dt.Rows[0]["建设用地合法性"].ToString();
                comboBox2.Text = dt.Rows[0]["复垦义务人情况"].ToString();
                if (dt.Rows[0]["是否符合"].ToString() == "是")
                {
                    checkBox2.Checked = true;
                }
                else
                {
                    checkBox2.Checked = false;
                }
                if (dt.Rows[0]["是否符合土地利用总体规划"].ToString() == "是")
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
                if (dt.Rows[0]["现状有无污染状况"].ToString() == "有")
                {
                    checkBox3.Checked = true;
                }
                else
                {
                    checkBox3.Checked = false;
                }
                textBox7.Text = dt.Rows[0]["现状污染状况"].ToString();
                if (dt.Rows[0]["现状有无地质灾害隐患"].ToString() == "有")
                {
                    checkBox4.Checked = true;
                }
                else
                {
                    checkBox4.Checked = false;
                }
                textBox9.Text = dt.Rows[0]["交通运输用地"].ToString();
                textBox25.Text = dt.Rows[0]["工业用地"].ToString();
                textBox11.Text = dt.Rows[0]["其他建设用地"].ToString();
                textBox12.Text = dt.Rows[0]["采矿用地"].ToString();
                textBox8.Text = dt.Rows[0]["水域及水利设施用地"].ToString();
                textBox13.Text = dt.Rows[0]["现状小计"].ToString();
                textBox3.Text = dt.Rows[0]["有效土层厚度"].ToString();
                if (dt.Rows[0]["预期有无污染状况"].ToString() == "有")
                {
                    checkBox5.Checked = true;
                }
                else
                {
                    checkBox5.Checked = false;
                }
                textBox15.Text = dt.Rows[0]["预期污染状况"].ToString();
                if (dt.Rows[0]["预期有无地质灾害隐患"].ToString() == "有")
                {
                    checkBox6.Checked = true;
                }
                else
                {
                    checkBox6.Checked = false;
                }
                textBox16.Text = dt.Rows[0]["农村道路"].ToString();
                textBox18.Text = dt.Rows[0]["耕地"].ToString();
                textBox17.Text = dt.Rows[0]["园地"].ToString();
                textBox19.Text = dt.Rows[0]["坑塘水面"].ToString();
                textBox20.Text = dt.Rows[0]["林地"].ToString();
                textBox21.Text = dt.Rows[0]["草地"].ToString();
                textBox22.Text = dt.Rows[0]["其他农用地"].ToString();
                textBox23.Text = dt.Rows[0]["沟渠"].ToString();
                textBox24.Text = dt.Rows[0]["预期小计"].ToString();
                textBox26.Text = dt.Rows[0]["地块名称"].ToString();
                textBox27.Text = dt.Rows[0]["地块面积"].ToString();
                textBox28.Text = dt.Rows[0]["坐标系"].ToString();
                textBox29.Text = dt.Rows[0]["界址点数"].ToString();
                textBox30.Text = dt.Rows[0]["几度分带"].ToString();
                textBox31.Text = dt.Rows[0]["精度"].ToString();
                textBox32.Text = dt.Rows[0]["计量单位"].ToString();
                textBox33.Text = dt.Rows[0]["记录图形属性"].ToString();
                textBox34.Text = dt.Rows[0]["投影类型"].ToString();
                button2.Text = "更新";
                textBox10.ReadOnly = true;
                textBox1.ReadOnly = true;
            }
            insertUpdateFlag = inputStatus;
        }
       #endregion

        #region 入库按钮处理
        private void button1_Click(object sender, EventArgs e)
        {

            if (insertUpdateFlag == "已录入")
            {
                UpdateData();
            }
            else {
                insertData();
            
            }

        }
        #endregion

        #region 数字输入检查
        public void IsNumber(KeyPressEventArgs e, object sender)
        {
            if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键  
            if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数  
            if (e.KeyChar > 0x20)
            {
                try
                {
                    double.Parse(((TextBox)sender).Text + e.KeyChar.ToString());
                }
                catch
                {
                    e.KeyChar = (char)0;   //处理非法字符  
                    MessageBox.Show("只能输入数字！");
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox25_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox19_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox20_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox22_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox24_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox27_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox29_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox30_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox31_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        #endregion

        #region 数据插入处理
        private void insertData()
        {
            #region 属性记录补登

            //----------------拼接行政区图形数据存储表名
            string tableName = gkfqd.Common.DbUse.GetTownCode(textBox35.Text) ;

            //文件上传成功后，文件表插入成功后，更新属性表记录
            //获得用于操作的两个数据集
            DatasetVector gkfqdDatasetVector = (DatasetVector)gk013Datasource.Datasets[tableName];
            DatasetVector tempDatasetVector = (DatasetVector)gk013Datasource.Datasets["temp_gkfqd"];

            string strSmid = gk013DataGridViewRow.Cells["SMID"].Value.ToString();//获取smid值
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
            if (gkfqdRecordset==null)
            {
                 flag =false;
            } else if(gkfqdRecordset.RecordCount > 0)
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
                //国有面积
                if (textBox2.Text == "")
                {
                    MessageBox.Show("请输入国有面积！");
                    return;
                }
                //地块位置
                if (textBox4.Text == "")
                {
                    MessageBox.Show("请输入地块位置！");
                    return;
                }
                //集体面积
                if (textBox5.Text == "")
                {
                    MessageBox.Show("请输入集体面积！");
                    return;
                }
                //土源保障情况
                if (comboBox9.Text == "")
                {
                    MessageBox.Show("请输入土源保障情况！");
                    return;
                }
                //地块现状的地形坡度
                if (comboBox10.Text == "")
                {
                    MessageBox.Show("请输入地块现状的地形坡度！");
                    return;
                }
                //水源保障情况
                if (comboBox4.Text == "")
                {
                    MessageBox.Show("请输入水源保障情况！");
                    return;
                }
                //预期复垦更低等级
                if (textBox14.Text == "")
                {
                    MessageBox.Show("请输入预期复垦更低等级！");
                    return;
                }
                //预期地块的地形坡度
                if (comboBox16.Text == "")
                {
                    MessageBox.Show("请输入预期地块的地形坡度！");
                    return;
                }
                //建设用地合法性
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("请选择建设用地合法性！");
                    return;
                }
                //复垦义务人情况
                if (comboBox2.Text == "")
                {
                    MessageBox.Show("请选择复垦义务人情况！");
                    return;
                }
                //查询重复地块编号，若重复则重新输入地块编号
                sqlQuery.Clear();
                sqlQuery.Append(" SELECT DKBH  FROM  " + tableName);
                sqlQuery.Append(" WHERE DKBH ='" + textBox1.Text + "'");

                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gkfqd.Common.DbUse.GetOleDbconnection().Open();
                DataSetDkbh.Clear();
                OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                MyAdapter.Fill(DataSetDkbh);
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                if (DataSetDkbh.Tables[0].Rows.Count != 0)
                {
                    MessageBox.Show("该地块编号已经存在,请重新录入！");
                    return;
                }
                //临时图层到正式图层赋值
                gkfqdRecordset.AddNew(tempRecordset.GetGeometry());
                getSmid = gkfqdRecordset.GetFieldValue("SMID").ToString();
                //复垦项目名称 主键
                gkfqdRecordset.SetFieldValue("FKXMMC", textBox10.Text);
                //地块编号
                gkfqdRecordset.SetFieldValue("DKBH", textBox1.Text);
                //地块位置
                gkfqdRecordset.SetFieldValue("DKWZ", textBox4.Text);
                //地块中的国有面积
                gkfqdRecordset.SetFieldValue("DKZGYMJ", double.Parse(textBox2.Text));
                //地块中的集体面积
                gkfqdRecordset.SetFieldValue("DKZJTMJ", double.Parse(textBox5.Text));
                //土源保障情况
                gkfqdRecordset.SetFieldValue("TYBZQK", comboBox9.Text);
                //水源保障情况
                gkfqdRecordset.SetFieldValue("SYBZQK", comboBox4.Text);
                //现状地形坡度
                gkfqdRecordset.SetFieldValue("XZDXPD", comboBox10.Text);
                //预期复垦更低等级
                gkfqdRecordset.SetFieldValue("YQFKGDDJ", textBox14.Text);
                //预期地形坡度
                gkfqdRecordset.SetFieldValue("YQDXPD", comboBox4.Text);
                //建设用地合法性
                gkfqdRecordset.SetFieldValue("JSYDHF", comboBox1.Text);
                //复垦义务人情况
                gkfqdRecordset.SetFieldValue("FKYWR", comboBox2.Text);
                //其他规划名称
                if (textBox6.Text != null)
                {
                    gkfqdRecordset.SetFieldValue("QTGHMC", textBox6.Text);
                }
                //是否符合
                if (checkBox2.Checked == true)
                {
                    gkfqdRecordset.SetFieldValue("SFFH", "是");
                }
                //是否符合土地利用总体规划
                if (checkBox1.Checked == true)
                {
                    gkfqdRecordset.SetFieldValue("SFFHTDLYZTGH", "是");
                }
                //现状有无污染状况
                if (checkBox3.Checked == true)
                {
                    gkfqdRecordset.SetFieldValue("XZYWWRZK", "有");
                }
                //现状污染状况
                if (textBox7.Text != null)
                {
                    gkfqdRecordset.SetFieldValue("XZWRZK", textBox7.Text);
                }
                //现状有无地质灾害隐患
                if (checkBox4.Checked == true)
                {
                    gkfqdRecordset.SetFieldValue("XZYWDZZHYH", "有");
                }
                //交通运输用地
                if (textBox9.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("JTYSYD", double.Parse(textBox9.Text));
                }
                //工业用地
                if (textBox25.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("GYYD", double.Parse(textBox25.Text));
                }
                //其他建设用地
                if (textBox11.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("QTJSYD", double.Parse(textBox11.Text));
                }
                //采矿用地
                if (textBox12.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("CKYD", double.Parse(textBox12.Text));
                }
                //水域及水利设施用地
                if (textBox8.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("SYJSLSSYD", double.Parse(textBox8.Text));
                }
                //地块现状小计
                if (textBox13.Text != "")
                {
                    // double result = double.Parse(textBox9.Text) + double.Parse(textBox25.Text) + double.Parse(textBox11.Text) + double.Parse(textBox12.Text) + double.Parse(textBox8.Text);
                    gkfqdRecordset.SetFieldValue("XZXJ", double.Parse(textBox13.Text));
                }
                //有效土层厚度
                if (textBox3.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("YXTCHD", double.Parse(textBox3.Text));
                }
                //预期有无污染状况
                if (checkBox5.Checked == true)
                {
                    gkfqdRecordset.SetFieldValue("YQYWWRZK", "有");
                }
                //预期污染状况
                if (textBox15.Text != null)
                {
                    gkfqdRecordset.SetFieldValue("YQWRZK", textBox15.Text);
                }
                //预期有无地质灾害隐患
                if (checkBox6.Checked == true)
                {
                    gkfqdRecordset.SetFieldValue("YQYWDZZHYH", "有");
                }
                //农村道路
                if (textBox16.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("NCDL", double.Parse(textBox16.Text));
                }
                //耕地
                if (textBox18.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("GD", double.Parse(textBox18.Text));
                }
                //园地
                if (textBox17.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("YD", double.Parse(textBox17.Text));
                }
                //坑塘水面
                if (textBox19.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("KTSM", double.Parse(textBox19.Text));
                }
                //林地
                if (textBox20.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("LD", double.Parse(textBox20.Text));
                }
                //草地
                if (textBox21.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("CD", double.Parse(textBox21.Text));
                }
                //其他农用地
                if (textBox22.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("QTNYD", double.Parse(textBox22.Text));
                }
                //沟渠
                if (textBox23.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("GQ", double.Parse(textBox23.Text));
                }
                //预期地块小计
                if (textBox24.Text != "")
                {
                    // double result = double.Parse(textBox16.Text) + double.Parse(textBox17.Text) + double.Parse(textBox18.Text) + double.Parse(textBox19.Text) + double.Parse(textBox20.Text) + double.Parse(textBox21.Text) + double.Parse(textBox22.Text) + double.Parse(textBox23.Text);
                    gkfqdRecordset.SetFieldValue("YQXJ", double.Parse(textBox24.Text));
                }
                //地块名称
                if (textBox26.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("DKMC", textBox26.Text);
                }
                //地块面积（亩）
                if (textBox27.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("DKMJ", double.Parse(textBox27.Text));
                }
                //坐标系
                if (textBox28.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("ZBX", textBox28.Text);
                }
                //界址点数
                if (textBox29.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("JZDS", double.Parse(textBox29.Text));
                }
                 //几度分带
                if (textBox30.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("JDFD", double.Parse(textBox30.Text));
                }
                  //精度
                if (textBox31.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("JD", double.Parse(textBox31.Text));
                }
                   //计量单位
                if (textBox32.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("JLDW", textBox32.Text);
                }
                // 记录图形属性
                if (textBox33.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("JLTCSX", textBox33.Text);
                }
                // 投影类型
                if (textBox34.Text != "")
                {
                    gkfqdRecordset.SetFieldValue("TYLX", textBox34.Text);
                }
                gkfqdRecordset.Update();
                //防止第二次插入 设置按钮不能点
                button2.Enabled = false;
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
            //工矿废弃地表   地块编号	
            sqlQuery.Append(" UPDATE  "+gkfqd.Common.DbUse.GetTownCode(textBox35.Text));
            // 地块中的国有面积
            sqlQuery.Append(" SET DKZGYMJ=" + textBox2.Text + ",");
            //地块位置   DKWZ
            sqlQuery.Append("DKWZ='" + textBox4.Text + "',");
            //地块中的集体面积	DKZJTMJ
            sqlQuery.Append("DKZJTMJ=" + textBox5.Text + ",");
            //土源保障情况	TYBZQK
            sqlQuery.Append("TYBZQK='" + comboBox9.Text + "',");
            //现状地形坡度	XZDXPD
            sqlQuery.Append("XZDXPD='" + comboBox10.Text + "',");
            //水源保障情况	SYBZQK
            sqlQuery.Append("SYBZQK='" + comboBox4.Text + "',");
            //预期复垦耕地等级 YQFKGDDJ
            sqlQuery.Append("YQFKGDDJ='" + textBox14.Text + "',");
            //预期地形坡度	YQDXPD
            sqlQuery.Append("YQDXPD='" + comboBox16.Text + "',");
            //建设用地合法性
            sqlQuery.Append("JSYDHF='" + comboBox1.Text + "',");
            //复垦义务人情况
            sqlQuery.Append("FKYWR='" + comboBox2.Text + "'");
            //其他规划名称	QTGHMC
            if (textBox6.Text != "")
            {
                sqlQuery.Append(", QTGHMC='" + textBox6.Text + "'");
            }
            //是否符合	SFFH
            if (checkBox2.Checked == true)
            {
                sqlQuery.Append(", SFFH='" + "是" + "'");
            }
            else
            {
                sqlQuery.Append(", SFFH='" + "否" + "'");
            }
            //是否符合土地利用总体规划	SFFHTDLYZTGH
            if (checkBox1.Checked == true)
            {
                sqlQuery.Append(", SFFHTDLYZTGH='" + "是" + "'");
            }
            else
            {
                sqlQuery.Append(", SFFHTDLYZTGH='" + "否" + "'");
            }
            //现状有无污染状况	XZYWWRZK
            if (checkBox3.Checked == true)
            {
                sqlQuery.Append(", XZYWWRZK='" + "有" + "'");
            }
            else
            {
                sqlQuery.Append(", XZYWWRZK='" + "无" + "'");
            }
            //现状污染状况  XZWRZK
            if (textBox7.Text != "")
            {
                sqlQuery.Append(",XZWRZK='" + textBox7.Text + "'");
            }
            //现状有无地质灾害隐患   XZYWDZZHYH
            if (checkBox4.Checked == true)
            {
                sqlQuery.Append(", XZYWDZZHYH='" + "有" + "'");
            }
            else
            {
                sqlQuery.Append(", XZYWDZZHYH='" + "无" + "'");
            }
            //交通运输用地  JTYSYD
            if (textBox9.Text != "")
            {
                sqlQuery.Append(",JTYSYD=" + textBox9.Text );
            }
            //工业用地  GKYD
            if (textBox25.Text != "")
            {
                sqlQuery.Append(",GYYD=" + textBox25.Text );
            }
            //其他建设用地  QTJSYD
            if (textBox11.Text != "")
            {
                sqlQuery.Append(",QTJSYD=" + textBox11.Text );
            }
            //采矿用地  GKYD
            if (textBox12.Text != "")
            {
                sqlQuery.Append(",CKYD=" + textBox12.Text );
            }
            //水域及水利设施用地  SYJSLSSYD
            if (textBox8.Text != "")
            {
                sqlQuery.Append(",SYJSLSSYD=" + textBox8.Text );
            }
            //现状小计  XZXJ
            if (textBox13.Text != "")
            {
                sqlQuery.Append(",XZXJ=" + textBox13.Text );
            }
            //有效土层厚度   YXTCHD
            if (textBox3.Text != "")
            {
                sqlQuery.Append(",YXTCHD=" + textBox3.Text );
            }
            //预期有无污染状况	YQYWWRZK
            if (checkBox3.Checked == true)
            {
                sqlQuery.Append(", YQYWWRZK='" + "有" + "'");
            }
            else
            {
                sqlQuery.Append(", YQYWWRZK='" + "无" + "'");
            }
            //预期污染状况  YQWRZK
            if (textBox15.Text != "")
            {
                sqlQuery.Append(",YQWRZK='" + textBox15.Text + "'");
            }
            //预期有无地质灾害隐患	YQYWDZZHYH
            if (checkBox3.Checked == true)
            {
                sqlQuery.Append(", YQYWDZZHYH='" + "有" + "'");
            }
            else
            {
                sqlQuery.Append(", YQYWDZZHYH='" + "无" + "'");
            }
            //农村道路  NCDL
            if (textBox16.Text != "")
            {
                sqlQuery.Append(",NCDL=" + textBox16.Text );
            }
            //耕地  GD
            if (textBox18.Text != "")
            {
                sqlQuery.Append(",GD=" + textBox18.Text );
            }
            //园地  YD
            if (textBox17.Text != "")
            {
                sqlQuery.Append(",YD=" + textBox17.Text );
            }
            //坑塘水面  KTSM
            if (textBox19.Text != "")
            {
                sqlQuery.Append(",KTSM=" + textBox19.Text );
            }
            //林地  LD
            if (textBox20.Text != "")
            {
                sqlQuery.Append(",LD=" + textBox20.Text );
            }
            //草地  CD
            if (textBox21.Text != "")
            {
                sqlQuery.Append(",CD=" + textBox21.Text );
            }
            //其他农用地  QTNYD
            if (textBox22.Text != "")
            {
                sqlQuery.Append(",QTNYD=" + textBox22.Text );
            }
            //沟渠  GQ
            if (textBox23.Text != "")
            {
                sqlQuery.Append(",GQ=" + textBox23.Text );
            }
            //预期小计  YQXJ
            if (textBox24.Text != "")
            {
                sqlQuery.Append(",YQXJ=" + textBox24.Text );
            }
            //地块名称
            if (textBox26.Text != "")
            {
                sqlQuery.Append(",DKMC='" + textBox26.Text+"'");
            }
            //地块面积（亩）
            if (textBox27.Text != "")
            {
                sqlQuery.Append(",DKMJ=" + textBox27.Text);
            }
            //坐标系
            if (textBox28.Text != "")
            {
                sqlQuery.Append(",ZBX='" + textBox28.Text+"'");
            }
            //界址点数
            if (textBox29.Text != "")
            {
                sqlQuery.Append(",JZDS=" + textBox29.Text);
            }
            //几度分带
            if (textBox30.Text != "")
            {
                sqlQuery.Append(",JDFD=" + textBox30.Text);
            }
            //精度
            if (textBox31.Text != "")
            {
                sqlQuery.Append(",JD=" + textBox31.Text);
            }
            //计量单位
            if (textBox32.Text != "")
            {
                sqlQuery.Append(",JLDW='" + textBox32.Text+"'");
            }
            // 记录图形属性
            if (textBox33.Text != "")
            {
                sqlQuery.Append(",JLTCSX='" + textBox33.Text + "'");
            }
            // 投影类型
            if (textBox34.Text != "")
            {
                sqlQuery.Append(",TYLX='" + textBox34.Text + "'");
            }
            //项目名称	FKXMMC
            sqlQuery.Append("   WHERE FKXMMC = '" + textBox10.Text + "'");
            sqlQuery.Append("   AND  DKBH = '" + textBox1.Text + "'");
            gkfqd.Common.DbUse.conn.Close();
            gkfqd.Common.DbUse.conn.Open();
            OleDbCommand updateCommand = new OleDbCommand(sqlQuery.ToString(), gkfqd.Common.DbUse.conn);
            updateCommand.ExecuteNonQuery();
            gkfqd.Common.DbUse.conn.Close();
            MessageBox.Show("复垦地块表更新成功");
        }
        #endregion

    }
}
