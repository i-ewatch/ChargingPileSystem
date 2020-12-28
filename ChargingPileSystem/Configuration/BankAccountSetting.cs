namespace ChargingPileSystem.Configuration
{
    public class BankAccountSetting
    {
        /// <summary>
        /// 銀行名稱
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 銀行代碼
        /// </summary>
        public int BankCode { get; set; }
        /// <summary>
        /// 銀行帳戶
        /// </summary>
        public string BankAccount { get; set; }
        /// <summary>
        /// 帳戶名稱
        /// </summary>
        public string AccountTitle { get; set; }
        /// <summary>
        /// 聯絡人
        /// </summary>
        public string ContactPerson { get; set; }
        /// <summary>
        /// 聯絡電話
        /// </summary>
        public string ContactNumber { get; set; }
        /// <summary>
        /// 繳費期限
        /// </summary>
        public int PaymentDeadiline { get; set; }
        /// <summary>
        /// 電度費用
        /// </summary>
        public double ElectricityCost { get; set; }
        /// <summary>
        /// 系統密碼
        /// </summary>
        public string SystemPassword { get; set; }
    }
}
