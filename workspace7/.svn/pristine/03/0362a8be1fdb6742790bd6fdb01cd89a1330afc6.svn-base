using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Diagnostics;

namespace gkfqd.ui.gk01
{
    public partial class gk017 : Form
    {
        #region 变量区
        //oracle 连接 操作
     //   public static OleDbconnection conn = new OleDbconnection("Provider=MSDAORA.1;User ID=gkfqd;Password=123456;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST =192.168.1.103)(PORT = 1521))) (connECT_DATA = (SERVICE_NAME = orcl)))");
        //临时用 dataSet
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
        public gk017()
        {
            InitializeComponent();
            //不显示dataGridView1最后一行
            dataGridView1.AllowUserToAddRows = false;
            textBox6.ReadOnly = true;
            //文档类型默认为扫描附件
            comboBox1.SelectedIndex = 1;
        }
        public gk017(string projectId)
        {
            InitializeComponent();
            //toolStripInitialize();
            //不显示dataGridView1最后一行
            dataGridView1.AllowUserToAddRows = false;
            //  fileWorkSpaceOpen();
            textBox6.Text = projectId;
            textBox6.ReadOnly = true;
            DataGridFileGet();
            //文档类型默认为扫描附件
            comboBox1.SelectedIndex = 0;
        }
        #endregion

        #region 录入处理
        private void button1_Click(object sender, EventArgs e)
        {
            //导入文档
            ImportFile();
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
             webServiceTransFile = new gkfqd.ServiceReference3.Service1SoapClient();

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


        public string CoverFileByDataGrid(string path, string fileName)
        {
            gkfqd.ServiceReference3.Service1SoapClient
             webServiceTransFile = new gkfqd.ServiceReference3.Service1SoapClient();

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
                    transFileFlag = webServiceTransFile.CoverFile(ConvertToBinary(ii, ifEnd, fileLength, path), fileName, true);
                  
                }
                else
                {
                    transFileFlag = webServiceTransFile.CoverFile(ConvertToBinary(ii, ifEnd, fileLength, path), fileName, false);
                  
                }
            }
            return transFileFlag;
        }

        #endregion
        
        #region 文件导入处理
        string fileSize;
        public void ImportFile()
        {
            //判断服务器是否开启

            if (!gkfqd.Common.Tool.IsUrlExist("http://localhost:6721/"))
            {
                MessageBox.Show("文件服务器未开启，请联系系统管理员确认！");
                return;
            }
            bool splashScreenManagerCloseFlag = false;
            //传输等待框打开
            splashScreenManager2.ShowWaitForm();
            splashScreenManager2.SetWaitFormCaption("文件传输中");
            splashScreenManager2.SetWaitFormDescription("请等待...");
            for (int k = 0; k < this.dataGridView1.Rows.Count; k++)
            {
                //文件路径
                string filePath = dataGridView1.Rows[k].Cells[3].Value.ToString();
                //文件名
                string fileName = dataGridView1.Rows[k].Cells[0].Value.ToString();
                //文件大小
                 fileSize = dataGridView1.Rows[k].Cells[1].Value.ToString();
                //多次点击录入或更新按钮不重复上传文件
                if (dataGridView1.Rows[k].Cells[4].Value == "已上传" && button1.Text != "修改")
                {
                    if (!splashScreenManagerCloseFlag)
                    {
                        splashScreenManager2.CloseWaitForm();
                        splashScreenManagerCloseFlag = true;
                    }
                    return;
                }
                //
                
                //传输处理
                string transFlag = TransFileByDataGrid(filePath, fileName);
                if (transFlag == "2")
                {
                    dataGridView1.Rows[k].Cells[3].Value = "传输失败";
                }
                else if (transFlag == "0")
                {
                    dataGridView1.Rows[k].Cells[3].Value = "文件长度为0";
                }
                else if (transFlag == "3")
                {
                    DialogResult RSS = MessageBox.Show(this, "服务存在同名文件，是否覆盖服务器文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    switch (RSS)
                    {
                        case DialogResult.Yes:
                             transFlag = CoverFileByDataGrid(filePath, fileName);
                            if (transFlag == "1")
                            {
                                dataGridView1.Rows[k].Cells[4].Value = "传输成功";
                                if (!ServerFileExist(k))
                                {
                                    InsertGkfqdWd();
                                }
                            }
                            break;
                        case DialogResult.No:
                            break;
                    }
                }
                else if (transFlag == "1")
                {
                    dataGridView1.Rows[k].Cells[4].Value = "传输成功";
                    if (!ServerFileExist(k))
                    {
                        InsertGkfqdWd();
                    }
                }
            }
            if (!splashScreenManagerCloseFlag)
            {
                splashScreenManager2.CloseWaitForm();
                splashScreenManagerCloseFlag = true;
            }
        }
        #endregion
       
        #region 插入复垦项目对应文档处理
        public void InsertGkfqdWd()
        {
            try
            {
                gkfqd.Common.DbUse.conn.Close();
                gkfqd.Common.DbUse.conn.Open();
                sqlQuery.Clear();
                //工矿废弃地文档库录入
                sqlQuery.Append("INSERT INTO GKFQD_WD ( ");
                //项目名称
                sqlQuery.Append(" PROJECT_NAME,");
                //文件名
                sqlQuery.Append("FILE_NAME,");
                //文件保存文件夹名称web服务器文件夹
                sqlQuery.Append("FILE_PATH,");
                //文件大小
                sqlQuery.Append("FILE_SIZE,");
                //文件TYPE
                sqlQuery.Append("FILE_TYPE,");
                //更新日期
                sqlQuery.Append(" UPLOAD_DATE )");
                //项目名称
                sqlQuery.Append(" VALUES ( '" + textBox6.Text + "','");
                //文件名
                sqlQuery.Append(fileName + "','");
                //文件保存文件夹名称web服务器文件夹
                sqlQuery.Append("gkfqdFile");
                //文件大小
                sqlQuery.Append("','" + fileSize);
                //文件类型
                sqlQuery.Append("','" + comboBox1.Text);
                //更新日期
                sqlQuery.Append("',sysdate )");
                OleDbCommand insertCommand = new OleDbCommand(sqlQuery.ToString(), gkfqd.Common.DbUse.conn);
                insertCommand.ExecuteNonQuery();
                gkfqd.Common.DbUse.conn.Close();
                MessageBox.Show("复垦文档数据上传成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("项目对应文档表插入处理异常！");
                return;
            }
        }
        #endregion

        #region 选择文件处理
        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog objFile = new OpenFileDialog();
            objFile.Filter = "Excel2003文件(.xls)|*.xls|PDF文件(.pdf)|*.pdf|Word2003文件(.doc)|*.doc|Excel2007文件(.xlsx)|*.xlsx|Word2007文件(.docx)|*.docx|其他文件(.*)|*.*";
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
           
            DataGridViewRefresh();
        }
        #endregion
        
        #region datagridview 数据添加 更新处理
        //更新datagridview
        public void DataGridViewRefresh()
        {
            dataGridView1.DataSource = null;
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.HeaderText = "文件名称";

                column.ValueType = System.Type.GetType("System.String");//该列的数据类型 
                dataGridView1.Columns.Add(column);
                DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
                column1.ValueType = System.Type.GetType("System.String");//该列的数据类型 
                column1.HeaderText = "文件大小";
                dataGridView1.Columns.Add(column1);
                DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
                column4.ValueType = System.Type.GetType("System.String");//该列的数据类型 
                column4.HeaderText = "上传日期";
                dataGridView1.Columns.Add(column4);
                DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
                column2.HeaderText = "文件所在路径";
                column2.ValueType = System.Type.GetType("System.String");//该列的数据类型 
                dataGridView1.Columns.Add(column2);
                DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
                column3.HeaderText = "文件状态";
                column3.ValueType = System.Type.GetType("System.String");//该列的数据类型 
                dataGridView1.Columns.Add(column3);
            }
            // dataGridView1添加内容

            //初始化row
            DataGridViewRow row = null;
            row = new DataGridViewRow();
            for (int i = 0; i < this.dataGridView1.ColumnCount; i++)
            {
                //将字段值添加到dataGridView中对应的位置
                DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                if (i == 0)
                {
                    for (int k = 0; k < this.dataGridView1.Rows.Count; k++)
                    {
                        if (k >= 1 && dataGridView1.Rows[k - 1].Cells[0].Value.ToString().Equals(fileName))
                        {
                            MessageBox.Show("添加文件已经存在,请重新选择！");
                            return;
                        }
                    }
                    cell.Value = fileName;
                }
                if (i == 1)
                {
                    cell.Value = fileLength + "byte";
                }
                if (i == 2)
                {
                    cell.Value = DateTime.Now.ToString("yyyyMMdd");
                }
                if (i == 3)
                {
                    cell.Value = filePath;
                }
                if (i == 4)
                {
                    if (transFileFlag == null || transFileFlag == "0")
                    {
                        cell.Value = "未传输";
                    }
                    else if (transFileFlag == "1")
                    {
                        cell.Value = "传输成功";
                    }
                    else if (transFileFlag == "2")
                    {
                        cell.Value = "传输失败";
                    }
                    else if (transFileFlag == "3")
                    {
                        cell.Value = "服务器存在同名文件";
                    }
                }
                row.Cells.Add(cell);
            }
            dataGridView1.Rows.Add(row);
            this.dataGridView1.Update();
       }
        #endregion
        
        #region 更新datagridview文件传输状态
        //更新datagridview文件传输状态
        public void DataGridViewRefreshClo()
        {
            // dataGridView1添加内容
            //初始化row
            DataGridViewRow row = null;
            row = new DataGridViewRow();
            for (int i = 0; i < this.dataGridView1.ColumnCount; i++)
            {
                //将字段值添加到dataGridView中对应的位置
                DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                if (i == 4)
                {
                    if (transFileFlag == null || transFileFlag == "0")
                    {
                        cell.Value = "未传输";
                    }
                    else if (transFileFlag == "1")
                    {
                        cell.Value = "传输成功";
                    }
                    else if (transFileFlag == "2")
                    {
                        cell.Value = "传输失败";
                    }
                }
                row.Cells.Add(cell);
            }
            this.dataGridView1.Rows.Add(row);
            this.dataGridView1.Update();
        }
        #endregion
        
        #region 删除文档列表
        private void button2_Click(object sender, EventArgs e)
        {
            
            DialogResult RSS = MessageBox.Show(this, "确定要删除选中行文档数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            switch (RSS)
            {
                case DialogResult.Yes:
                    if (this.dataGridView1.SelectedRows.Count == 0)
                    {
                        MessageBox.Show("请点击行头选择整行删除！");
                        return;
                    }
                    DataGridViewRow dgvr = dataGridView1.CurrentRow;
                    string filePath = dgvr.Cells[3].Value.ToString();
                    string fileName = dgvr.Cells[0].Value.ToString();
                    string uploadDate = dgvr.Cells[2].Value.ToString();
                    string fileStatus = dgvr.Cells[4].Value.ToString();

                    for (int i = this.dataGridView1.SelectedRows.Count; i > 0; i--)
                    {
                        if (fileStatus == "传输成功" || fileStatus == "已上传")
                        {
                            if (!gkfqd.Common.Tool.IsUrlExist("http://localhost:6721/"))
                            {
                                MessageBox.Show("文件服务器未开启，请联系系统管理员确认！");
                                return;
                            }
                            //服务器文件先删除表记录，然后删除文件
                            //资金安排	ZJAP
                            sqlQuery.Clear();
                            sqlQuery.Append("DELETE FROM GKFQD_WD");
                            sqlQuery.Append(" WHERE PROJECT_NAME='" + textBox6.Text + "'");
                            sqlQuery.Append(" AND  FILE_NAME='" + fileName + "'");
                            gkfqd.Common.DbUse.conn.Close();
                            gkfqd.Common.DbUse.conn.Open();
                            OleDbCommand deleteCommand = new OleDbCommand(sqlQuery.ToString(), gkfqd.Common.DbUse.conn);
                            deleteCommand.ExecuteNonQuery();
                            gkfqd.Common.DbUse.conn.Close();
                           
                           gkfqd.ServiceReference3.Service1SoapClient webServiceTransFile = new gkfqd.ServiceReference3.Service1SoapClient();
                           string returnFlag = webServiceTransFile.DeleteFile(uploadDate + "\\" + fileName);
                            if (returnFlag == "4")
                            {
                                MessageBox.Show("服务器文件未删除！");
                            }
                            if (returnFlag == "5")
                            {
                                //删除dataGridView1表格中数据
                                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i - 1].Index);
                                MessageBox.Show("服务器文件删除成功");
                            }
                            if (returnFlag == "6")
                            {
                                MessageBox.Show("服务器文件删除失败");
                            }
                        } 
                        else if (filePath != "服务器文件")
                        {
                            //本地文件直接删除
                            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i - 1].Index);
                        }
                        
                    }
                    break;
                case DialogResult.No:
                    break;
            }
        }
        #endregion

        #region  打开文档
        private void button3_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvr = dataGridView1.CurrentRow;
            string fileName = null;
            string filePath = null;
            if (dgvr != null)
            {
                filePath = dgvr.Cells[3].Value.ToString();
                fileName = dgvr.Cells[0].Value.ToString();
            }
            if (filePath != "服务器文件")
            {
                if (dataGridView1.RowCount != 0)
                {

                    string path = dgvr.Cells[3].Value.ToString();
                    System.Diagnostics.Process.Start("explorer.exe", path);
                }
            }
            else
            {
                if (!gkfqd.Common.Tool.IsUrlExist("http://localhost:6721/"))
                {
                    MessageBox.Show("文件服务器未开启，请联系系统管理员确认！");
                    return;
                }
                sqlQuery.Clear();
                sqlQuery.Append("SELECT FILE_NAME, ");
                sqlQuery.Append("       FILE_PATH, ");
                sqlQuery.Append("     TO_CHAR(UPLOAD_DATE, 'YYYYMMDD ') AS UPLOAD_DATE ");
                sqlQuery.Append("FROM       GKFQD_WD ");
                sqlQuery.Append("WHERE      FILE_NAME ='" + fileName + "'");
                sqlQuery.Append("   AND      PROJECT_NAME ='" + textBox6.Text + "'");
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gkfqd.Common.DbUse.GetOleDbconnection().Open();
                dataSet7.Clear();
                OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                MyAdapter.Fill(dataSet7);
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                string url = "http://localhost:6721/gkfqdFile/" + dataSet7.Tables[0].Rows[0]["UPLOAD_DATE"].ToString().Trim() + "/" + fileName;
                Process.Start(url);
            }


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

            dataGridView1.DataSource = dataSet6.Tables[0];
        }
        #endregion

        #region 服务器文件是否存在判断
        public bool ServerFileExist(int k)
        {
            bool flag = false;
            //文件名
            string fileName = dataGridView1.Rows[k].Cells[0].Value.ToString();
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
    }
}
