﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormsUI.Docking;



using gkfqd;

using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace gkfqd.ui.gk01
{
    public partial class gk014 : WinFormsUI.Docking.DockContent
    {
        #region 变量区
        //oracle 连接 操作
       // public static OleDbConnection conn = new OleDbConnection("Provider=MSDAORA.1;User ID=gkfqd;Password=123456;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST =192.168.1.103)(PORT = 1521))) (connECT_DATA = (SERVICE_NAME = orcl)))");

        //临时用 dataSet
        DataSet dataSet = new DataSet();
        DataSet dataSet4 = new DataSet();
        DataSet dataSet1 = new DataSet();
        DataSet dataSet2 = new DataSet();
        DataSet dataSet5 = new DataSet();
        DataSet dataSet6 = new DataSet();
        DataSet dataSet7 = new DataSet();
        DataSet dataSet8 = new DataSet();
        //选择上传文件名
        string fileName = null;
        //文件长度
        long fileLength;
        //传输文件返回状态
        string transFileFlag;
        //选择上传文件路径
        String filePath = null;
        
        //共同用  sqlQuery
        StringBuilder sqlQuery = new StringBuilder();
        #endregion

        #region 初始化
        public gk014()
        {
            InitializeComponent();
            Initialize();
            //复垦进度默认为未复垦
            comboBox7.SelectedIndex = 0;
            //验收状态默认为未验收
            comboBox8.SelectedIndex = 1;
            //指标使用默认为未使用
            comboBox9.SelectedIndex = 1;
            //项目区分默认为新建项目
            comboBox4.SelectedIndex = 1;
            //权属调整默认为未调整
            comboBox6.SelectedIndex = 0;
            //考核状态默认为未考核
            comboBox10.SelectedIndex = 1;
            //资金安排默认为未安排
            comboBox5.SelectedIndex = 1;
        }

        public gk014(DataTable dt)
        {
            InitializeComponent();
            Initialize();

            comboBox1.Text = dt.Rows[0]["项目所在省"].ToString();
            comboBox2.Text = dt.Rows[0]["项目所在市"].ToString();
            comboBox3.Text = dt.Rows[0]["项目所在县"].ToString();
            textBox6.Text = dt.Rows[0]["项目名称"].ToString();
            textBox7.Text = dt.Rows[0]["组织实施单位"].ToString();
            string[] sArray = Regex.Split(dt.Rows[0]["批准文号"].ToString(), "-", RegexOptions.IgnoreCase);
            textBox8.Text = sArray[0];
            textBox9.Text = sArray[1];
            textBox11.Text = sArray[2];
            textBox12.Text = dt.Rows[0]["批准机关"].ToString();
            dateTimePicker1.Text = dt.Rows[0]["批准时间"].ToString();
            dateTimePicker2.Text = dt.Rows[0]["计划开始日期"].ToString();
            dateTimePicker3.Text = dt.Rows[0]["计划结束日期"].ToString();
            textBox16.Text = dt.Rows[0]["项目涉及权益单位个数"].ToString();
            textBox17.Text = dt.Rows[0]["征求意见单位数量"].ToString();
            textBox18.Text = dt.Rows[0]["同意数量"].ToString();
            textBox3.Text = dt.Rows[0]["复垦规模"].ToString();
            textBox2.Text = dt.Rows[0]["复垦耕地规模"].ToString();
            textBox1.Text = dt.Rows[0]["土地复垦资金预算"].ToString();
            textBox5.Text = dt.Rows[0]["拆迁费"].ToString();
            textBox4.Text = dt.Rows[0]["其他"].ToString();
            textBox10.Text = dt.Rows[0]["合计"].ToString();
            textBox19.Text = dt.Rows[0]["财政资金"].ToString();
            textBox20.Text = dt.Rows[0]["社会资金"].ToString();
            textBox22.Text = dt.Rows[0]["信贷资金"].ToString();
            textBox21.Text = dt.Rows[0]["其他资金"].ToString();
            textBox14.Text = dt.Rows[0]["权属性质"].ToString();
            textBox23.Text = dt.Rows[0]["权属单位名称"].ToString();
            textBox25.Text = dt.Rows[0]["权属单位代码"].ToString();
            textBox24.Text = dt.Rows[0]["坐落单位名"].ToString();
            comboBox7.Text = dt.Rows[0]["复垦进度"].ToString();
            comboBox8.Text = dt.Rows[0]["验收状态"].ToString();
            textBox15.Text = dt.Rows[0]["坐落单位代码"].ToString();
            textBox26.Text = dt.Rows[0]["复垦建新规模"].ToString();
            comboBox4.Text = dt.Rows[0]["项目区分"].ToString();
            comboBox9.Text = dt.Rows[0]["指标使用"].ToString();
            comboBox5.Text =dt.Rows[0]["资金安排"].ToString();
            comboBox6.Text = dt.Rows[0]["权属调整"].ToString();
            comboBox10.Text = dt.Rows[0]["考核状态"].ToString();
            dateTimePicker4.Text = dt.Rows[0]["验收时间"].ToString();
            richTextBox1.Text = dt.Rows[0]["项目概况"].ToString();
            button1.Text = "修改";
            textBox6.ReadOnly = true;
           // DataGridFileGet();
        }

        #region 初始化 部分
        public void Initialize()
        {
            //不显示dataGridView1最后一行
           // dataGridView1.AllowUserToAddRows = false;
            comboBox1.SelectedIndex = 0;
            //区县comboBox列表加载
            LoadComboBox();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker4.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd"; //设置显示格式
            dateTimePicker2.CustomFormat = "yyyy-MM-dd"; //设置显示格式
            dateTimePicker3.CustomFormat = "yyyy-MM-dd"; //设置显示格式
            dateTimePicker4.CustomFormat = "yyyy-MM-dd"; //设置显示格式
        }
        #endregion

        #endregion

        #region 录入处理
        private void button1_Click(object sender, EventArgs e)
        {
            //项目所在省
            if (comboBox1.Text == "")
            {
                MessageBox.Show("项目所在省不能为空，请输入！");
                return;
            }
            // 项目所在市
            if (comboBox2.Text == "")
            {
                MessageBox.Show("项目所在市不能为空，请输入！");
                return;
            }
            //项目所在县	XMSZX
            if (comboBox3.Text == "")
            {
                MessageBox.Show("项目所在县不能为空，请输入！");
                return;
            }
            //项目名称	XMMC
            if (textBox6.Text == "")
            {
                MessageBox.Show("项目名称不能为空，请输入！");
                return;
            }
            //组织实施单位	ZZSSDW
            if (textBox7.Text == "")
            {
                MessageBox.Show("组织实施单位不能为空，请输入！");
                return;
            }
            //批准文号	PZWH
            if (textBox8.Text == "")
            {
                MessageBox.Show("批准文号不能为空，请输入！");
                return;
            }
            //批准机关	PZJG
            if (textBox12.Text == "")
            {
                MessageBox.Show("批准机关不能为空，请输入！");
                return;
            }
            //批准时间	PZSJ
            if (dateTimePicker1.Text == "")
            {
                MessageBox.Show("批准时间不能为空，请输入！");
                return;
            }
            //计划开始日期	JHKSRQ
            if (dateTimePicker2.Text == "")
            {
                MessageBox.Show("计划开始日期不能为空，请输入！");
                return;
            }
            //计划结束日期	JHJSRQ
            if (dateTimePicker3.Text == "")
            {
                MessageBox.Show("计划结束日期不能为空，请输入！");
                return;
            }
            //项目涉及权益单位个数	XMSJQYDWGS
            if (textBox16.Text == "")
            {
                MessageBox.Show("项目涉及权益单位个数不能为空，请输入！");
                return;
            }
            //征求意见单位数量	ZQYJDWSL
            if (textBox17.Text == "")
            {
                MessageBox.Show("征求意见单位数量不能为空，请输入！");
                return;
            }
            //同意数量	TYSL
            if (textBox18.Text == "")
            {
                MessageBox.Show("同意数量不能为空，请输入！");
                return;
            }
            if(button1.Text!="修改")
            {
                //查询重复项目名称
                sqlQuery.Clear();
                sqlQuery.Append(" SELECT XMMC  FROM  FKXM  ");
                sqlQuery.Append(" WHERE XMMC ='" + textBox6.Text + "'");

                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gkfqd.Common.DbUse.GetOleDbconnection().Open();
                dataSet5.Clear();
                OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                MyAdapter.Fill(dataSet5);
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                if (dataSet5.Tables[0].Rows.Count != 0)
                {
                    MessageBox.Show("该项目名已经存在,请重新录入！");
                    return;
                }
            }
           
            try
            {
                if (button1.Text == "修改")
                {
                    UpdateData();
                    MessageBox.Show("复垦项目数据更新处理成功！");
                }
                else 
                {
                    InsertData();
                    MessageBox.Show("复垦项目数据插入处理成功！");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("复垦项目数据提交处理异常！");
                return;
            }
            
            
        }
        #endregion

        #region 区县级联选择处理
        public void LoadComboBox()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append(" SELECT XZQM  FROM  XZQ  ");
            sqlQuery.Append(" WHERE CJ ='1'  ");
            sqlQuery.Append(" AND  BHBS <> '0000'  ");
            sqlQuery.Append(" ORDER BY  XZQDM  ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSet);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
         
            comboBox2.DataSource = dataSet.Tables[0];
            comboBox2.DisplayMember = "XZQM";
            comboBox2.ValueMember = "XZQM";


            sqlQuery.Clear();
            sqlQuery.Append(" SELECT XZQM  FROM  XZQ  ");
            sqlQuery.Append(" WHERE CJ ='2'  ");
            sqlQuery.Append(" AND BHBS ='1001'");
            sqlQuery.Append(" ORDER BY  XZQDM  ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet4.Clear();

            OleDbDataAdapter MyAdapter2 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter2.Fill(dataSet4);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            comboBox3.DataSource = dataSet4.Tables[0];
            comboBox3.DisplayMember = "XZQM";
            comboBox3.ValueMember = "XZQM";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append(" SELECT BHBS  FROM  XZQ  ");
            sqlQuery.Append(" WHERE XZQM  = '" + comboBox2.SelectedValue+"'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet1.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSet1);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //取得包含标识
            String  strBhbs =  dataSet1.Tables[0].Rows[0][0].ToString();
            sqlQuery.Clear();
            sqlQuery.Append(" SELECT XZQM  FROM  XZQ  ");
            sqlQuery.Append(" WHERE CJ ='2'  ");
            sqlQuery.Append(" AND BHBS ='" + strBhbs+"'");
            sqlQuery.Append(" ORDER BY  XZQDM  ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet2.Clear();
            OleDbDataAdapter MyAdapter2 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter2.Fill(dataSet2);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            comboBox3.DataSource = dataSet2.Tables[0];
            comboBox3.DisplayMember = "XZQM";
            comboBox3.ValueMember = "XZQM";
        }
        #endregion

        #region 文件传输处理调用


        public static byte[] ConvertToBinary(string Path)
        {
            FileStream stream = new FileInfo(Path).OpenRead();
            byte[] buffer = new byte[stream.Length];
            Console.WriteLine("The lenght of the file is " + buffer.Length);
            stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
            return buffer;
        }
        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static long LengthOfFile(string path)
        {
            FileInfo fi = new FileInfo(path);
            return fi.Length;

        }

        public static byte[] ConvertToBinary(long index, bool ifEnd, long fileLenght, string path)
        {
            //20971520

            byte[] bysFile;//临时二进制数组，最大20M
            //index = buffer.Length / 20971520;
            //index += buffer.Length % 20971520 == 0 ? 0 : 1;
            //byte[] bys;//临时二进制数组，最大20M
            if (ifEnd == false)
            {
                bysFile = GetFileBloc(20971520 * index, 20971520, path);
            }
            else
            {
                bysFile = GetFileBloc(20971520 * index, fileLenght - 20971520 * (index), path);
            }
            Console.WriteLine("The length of the current buffer is " + bysFile.Length);
            return bysFile;

        }
        public static byte[] ChangeFileToByte(string path)
        {
            FileStream stream = new FileInfo(path).OpenRead();
            byte[] Filebuffer = new byte[stream.Length];
            stream.Read(Filebuffer, 0, Convert.ToInt32(stream.Length));
            return Filebuffer;
        }
        /// <summary>
        /// 获取文件二进制块
        /// </summary>
        /// <param name="path"></param>
        /// <param name="byteIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GetFileBloc(long byteIndex, long length, string path)
        {
            //string path = System.Configuration.ConfigurationSettings.AppSettings["path"].ToString();
            FileStream stream = new FileInfo(path).OpenRead();
            stream.Position = byteIndex;
            byte[] Filebuffer = new byte[length];
            stream.Read(Filebuffer, 0, Convert.ToInt32(length));
            return Filebuffer;
        }

        public string TransFileByDataGrid(string path, string fileName)
        {
             gkfqd.ServiceReference3.Service1SoapClient
             webServiceTransFile = new  gkfqd.ServiceReference3.Service1SoapClient();

            fileLength = LengthOfFile(path);//计算文件大小
            long countOfPk = fileLength / Convert.ToInt64(20971520);
            countOfPk += fileLength % 20971520 == 0 ? 0 : 1;
            bool ifEnd = false;
            for (long ii = 0; ii < countOfPk; ii++)//分块传输
            {
                if (ii == countOfPk - 1)
                    ifEnd = true;
                if (ii == 0)
                {
                    transFileFlag = webServiceTransFile.TransFile(ConvertToBinary(ii, ifEnd, fileLength, path), fileName, true);
                    if (transFileFlag == "3")
                    {
                        return transFileFlag;
                    }
                }
                else
                {
                    transFileFlag = webServiceTransFile.TransFile(ConvertToBinary(ii, ifEnd, fileLength, path), fileName, false);
                    if (transFileFlag == "3")
                    {
                        return transFileFlag;
                    }
                }
            }
            return transFileFlag;
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
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox22_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(e, sender);
        }
        #endregion

        #region 选择文件处理
        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog objFile = new OpenFileDialog();
            objFile.Filter = "Excel文件(.xls)|*.xls|PDF文件(.pdf)|*.pdf|Word文件(.doc)|*.doc";
            objFile.ShowDialog();
            fileName = objFile.SafeFileName;
            filePath = objFile.FileName;
            if (filePath != "")
            {
                fileLength = LengthOfFile(filePath);//计算文件大小
            }
            else
            {
                //不选择文件直接返回
                return;
            }
            //DataGridViewRefresh();
        }
        #endregion
   
        #region 数据插入处理
        public void InsertData()
        {
                sqlQuery.Clear();
                //复垦项目表   项目所在省	
                sqlQuery.Append(" INSERT INTO FKXM (XMSZ, ");
                // 项目所在市
                sqlQuery.Append("XMSZS,");
                //项目所在县	XMSZX
                sqlQuery.Append("XMSZX,");
                //项目名称	XMMC
                sqlQuery.Append("XMMC,");
                //组织实施单位	ZZSSDW
                sqlQuery.Append("ZZSSDW,");
                //批准文号	PZWH
                sqlQuery.Append("PZWH,");
                //批准机关	PZJG
                sqlQuery.Append("PZJG,");
                //批准时间	PZSJ
                sqlQuery.Append("PZSJ,");
                //计划开始日期	JHKSRQ
                sqlQuery.Append("JHKSRQ,");
                //计划结束日期	JHJSRQ
                sqlQuery.Append("JHJSRQ,");
                //项目涉及权益单位个数	XMSJQYDWGS
                sqlQuery.Append("XMSJQYDWGS,");
                //征求意见单位数量	ZQYJDWSL
                sqlQuery.Append("ZQYJDWSL,");
                //同意数量	TYSL
                sqlQuery.Append("TYSL,");
                //用户ID	
                sqlQuery.Append("YHID,");
                //记录插入时间
                sqlQuery.Append("INSERT_TIME");
                if (textBox3.Text != "")
                {
                    //复垦规模	FKGM
                    sqlQuery.Append(",FKGM");
                }
                if (textBox2.Text != "")
                {
                    //复垦耕地规模	FKGDGM
                    sqlQuery.Append(",FKGDGM");
                }
                if (textBox1.Text != "")
                {
                    //土地复垦资金预算	FKZJ
                    sqlQuery.Append(",FKZJ");
                }
                if (textBox5.Text != "")
                {
                    //拆迁费	CQF
                    sqlQuery.Append(",CQF");
                }
                if (textBox4.Text != "")
                {
                    //其他	QT
                    sqlQuery.Append(",QT");
                }
                if (textBox10.Text != "")
                {
                    //合计	HJ
                    sqlQuery.Append(",HJ");
                }
                if (textBox19.Text != "")
                {
                    //财政资金	CZZJ
                    sqlQuery.Append(",CZZJ");
                }
                if (textBox20.Text != "")
                {
                    //社会资金	SHZJ
                    sqlQuery.Append(",SHZJ");
                }
                if (textBox22.Text != "")
                {
                    //信贷资金	XDZJ
                    sqlQuery.Append(",XDZJ");
                }
                if (textBox21.Text != "")
                {
                    //其他资金	QTZJ
                    sqlQuery.Append(",QTZJ");
                }
                if (textBox14.Text != "")
                {
                    //权属性质	QSXZ
                    sqlQuery.Append(",QSXZ");
                }
                if (textBox23.Text != "")
                {
                    //权属单位名称	QSDWMC
                    sqlQuery.Append(",QSDWMC");
                }

                if (textBox25.Text != "")
                {
                    //权属单位代码	QSDWDM
                    sqlQuery.Append(",QSDWDM");
                }
                if (textBox24.Text != "")
                {
                    //坐落单位名	ZLDWM
                    sqlQuery.Append(",ZLDWM");
                }
                if (comboBox7.Text != "")
                {
                    //复垦进度	FKJD
                    sqlQuery.Append(",FKJD");
                }
                if (comboBox8.Text != "")
                {
                    //验收状态	YSZT
                    sqlQuery.Append(",YSZT");
                }

                if (textBox15.Text != "")
                {
                    //坐落单位代码	ZLDWDM
                    sqlQuery.Append(",ZLDWDM");
                }
                if (textBox26.Text != "")
                {
                    //复垦建新规模	FKJXGM
                    sqlQuery.Append(",FKJXGM");
                }
                if (comboBox4.Text != "")
                {
                    //项目区分	XMQF
                    sqlQuery.Append(",XMQF");
                }

                if (comboBox9.Text != "")
                {
                    //指标使用	ZBSY
                    sqlQuery.Append(",ZBSY");
                }
                if (comboBox5.Text != "")
                {
                    //资金安排	ZJAP
                    sqlQuery.Append(",ZJAP");
                }
                if (dateTimePicker4.Text != "")
                {
                    //验收时间	YSSJ
                    sqlQuery.Append(",YSSJ");
                }
                if (comboBox6.Text != "")
                {
                    //权属调整	qstz
                    sqlQuery.Append(",QSTZ");
                }
                if (comboBox10.Text != "")
                {
                    //考核状态	khzt
                    sqlQuery.Append(",KHZT");
                }
                if (richTextBox1.Text != "")
                {
                    //项目概况	xmgk
                    sqlQuery.Append(",XMGK");
                }
            

                sqlQuery.Append(" ) VALUES (  '");
                //项目所在省	
                sqlQuery.Append(comboBox1.Text + "','");
                // 项目所在市
                sqlQuery.Append(comboBox2.Text + "','");
                //项目所在县	XMSZX
                sqlQuery.Append(comboBox3.Text + "','");
                //项目名称	XMMC
                sqlQuery.Append(textBox6.Text + "','");
                //组织实施单位	ZZSSDW
                sqlQuery.Append(textBox7.Text + "','");
                //批准文号	PZWH
                sqlQuery.Append(textBox8.Text + "-" + textBox9.Text + "-" + textBox11.Text + "','");
                //批准机关	PZJG
                sqlQuery.Append(textBox12.Text + "',");
                //批准时间	PZSJ
                sqlQuery.Append("to_date('" + dateTimePicker1.Text + "','yyyy-mm-dd')" + ",");
                //计划开始日期	JHKSRQ
                sqlQuery.Append("to_date('" + dateTimePicker2.Text + "','yyyy-mm-dd')" + ",");
                //计划结束日期	JHJSRQ
                sqlQuery.Append("to_date('" + dateTimePicker3.Text + "','yyyy-mm-dd')" + ",'");
                //项目涉及权益单位个数	XMSJQYDWGS
                sqlQuery.Append(textBox16.Text + "','");
                //征求意见单位数量	ZQYJDWSL
                sqlQuery.Append(textBox17.Text + "','");
                //同意数量	TYSL
                sqlQuery.Append(textBox18.Text + "','");
                //用户ID记录那个用户插入的数据
                sqlQuery.Append(login.userId + "',");
                //记录插入时间
                sqlQuery.Append("sysdate ");
                if (textBox3.Text != "")
                {
                    //复垦规模	FKGM
                    sqlQuery.Append(",'" + textBox3.Text + "'");
                }
                if (textBox2.Text != "")
                {
                    //复垦耕地规模	FKGDGM
                    sqlQuery.Append(",'" + textBox2.Text + "'");
                }
                if (textBox1.Text != "")
                {
                    //土地复垦资金预算	FKZJ
                    sqlQuery.Append(",'" + textBox1.Text + "'");
                }
                if (textBox5.Text != "")
                {
                    //拆迁费	CQF
                    sqlQuery.Append(",'" + textBox5.Text + "'");
                }
                if (textBox4.Text != "")
                {
                    //其他	QT
                    sqlQuery.Append(",'" + textBox4.Text + "'");
                }
                if (textBox10.Text != "")
                {
                    //合计	HJ
                    sqlQuery.Append(",'" + textBox10.Text + "'");
                }
                if (textBox19.Text != "")
                {
                    //财政资金	CZZJ
                    sqlQuery.Append(",'" + textBox19.Text + "'");
                }
                if (textBox20.Text != "")
                {
                    //社会资金	SHZJ
                    sqlQuery.Append(",'" + textBox20.Text + "'");
                }
                if (textBox22.Text != "")
                {
                    //信贷资金	XDZJ
                    sqlQuery.Append(",'" + textBox22.Text + "'");
                }
                if (textBox21.Text != "")
                {
                    //其他资金	QTZJ
                    sqlQuery.Append(",'" + textBox21.Text + "'");
                }

                if (textBox14.Text != "")
                {
                    //权属性质	QSXZ
                    sqlQuery.Append(",'" + textBox14.Text + "'");
                }
                if (textBox23.Text != "")
                {
                    //权属单位名称	QSDWMC
                    sqlQuery.Append(",'" + textBox23.Text + "'");
                }

                if (textBox25.Text != "")
                {
                    //权属单位代码	QSDWDM
                    sqlQuery.Append(",'" + textBox25.Text + "'");
                }
                if (textBox24.Text != "")
                {
                    //坐落单位名	ZLDWM
                    sqlQuery.Append(",'" + textBox24.Text + "'");
                }
                if (comboBox7.Text != "")
                {
                    //复垦进度	FKJD
                    sqlQuery.Append(",'" + comboBox7.Text + "'");
                }
                if (comboBox8.Text != "")
                {
                    //验收状态	YSZT
                    sqlQuery.Append(",'" + comboBox8.Text + "'");
                }

                if (textBox15.Text != "")
                {
                    //坐落单位代码	ZLDWDM
                    sqlQuery.Append(",'" + textBox15.Text + "'");
                }
                if (textBox26.Text != "")
                {
                    //复垦建新规模	FKJXGM
                    sqlQuery.Append(",'" + textBox26.Text + "'");
                }
                if (comboBox4.Text != "")
                {
                    //项目区分	XMQF
                    sqlQuery.Append(",'" + comboBox4.Text + "'");
                }

                if (comboBox9.Text != "")
                {
                    //指标使用	ZBSY
                    sqlQuery.Append(",'" + comboBox9.Text + "'");
                }
                if (comboBox5.Text != "")
                {
                    //资金安排	ZJAP
                    sqlQuery.Append(",'" + comboBox5.Text + "'");
                }
                if (dateTimePicker4.Text != "")
                {
                    //验收时间	YSSJ
                    sqlQuery.Append("," + "to_date('" + dateTimePicker4.Text + "','yyyy-mm-dd')" );
                }
                if (comboBox6.Text != "")
                {
                    //权属调整  qstz
                    sqlQuery.Append(",'" + comboBox6.Text + "'");
                }
                if (comboBox10.Text != "")
                {
                    //考核状态  khzt
                    sqlQuery.Append(",'" + comboBox10.Text + "'");
                }
                if (richTextBox1.Text != "")
                {
                    //项目概况  XMGK
                    sqlQuery.Append(",'" + richTextBox1.Text + "'");
                }
                sqlQuery.Append(")");
               // gkfqd.Common.DbUse.GetOleDbconnection().Close();
               /* gkfqd.Common.DbUse.GetOleDbconnection().Open();
                OleDbCommand insertCommand = new OleDbCommand(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                insertCommand.ExecuteNonQuery();
                gkfqd.Common.DbUse.GetOleDbconnection().Close();*/
                gkfqd.Common.DbUse.conn.Close();
                gkfqd.Common.DbUse.conn.Open();
                OleDbCommand insertCommand = new OleDbCommand(sqlQuery.ToString(), gkfqd.Common.DbUse.conn);
                insertCommand.ExecuteNonQuery();
                gkfqd.Common.DbUse.conn.Close();
            
        }
        #endregion

        #region 数据更新处理
        public void UpdateData()
        {
                sqlQuery.Clear();
                //复垦项目表   项目所在省	
                sqlQuery.Append(" UPDATE   FKXM SET XMSZ = '"+comboBox1.Text +"',");
                // 项目所在市
                sqlQuery.Append("XMSZS='" + comboBox2.Text+"',");
                //项目所在县	XMSZX
                sqlQuery.Append("XMSZX='" + comboBox3.Text + "',");
                //项目名称	XMMC
                //sqlQuery.Append("XMMC,");
                //组织实施单位	ZZSSDW
                sqlQuery.Append("ZZSSDW='" + textBox7.Text + "',");
                //批准文号	PZWH
                sqlQuery.Append("PZWH='" + textBox8.Text + "-" + textBox9.Text + "-" + textBox11.Text + "',");
                //批准机关	PZJG
                sqlQuery.Append("PZJG='" + textBox12.Text + "',");
                //批准时间	PZSJ
                sqlQuery.Append("PZSJ=" + "to_date('" + dateTimePicker1.Text + "','yyyy-mm-dd')" + ",");
                //计划开始日期	JHKSRQ
                sqlQuery.Append("JHKSRQ="+"to_date('" + dateTimePicker2.Text + "','yyyy-mm-dd')" + ",");
                //计划结束日期	JHJSRQ
                sqlQuery.Append("JHJSRQ=" + "to_date('" + dateTimePicker3.Text + "','yyyy-mm-dd')" + ",");
                //项目涉及权益单位个数	XMSJQYDWGS
                sqlQuery.Append("XMSJQYDWGS='" + textBox16.Text + "',");
                //征求意见单位数量	ZQYJDWSL
                sqlQuery.Append("ZQYJDWSL='" + textBox17.Text + "',");
                //同意数量	TYSL
                sqlQuery.Append("TYSL='" + textBox18.Text + "',");
                //记录插入时间
                sqlQuery.Append("INSERT_TIME=sysdate ");
                if (textBox3.Text != "")
                {
                    //复垦规模	FKGM
                    sqlQuery.Append(",FKGM='" + textBox3.Text+"'");
                }
                if (textBox2.Text != "")
                {
                    //复垦耕地规模	FKGDGM
                    sqlQuery.Append(", FKGDGM='" + textBox2.Text +"'");
                }
                if (textBox1.Text != "")
                {
                    //土地复垦资金预算	FKZJ
                    sqlQuery.Append(", FKZJ='" + textBox1.Text + "'");
                }
                if (textBox5.Text != "")
                {
                    //拆迁费	CQF
                    sqlQuery.Append(", CQF='" + textBox5.Text + "'");
                }
                if (textBox4.Text != "")
                {
                    //其他	QT
                    sqlQuery.Append(", QT='" + textBox4.Text + "'");
                }
                if (textBox10.Text != "")
                {
                    //合计	HJ
                    sqlQuery.Append(", HJ='" + textBox10.Text + "'");
                }
                if (textBox19.Text != "")
                {
                    //财政资金	CZZJ
                    sqlQuery.Append(", CZZJ='" + textBox19.Text + "'");
                }
                if (textBox20.Text != "")
                {
                    //社会资金	SHZJ
                    sqlQuery.Append(", SHZJ='" + textBox20.Text + "'");
                }
                if (textBox22.Text != "")
                {
                    //信贷资金	XDZJ
                    sqlQuery.Append(", XDZJ='" + textBox22.Text + "'");
                }
                if (textBox21.Text != "")
                {
                    //其他资金	QTZJ
                    sqlQuery.Append(", QTZJ='" + textBox21.Text + "'");
                }
                if (textBox14.Text != "")
                {
                    //权属性质	QSXZ
                    sqlQuery.Append(", QSXZ='" + textBox14.Text + "'");
                }
                if (textBox23.Text != "")
                {
                    //权属单位名称	QSDWMC
                    sqlQuery.Append(", QSDWMC='" + textBox23.Text + "'");
                }
                if (textBox25.Text != "")
                {
                    //权属单位代码	QSDWDM
                    sqlQuery.Append(", QSDWDM='" + textBox25.Text + "'");
                }
                if (textBox24.Text != "")
                {
                    //坐落单位名	ZLDWM
                    sqlQuery.Append(", ZLDWM='" + textBox24.Text + "'");
                }
                if (comboBox7.Text != "")
                {
                    //复垦进度	FKJD
                    sqlQuery.Append(", FKJD='" + comboBox7.Text + "'");
                }
                if (comboBox8.Text != "")
                {
                    //验收状态	YSZT
                    sqlQuery.Append(", YSZT='" + comboBox8.Text + "'");
                }

                if (textBox15.Text != "")
                {
                    //坐落单位代码	ZLDWDM
                    sqlQuery.Append(", ZLDWDM='" + textBox15.Text + "'");
                }
                if (textBox26.Text != "")
                {
                    //复垦建新规模	FKJXGM
                    sqlQuery.Append(", FKJXGM='" + textBox26.Text + "'");
                }
                if (comboBox4.Text != "")
                {
                    //项目区分	XMQF
                    sqlQuery.Append(", XMQF='" + comboBox4.Text + "'");
                }

                if (comboBox9.Text != "")
                {
                    //指标使用	ZBSY
                    sqlQuery.Append(", ZBSY='" + comboBox9.Text + "'");
                }
                if (comboBox5.Text != "")
                {
                    //资金安排	ZJAP
                    sqlQuery.Append(", ZJAP='" + comboBox5.Text +"'");
                }
                if (dateTimePicker4.Text != "")
                {
                    //计划结束日期	JHJSRQ
                    sqlQuery.Append(", YSSJ=" + "to_date('" + dateTimePicker4.Text + "','yyyy-mm-dd')" );
                }
                if (comboBox6.Text != "")
                {
                    //权属调整 qstz
                    sqlQuery.Append(", QSTZ='" + comboBox6.Text + "'");
                }
                if (comboBox10.Text != "")
                {
                    //考核状态 khzt
                    sqlQuery.Append(", KHZT='" + comboBox10.Text + "'");
                }
                if (richTextBox1.Text != "")
                {
                    //项目概况 xmgk
                    sqlQuery.Append(", XMGK='" + richTextBox1.Text + "'");
                }

                //项目名称	XMMC
                sqlQuery.Append("   WHERE XMMC='" + textBox6.Text + "'");
                gkfqd.Common.DbUse.conn.Close();
                gkfqd.Common.DbUse.conn.Open();
                OleDbCommand updateCommand = new OleDbCommand(sqlQuery.ToString(), gkfqd.Common.DbUse.conn);
                updateCommand.ExecuteNonQuery();   
                gkfqd.Common.DbUse.conn.Close();
        }
        #endregion

        #region  工矿废弃地文档库更新

        public void DeleteFileData()
        {
            gkfqd.Common.DbUse.conn.Close();
            gkfqd.Common.DbUse.conn.Open();
            sqlQuery.Clear();
            //工矿废弃地文档库录入
            sqlQuery.Append("DELETE FROM GKFQD_WD WHERE  ");
            //项目名称
            sqlQuery.Append(" PROJECT_NAME='" + textBox6.Text+"',");
             //文件名称
            sqlQuery.Append(" AND FILE_NAME='" + fileName + "'");

            OleDbCommand deleteCommand = new OleDbCommand(sqlQuery.ToString(), gkfqd.Common.DbUse.conn);
            deleteCommand.ExecuteNonQuery();
            gkfqd.Common.DbUse.conn.Close();
        }
        #endregion

        #region  取得服务器文件信息
        public void DataGridFileGet()
        {

           // dataGridView1.Columns.Clear();
            sqlQuery.Clear();
            sqlQuery.Append("SELECT  FILE_NAME  AS 文件名称, ");
            sqlQuery.Append("       FILE_SIZE AS 文件大小, ");
            sqlQuery.Append("     TO_CHAR(UPLOAD_DATE, 'YYYYMMDD ') AS 上传日期 ");
            sqlQuery.Append(" FROM  GKFQD_WD  ");
            //项目名称
            sqlQuery.Append("  WHERE PROJECT_NAME ='" + textBox6.Text + "'");

            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet6.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSet6);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            
            //创建table的列 
            DataColumn fileColumn = new DataColumn();
            fileColumn.DataType = System.Type.GetType("System.String");//该列的数据类型 
            fileColumn.ColumnName = "文件所在路径";//该列得名称 
            fileColumn.DefaultValue = "服务器文件";//该列得默认值
            //创建table的列 
            DataColumn stateColumn = new DataColumn();
            stateColumn.DataType = System.Type.GetType("System.String");//该列的数据类型 
            stateColumn.ColumnName = "传输状态";//该列得名称 
            stateColumn.DefaultValue = "已上传";//该列得默认值
            dataSet6.Tables[0].Columns.Add(fileColumn);
            dataSet6.Tables[0].Columns.Add(stateColumn); 
          
           
        }
        #endregion

        #region 服务器文件是否存在判断
        public bool ServerFileExist( int k){
                bool flag = false;
                //文件名
               // string fileName = dataGridView1.Rows[k].Cells[0].Value.ToString();
                sqlQuery.Clear();
                sqlQuery.Append("SELECT FILE_NAME, ");
                sqlQuery.Append("       FILE_PATH, ");
                sqlQuery.Append("     TO_CHAR(UPLOAD_DATE, 'YYYYMMDD ') AS UPLOAD_DATE ");
                sqlQuery.Append("FROM       GKFQD_WD ");
                sqlQuery.Append("WHERE      FILE_NAME ='" + fileName + "'");
                sqlQuery.Append("   AND      PROJECT_NAME ='" + textBox6.Text + "'");
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gkfqd.Common.DbUse.GetOleDbconnection().Open();
                dataSet8.Clear();
                OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                MyAdapter.Fill(dataSet8);
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                if (dataSet8.Tables[0].Rows.Count > 0)
                {
                    flag = true;
                }
            
            return flag;
        }
        #endregion

        #region 项目概况报告 窗体
        private void button2_Click(object sender, EventArgs e)
        {
            
            gk01f gk01f = new gk01f();
            gk01f.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
            gk01f.StartPosition = FormStartPosition.CenterScreen;//窗体居中
            gk01f.ShowDialog();
        }
        #endregion
    }
}
