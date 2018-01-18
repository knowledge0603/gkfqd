using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace gkfqd
{
    public partial class AlertWindows : Form
    {
        #region 变量区


        private InformStyle InfoStyle = InformStyle.Vanish;//定义变量为隐藏  
        private System.Drawing.Rectangle Rect;//定义一个存储矩形框的数组  
        private bool isMouseMove = false;//是否在窗体中移动  
        static private AlertWindows alertwindows = new AlertWindows();//实例化当前窗体
        protected enum InformStyle
        {
            /// <summary>  
            /// 隐藏  
            /// </summary>  
            Vanish = 0,
            /// <summary>  
            /// 显视  
            /// </summary>  
            Display = 1,
            /// <summary>  
            /// 显视中  
            /// </summary>  
            Displaying = 2,
            /// <summary>  
            /// 隐藏中  
            /// </summary>  
            Vanishing = 3
        }
        /// <summary>  
        /// 获取或设置当前的操作状态  
        /// </summary>  
        protected InformStyle InfoState
        {
            get { return this.InfoStyle; }
            set { this.InfoStyle = value; }
        }
        #endregion

        #region 初始化
        public AlertWindows()
        {
            InitializeComponent();
            this.alerttimer.Stop();//停止计时器  
            //初始化工作区大小  
            System.Drawing.Rectangle rect = System.Windows.Forms.Screen.GetWorkingArea(this);
            this.Rect = new System.Drawing.Rectangle(rect.Right - this.Width - 1, rect.Bottom - this.Height - 1, this.Width, this.Height);

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(SystemInformation.WorkingArea.Width - this.Width, SystemInformation.WorkingArea.Height - this.Height);
        }
        #endregion

        #region 返回当前窗体的实例化
        /// <summary>  
        /// 返回此对象的实例  
        /// </summary>  
        /// <returns></returns>  
        static public AlertWindows Instance()
        {
            return alertwindows;
        }
        #endregion

        #region 声明WinAPI
        /// <summary>  
        /// 显示窗体  
        /// </summary>  
        /// <param name="hWnd"></param>  
        /// <param name="nCmdShow"></param>  
        /// <returns></returns>  
        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);
        #endregion

        #region 方法
        /// <summary>  
        /// 显示窗体  
        /// </summary>  
        public void Show(string value)
        {
            this.label2.Text = value;
            switch (this.InfoState)
            {
                case InformStyle.Vanish://窗体隐藏  
                    this.InfoState = InformStyle.Displaying;//设置窗体的操作状态为显示中  
                    this.SetBounds(Rect.X, Rect.Y + Rect.Height, Rect.Width, 0);//显示Popup窗体，并放置到屏幕的底部  
                    ShowWindow(this.Handle, 4);//显示窗体  
                    this.alerttimer.Interval = 200;//设置时间间隔为100  
                    this.alerttimer.Start();//启动计时器  
                    break;
                case InformStyle.Display://窗体显示  
                    this.alerttimer.Stop();//停止计时器  
                    this.alerttimer.Interval = 5000;//设置时间间隔为5000  
                    this.alerttimer.Start();//启动记时器  
                    break;
            }
        }
        #endregion  

        private void AlertWindows_MouseHover(object sender, EventArgs e)
        {
            this.isMouseMove = true;//当鼠标移入时，设为true
        }

        private void AlertWindows_MouseLeave(object sender, EventArgs e)
        {
            this.isMouseMove = false;//当鼠标移出时，设为false  
        }

        private void alerttimer_Tick(object sender, EventArgs e)
        {
            switch (this.InfoState)
            {
                case InformStyle.Display://显示当前窗体  
                    this.alerttimer.Stop();//停止计时器  
                    this.alerttimer.Interval = 200;//设置时间间隔为100  
                    if (!(this.isMouseMove))//如果鼠标不在窗体中移动  
                        this.InfoState = InformStyle.Vanishing;//设置当前窗体的操作状态为隐藏中  
                    this.alerttimer.Start();//启动计时器  
                    break;
                case InformStyle.Displaying://当前窗体显示中  
                    if (this.Height <= this.Rect.Height - 12)//当窗体没有完全显示时  
                        this.SetBounds(Rect.X, this.Top - 12, Rect.Width, this.Height + 12);//使窗体不断上移  
                    else
                    {
                        this.alerttimer.Stop();//停止计时器  
                        this.SetBounds(Rect.X, Rect.Y, Rect.Width, Rect.Height);//设置当前窗体的边界  
                        this.InfoState = InformStyle.Display;//设置当前窗体的操作状态为显示  
                        this.alerttimer.Interval = 5000;//设置时间间隔为5000  
                        this.alerttimer.Start();//启动计时器  
                    }
                    break;
                case InformStyle.Vanishing://隐藏当前窗体  
                    if (this.isMouseMove)//如果鼠标在窗体中移动  
                        this.InfoState = InformStyle.Displaying;//设置窗体的操作状态为显示  
                    else
                    {
                        if (this.Top <= this.Rect.Bottom - 12)//如果窗体没有完全隐藏  
                            this.SetBounds(Rect.X, this.Top + 12, Rect.Width, this.Height - 12);//使窗体不断下移  
                        else
                        {
                            this.Hide();//隐藏当前窗体  
                            this.InfoState = InformStyle.Vanish;//设置窗体的操作状态为隐藏  
                        }
                    }
                    break;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (this.InfoState != InformStyle.Vanish)//如果窗体的状态不是隐藏  
            {
                this.alerttimer.Stop();//停止计时器  
                this.InfoState = InformStyle.Vanish;//设置窗体的操作状态为隐藏  
                this.Hide();
                //base.Hide();//隐藏当前窗体  
                // this.Close();  
            }  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gkfqd.ui.gk07.gk071 frmgk071 = new gkfqd.ui.gk07.gk071(label2.Text);
            frmgk071.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
            frmgk071.StartPosition = FormStartPosition.CenterScreen;//窗体居中
            frmgk071.ShowDialog();
        }

        
    }
}
