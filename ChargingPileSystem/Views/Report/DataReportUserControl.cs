using ChargingPileSystem.EF_Modules;
using ChargingPileSystem.Enums;
using ChargingPileSystem.Modules;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                        if (Form1.ConnectionFlag)
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
                                        diagram.EnableAxisXZooming = true;//放大縮小
                                        diagram.EnableAxisXScrolling = true;//拖曳
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
                        else
                        {
                            TimeSpan timeSpan = Convert.ToDateTime(EnddateEdit.EditValue).Subtract(Convert.ToDateTime(StartdateEdit.EditValue));
                            List<LineModule> Data = new List<LineModule>();
                            for (int day = 0; day < timeSpan.TotalDays + 1; day++)
                            {
                                var data = Create_Line(Convert.ToDateTime(StartdateEdit.EditValue).AddDays(day));
                                Data.AddRange(data);
                            }
                            gridControl.DataSource = Data;
                            chartControl.DataSource = Data;
                            #region 報表
                            for (int i = 0; i < gridView1.Columns.Count; i++)
                            {
                                if (gridView1.Columns[i].FieldName == "Argument")
                                {
                                    gridView1.Columns[i].Caption = "時間";
                                    gridView1.Columns[i].DisplayFormat.FormatString = "yyyy/MM/dd HH:mm";
                                    gridView1.Columns[i].BestFit();
                                }
                                else if (gridView1.Columns[i].FieldName == "Value")
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
                            series.ArgumentDataMember = "Argument";
                            series.ValueDataMembers.AddRange(new string[] { "Value" });
                            series.CrosshairLabelPattern = "{S} \r時間 : {A:yyyy-MM-dd HH:mm}\r{V:0.##} kW";
                            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                            chartControl.Series.Add(series);
                            if (chartControl.DataSource != null && chartControl.Series.Count > 0)
                            {
                                XYDiagram diagram = (XYDiagram)chartControl.Diagram;
                                if (diagram != null)
                                {
                                    diagram.EnableAxisXZooming = true;//放大縮小
                                    diagram.EnableAxisXScrolling = true;//拖曳
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
                        if (Form1.ConnectionFlag)
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
                        else
                        {
                            TimeSpan timeSpan = Convert.ToDateTime(EnddateEdit.EditValue).Subtract(Convert.ToDateTime(StartdateEdit.EditValue));
                            List<LineModule> Data = new List<LineModule>();
                            for (int day = 0; day < timeSpan.TotalDays + 1; day++)
                            {
                                var data = new LineModule()
                                {
                                    Argument = Convert.ToDateTime($"{Convert.ToDateTime(StartdateEdit.EditValue).AddDays(day):yyyy-MM-dd} 00:00:00"),
                                    Value = rnd.Next(200, 400)
                                }; ;
                                Data.Add(data);
                            }
                            gridControl.DataSource = Data;
                            chartControl.DataSource = Data;
                            #region 報表
                            for (int i = 0; i < gridView1.Columns.Count; i++)
                            {
                                if (gridView1.Columns[i].FieldName == "Argument")
                                {
                                    gridView1.Columns[i].Caption = "時間";
                                    gridView1.Columns[i].DisplayFormat.FormatString = "yyyy/MM/dd";
                                    gridView1.Columns[i].BestFit();
                                }
                                else if (gridView1.Columns[i].FieldName == "Value")
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
                            series.ArgumentDataMember = "Argument";
                            series.ValueDataMembers.AddRange(new string[] { "Value" });
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

        #region 曲線圖初始
        /// <summary>
        /// 曲線圖初始
        /// </summary>
        /// <returns></returns>
        public List<LineModule> Create_Line(DateTime nowdatetime)
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
                            Argument = Convert.ToDateTime($"{nowdatetime:yyyy-MM-dd} {i / 60}:{i % 60}:00"),
                            Value = rnd.Next(200, 400)
                        };
                        line.Add(lineModule);
                    }
                    else
                    {
                        LineModule lineModule = new LineModule()
                        {
                            Argument = Convert.ToDateTime($"{nowdatetime:yyyy-MM-dd} {i / 60}:0{i % 60}:00"),
                            Value = rnd.Next(200, 400)
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
                            Argument = Convert.ToDateTime($"{nowdatetime:yyyy-MM-dd} 0{i / 60}:{i % 60}:00"),
                            Value = rnd.Next(200, 400)
                        };
                        line.Add(lineModule);
                    }
                    else
                    {
                        LineModule lineModule = new LineModule()
                        {
                            Argument = Convert.ToDateTime($"{nowdatetime:yyyy-MM-dd} 0{i / 60}:0{i % 60}:00"),
                            Value = rnd.Next(200, 400)
                        };
                        line.Add(lineModule);
                    }
                }

            }
            return line;
        }
        #endregion
    }
}
