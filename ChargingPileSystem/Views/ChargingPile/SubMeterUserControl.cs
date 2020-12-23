using ChargingPileSystem.EF_Module;
using ChargingPileSystem.EF_Modules;
using ChargingPileSystem.Enums;
using ChargingPileSystem.Methods;
using ChargingPileSystem.Protocols.ElectricMeter;
using ChargingPileSystem.Views.Setting;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChargingPileSystem.Views.ChargingPile
{
    public partial class SubMeterUserControl : Field4UserControl
    {
        /// <summary>
        /// 電表類型
        /// </summary>
        private ElectricEnumType ElectricEnumType { get; set; }
        /// <summary>
        /// 相位類型
        /// </summary>
        private PhaseEnumType PhaseEnumType { get; set; }

        public SubMeterUserControl(ElectricConfig electricConfig, SqlMethod sqlMethod, Form1 form1, List<GatewayConfig> gatewayConfigs)
        {
            InitializeComponent();
            GatewayConfigs = gatewayConfigs;
            Form1 = form1;
            groupControl.CustomHeaderButtons[0].Properties.Image = imageCollection1.Images["aligncenter"];
            ValueFont = NowkWlabelControl.Font;//即時用電、本日累積用電、昨日累積用電、總累積用電 字型大小一樣
            if (electricConfig != null)
            {
                groupControl.Text = $"{electricConfig.DeviceName}";//電表名稱
                var NowkWh = sqlMethod.Search_ElectricTotalPrice(0, electricConfig.GatewayIndex, electricConfig.DeviceIndex);//本日累積用電度
                var AfterkWh = sqlMethod.Search_ElectricTotalPrice(1, electricConfig.GatewayIndex, electricConfig.DeviceIndex);//昨日累積用電度
                var TotalkWh = sqlMethod.Search_ElectricTotalPrice(3, electricConfig.GatewayIndex, electricConfig.DeviceIndex);//總累積用電度
                if (NowkWh.Count > 0)
                {
                    NowkWhlabelControl.Text = NowkWh[0].KwhTotal.ToString("F1");
                }
                if (AfterkWh.Count > 0)
                {
                    AfterkWhlabelControl.Text = AfterkWh[0].KwhTotal.ToString("F1");
                }
                if (TotalkWh.Count > 0)
                {
                    TotalkWhlabelControl.Appearance.Font = CalculateFontSize(TotalkWh[0].KwhTotal.ToString("F1"), TotalkWhlabelControl);
                    TotalkWhlabelControl.Text = TotalkWh[0].KwhTotal.ToString("F1");
                }
            }
            groupControl.CustomHeaderButtons[0].Properties.Enabled = false;
        }
        public override void TextChange()
        {
            if (ElectricConfig != null)
            {
                groupControl.CustomHeaderButtons[0].Properties.Enabled = Form1.AdministraturFlag;//更改名稱按鈕
                #region 電表名稱
                if (groupControl.Text != $"{ElectricConfig.DeviceName}")
                {
                    groupControl.Text = $"{ElectricConfig.DeviceName}";
                }
                #endregion
                #region 電表數值
                var Data = AbsProtocols.Where(g => g.GatewayIndex == ElectricConfig.GatewayIndex & g.DeviceIndex == ElectricConfig.DeviceIndex).ToList();
                if (Data.Count > 0)
                {
                    switch (ElectricEnumType)
                    {
                        case ElectricEnumType.PA310:
                            {
                                PA310Protocol protocol = (PA310Protocol)Data[0];
                                NowkWlabelControl.Text = protocol.kW.ToString("F2");
                            }
                            break;
                        case ElectricEnumType.HC660:
                            {
                                {
                                    HC6600Protocol protocol = (HC6600Protocol)Data[0];
                                    NowkWlabelControl.Text = protocol.kW.ToString("F2");
                                }
                            }
                            break;
                        case ElectricEnumType.CPM6:
                            {
                                {
                                    CPM6Protocol protocol = (CPM6Protocol)Data[0];
                                    NowkWlabelControl.Text = protocol.kW.ToString("F2");
                                }
                            }
                            break;
                        case ElectricEnumType.PA60:
                            {
                                {
                                    PA60Protocol protocol = (PA60Protocol)Data[0];
                                    NowkWlabelControl.Text = protocol.kW[ElectricConfig.LoopEnumType].ToString("F2");
                                }
                            }
                            break;
                        case ElectricEnumType.ABBM2M:
                            {
                                {
                                    ABBM2MProtocol protocol = (ABBM2MProtocol)Data[0];
                                    NowkWlabelControl.Text = protocol.kW.ToString("F2");
                                }
                            }
                            break;
                    }
                }
                #endregion
            }
            var NowkWh = SqlMethod.Search_ElectricTotalPrice(0, ElectricConfig.GatewayIndex, ElectricConfig.DeviceIndex);//本日累積用電度
            var AfterkWh = SqlMethod.Search_ElectricTotalPrice(1, ElectricConfig.GatewayIndex, ElectricConfig.DeviceIndex);//昨日累積用電度
            var TotalkWh = SqlMethod.Search_ElectricTotalPrice(3, ElectricConfig.GatewayIndex, ElectricConfig.DeviceIndex);//總累積用電度
            if (NowkWh.Count > 0)
            {
                NowkWhlabelControl.Text = NowkWh[0].KwhTotal.ToString("F1");
            }
            if (AfterkWh.Count > 0)
            {
                AfterkWhlabelControl.Text = AfterkWh[0].KwhTotal.ToString("F1");
            }
            if (TotalkWh.Count > 0)
            {
                TotalkWhlabelControl.Appearance.Font = CalculateFontSize(TotalkWh[0].KwhTotal.ToString("F1"), TotalkWhlabelControl);
                TotalkWhlabelControl.Text = TotalkWh[0].KwhTotal.ToString("F1");
            }
        }

        private void groupControl_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            Form1.AfterLockFlag = Form1.LockFlag;
            Form1.LockFlag = false;
            if (!Form1.FlyoutFlag)
            {
                Form1.FlyoutFlag = true;
                PanelControl panelControl = new PanelControl()
                {
                    Size = new Size(359, 210)
                };
                Form1.flyout = new FlyoutDialog(Form1, panelControl);
                Form1.flyout.Properties.Style = FlyoutStyle.Popup;
                var GatewayConfig = GatewayConfigs.Where(g => g.GatewayIndex == ElectricConfig.GatewayIndex).Single();
                DeviceNameSettingUserControl systemSettingUserControl = new DeviceNameSettingUserControl(GatewayConfig.GatewayName, ElectricConfig.DeviceID,ElectricConfig.DeviceName, ElectricConfig.GatewayIndex, ElectricConfig.DeviceIndex) { Form1 = Form1, SqlMethod = SqlMethod };
                systemSettingUserControl.Parent = panelControl;
                Form1.flyout.Show();
            }
            else
            {
                Form1.FlyoutFlag = false;
                Form1.flyout.Close();
            }
        }
    }
}
