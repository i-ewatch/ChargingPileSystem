using ChargingPileSystem.EF_Module;
using ChargingPileSystem.EF_Modules;
using ChargingPileSystem.Methods;
using ChargingPileSystem.Protocols;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChargingPileSystem.Views
{
    public class Field4UserControl : XtraUserControl
    {
        /// <summary>
        /// 亂數產生
        /// </summary>
        public Random rnd { get; set; } = new Random();
        /// <summary>
        /// 主畫面物件
        /// </summary>
        public Form1 Form1 { get; set; }
        /// <summary>
        /// 資料庫方法
        /// </summary>
        public SqlMethod SqlMethod { get; set; }
        /// <summary>
        /// 總電表設備資訊
        /// </summary>
        public List<ElectricConfig> ElectricConfigs { get; set; } = new List<ElectricConfig>();
        /// <summary>
        /// 各別電表設備資訊
        /// </summary>
        public ElectricConfig ElectricConfig { get; set; }
        /// <summary>
        /// 閘道器設備資訊
        /// </summary>
        public List<GatewayConfig> GatewayConfigs { get; set; } = new List<GatewayConfig>();
        /// <summary>
        /// 總通訊數值
        /// </summary>
        public List<AbsProtocol> AbsProtocols { get; set; }
        /// <summary>
        /// 顯示變更
        /// </summary>
        public virtual void TextChange() { }
        #region 字形縮小
        /// <summary>
        /// 數值顯示字型
        /// </summary>
        public Font ValueFont = new Font("微軟正黑體", 16);
        /// <summary>
        /// 字型縮小
        /// </summary>
        /// <param name="DisplayString"></param>
        /// <param name="InputUI"></param>
        /// <returns></returns>
        public Font CalculateFontSize(string DisplayString, Control InputUI)
        {
            Font TextFont = ValueFont;
            Size ControlSize = InputUI.Size;
            Size sz = TextRenderer.MeasureText(DisplayString, TextFont);
            while (sz.Width > ControlSize.Width && TextFont.Size >= 8)
            {
                float emSize = TextFont.Size;
                TextFont = new Font(TextFont.FontFamily, emSize - 0.1F);
                sz = TextRenderer.MeasureText(DisplayString, TextFont);
            }
            return TextFont;
        }
        #endregion
    }
}
