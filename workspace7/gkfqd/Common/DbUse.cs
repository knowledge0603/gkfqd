using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Configuration;
using System.Data;

namespace gkfqd.Common
{


    public  class DbUse
    {


        //共同用  sqlQuery
        static StringBuilder sqlQuery = new StringBuilder();

        static string file = System.Windows.Forms.Application.ExecutablePath;
        static System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(file);
        public static string connectionString = config.ConnectionStrings.ConnectionStrings["gkfqd.Common.OleDbconnection"].ConnectionString.ToString();


        //oracle 连接 操作
        public static OleDbConnection conn = new OleDbConnection(connectionString);

        /// <summary>
        /// 依据连接串名字connectionName返回数据连接字符串
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static void  GetconnectionStringsConfig()
        {
            //指定config文件读取              
            string file = System.Windows.Forms.Application.ExecutablePath;       
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            string connectionString = config.ConnectionStrings.ConnectionStrings["gkfqd.Common.Supermap.connectionInfo"].ConnectionString.ToString();
            string[] temp = connectionString.Split(';');
        }


        public static OleDbConnection GetOleDbconnection()
        {
            //指定config文件读取              
            string file = System.Windows.Forms.Application.ExecutablePath;
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            string connectionString = config.ConnectionStrings.ConnectionStrings["gkfqd.Common.OleDbconnection"].ConnectionString.ToString();
            return new OleDbConnection(connectionString);
        }
        
        public static string  GetTownCode ( string townName)
        {
             //行政区名
             DataSet dataSetTownName = new DataSet();
            //----------------取得行政区代码
            sqlQuery.Clear();
            sqlQuery.Append("SELECT XZQDM AS 行政区代码");
            sqlQuery.Append(" FROM   XZQ ");
            sqlQuery.Append(" WHERE  XZQM='" + townName + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetTownName.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSetTownName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //----------------拼接行政区图形数据存储表名
              string tableName = "T" + dataSetTownName.Tables[0].Rows[0]["行政区代码"].ToString() + "_2017" ;
              return tableName;
        }
        //工矿废弃地图层
        public static string FXGetTownCode(string townName)
        {
            //行政区名
            DataSet dataSetTownName = new DataSet();
            //----------------取得行政区代码
            sqlQuery.Clear();
            sqlQuery.Append("SELECT XZQDM AS 行政区代码");
            sqlQuery.Append(" FROM   XZQ ");
            sqlQuery.Append(" WHERE  XZQM='" + townName + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetTownName.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSetTownName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //----------------拼接行政区图形数据存储表名
            string tableName = "T" + dataSetTownName.Tables[0].Rows[0]["行政区代码"].ToString() ;
            return tableName;
        }

        //土地现状图层
        public static string XZGetTownCode(string townName)
        {
            //行政区名
            DataSet dataSetTownName = new DataSet();
            //----------------取得行政区代码
            sqlQuery.Clear();
            sqlQuery.Append("SELECT XZQDM AS 行政区代码");
            sqlQuery.Append(" FROM   XZQ ");
            sqlQuery.Append(" WHERE  XZQM='" + townName + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetTownName.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSetTownName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //----------------拼接行政区图形数据存储表名
            string tableName = "XZ" + dataSetTownName.Tables[0].Rows[0]["行政区代码"].ToString() ;
            return tableName;
        }
        //规划图层
        public static string GHGetTownCode(string townName)
        {
            //行政区名
            DataSet dataSetTownName = new DataSet();
            //----------------取得行政区代码
            sqlQuery.Clear();
            sqlQuery.Append("SELECT XZQDM AS 行政区代码");
            sqlQuery.Append(" FROM   XZQ ");
            sqlQuery.Append(" WHERE  XZQM='" + townName + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetTownName.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSetTownName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //----------------拼接行政区图形数据存储表名
            string tableName = "GH" + dataSetTownName.Tables[0].Rows[0]["行政区代码"].ToString() ;
            return tableName;
        }

        public static string GetTownCodeByProjectName(string projectName)
        {
            //行政区名
            DataSet dataSetTownName = new DataSet();
           //-----
            sqlQuery.Clear();
            sqlQuery.Append("SELECT XMSZX AS 项目所在县 ");
            sqlQuery.Append("FROM      FKXM ");
            sqlQuery.Append("WHERE XMMC= '" + projectName + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetTownName.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSetTownName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            string tableName = gkfqd.Common.DbUse.GetTownCode(dataSetTownName.Tables[0].Rows[0]["项目所在县"].ToString());
            //-----
            return tableName;
        }
        //建新项目取得表名
        public static string GetTownCodeByProjectNameJx(string projectName)
        {
            //行政区名
            DataSet dataSetTownName = new DataSet();
            //-----
            sqlQuery.Clear();
            sqlQuery.Append("SELECT XMSZX AS 项目所在县 ");
            sqlQuery.Append("FROM      JXXM  ");
            sqlQuery.Append("WHERE XMMC= '" + projectName + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetTownName.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSetTownName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            string tableName = "JX" + gkfqd.Common.DbUse.GetTownCode(dataSetTownName.Tables[0].Rows[0]["项目所在县"].ToString());
            //-----
            return tableName;
        }
        //补耕项目取得表名
        public static string GetTownCodeByProjectNameBg(string projectName)
        {
            //行政区名
            DataSet dataSetTownName = new DataSet();
            //-----
            sqlQuery.Clear();
            sqlQuery.Append("SELECT XMSZX AS 项目所在县 ");
            sqlQuery.Append("FROM      JXXM  ");
            sqlQuery.Append("WHERE XMMC= '" + projectName + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetTownName.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSetTownName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            string tableName = "BG" + gkfqd.Common.DbUse.GetTownCode(dataSetTownName.Tables[0].Rows[0]["项目所在县"].ToString());
            //-----
            return tableName;
        }
        //取建新图层
        public static string GetTownCodeJX(string townNameJX)
        {
            //行政区名
            DataSet dataSetTownName = new DataSet();
            //----------------取得行政区代码
            sqlQuery.Clear();
            sqlQuery.Append("SELECT XZQDM AS 行政区代码");
            sqlQuery.Append(" FROM   XZQ ");
            sqlQuery.Append(" WHERE  XZQM='" + townNameJX + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetTownName.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSetTownName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //----------------拼接行政区图形数据存储表名
            string tableName = "JXT" + dataSetTownName.Tables[0].Rows[0]["行政区代码"].ToString() + "_2017";
            return tableName;
        }


        #region 分析图层

       
        //建新图层
        public static string JXGetTownCode(string townNameJX)
        {
            //行政区名
            DataSet dataSetTownName = new DataSet();
            //----------------取得行政区代码
            sqlQuery.Clear();
            sqlQuery.Append("SELECT XZQDM AS 行政区代码");
            sqlQuery.Append(" FROM   XZQ ");
            sqlQuery.Append(" WHERE  XZQM='" + townNameJX + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetTownName.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSetTownName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //----------------拼接行政区图形数据存储表名
            string tableName = "JXT" + dataSetTownName.Tables[0].Rows[0]["行政区代码"].ToString();
            return tableName;
        }
        //现状图层
      
       

        #endregion
    }
}
