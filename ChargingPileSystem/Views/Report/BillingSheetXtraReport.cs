using ChargingPileSystem.Configuration;
using ChargingPileSystem.EF_Module;
using System;

namespace ChargingPileSystem.Views.Report
{
    public partial class BillingSheetXtraReport : DevExpress.XtraReports.UI.XtraReport
    {
        /// <summary>
        /// 銀行與系統密碼資訊
        /// </summary>
        public BankAccountSetting bankAccount { get; set; }
        public BillingSheetXtraReport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 產生計費單
        /// </summary>
        /// <param name="RoomName1">住戶名1</param>
        /// <param name="NowbillData1">住戶1本月資料</param>
        /// <param name="AfterbillData1">住戶1上月資料</param>
        /// <param name="RoomName2">住戶名2</param>
        /// <param name="NowbillData2">住戶2本月資料</param>
        /// <param name="AfterbillData2">住戶2上月資料</param>
        /// <param name="OddEvenFlag">奇偶數旗標</param>
        /// <param name="SearchTime">查詢時間</param>
        public void Create_BillingSheet(DateTime SearchTime, bool OddEvenFlag , string RoomName1, ElectricTotalPrice NowbillData1, ElectricTotalPrice AfterbillData1, string RoomName2 = null, ElectricTotalPrice NowbillData2 = null, ElectricTotalPrice AfterbillData2 = null)
        {
            if (OddEvenFlag)
            {
                #region 第一張
                if (RoomName1 != null)
                {
                    xrTableCell5.Text = RoomName1;
                }
                if (NowbillData1 != null)
                {
                    xrTableCell7.Text = NowbillData1.KwhTotal.ToString("F2");
                    xrTableCell8.Text = Convert.ToInt32(NowbillData1.Price).ToString();
                }
                if (AfterbillData1 != null)
                {
                    xrTableCell6.Text = AfterbillData1.KwhTotal.ToString("F2");
                }
                if (bankAccount != null)
                {
                    xrLabel2.Text = $"計費時間 {SearchTime:yyyy年MM月01}日 ~ {SearchTime:yyyy年MM月}{DateTime.DaysInMonth(SearchTime.Year, SearchTime.Month)}日";
                    xrLabel3.Text = $"●請轉帳至{bankAccount.BankName}(銀行代碼:{bankAccount.BankCode})";
                    xrLabel4.Text = $"戶名:{bankAccount.AccountTitle}";
                    xrLabel5.Text = $"帳號:{bankAccount.BankAccount}";
                    xrLabel6.Text = $"●請於每月{bankAccount.PaymentDeadiline}日前完成轉帳";
                    xrLabel7.Text = $"若有問題請與管委會聯絡，電話:{bankAccount.ContactNumber}";
                }
                #endregion
                #region 第二張
                if (RoomName2 != null)
                {
                    xrTableCell13.Text = RoomName2;
                }
                if (NowbillData2 != null)
                {
                    xrTableCell15.Text = NowbillData2.KwhTotal.ToString("F2");
                    xrTableCell16.Text = Convert.ToInt32(NowbillData2.Price).ToString();
                }
                if (AfterbillData2 != null)
                {
                    xrTableCell14.Text = AfterbillData2.KwhTotal.ToString("F2");
                }
                if (bankAccount != null)
                {
                    xrLabel9.Text = $"計費時間 {SearchTime:yyyy年MM月01}日 ~ {SearchTime:yyyy年MM月}{DateTime.DaysInMonth(SearchTime.Year, SearchTime.Month)}日";
                    xrLabel10.Text = $"1、請轉帳至{bankAccount.BankName}(銀行代碼:{bankAccount.BankCode})";
                    xrLabel11.Text = $"戶名:{bankAccount.AccountTitle}";
                    xrLabel12.Text = $"帳號:{bankAccount.BankAccount}";
                    xrLabel13.Text = $"2、請於每月{bankAccount.PaymentDeadiline}日前完成轉帳";
                    xrLabel14.Text = $"若有問題請與管委會聯絡，電話:{bankAccount.ContactNumber}";
                }
                #endregion

            }
            else
            {
                #region 第一張
                if (RoomName1 != null)
                {
                    xrTableCell5.Text = RoomName1;
                }
                if (NowbillData1 != null)
                {
                    xrTableCell7.Text = NowbillData1.KwhTotal.ToString("F2");
                    xrTableCell8.Text = Convert.ToInt32(NowbillData1.Price).ToString();
                }
                if (AfterbillData1 != null)
                {
                    xrTableCell6.Text = AfterbillData1.KwhTotal.ToString("F2");
                }
                if (bankAccount != null)
                {
                    xrLabel2.Text = $"計費時間 {SearchTime:yyyy年MM月01}日 ~ {SearchTime:yyyy年MM月}{DateTime.DaysInMonth(SearchTime.Year, SearchTime.Month)}日";
                    xrLabel3.Text = $"1、請轉帳至{bankAccount.BankName}(銀行代碼:{bankAccount.BankCode})";
                    xrLabel4.Text = $"戶名:{bankAccount.AccountTitle}";
                    xrLabel5.Text = $"帳號:{bankAccount.BankAccount}";
                    xrLabel6.Text = $"2、請於每月{bankAccount.PaymentDeadiline}日前完成轉帳";
                    xrLabel7.Text = $"若有問題請與管委會聯絡，電話:{bankAccount.ContactNumber}";
                }
                #endregion
                xrPanel2.Visible = false;
            }
        }
    }
}
