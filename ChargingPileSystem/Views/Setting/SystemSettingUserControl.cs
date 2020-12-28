using ChargingPileSystem.Configuration;
using ChargingPileSystem.Methods;
using System;

namespace ChargingPileSystem.Views.Setting
{
    public partial class SystemSettingUserControl : Field4UserControl
    {
        /// <summary>
        /// 主視窗畫面
        /// </summary>
        public Form1 Form1 { get; set; }
        /// <summary>
        /// 系統設定畫面
        /// </summary>
        /// <param name="bankAccount"></param>
        public SystemSettingUserControl(BankAccountSetting bankAccount)
        {
            InitializeComponent();
            BankNametextEdit.Text = bankAccount.BankName;
            BankCodetextEdit.Text = bankAccount.BankCode.ToString();
            BankAccounttextEdit.Text = bankAccount.BankAccount;
            AccountTitletextEdit.Text = bankAccount.AccountTitle;
            ContactPersontextEdit.Text = bankAccount.ContactPerson;
            ContactNumbertextEdit.Text = bankAccount.ContactNumber;
            PaymentDeadilinetextEdit.Text = bankAccount.PaymentDeadiline.ToString();
            ElectricityCosttextEdit.Text = bankAccount.ElectricityCost.ToString();
            SystemPasswordtextEdit.Text = bankAccount.SystemPassword;
        }

        private void SavesimpleButton_Click(object sender, EventArgs e)
        {
            Form1.BankAccountSetting.BankName = BankNametextEdit.Text;
            Form1.BankAccountSetting.BankCode = Convert.ToInt32(BankCodetextEdit.Text);
            Form1.BankAccountSetting.BankAccount = BankAccounttextEdit.Text;
            Form1.BankAccountSetting.AccountTitle = AccountTitletextEdit.Text;
            Form1.BankAccountSetting.ContactPerson = ContactPersontextEdit.Text;
            Form1.BankAccountSetting.ContactNumber = ContactNumbertextEdit.Text;
            Form1.BankAccountSetting.PaymentDeadiline = Convert.ToInt32(PaymentDeadilinetextEdit.Text);
            Form1.BankAccountSetting.ElectricityCost = Convert.ToDouble(ElectricityCosttextEdit.Text);
            Form1.BankAccountSetting.SystemPassword = SystemPasswordtextEdit.Text;
            InitialMethod.Save_BankAccount(Form1.BankAccountSetting);
            Form1.LockFlag = Form1.AfterLockFlag;
            Form1.FlyoutFlag = false;
            Form1.flyout.Close();
        }

        private void CancelsimpleButton_Click(object sender, EventArgs e)
        {
            /*復原GIA切換畫面旗標*/
            Form1.LockFlag = Form1.AfterLockFlag;
            Form1.FlyoutFlag = false;
            Form1.flyout.Close();
        }
    }
}
