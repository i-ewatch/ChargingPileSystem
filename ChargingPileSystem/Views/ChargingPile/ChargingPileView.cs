using DevExpress.XtraEditors;
using ChargingPileSystem.EF_Modules;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChargingPileSystem.Methods;

namespace ChargingPileSystem.Views.ChargingPile
{
    public partial class ChargingPileView : Field4UserControl
    {
        /// <summary>
        /// 總電表物件
        /// </summary>
        private List<Field4UserControl> MasterMeters { get; set; } = new List<Field4UserControl>();
        /// <summary>
        /// 總電表數量
        /// </summary>
        private int MasterMeterIndex = 0;
        /// <summary>
        /// 分電表物件
        /// </summary>
        private ChargingPileUserControl ChargingPileUserControl { get; set; }
        /// <summary>
        /// 切換畫面時間
        /// </summary>
        private DateTime ChangeViewTime { get; set; }
        /// <summary>
        /// 頁數
        /// </summary>
        private int ViewIndex { get; set; } = 0;
        public ChargingPileView(List<ElectricConfig> electricConfigs, SqlMethod sqlMethod,Form1 form1)
        {
            InitializeComponent();
            Form1 = form1;
            if (electricConfigs != null)
            {
                foreach (var item in electricConfigs)
                {
                    if (item.TotalMeterFlag)//總電表
                    {
                        MasterMeterUserControl masterMeter = new MasterMeterUserControl(item, sqlMethod, form1) { SqlMethod = sqlMethod };
                        MasterMeters.Add(masterMeter);
                        MasterMetenavigationFrame.AddPage(masterMeter);
                    }
                }
                ChargingPileUserControl = new ChargingPileUserControl(electricConfigs, sqlMethod, form1) { SqlMethod = sqlMethod };
                ChargingPilepanelControl.Controls.Add(ChargingPileUserControl);
                ChangeViewTime = DateTime.Now;
            }
            else
            {
                Log.Error("無電表資訊，請檢查資料");
            }
        }
        public override void TextChange()
        {
            #region 總電表
            TimeSpan timeSpan = DateTime.Now.Subtract(ChangeViewTime);
            if (timeSpan.TotalSeconds > 10 && Form1.LockFlag)
            {
                if (MasterMeters.Count > ViewIndex)
                {
                    ViewIndex++;
                    MasterMetenavigationFrame.SelectedPageIndex = ViewIndex;
                    ChangeViewTime = DateTime.Now;
                }
                else
                {
                    ViewIndex = 0;
                    MasterMetenavigationFrame.SelectedPageIndex = ViewIndex;
                    ChangeViewTime = DateTime.Now;
                }
            }
            else if(timeSpan.TotalSeconds > 10 && !Form1.LockFlag)
            {
                ChangeViewTime = DateTime.Now;
            }
            if (ElectricConfigs != null)
            {
                MasterMeterIndex = 0;
                foreach (var item in ElectricConfigs)
                {
                    if (item.TotalMeterFlag)
                    {
                        MasterMeters[MasterMeterIndex].ElectricConfig = item;
                        MasterMeterIndex++;
                    }
                }
                MasterMeters[MasterMetenavigationFrame.SelectedPageIndex].AbsProtocols = AbsProtocols;
                MasterMeters[MasterMetenavigationFrame.SelectedPageIndex].TextChange();
                #endregion
                #region 分電表
                ChargingPileUserControl.AbsProtocols = AbsProtocols;
                ChargingPileUserControl.ElectricConfigs = ElectricConfigs;
                ChargingPileUserControl.TextChange();
                #endregion
            }
            else
            {
                Log.Error("無電表資訊，請檢查資料");
            }
        }
    }
}
