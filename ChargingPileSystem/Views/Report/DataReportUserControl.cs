using ChargingPileSystem.EF_Modules;
using ChargingPileSystem.Enums;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChargingPileSystem.Views.Report
{
    public partial class DataReportUserControl : Field4UserControl
    {
        /// <summary>
        /// 報表查詢類別
        /// </summary>
        private ReportSearchEnumType ReportSearchEnumType { get; set; }
        /// <summary>
        /// 儲存檔案物件
        /// </summary>
        private SaveFileDialog SaveFile;

        public DataReportUserControl(List<ElectricConfig> electricConfigs)
        {
            InitializeComponent();
            StartdateEdit.Properties.ContextImageOptions.Image = imageCollection1.Images["calendar"];
            EnddateEdit.Properties.ContextImageOptions.Image = imageCollection1.Images["calendar"];
            DeviceItemRefresh(electricConfigs);
        }
        /// <summary>
        /// 更新設備列表名稱
        /// </summary>
        /// <param name="electricConfigs"></param>
        public void DeviceItemRefresh(List<ElectricConfig> electricConfigs)
        {
            ElectricConfigs = electricConfigs;
            if (SearchDevicecomboBoxEdit.Properties.Items.Count > 0)
            {
                SearchDevicecomboBoxEdit.Properties.Items.Clear();
            }
            foreach (var item in electricConfigs)
            {
                SearchDevicecomboBoxEdit.Properties.Items.Add(item.DeviceName);
            }
        }

        private void SearchsimpleButton_Click(object sender, EventArgs e)
        {
            if (gridView1.Columns.Count > 0)
            {
                gridView1.Columns.Clear();
            }
            if (chartControl.Series.Count > 0)
            {
                chartControl.Series.Clear();
            }
            ReportSearchEnumType = (ReportSearchEnumType)SearchTypecomboBoxEdit.SelectedIndex;
            switch (ReportSearchEnumType)
            {
                case ReportSearchEnumType.kW:
                    {
                        var ElectricConfig = ElectricConfigs.Where(g => g.DeviceName == SearchDevicecomboBoxEdit.Text).Single();
                        var data = SqlMethod.Search_ThreePhaseElectricMeter_Log(Convert.ToDateTime(StartdateEdit.EditValue).ToString("yyyyMMdd"), Convert.ToDateTime(EnddateEdit.EditValue).ToString("yyyyMMdd"), ElectricConfig.GatewayIndex, ElectricConfig.DeviceIndex);
                        if (data != null)
                        {
                            gridControl.DataSource = data;
                            chartControl.DataSource = data;
                            #region 報表
                            for (int i = 0; i < gridView1.Columns.Count; i++)
                            {
                                if (gridView1.Columns[i].FieldName == "ttimen")
                                {
                                    gridView1.Columns[i].Caption = "時間";
                                    gridView1.Columns[i].DisplayFormat.FormatString = "yyyy/MM/dd HH:mm";
                                    gridView1.Columns[i].BestFit();
                                }
                                else if (gridView1.Columns[i].FieldName == "kw")
                                {
                                    gridView1.Columns[i].Caption = "即時用電";
                                    gridView1.Columns[i].BestFit();
                                }
                                else
                                {
                                    gridView1.Columns[i].Visible = false;
                                }
                            }
                            #endregion
                            #region 圖表
                            Series series = new Series($"{SearchDevicecomboBoxEdit.Text}", ViewType.Line);
                            series.ArgumentDataMember = "ttimen";
                            series.ValueDataMembers.AddRange(new string[] { "kw" });
                            series.CrosshairLabelPattern = "{S} \r時間 : {A:yyyy-MM-dd HH:mm}\r{V:0.##} kW";
                            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                            chartControl.Series.Add(series);
                            if (chartControl.DataSource != null && chartControl.Series.Count > 0)
                            {
                                XYDiagram diagram = (XYDiagram)chartControl.Diagram;
                                if (diagram != null)
                                {
                                    diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Minute; // 顯示設定
                                    diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Minute; // 刻度設定 
                                    diagram.AxisX.Label.Angle = 90;
                                    diagram.AxisX.Label.TextPattern = "{A:yyyy-MM-dd HH:mm}";//X軸顯示
                                    diagram.AxisX.WholeRange.SideMarginsValue = 0;//不需要邊寬
                                }
                                chartControl.CrosshairOptions.ShowArgumentLabels = false;//是否顯示Y軸垂直線
                                chartControl.CrosshairOptions.ShowArgumentLine = false;//是否顯示Y軸垂直線
                                //chartControl.CrosshairOptions.ShowCrosshairLabels = false;//是否顯示Y軸垂直線
                            }
                            #endregion
                        }
                    }
                    break;
                case ReportSearchEnumType.kWh:
                    {
                        var ElectricConfig = ElectricConfigs.Where(g => g.DeviceName == SearchDevicecomboBoxEdit.Text).Single();
                        var data = SqlMethod.Search_ElectricTotalPrice(Convert.ToDateTime(StartdateEdit.EditValue).ToString("yyyyMMdd"), Convert.ToDateTime(EnddateEdit.EditValue).ToString("yyyyMMdd"), ElectricConfig.GatewayIndex, ElectricConfig.DeviceIndex);
                        if (data != null)
                        {
                            gridControl.DataSource = data;
                            chartControl.DataSource = data;
                            #region 報表
                            for (int i = 0; i < gridView1.Columns.Count; i++)
                            {
                                if (gridView1.Columns[i].FieldName == "ttimen")
                                {
                                    gridView1.Columns[i].Caption = "時間";
                                    gridView1.Columns[i].DisplayFormat.FormatString = "yyyy/MM/dd";
                                    gridView1.Columns[i].BestFit();
                                }
                                else if (gridView1.Columns[i].FieldName == "KwhTotal")
                                {
                                    gridView1.Columns[i].Caption = "累積用電";
                                    gridView1.Columns[i].BestFit();
                                }
                                else
                                {
                                    gridView1.Columns[i].Visible = false;
                                }
                            }
                            #endregion
                            #region 圖表
                            Series series = new Series($"{SearchDevicecomboBoxEdit.Text}", ViewType.Bar);
                            series.ArgumentDataMember = "ttimen";
                            series.ValueDataMembers.AddRange(new string[] { "KwhTotal" });
                            series.CrosshairLabelPattern = "{S} \r時間 : {A:yyyy-MM-dd HH:mm}\r{V:0.##} kWh";
                            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                            chartControl.Series.Add(series);
                            if (chartControl.DataSource != null && chartControl.Series.Count > 0)
                            {
                                XYDiagram diagram = (XYDiagram)chartControl.Diagram;
                                if (diagram != null)
                                {
                                    diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Day; // 顯示設定
                                    diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day; // 刻度設定 
                                    diagram.AxisX.Label.Angle = 90;
                                    diagram.AxisX.Label.TextPattern = "{A:yyyy-MM-dd}";//X軸顯示
                                    diagram.AxisX.WholeRange.SideMarginsValue = 0;//不需要邊寬
                                }
                                chartControl.CrosshairOptions.ShowArgumentLabels = false;//是否顯示Y軸垂直線
                                chartControl.CrosshairOptions.ShowArgumentLine = false;//是否顯示Y軸垂直線
                                //chartControl.CrosshairOptions.ShowCrosshairLabels = false;//是否顯示Y軸垂直線
                            }
                            #endregion
                        }
                    }
                    break;
            }
        }

        private void ExportsimpleButton_Click(object sender, EventArgs e)
        {
            if (gridView1.DataSource != null)
            {
                SaveFile = new SaveFileDialog() { Filter = "*.Xlsx| *.xlsx" };
                if (SaveFile.ShowDialog() == DialogResult.OK)
                {
                    switch (ReportSearchEnumType)
                    {
                        case ReportSearchEnumType.kW:
                            {
                                gridControl.ExportToXlsx($"{SaveFile.FileName}");
                            }
                            break;
                        case ReportSearchEnumType.kWh:
                            {
                                gridControl.ExportToXlsx($"{SaveFile.FileName}");
                            }
                            break;
                    }
                }
            }
        }
    }
}
