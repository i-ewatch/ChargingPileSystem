using ChargingPileSystem.Configuration;
using ChargingPileSystem.EF_Module;
using ChargingPileSystem.EF_Modules;
using ChargingPileSystem.Enums;
using ChargingPileSystem.Methods;
using ChargingPileSystem.Protocols;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace ChargingPileSystem.Components
{
    public class Field4Component : Component
    {
        /// <summary>
        /// 銀行與系統密碼
        /// </summary>
        public BankAccountSetting BankAccountSetting { get; set; }
        /// <summary>
        /// 通訊數值物件
        /// </summary>
        public List<AbsProtocol> AbsProtocols { get; set; } = new List<AbsProtocol>();
        /// <summary>
        /// 通訊資訊
        /// </summary>
        public GatewayConfig Gatewayconfig { get; set; }
        /// <summary>
        /// 電表資訊
        /// </summary>
        public List<ElectricConfig> ElectricConfigs { get; set; }
        /// <summary>
        /// 通訊執行緒
        /// </summary>
        public Thread ReadThread { get; set; }
        /// <summary>
        /// 最後讀取時間
        /// </summary>
        public DateTime ReadTime { get; set; }
        
        /// <summary>
        /// 電表類型
        /// </summary>
        public ElectricEnumType ElectricEnumType { get; set; }
        /// <summary>
        /// 感測器類型
        /// </summary>
        public SenserEnumType SenserEnumType { get; set; }
        /// <summary>
        /// 相位類型
        /// </summary>
        public PhaseEnumType PhaseEnumType { get; set; }
        /// <summary>
        /// 資料庫方法
        /// </summary>
        public SqlMethod SqlMethod { get; set; }
        /// <summary>
        /// 錯誤字串
        /// </summary>
        public string ErrorString { get; set; }
        /// <summary>
        /// 斷線旗標
        /// True = 斷線
        /// False = 連線
        /// </summary>
        public bool ComponentFlag { get; set; } = false;

        #region 啟動初始設定
        public Field4Component()
        {
            OnMyWorkStateChanged += new MyWorkStateChanged(AfterMyWorkStateChanged);
        }
        protected void WhenMyWorkStateChange()
        {
            OnMyWorkStateChanged?.Invoke(this, null);
        }
        public delegate void MyWorkStateChanged(object sender, EventArgs e);
        public event MyWorkStateChanged OnMyWorkStateChanged;
        /// <summary>
        /// 系統工作路徑
        /// </summary>
        protected readonly string WorkPath = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 通訊功能啟動判斷旗標
        /// </summary>
        protected bool myWorkState;
        /// <summary>
        /// 通訊功能啟動旗標
        /// </summary>
        public bool MyWorkState
        {
            get { return myWorkState; }
            set
            {
                if (value != myWorkState)
                {
                    myWorkState = value;
                    WhenMyWorkStateChange();
                }
            }
        }
        /// <summary>
        /// 執行續工作狀態改變觸發事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void AfterMyWorkStateChanged(object sender, EventArgs e) { }
        #endregion
    }
}
