﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.IO;

namespace WebService
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    
    public class Service1 : System.Web.Services.WebService
    { 
        #region webservice 测试 方法HelloWorld
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        #endregion

        #region webservice 接收文件服务端处理
        
        [WebMethod(Description = "TransFile")]
        public string TransFile(byte[] fileBt, string fileName, bool ifCreate)
        {
            // 0长度文件返回 0
            string rst = "0";
            if (fileBt.Length == 0){
                return rst;
            }
            string filePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CSVPath"].ToString());   //存储文件路径
            //创建系统日期文件夹,避免同一文件夹下文件太多问题，避免同一地图应文档重名
            filePath = filePath + DateTime.Now.ToString("yyyyMMdd") + "\\";
            if (!Directory.Exists(filePath))
            {
                // Create the directory it does not exist.
                Directory.CreateDirectory(filePath);
            }
            if (File.Exists(filePath + fileName) && ifCreate)
            {
                // 同一天日期文件夹下服务器已存在文件 返回 3
                return "3";
            }
           
            FileStream fstream;
            //是否创建新文件
            if (ifCreate)
            {
                fstream = new FileStream(filePath + fileName, FileMode.Create);
            }
            else
            {
                fstream = new FileStream(filePath + fileName, FileMode.Append);
            }
            try
            {
                fstream.Write(fileBt, 0, fileBt.Length);   //二进制转换成文件
                //上传成功返回 1
                rst = "1";
                fstream.Close();
            }
            catch (Exception ex)
            {
                //上传失败返回 1
                rst = "2";
            }
            finally
            {
                fstream.Close();
            }
            return rst;
        }
        #endregion

        #region webservice  方法DeleteFile
        [WebMethod]
        public string DeleteFile(string filePathName)
        {
            string filePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CSVPath"].ToString());   //存储文件路径
            //未删除
            string returnFlag = "4";
            if (File.Exists(filePath+filePathName))
            {
                try{
                    File.Delete(filePath + filePathName);
                    //删除成功
                    returnFlag = "5";
                }catch{
                    //删除失败
                    returnFlag = "6";
                }
            }
            return returnFlag;
        }
        #endregion

        #region webservice 覆盖文件服务端处理

        [WebMethod(Description = "CoverFile")]
        public string CoverFile(byte[] fileBt, string fileName, bool ifCreate)
        {
            // 0长度文件返回 0
            string rst = "0";
            if (fileBt.Length == 0)
            {
                return rst;
            }
            string filePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CSVPath"].ToString());   //存储文件路径
            //创建系统日期文件夹,避免同一文件夹下文件太多问题，避免同一地图应文档重名
            filePath = filePath + DateTime.Now.ToString("yyyyMMdd") + "\\";
            if (!Directory.Exists(filePath))
            {
                // Create the directory it does not exist.
                Directory.CreateDirectory(filePath);
            }
           

            FileStream fstream;
            //是否创建新文件
            if (ifCreate)
            {
                fstream = new FileStream(filePath + fileName, FileMode.Create);
            }
            else
            {
                fstream = new FileStream(filePath + fileName, FileMode.Append);
            }
            try
            {
                fstream.Write(fileBt, 0, fileBt.Length);   //二进制转换成文件
                //上传成功返回 1
                rst = "1";
                fstream.Close();
            }
            catch (Exception ex)
            {
                //上传失败返回 1
                rst = "2";
            }
            finally
            {
                fstream.Close();
            }
            return rst;
        }
        #endregion

    }
}