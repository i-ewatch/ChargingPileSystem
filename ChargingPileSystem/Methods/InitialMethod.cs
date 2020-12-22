using ChargingPileSystem.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingPileSystem.Methods
{
    public static class InitialMethod
    {
        /// <summary>
        /// 初始路徑
        /// </summary>
        private static string MyWorkPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;

        #region Logo圖片資訊
        /// <summary>
        /// Logo圖片資訊
        /// </summary>
        /// <returns></returns>
        public static LogoSetting LogoLoad()
        {
            LogoSetting setting = null;
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\Logo.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<LogoSetting>(json);
                }
                else
                {
                    LogoSetting Setting = new LogoSetting()
                    {
                        LogoPath = ""
                    };
                    setting = Setting;
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, " Logo資訊設定載入錯誤");
            }
            return setting;
        }
        /// <summary>
        /// Logo圖片資訊-儲存
        /// </summary>
        /// <param name="setting"></param>
        public static void Save_Logo(LogoSetting setting)
        {
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\Log.json";
            string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
            File.WriteAllText(SettingPath, output);
        }
        #endregion

        #region 資料庫資訊
        public static SqlDBSetting SqlDBLoad()
        {
            SqlDBSetting setting = null;
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\SqlDB.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<SqlDBSetting>(json);
                }
                else
                {
                    SqlDBSetting Setting = new SqlDBSetting()
                    {
                        SQLEnumsType = 1,
                        DataSource = "127.0.0.1",
                        InitialCatalog = "chargingpiles",
                        UserID = "root",
                        Password = "1234"
                    };
                    setting = Setting;
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, " SQLDB資訊設定載入錯誤");
            }
            return setting;
        }
        #endregion

        #region 按鈕資訊
        public static ButtonSetting ButtonLoad()
        {
            ButtonSetting setting = null;
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\Button.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<ButtonSetting>(json);
                }
                else
                {
                    ButtonSetting Setting = new ButtonSetting()
                    {
                        ButtonGroupSettings =
                        {
                            new ButtonGroupSetting()
                            {
                                // 0 = 群組，1 = 列表
                                ButtonEnumType = 1,
                                //群組名稱
                                GroupName = "群組名稱",
                                // 群組標註
                                GroupTag = 0,
                                //列表按鈕設定
                                ButtonItemSettings=
                                {
                                    new ButtonItemSetting()
                                    {
                                        //列表名稱
                                        ItemName = "列表名稱",
                                        //按鈕列表類型
                                        ButtonItemEnumType = 0,
                                    }
                                }
                            }
                        }
                    };
                    setting = Setting;
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, " 按鈕資訊設定載入錯誤");
            }
            return setting;
        }
        #endregion

        #region 銀行與系統密碼資訊
        /// <summary>
        /// 銀行與系統密碼資訊
        /// </summary>
        /// <returns></returns>
        public static BankAccountSetting BankAccountLoad()
        {
            BankAccountSetting setting = null;
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\BankAccount.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<BankAccountSetting>(json);
                }
                else
                {
                    BankAccountSetting Setting = new BankAccountSetting()
                    {
                        BankName ="XX銀行",
                        BankCode = 123,
                        BankAccount = "123456789",
                        AccountTitle = "XX公司",
                        ContactPerson="X先生/女士",
                        ContactNumber = "09-12345678",
                        PaymentDeadiline = 5,
                        ElectricityCost = 6,
                        SystemPassword = "1234"
                    };
                    setting = Setting;
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, " 銀行與系統密碼資訊設定載入錯誤");
            }
            return setting;
        }

        public static void Save_BankAccount(BankAccountSetting setting)
        {
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\BankAccount.json";
            string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
            File.WriteAllText(SettingPath, output);
        }
        #endregion
    }
}
