using DevExpress.XtraEditors;
using ChargingPileSystem.EF_Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChargingPileSystem.Enums;
using ChargingPileSystem.Protocols.ElectricMeter;
using DevExpress.XtraCharts;
using ChargingPileSystem.Modules;
using ChargingPileSystem.Methods;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using ChargingPileSystem.Views.Setting;
using ChargingPileSystem.EF_Module;

namespace ChargingPileSystem.Views.ChargingPile
{
    public partial class MasterMeterUserControl : Field4UserControl
    {
        /// <summary>
        /// 電表類型
        /// </summary>
        private ElectricEnumType ElectricEnumType { get; set; }
        /// <summary>
        /// 相位類型
        /// </summary>
        private PhaseEnumType PhaseEnumType { get; set; }
        /// <summary>
        /// 總電表圖表
        /// </summary>
        private Series TotalMeterSeries { get; set; }
        /// <summary>
        /// 圖表時間
        /// </summary>
        private DateTime LineTime { get; set; }
        public MasterMeterUserControl(ElectricConfig electricConfig, SqlMethod sqlMethod,Form1 form1, List<GatewayConfig> gatewayConfigs)
        {
            InitializeComponent();
            GatewayConfigs = gatewayConfigs;
            Form1 = form1;
            groupControl.CustomHeaderButtons[0].Properties.Image = imageCollection1.Images["aligncenter"];
            ValueFont = DaykWhlabelControl.Font;//本日累積用電、本月累積用電 字型大小一樣
            if (electricConfig != null)
            {
                groupControl.Text = $"總表 - {electricConfig.DeviceName}";//電表名稱
                var NowkWh = sqlMethod.Search_ElectricTotalPrice(0, electricConfig.GatewayIndex, electricConfig.DeviceIndex);//本日累積用電度
                var MonthkWh = sqlMethod.Search_ElectricTotalPrice(2, electricConfig.GatewayIndex, electricConfig.DeviceIndex);//本月累積用電度
                if (NowkWh.Count > 0)
                {
                    DaykWhlabelControl.Appearance.Font = CalculateFontSize(NowkWh[0].KwhTotal.ToString("F1") + " kWh", DaykWhlabelControl);
                    DaykWhlabelControl.Text = NowkWh[0].KwhTotal.ToString("F1") + " kWh";
                }
                if (MonthkWh.Count > 0)
                {
                    MonthkWhlabelControl.Appearance.Font = CalculateFontSize(MonthkWh[0].KwhTotal.ToString("F1") + " kWh", MonthkWhlabelControl);
                    MonthkWhlabelControl.Text = MonthkWh[0].KwhTotal.ToString("F1") + " kWh";
                }
            }
            #region 曲線圖
            LinechartControl.Legend.Direction = LegendDirection.TopToBottom;//線條說明的排序
            LinechartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;//線條說明顯示
            LinechartControl.CrosshairOptions.CrosshairLabelMode = CrosshairLabelMode.ShowCommonForAllSeries; //顯示全部線條內容
            LinechartControl.CrosshairOptions.LinesMode = CrosshairLinesMode.Auto;//自動獲取點上面的數值
            LinechartControl.CrosshairOptions.GroupHeaderTextOptions.Font = new System.Drawing.Font("微軟正黑體", 12);
            LinechartControl.CrosshairOptions.ShowArgumentLabels = true;//是否顯示Y軸垂直線
            LinechartControl.SideBySideEqualBarWidth = false;//線條是否需要相等寬度
            //List<LineModule> line = Create_Line();
            //if (sqlMethod != null)
            //{
                var SQLline = sqlMethod.Search_ThreePhaseElectricMeter_Log(DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"), electricConfig.GatewayIndex, electricConfig.DeviceIndex);
            //    for (int i = 0; i < SQLline.Count; i++)
            //    {
            //        foreach (var item in line)
            //        {
            //            if (item.Argument == SQLline[i].ttimen)
            //            {
            //                item.Value = Convert.ToDouble(SQLline[i].kw);
            //                break;
            //            }
            //        }
            //    }
            //}
            TotalMeterSeries = new Series("總表累積量", ViewType.Line);
            TotalMeterSeries.DataSource = SQLline;
            TotalMeterSeries.ArgumentDataMember = "ttimen";
            TotalMeterSeries.ValueDataMembers.AddRange(new string[] { "kw" });
            TotalMeterSeries.CrosshairLabelPattern = "{S} \r時間 : {A:yyyy-MM-dd HH:mm}\r{V:0.##} kW";
            //TotalMeterSeries.LabelsVisibility = DefaultBoolean.False;
            if (LinechartControl != null)
            {
                if (LinechartControl.Series.Count == 0)
                {
                    LinechartControl.Series.Add(TotalMeterSeries);
                }
            }
            if (TotalMeterSeries.DataSource != null && LinechartControl.Series.Count > 0)
            {
                XYDiagram diagram = (XYDiagram)LinechartControl.Diagram;
                if (diagram != null)
                {
                    diagram.EnableAxisXZooming = true;//放大縮小
                    diagram.EnableAxisXScrolling = true;//拖曳
                    diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Minute; // 顯示設定
                    diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Minute; // 刻度設定 
                    diagram.AxisX.Label.Angle = 90;
                    diagram.AxisX.Label.TextPattern = "{A:HH:mm}";//X軸顯示
                    diagram.AxisX.WholeRange.SideMarginsValue = 0;//不需要邊寬
                    diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;
                }
                LinechartControl.CrosshairOptions.ShowArgumentLabels = false;//是否顯示Y軸垂直線
                LinechartControl.CrosshairOptions.ShowArgumentLine = false;//是否顯示Y軸垂直線
                //LinechartControl.CrosshairOptions.ShowCrosshairLabels = false;//是否顯示Y軸垂直線
            }
            LineTime = DateTime.Now;
            groupControl.CustomHeaderButtons[0].Properties.Enabled = false;
            #endregion
        }
        public override void TextChange()
        {
            if (ElectricConfig != null)
            {
                groupControl.CustomHeaderButtons[0].Properties.Enabled = Form1.AdministraturFlag;//更改名稱按鈕
                ElectricEnumType = (ElectricEnumType)ElectricConfig.ElectricEnumType;
                PhaseEnumType = (PhaseEnumType)ElectricConfig.PhaseEnumType;
                #region 電表名稱
                if (groupControl.Text != $"總表 - {ElectricConfig.DeviceName}")
                {
                    groupControl.Text = $"總表 - {ElectricConfig.DeviceName}";
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
                                RSvlabelControl.Text = protocol.RSv.ToString("F1");
                                STvlabelControl.Text = protocol.STv.ToString("F1");
                                TRvlabelControl.Text = protocol.TRv.ToString("F1");
                                RalabelControl.Text = protocol.RA.ToString("F1");
                                SalabelControl.Text = protocol.SA.ToString("F1");
                                TalabelControl.Text = protocol.TA.ToString("F1");
                                PFlabelControl.Text = protocol.PF.ToString("F2");
                                kWlabelControl.Text = protocol.kW.ToString("F2");
                                kWhlabelControl.Text = protocol.kWh.ToString("F2");
                            }
                            break;
                        case ElectricEnumType.HC660:
                            {
                                HC6600Protocol protocol = (HC6600Protocol)Data[0];
                                RSvlabelControl.Text = protocol.RSv.ToString("F1");
                                STvlabelControl.Text = protocol.STv.ToString("F1");
                                TRvlabelControl.Text = protocol.TRv.ToString("F1");
                                RalabelControl.Text = protocol.RA.ToString("F1");
                                SalabelControl.Text = protocol.SA.ToString("F1");
                                TalabelControl.Text = protocol.TA.ToString("F1");
                                PFlabelControl.Text = protocol.PF.ToString("F2");
                                kWlabelControl.Text = protocol.kW.ToString("F2");
                                kWhlabelControl.Text = protocol.kWh.ToString("F2");
                            }
                            break;
                        case ElectricEnumType.CPM6:
                            {
                                CPM6Protocol protocol = (CPM6Protocol)Data[0];
                                RSvlabelControl.Text = protocol.RSv.ToString("F1");
                                STvlabelControl.Text = protocol.STv.ToString("F1");
                                TRvlabelControl.Text = protocol.TRv.ToString("F1");
                                RalabelControl.Text = protocol.RA.ToString("F1");
                                SalabelControl.Text = protocol.SA.ToString("F1");
                                TalabelControl.Text = protocol.TA.ToString("F1");
                                PFlabelControl.Text = protocol.PF.ToString("F2");
                                kWlabelControl.Text = protocol.kW.ToString("F2");
                                kWhlabelControl.Text = protocol.kWh.ToString("F2");
                            }
                            break;
                        case ElectricEnumType.PA60:
                            {
                                PA60Protocol protocol = (PA60Protocol)Data[0];
                                RSvlabelControl.Text = protocol.RSv[ElectricConfig.LoopEnumType].ToString("F1");
                                STvlabelControl.Text = protocol.STv[ElectricConfig.LoopEnumType].ToString("F1");
                                TRvlabelControl.Text = protocol.TRv[ElectricConfig.LoopEnumType].ToString("F1");
                                RalabelControl.Text = protocol.RA[ElectricConfig.LoopEnumType].ToString("F1");
                                SalabelControl.Text = protocol.SA[ElectricConfig.LoopEnumType].ToString("F1");
                                TalabelControl.Text = protocol.TA[ElectricConfig.LoopEnumType].ToString("F1");
                                PFlabelControl.Text = protocol.PF[ElectricConfig.LoopEnumType].ToString("F2");
                                kWlabelControl.Text = protocol.kW[ElectricConfig.LoopEnumType].ToString("F2");
                                kWhlabelControl.Text = protocol.kWh[ElectricConfig.LoopEnumType].ToString("F2");
                            }
                            break;
                        case ElectricEnumType.ABBM2M:
                            {
                                ABBM2MProtocol protocol = (ABBM2MProtocol)Data[0];
                                RSvlabelControl.Text = protocol.RSv.ToString("F1");
                                STvlabelControl.Text = protocol.STv.ToString("F1");
                                TRvlabelControl.Text = protocol.TRv.ToString("F1");
                                RalabelControl.Text = protocol.RA.ToString("F1");
                                SalabelControl.Text = protocol.SA.ToString("F1");
                                TalabelControl.Text = protocol.TA.ToString("F1");
                                PFlabelControl.Text = protocol.PF.ToString("F2");
                                kWlabelControl.Text = protocol.kW.ToString("F2");
                                kWhlabelControl.Text = protocol.kWh.ToString("F2");
                            }
                            break;
                    }
                }
                #endregion

                var NowkWh = SqlMethod.Search_ElectricTotalPrice(0, ElectricConfig.GatewayIndex, ElectricConfig.DeviceIndex);//本日累積用電度
                var MonthkWh = SqlMethod.Search_ElectricTotalPrice(2, ElectricConfig.GatewayIndex, ElectricConfig.DeviceIndex);//本月累積用電度
                if (NowkWh.Count > 0)
                {
                    DaykWhlabelControl.Appearance.Font = CalculateFontSize(NowkWh[0].KwhTotal.ToString("F1") + " kWh", DaykWhlabelControl);
                    DaykWhlabelControl.Text = NowkWh[0].KwhTotal.ToString("F1") + " kWh";
                }
                if (MonthkWh.Count > 0)
                {
                    MonthkWhlabelControl.Appearance.Font = CalculateFontSize(MonthkWh[0].KwhTotal.ToString("F1") + " kWh", MonthkWhlabelControl);
                    MonthkWhlabelControl.Text = MonthkWh[0].KwhTotal.ToString("F1") + " kWh";
                }
                #region 曲線圖
                TimeSpan timeSpan = DateTime.Now.Subtract(LineTime);
                if (timeSpan.TotalSeconds > 20)
                {
                //    List<LineModule> line = Create_Line();
                //    if (SqlMethod != null)
                //    {
                       var SQLline = SqlMethod.Search_ThreePhaseElectricMeter_Log(DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"), ElectricConfig.GatewayIndex, ElectricConfig.DeviceIndex);
                //        for (int i = 0; i < SQLline.Count; i++)
                //        {
                //            foreach (var item in line)
                //            {
                //                if (item.Argument == SQLline[i].ttimen)
                //                {
                //                    item.Value = Convert.ToDouble(SQLline[i].kw);
                //                    break;
                //                }
                //            }
                //        }
                //    }
                    TotalMeterSeries.DataSource = SQLline;
                    LinechartControl.Refresh();
                    LineTime = DateTime.Now;
                }
                #endregion
            }
        }
        #region 曲線圖初始
        /// <summary>
        /// 曲線圖初始
        /// </summary>
        /// <returns></returns>
        public List<LineModule> Create_Line()
        {
            List<LineModule> line = new List<LineModule>();
            for (int i = 0; i < 1440; i++)
            {
                if ((i / 60).ToString().Length > 1)
                {
                    if ((i % 60).ToString().Length > 1)
                    {
                        LineModule lineModule = new LineModule()
                        {
                            Argument = Convert.ToDateTime($"{DateTime.Now:yyyy-MM-dd} {i / 60}:{i % 60}:00"),
                            Value = 0
                        };
                        line.Add(lineModule);
                    }
                    else
                    {
                        LineModule lineModule = new LineModule()
                        {
                            Argument = Convert.ToDateTime($"{DateTime.Now:yyyy-MM-dd} {i / 60}:0{i % 60}:00"),
                            Value = 0
                        };
                        line.Add(lineModule);
                    }
                }
                else
                {
                    if ((i % 60).ToString().Length > 1)
                    {
                        LineModule lineModule = new LineModule()
                        {
                            Argument = Convert.ToDateTime($"{DateTime.Now:yyyy-MM-dd} 0{i / 60}:{i % 60}:00"),
                            Value = 0
                        };
                        line.Add(lineModule);
                    }
                    else
                    {
                        LineModule lineModule = new LineModule()
                        {
                            Argument = Convert.ToDateTime($"{DateTime.Now:yyyy-MM-dd} 0{i / 60}:0{i % 60}:00"),
                            Value = 0
                        };
                        line.Add(lineModule);
                    }
                }

            }
            return line;
        }
        #endregion

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
