using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingPileSystem.Controllers
{
    public class SystemControl
    {
        /// <summary>
        /// 繼承主視窗
        /// </summary>
        public Form1 Form1 { get; set; }
        #region 確認程式是否重複啟動
        /// <summary>
        /// 確認程式是否重複啟動
        /// </summary>
        public bool CHeckedSoftware()
        {
            string ProcessName = Process.GetCurrentProcess().ProcessName;
            Process[] p = Process.GetProcessesByName(ProcessName);
            if (p.Length > 1)
            {
                FlyoutAction action = new FlyoutAction();
                action.Caption = "程式重複啟動";
                action.Description = "程式已開啟";
                action.Commands.Add(FlyoutCommand.OK);
                FlyoutDialog.Show(Form1, action);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
