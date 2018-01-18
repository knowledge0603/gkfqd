using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;



namespace gkfqd.ui.gk01
{
    public partial class gk018 : WinFormsUI.Docking.DockContent
    {
        #region 变量区
            //临时用 dataSet
            DataSet dataSetCity = new DataSet();
            DataSet dataSetCounty = new DataSet();
            DataSet dataSetCountySelect = new DataSet();
            DataSet dataSetCountyName = new DataSet();
            DataSet dataSetProject = new DataSet();
            DataSet dataSetProjectSelect = new DataSet();
            DataSet dataSetModify= new DataSet();

            //oracle 连接 操作
          //  public static OleDbConnection conn = new OleDbConnection("Provider=MSDAORA.1;User ID=gkfqd;Password=123456;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST =192.168.1.103)(PORT = 1521))) (connECT_DATA = (SERVICE_NAME = orcl)))");

            StringBuilder sqlQuery = new StringBuilder();
        #endregion

        #region 初始化
        public gk018()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            //建新批次号默认为1
            comboBox5.SelectedIndex = 0;
            LoadComboBox();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd"; //设置显示格式
        }

        
        public gk018(DataTable dt)
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;

            LoadComboBox();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;

            dateTimePicker1.CustomFormat = "yyyy-MM-dd"; //设置显示格式

            comboBox1.Text = dt.Rows[0]["项目所在省"].ToString();
            comboBox2.Text = dt.Rows[0]["项目所在市"].ToString();
            comboBox3.Text = dt.Rows[0]["项目所在县"].ToString();
            comboBox4.Text = dt.Rows[0]["所属复垦项目名称"].ToString();
            comboBox5.Text = dt.Rows[0]["批次号"].ToString();
            string[] sArray = Regex.Split(dt.Rows[0]["批准文号"].ToString(), "-", RegexOptions.IgnoreCase);
            textBox8.Text = sArray[0];
            textBox9.Text = sArray[1];
            textBox11.Text = sArray[2];
            textBox1.Text = dt.Rows[0]["建新文件名称"].ToString();
            if (dt.Rows[0]["符合土地利用规划"].ToString()== "是")
            {
                checkBox1.Checked = true;
            } 
             else
            {
               checkBox1.Checked = false;
            }

            dateTimePicker1.Text = dt.Rows[0]["实际批准建新规模批准时间"].ToString();
            textBox16.Text = dt.Rows[0]["实际批准建新规模"].ToString();
            textBox2.Text = dt.Rows[0]["建新占用耕地面积"].ToString();
            textBox3.Text = dt.Rows[0]["征收总面积"].ToString();
            textBox4.Text = dt.Rows[0]["征收总费用"].ToString();
            textBox5.Text = dt.Rows[0]["安置总人口"].ToString();
            textBox7.Text = dt.Rows[0]["劳动力人口"].ToString();
            textBox10.Text = dt.Rows[0]["占用总面积"].ToString();
            textBox12.Text = dt.Rows[0]["占用耕地面积"].ToString();
            textBox13.Text = dt.Rows[0]["占用耕地质量"].ToString();
            textBox14.Text = dt.Rows[0]["农村安置住房"].ToString();
            textBox15.Text = dt.Rows[0]["非农业发展用地"].ToString();
            textBox17.Text = dt.Rows[0]["农村占地"].ToString();
            textBox18.Text = dt.Rows[0]["其他建设用地农村"].ToString();
            textBox22.Text = dt.Rows[0]["商服用地"].ToString();
            textBox21.Text = dt.Rows[0]["工矿仓储"].ToString();
            textBox20.Text = dt.Rows[0]["住宅用地"].ToString();
            textBox19.Text = dt.Rows[0]["其他建设用地城镇"].ToString();
            button1.Text = "修改";
            //建新文件名称 设置为只读 目的是 建新文件名称 更新时不能重复
            textBox1.ReadOnly = true;
           
        }
        #endregion

        # region  区县旗县选择框  加载
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlQuery.Clear();
         
            sqlQuery.Append(" SELECT BHBS  FROM  XZQ  ");
            sqlQuery.Append(" WHERE XZQM  = '" + comboBox2.SelectedValue + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetCountySelect.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSetCountySelect);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //取得包含标识
            String strBhbs = dataSetCountySelect.Tables[0].Rows[0][0].ToString();
            StringBuilder sqlQueryCountyName = new StringBuilder();
            sqlQueryCountyName.Clear();
            sqlQueryCountyName.Append(" SELECT XZQM  FROM  XZQ  ");
            sqlQueryCountyName.Append(" WHERE CJ ='2'  ");
            sqlQueryCountyName.Append(" AND BHBS ='" + strBhbs + "'");
            sqlQueryCountyName.Append(" ORDER BY  XZQDM  ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetCountyName.Clear();
            OleDbDataAdapter MyAdapter2 = new OleDbDataAdapter(sqlQueryCountyName.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter2.Fill(dataSetCountyName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            comboBox3.DataSource = dataSetCountyName.Tables[0];
            comboBox3.DisplayMember = "XZQM";
            comboBox3.ValueMember = "XZQM";
        }
        #endregion

        #region  comboBox load
        public void LoadComboBox()
        {
            sqlQuery.Clear();
            sqlQuery.Append(" SELECT XZQM  FROM  XZQ  ");
            sqlQuery.Append(" WHERE CJ ='1'  ");
            sqlQuery.Append(" AND  BHBS <> '0000'  ");
            sqlQuery.Append(" ORDER BY  XZQDM  ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetCity.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSetCity);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();

            comboBox2.DataSource = dataSetCity.Tables[0];
            comboBox2.DisplayMember = "XZQM";
            comboBox2.ValueMember = "XZQM";
            comboBox2.Text = dataSetCity.Tables[0].Rows[0]["XZQM"].ToString(); 

            sqlQuery.Clear();
            sqlQuery.Append(" SELECT XZQM  FROM  XZQ  ");
            sqlQuery.Append(" WHERE CJ ='2'  ");
            sqlQuery.Append(" AND BHBS ='1001'");
            sqlQuery.Append(" ORDER BY  XZQDM  ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetCounty.Clear();

            OleDbDataAdapter MyAdapter2 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter2.Fill(dataSetCounty);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            comboBox3.DataSource = dataSetCounty.Tables[0];
            comboBox3.DisplayMember = "XZQM";
            comboBox3.ValueMember = "XZQM";

            comboBox3.Text = dataSetCounty.Tables[0].Rows[0]["XZQM"].ToString(); 
           
            sqlQuery.Clear();
            sqlQuery.Append(" SELECT XMMC  FROM  FKXM  ");
            sqlQuery.Append(" WHERE XMSZ  = '" + comboBox1.Text + "'");
            sqlQuery.Append("  AND   XMSZS  = '" + comboBox2.Text + "'");
            sqlQuery.Append("  AND   XMSZX  = '" + comboBox3.Text + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetProject.Clear();
            OleDbDataAdapter MyAdapter3 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter3.Fill(dataSetProject);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            comboBox4.DataSource = dataSetProject.Tables[0];
            comboBox4.DisplayMember = "XMMC";
            comboBox4.ValueMember = "XMMC";
        }
        #endregion

        #region 更新插入处理
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
            //所属复垦项目项目名称	XMMC
            if (comboBox4.Text == "")
            {
                MessageBox.Show("项目名称不能为空，请输入！");
                return;
            }
           
            //批准文号	PZWH
            if (textBox8.Text == "")
            {
                MessageBox.Show("批准文号不能为空，请输入！");
                return;
            }
            //批准文号	PZWH
            if (textBox9.Text == "")
            {
                MessageBox.Show("批准文号不能为空，请输入！");
                return;
            }
            //批准文号	PZWH
            if (textBox11.Text == "")
            {
                MessageBox.Show("批准文号不能为空，请输入！");
                return;
            }
            //建新文件名称
            if (textBox1.Text == "")
            {
                MessageBox.Show("建新文件名称不能为空，请输入！");
                return;
            }

            //实际批准建新规模时间
            if (dateTimePicker1.Text == "")
            {
                MessageBox.Show("实际批准建新规模时间不能为空，请输入！");
                return;
            }
            //实际批准建新规模	PZGM
            if (textBox16.Text == "")
            {
                MessageBox.Show("实际批准建新规模不能为空，请输入！");
                return;
            }
            //其中建新可占用耕地面积	JXZYGDMJ
            if (textBox2.Text == "")
            {
                MessageBox.Show("其中建新可占用耕地面积不能为空，请输入！");
                return;
            }
            
          
            if (button1.Text != "修改")
            {
               
                //查询重复建新文件名称
                sqlQuery.Clear();
                sqlQuery.Append(" SELECT JXWJMC  FROM  JXXM  ");
                sqlQuery.Append(" WHERE JXWJMC ='" + textBox1.Text + "'");

                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gkfqd.Common.DbUse.GetOleDbconnection().Open();
                dataSetModify.Clear();
                OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                MyAdapter.Fill(dataSetModify);
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                if (dataSetModify.Tables[0].Rows.Count != 0)
                {
                    MessageBox.Show("建新文件已经存在,请重新录入！");
                    return;
                }
            }

            try
            {
                if (button1.Text == "修改")
                {
                    UpdateData();
                    MessageBox.Show("建新项目数据更新处理成功！");
                }
                else
                {
                    InsertData();
                             MessageBox.Show("建新项目数据插入处理成功！");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("建新项目数据提交处理异常！");
                return;
            }
        }
        #endregion

        #region 点击县 加载县包含项目名称
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlQuery.Clear();
            sqlQuery.Append(" SELECT XMMC  FROM  FKXM  ");
            sqlQuery.Append(" WHERE XMSZ  = '" + comboBox1.Text + "'");
            sqlQuery.Append("  AND   XMSZS  = '" + comboBox2.Text + "'");
            sqlQuery.Append("  AND   XMSZX  = '" + comboBox3.Text + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetProjectSelect.Clear();
            OleDbDataAdapter MyAdapter3 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter3.Fill(dataSetProjectSelect);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            comboBox4.DataSource = dataSetProjectSelect.Tables[0];
            comboBox4.DisplayMember = "XMMC";
            comboBox4.ValueMember = "XMMC";
        }
         #endregion

        #region 数据更新
        private void UpdateData()
        {
            textBox1.ReadOnly = true;
            sqlQuery.Clear();
            //复垦项目表   项目所在省	
            sqlQuery.Append(" UPDATE   JXXM  SET XMSZ = '" + comboBox1.Text + "',");
            // 项目所在市
            sqlQuery.Append("XMSZS='" + comboBox2.Text + "',");
            //项目所在县	XMSZX
            sqlQuery.Append("XMSZX='" + comboBox3.Text + "',");
            //项目名称	XMMC
            sqlQuery.Append("XMMC='" + comboBox4.Text + "',");
            //批准文号	PZWH
            sqlQuery.Append("PZWH='" + textBox8.Text + "-" + textBox9.Text + "-" + textBox11.Text + "',");
            //是否符合规划
            if(checkBox1.Checked==true){
                sqlQuery.Append("FHGH='是',");
            }else{
                sqlQuery.Append("FHGH='否',");
            }
            //批准时间	PZSJ
            sqlQuery.Append("PZSJ=" + "to_date('" + dateTimePicker1.Text + "','yyyy-mm-dd')" + ",");
            //实际批准建新规模	PZGM
            sqlQuery.Append("PZGM='" + textBox16.Text + "',");
            //建新占用耕地面积	JXZYGDMJ
            sqlQuery.Append("JXZYGDMJ='" + textBox2.Text + "',");
            //批次号	PCH
            sqlQuery.Append("PCH='" + comboBox5.Text + "',");
            //记录更新时间
            sqlQuery.Append("UPDATE_TIME=sysdate ");
            if (textBox3.Text != "")
            {
                //征收总面积	ZSZMJ
                sqlQuery.Append(",ZSZMJ='" + textBox3.Text + "'");
            }
            if (textBox4.Text != "")
            {
                //征收总费用	ZSZFY
                sqlQuery.Append(", ZSZFY='" + textBox4.Text + "'");
            }
            if (textBox5.Text != "")
            {
                //安置总人口	AZZRK
                sqlQuery.Append(", AZZRK='" + textBox5.Text + "'");
            }
            if (textBox7.Text != "")
            {
                //劳动力人口	LDLRK
                sqlQuery.Append(", LDLRK='" + textBox4.Text + "'");
            }
            if (textBox10.Text != "")
            {
                //占用总面积	ZYZMJ
                sqlQuery.Append(", ZYZMJ='" + textBox10.Text + "'");
            }
            if (textBox12.Text != "")
            {
                //占用耕地面积	ZYGDMJ
                sqlQuery.Append(", ZYGDMJ='" + textBox19.Text + "'");
            }
            if (textBox13.Text != "")
            {
                //占用耕地质量	ZYGDZL
                sqlQuery.Append(", ZYGDZL='" + textBox13.Text + "'");
            }
            if (textBox14.Text != "")
            {
                //农村安置住房	NCAZZF
                sqlQuery.Append(", NCAZZF='" + textBox14.Text + "'");
            }
            if (textBox15.Text != "")
            {
                //非农业发展用地	FNYFZYD
                sqlQuery.Append(", FNYFZYD='" + textBox15.Text + "'");
            }
            if (textBox17.Text != "")
            {
                //农村基础设施和公共服务配套设施占地  	PTSS
                sqlQuery.Append(", PTSS='" + textBox17.Text + "'");
            }
            if (textBox18.Text != "")
            {
                //其他建设用地农村	QTJSYDNC
                sqlQuery.Append(", QTJSYDNC='" + textBox18.Text + "'");
            }
            if (textBox22.Text != "")
            {
                //商服用地	SFYD
                sqlQuery.Append(", SFYD='" + textBox22.Text + "'");
            }
            if (textBox21.Text != "")
            {
                //工矿仓储	GKCC
                sqlQuery.Append(", GKCC='" + textBox21.Text + "'");
            }
            if (textBox20.Text != "")
            {
                //住宅用地	ZZYD
                sqlQuery.Append(", ZZYD='" + textBox20.Text + "'");
            }
            if (textBox19.Text != "")
            {
                //其他建设用地城镇	QTJSYDCZ
                sqlQuery.Append(", QTJSYDCZ='" + textBox19.Text + "'");
            }
            //项目名称	XMMC
            sqlQuery.Append("   WHERE JXWJMC='" + textBox1.Text + "'");
          /*  gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            OleDbCommand updateCommand = new OleDbCommand(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            updateCommand.ExecuteNonQuery();
            gkfqd.Common.DbUse.GetOleDbconnection().Close();*/
            gkfqd.Common.DbUse.conn.Close();
            gkfqd.Common.DbUse.conn.Open();
            OleDbCommand updateCommand = new OleDbCommand(sqlQuery.ToString(), gkfqd.Common.DbUse.conn);
            updateCommand.ExecuteNonQuery();
            gkfqd.Common.DbUse.conn.Close();

        }
        #endregion

        #region 数据插入处理
        public void InsertData()
        {
           
            sqlQuery.Clear();
            //复垦项目表   项目所在省	
            sqlQuery.Append(" INSERT INTO JXXM (XMSZ, ");
            // 项目所在市
            sqlQuery.Append("XMSZS,");
            //项目所在县	XMSZX
            sqlQuery.Append("XMSZX,");
            //所属复垦项目名称	XMMC
            sqlQuery.Append("XMMC,");
            //批准文号	PZWH
            sqlQuery.Append("PZWH,");
            //建新文件名称  JXWJMC
            sqlQuery.Append("JXWJMC,");
            //符合土地利用规划	FHGH
            sqlQuery.Append("FHGH,");
            //实际批准建新规模批准时间	PZSJ
            sqlQuery.Append("PZSJ,");
            //实际批准建新规模	PZGM
            sqlQuery.Append("PZGM,");
            //建新占用耕地面积	JXZYGDMJ
            sqlQuery.Append("JXZYGDMJ,");
            //批次号	pch
            sqlQuery.Append("PCH,");
            //用户ID  记录 该记录是那个用户录入的
            sqlQuery.Append("YHID,");
            //记录插入时间
            sqlQuery.Append("INSERT_TIME");
            if (textBox3.Text != "")
            {
                //征收总面积 ZSZMJ
                sqlQuery.Append(",ZSZMJ");
            }
            if (textBox4.Text != "")
            {
                //征收总费用	ZSZFY
                sqlQuery.Append(",ZSZFY");
            }
            if (textBox5.Text != "")
            {
                //安置总人口	AZZRK
                sqlQuery.Append(",AZZRK");
            }
            if (textBox7.Text != "")
            {
                //劳动力人口	LDLRK
                sqlQuery.Append(",LDLRK");
            }
            if (textBox10.Text != "")
            {
                //占用总面积	ZYZMJ
                sqlQuery.Append(",ZYZMJ");
            }
            if (textBox12.Text != "")
            {
                //占用耕地面积	ZYGDMJ
                sqlQuery.Append(",ZYGDMJ");
            }
            if (textBox13.Text != "")
            {
                //占用耕地质量	ZYGDZL
                sqlQuery.Append(",ZYGDZL");
            }
            if (textBox14.Text != "")
            {
                //农村安置住房	NCAZZF
                sqlQuery.Append(",NCAZZF");
            }
            if (textBox15.Text != "")
            {
                //非农业发展用地	FNYFZYD
                sqlQuery.Append(",FNYFZYD");
            }
            if (textBox17.Text != "")
            {
                //农村基础设施和公共服务配套设施占地  	PTSS
                sqlQuery.Append(",PTSS");
            }
            if (textBox18.Text != "")
            {
                //其他建设用地农村	QTJSYDNC
                sqlQuery.Append(",QTJSYDNC");
            }
            if (textBox22.Text != "")
            {
                //商服用地	SFYD
                sqlQuery.Append(",SFYD");
            }

            if (textBox21.Text != "")
            {
                //工矿仓储	GKCC
                sqlQuery.Append(",GKCC");
            }
            if (textBox20.Text != "")
            {
                //住宅用地	ZZYD
                sqlQuery.Append(",ZZYD");
            }
            if (textBox19.Text != "")
            {
                //其他建设用地城镇	QTJSYDCZ
                sqlQuery.Append(",QTJSYDCZ");
            }
            sqlQuery.Append(" ) VALUES (  '");
            //项目所在省	
            sqlQuery.Append(comboBox1.Text + "','");
            // 项目所在市
            sqlQuery.Append(comboBox2.Text + "','");
            //项目所在县	XMSZX
            sqlQuery.Append(comboBox3.Text + "','");
            //所属复垦项目项目名称	XMMC
            sqlQuery.Append(comboBox4.Text + "','");
            //批准文号	PZWH
            sqlQuery.Append(textBox8.Text + "-" + textBox9.Text + "-" + textBox11.Text + "','");
            //建新文件名称  JXWJMC
            sqlQuery.Append(textBox1.Text + "','");
            if(checkBox1.Checked==true)
            {
                //符合土地利用规划	FHGH
                sqlQuery.Append("是" + "',");
            }
            else
            {
                //符合土地利用规划	FHGH
                sqlQuery.Append("否" + "',");
            }
            //实际批准建新规模批准时间	PZSJ
            sqlQuery.Append("to_date('" + dateTimePicker1.Text + "','yyyy-mm-dd')" + ",'");
            //实际批准建新规模	PZGM
            sqlQuery.Append(textBox16.Text + "','");
            //建新占用耕地面积	JXZYGDMJ
            sqlQuery.Append(textBox2.Text + "',");
            //批次号	pch
            sqlQuery.Append(comboBox5.Text + ",'");
            //用户ID 
            sqlQuery.Append(login.userId + "',");
            //记录插入时间
            sqlQuery.Append("sysdate ");
            if (textBox3.Text != "")
            {
                //征收总面积 ZSZMJ
                sqlQuery.Append(",'" + textBox3.Text + "'");
            }
            if (textBox4.Text != "")
            {
                ////征收总费用	ZSZFY
                sqlQuery.Append(",'" + textBox4.Text + "'");
            }
            if (textBox5.Text != "")
            {
                //安置总人口	AZZRK
                sqlQuery.Append(",'" + textBox5.Text + "'");
            }
            if (textBox7.Text != "")
            {
                //劳动力人口	LDLRK
                sqlQuery.Append(",'" + textBox7.Text + "'");
            }
            if (textBox10.Text != "")
            {
                // //占用总面积	ZYZMJ
                sqlQuery.Append(",'" + textBox10.Text + "'");
            }
            if (textBox12.Text != "")
            {
                ////占用耕地面积	ZYGDMJ
                sqlQuery.Append(",'" + textBox12.Text + "'");
            }
            if (textBox13.Text != "")
            {
                //占用耕地质量	ZYGDZL
                sqlQuery.Append(",'" + textBox13.Text + "'");
            }
            if (textBox14.Text != "")
            {
                //农村安置住房	NCAZZF
                sqlQuery.Append(",'" + textBox14.Text + "'");
            }
            if (textBox15.Text != "")
            {
                //非农业发展用地	FNYFZYD
                sqlQuery.Append(",'" + textBox15.Text + "'");
            }
            if (textBox17.Text != "")
            {
                //农村基础设施和公共服务配套设施占地  	PTSS
                sqlQuery.Append(",'" + textBox17.Text + "'");
            }

            if (textBox18.Text != "")
            {
                //其他建设用地农村	QTJSYDNC
                sqlQuery.Append(",'" + textBox18.Text + "'");
            }
            if (textBox22.Text != "")
            {
                //商服用地	SFYD
                sqlQuery.Append(",'" + textBox22.Text + "'");
            }

            if (textBox21.Text != "")
            {
                //工矿仓储	GKCC
                sqlQuery.Append(",'" + textBox21.Text + "'");
            }
            if (textBox20.Text != "")
            {
                //住宅用地	ZZYD
                sqlQuery.Append(",'" + textBox20.Text + "'");
            }
            if (textBox19.Text != "")
            {
                //其他建设用地城镇	QTJSYDCZ
                sqlQuery.Append(",'" + textBox19.Text + "'");
            }

            sqlQuery.Append(")");
           /* gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
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

        #region 数字检查
        private void textBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
           gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox22_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox20_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox19_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }
        #endregion
    }
}
