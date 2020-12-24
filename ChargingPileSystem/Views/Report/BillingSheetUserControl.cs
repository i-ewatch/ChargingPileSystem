using ChargingPileSystem.Configuration;
using ChargingPileSystem.EF_Module;
using ChargingPileSystem.EF_Modules;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChargingPileSystem.Views.Report
{
    public partial class BillingSheetUserControl : Field4UserControl
    {
        public BillingSheetUserControl(List<ElectricConfig> electricConfigs)
        {
            InitializeComponent();
            StartdateEdit.Properties.ContextImageOptions.Image = imageCollection1.Images["calendar"];
            Create_CheckedListBoxItem(electricConfigs);
        }
        public BankAccountSetting BankaccountSetting { get; set; }
        /// <summary>
        /// 勾選物件
        /// </summary>
        private List<CheckedListBoxItem> boxItems { get; set; }
        /// <summary>
        /// 創建物件
        /// </summary>
        /// <param name="electricConfigs"></param>
        public void Create_CheckedListBoxItem(List<ElectricConfig> electricConfigs)
        {
            if (DevicecheckedComboBoxEdit.Properties.Items.Count > 0)
            {
                DevicecheckedComboBoxEdit.Properties.Items.Clear();
            }
            boxItems = new List<CheckedListBoxItem>();
            foreach (var item in electricConfigs)
            {
                CheckedListBoxItem listBoxItem = new CheckedListBoxItem(item.DeviceName, false);
                listBoxItem.Tag = item;
                boxItems.Add(listBoxItem);
                DevicecheckedComboBoxEdit.Properties.Items.Add(listBoxItem);
            }
        }

        private void SearchsimpleButton_Click(object sender, EventArgs e)
        {
            string AfterStartTime;
            string AfterEndTime;
            string NowStartTime;
            string NowEndTime;
            int RoomCheck = 0;//勾選數量
            bool OddEvenFlag = false;//奇偶數
            List<string> RoomName = new List<string>();//勾選住戶名
            List<ElectricTotalPrice> NowbillData = new List<ElectricTotalPrice>();
            List<ElectricTotalPrice> AfterbillData = new List<ElectricTotalPrice>();
            if (Convert.ToDateTime(StartdateEdit.EditValue).Month == 1)
            {
                AfterStartTime = $"{Convert.ToDateTime(StartdateEdit.EditValue).AddYears(-1).Year}1201";
                AfterEndTime = $"{Convert.ToDateTime(StartdateEdit.EditValue).AddYears(-1).Year}1231";
            }
            else
            {
                AfterStartTime = $"{Convert.ToDateTime(StartdateEdit.EditValue).Year}{Convert.ToDateTime(StartdateEdit.EditValue).AddMonths(-1).Month}01";
                AfterEndTime = $"{Convert.ToDateTime(StartdateEdit.EditValue).Year}{Convert.ToDateTime(StartdateEdit.EditValue).AddMonths(-1).Month}{DateTime.DaysInMonth(Convert.ToDateTime(StartdateEdit.EditValue).Year, Convert.ToDateTime(StartdateEdit.EditValue).AddMonths(-1).Month)}";
            }
            NowStartTime = Convert.ToDateTime(StartdateEdit.EditValue).ToString("yyyyMM01");
            NowEndTime = Convert.ToDateTime(StartdateEdit.EditValue).ToString("yyyyMM") + DateTime.DaysInMonth(Convert.ToDateTime(StartdateEdit.EditValue).Year, Convert.ToDateTime(StartdateEdit.EditValue).Month);
            #region 塞入勾選房間房號
            foreach (var item in boxItems)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    ElectricConfig electric = (ElectricConfig)item.Tag;
                    RoomName.Add(electric.DeviceName);
                    if (Form1.ConnectionFlag)
                    {
                        var nowbilldata = SqlMethod.Search_ElectricTotalPrice_Billing(NowStartTime, NowEndTime, electric.GatewayIndex, electric.DeviceIndex);
                        NowbillData.Add(nowbilldata);
                        var afterbilldata = SqlMethod.Search_ElectricTotalPrice_Billing(AfterStartTime, AfterEndTime, electric.GatewayIndex, electric.DeviceIndex);
                        AfterbillData.Add(afterbilldata);
                    }
                    else
                    {
                        var nowbilldata = new ElectricTotalPrice() { Price = rnd.Next(500, 1000), KwhTotal = rnd.Next(100, 200) };
                        NowbillData.Add(nowbilldata);
                        var afterbilldata = new ElectricTotalPrice() { Price = rnd.Next(500, 1000), KwhTotal = rnd.Next(100, 200) };
                        AfterbillData.Add(afterbilldata);
                    }
                    RoomCheck++;
                }
            }
            #endregion
            #region 判斷奇數偶數
            if (RoomCheck % 2 == 0)
            {
                OddEvenFlag = true;//偶數
            }
            else
            {
                OddEvenFlag = false;//奇數
            }
            #endregion
            BillingSheetXtraReport[] billingSheets = new BillingSheetXtraReport[RoomCheck / 2 + RoomCheck % 2]; //計費單數量
            XtraReport xtraReport = new XtraReport();
            if (OddEvenFlag)//偶數
            {
                for (int i = 0; i < billingSheets.Length; i++)
                {
                    billingSheets[i] = new BillingSheetXtraReport() { bankAccount = BankaccountSetting };
                    billingSheets[i].Create_BillingSheet(Convert.ToDateTime(StartdateEdit.EditValue), true, RoomName[i + i], NowbillData[i + i], AfterbillData[i + i], RoomName[i + i + 1], NowbillData[i + i + 1], AfterbillData[i + 1]);
                    billingSheets[i].CreateDocument();
                    xtraReport.Pages.AddRange(billingSheets[i].Pages);
                }
                documentViewer1.DocumentSource = xtraReport;
            }
            else//奇數
            {
                for (int i = 0; i < billingSheets.Length; i++)
                {
                    billingSheets[i] = new BillingSheetXtraReport() { bankAccount = BankaccountSetting };
                    if (i != billingSheets.Length - 1)
                    {
                        billingSheets[i].Create_BillingSheet(Convert.ToDateTime(StartdateEdit.EditValue), true, RoomName[i + i], NowbillData[i + i], AfterbillData[i + i], RoomName[i + i + 1], NowbillData[i + i + 1], AfterbillData[i + 1]);
                    }
                    else
                    {
                        billingSheets[i].Create_BillingSheet(Convert.ToDateTime(StartdateEdit.EditValue), false, RoomName[i + i], NowbillData[i + i], AfterbillData[i + i]);
                    }
                    billingSheets[i].CreateDocument();
                    xtraReport.Pages.AddRange(billingSheets[i].Pages);
                }
                documentViewer1.DocumentSource = xtraReport;
            }
        }
    }
}
