using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using SuperMap.Data;
using System.Configuration;

namespace gkfqd.Common
{
    public class Tool
    {

        public static void IsNumber(KeyPressEventArgs e, object sender)
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

        /// <summary>
        /// 在指定时间内尝试连接指定主机上的指定端口。 （默认端口：80,默认链接超时：5000毫秒）         /// </summary>
        /// <param name="HostNameOrIp">主机名称或者IP地址</param>
        /// <param name="port">端口</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>返回布尔类型</returns>
        public static bool IsHostAlive(string HostNameOrIp)
        {
            TcpClient tc = new TcpClient();
            tc.SendTimeout =  5000;
            tc.ReceiveTimeout =5000;

            bool isAlive;
            try
            {
                tc.Connect(HostNameOrIp,  80);
                isAlive = true;
            }
            catch
            {
                isAlive = false;
            }
            finally
            {
                tc.Close();
            }
            return isAlive;
        }


        public static bool IsUrlExist(string Url)
        {
            bool IsExist = false;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Url));
            ServicePointManager.Expect100Continue = false;
            try
            {
                ((HttpWebResponse)request.GetResponse()).Close();
                IsExist = true;
            }
            catch (WebException exception)
            {
              /*  if (exception.Status != WebExceptionStatus.ProtocolError)
                {
                    return num;
                }
                if (exception.Message.IndexOf("500 ") > 0)
                {
                    return 500;
                }
                if (exception.Message.IndexOf("401 ") > 0)
                {
                    return 401;
                }
                if (exception.Message.IndexOf("404") > 0)
                {
                    return  404;
                }*/
                IsExist = false;
            }

            return IsExist;
        }

        public static SuperMap.Data.Workspace commonWorkspace = new SuperMap.Data.Workspace();
        public static Datasource commonDatasource;
      
        public static WorkspaceConnectionInfo GetConnectionInfo()
        {
            string file = System.Windows.Forms.Application.ExecutablePath;
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            string connectionString = config.ConnectionStrings.ConnectionStrings["gkfqd.Common.Supermap.connectionInfo"].ConnectionString.ToString();
            string[] temp = connectionString.Split(';');

            WorkspaceConnectionInfo workspaceconnectionInfo = new WorkspaceConnectionInfo();
            workspaceconnectionInfo.Type = WorkspaceType.Oracle;
           // workspaceconnectionInfo.Server = "ORCL";
            workspaceconnectionInfo.Server = temp[0].Split('=')[1];
            workspaceconnectionInfo.Database = "";
          //  workspaceconnectionInfo.Name = "workspace";
            workspaceconnectionInfo.Name = temp[5].Split('=')[1];
           // workspaceconnectionInfo.User = "gkfqd";
            workspaceconnectionInfo.User = temp[2].Split('=')[1];
          //  workspaceconnectionInfo.Password = "123456";
            workspaceconnectionInfo.Password = temp[3].Split('=')[1];
            return workspaceconnectionInfo;
          //  commonWorkspace.Open(workspaceconnectionInfo);
           // return commonDatasource = commonWorkspace.Datasources["ORCL"];
        }

        public static SuperMap.Data.Workspace GetWorkspace()
        {
           
            return commonWorkspace ;
        }

        public static string GetWorkspaceDataDatasources()
        {
            string file = System.Windows.Forms.Application.ExecutablePath;
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            string connectionString = config.ConnectionStrings.ConnectionStrings["gkfqd.Common.Supermap.connectionInfo"].ConnectionString.ToString();
            string[] temp = connectionString.Split(';');
            return temp[6].Split('=')[1];
        }
    }

}
