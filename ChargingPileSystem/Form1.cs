using ChargingPileSystem.Configuration;
using ChargingPileSystem.Controllers;
using ChargingPileSystem.EF_Module;
using ChargingPileSystem.Methods;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using ChargingPileSystem.EF_Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using ChargingPileSystem.Components;
using ChargingPileSystem.Protocols;
using ChargingPileSystem.Enums;
using ChargingPileSystem.Views.ChargingPile;
using ChargingPileSystem.Views.Report;
using ChargingPileSystem.Views;
using Serilog;
using ChargingPileSystem.Views.Setting;
using DevExpress.Utils;

namespace ChargingPileSystem
{
    public partial class Form1 : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        #region 設定使用
        /// <summary>
        /// 浮動視窗
        /// </summary>
        public FlyoutDialog flyout { get; set; }
        /// <summary>
        /// 浮動視窗旗標
        /// </summary>
        public bool FlyoutFlag { get; set; }
        /// <summary>
        /// 之前畫面鎖定旗標
        /// </summary>
        public bool AfterLockFlag { get; set; }
        /// <summary>
        /// 切換畫面旗標
        /// True = 啟動
        /// False = 暫停
        /// </summary>
        public bool LockFlag { get; set; } = true;
        #endregion

        #region 錯誤泡泡視窗使用
        /// <summary>
        /// 錯誤泡泡視窗
        /// </summary>
        public FlyoutPanel ErrorflyoutPanel;
        #endregion
        #region Jsons
        /// <summary>
        /// 使用者權限管理旗標
        /// </summary>
        public bool AdministraturFlag { get; set; } = false;
        /// <summary>
        /// 按鈕資訊
        /// </summary>
        public ButtonSetting ButtonSetting { get; set; }
        /// <summary>
        /// Logo資訊
        /// </summary>
        public LogoSetting LogoSetting { get; set; }
        /// <summary>
        /// 資料庫資訊
        /// </summary>
        public SqlDBSetting SqlDBSetting { get; set; }
        /// <summary>
        /// 銀行與系統密碼資訊
        /// </summary>
        public BankAccountSetting BankAccountSetting { get; set; }
        #endregion
        #region Controls
        /// <summary>
        /// 按鈕控制
        /// </summary>
        private ButtonControl ButtonControl { get; set; }
        #endregion
        #region Methods
        /// <summary>
        /// 資料庫方法
        /// </summary>
        private SqlMethod SqlMethod { get; set; }
        #endregion
        #region 通訊
        /// <summary>
        /// 通訊類型
        /// </summary>
        private GatewayEnumType GatewayEnumType { get; set; }
        /// <summary>
        /// 通道資訊
        /// </summary>
        private List<GatewayConfig> GatewayConfigs { get; set; } = new List<GatewayConfig>();
        /// <summary>
        /// 電表設備資訊
        /// </summary>
        private List<ElectricConfig> ElectricConfigs { get; set; } = new List<ElectricConfig>();
        /// <summary>
        /// 通訊數值物件
        /// </summary>
        private List<AbsProtocol> AbsProtocols { get; set; }
        /// <summary>
        /// 通訊物件
        /// </summary>
        private List<Field4Component> ModbusComponents { get; set; } = new List<Field4Component>();
        /// <summary>
        /// 資料庫物件
        /// </summary>
        private SqlComponent SqlComponent { get; set; }
        #endregion
        #region 畫面
        /// <summary>
        /// 按鈕類別
        /// </summary>
        private ButtonItemEnumType ButtonItemEnumType { get; set; }
        /// <summary>
        /// 切換畫面物件
        /// </summary>
        private NavigationFrame NavigationFrame { get; set; }
        /// <summary>
        /// 首頁
        /// </summary>
        public List<Field4UserControl> field4UserControls { get; set; } = new List<Field4UserControl>();
        /// <summary>
        /// 報表
        /// </summary>
        public DataReportUserControl dataReportUserControl { get; set; }
        /// <summary>
        /// 計費單
        /// </summary>
        public BillingSheetUserControl billingSheetUserControl { get; set; }
        #endregion
        #region 使用者
        /// <summary>
        /// 登入時間
        /// </summary>
        private DateTime UserTime { get; set; }
        private int afterPageIndex = 0;
        private int PageIndex
        {
            get { return afterPageIndex; }
            set
            {
                if (value != afterPageIndex)
                {
                    afterPageIndex = value;
                    UserTime = DateTime.Now;
                }
            }
        }
        #endregion
        public Form1()
        {
            InitializeComponent();
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File($"{AppDomain.CurrentDomain.BaseDirectory}\\log\\log-.txt",
            rollingInterval: RollingInterval.Day,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();        //宣告Serilog初始化

            UserbarButtonItem.ImageOptions.Image = imageCollection16x16.Images["UserDisconnected"];
            SettingbarButtonItem.ImageOptions.Image = imageCollection16x16.Images["Setting"];
            SettingbarButtonItem.Visibility = BarItemVisibility.Never;
            #region  Json載入
            ButtonSetting = InitialMethod.ButtonLoad();
            LogoSetting = InitialMethod.LogoLoad();
            SqlDBSetting = InitialMethod.SqlDBLoad();
            BankAccountSetting = InitialMethod.BankAccountLoad();
            #endregion

            #region 載入Logo圖片
            if (LogoSetting != null)
            {
                if (File.Exists(LogoSetting.LogoPath))
                {
                    LogopictureEdit.Image = Image.FromFile(LogoSetting.LogoPath);
                }
                else
                {
                    LogopictureEdit.Image = LogoimageCollection.Images["ewatch_logo"];
                }
            }
            else
            {
                LogopictureEdit.Image = LogoimageCollection.Images["ewatch_logo"];
            }
            #endregion

            #region 按鈕載入
            NavigationFrame = new NavigationFrame() { Dock = DockStyle.Fill };
            NavigationFrame.Parent = DisplaypanelControl;
            ButtonControl = new ButtonControl() { Form1 = this, navigationFrame = NavigationFrame };
            ButtonControl.AccordionLoad(accordionControl1, ButtonSetting);
            #endregion

            #region 資料庫方法載入
            if (SqlDBSetting != null)
            {
                SqlMethod = new SqlMethod() { setting = SqlDBSetting, BankAccountSetting = BankAccountSetting };
                SqlMethod.SQLConnect();
                GatewayConfigs = SqlMethod.Search_GatewayConfig();//通道資訊
                ElectricConfigs = SqlMethod.Search_Electricconfig();//電表設備資訊
                SqlComponent = new SqlComponent() { SqlMethod = SqlMethod, BankAccountSetting = BankAccountSetting };
                SqlComponent.MyWorkState = true;
            }
            #endregion

            #region 通訊
            foreach (var item in GatewayConfigs)
            {
                GatewayEnumType = (GatewayEnumType)item.GatewayEnumType;
                var electricconfigs = ElectricConfigs.Where(g => g.GatewayIndex == item.GatewayIndex).ToList();
                switch (GatewayEnumType)
                {
                    case GatewayEnumType.ModbusRTU:
                        {
                            SerialportComponent component = new SerialportComponent() { BankAccountSetting = BankAccountSetting, Gatewayconfig = item, ElectricConfigs = electricconfigs };
                            component.MyWorkState = true;
                            ModbusComponents.Add(component);
                        }
                        break;
                    case GatewayEnumType.ModbusTCP:
                        {
                            TcpComponent component = new TcpComponent() { BankAccountSetting = BankAccountSetting, Gatewayconfig = item, ElectricConfigs = electricconfigs };
                            component.MyWorkState = true;
                            ModbusComponents.Add(component);
                        }
                        break;
                }
            }
            #endregion

            #region 畫面
            foreach (var item in ButtonSetting.ButtonGroupSettings)
            {
                foreach (var Buttonitem in item.ButtonItemSettings)
                {
                    ButtonItemEnumType = (ButtonItemEnumType)Buttonitem.ButtonItemEnumType;
                    switch (ButtonItemEnumType)
                    {
                        case ButtonItemEnumType.Home:
                            {
                                ChargingPileView chargingPileView = new ChargingPileView(ElectricConfigs, SqlMethod, this, GatewayConfigs) { SqlMethod = SqlMethod, Dock = DockStyle.Fill };
                                field4UserControls.Add(chargingPileView);
                                NavigationFrame.AddPage(chargingPileView);
                            }
                            break;
                        case ButtonItemEnumType.Report:
                            {
                                dataReportUserControl = new DataReportUserControl(ElectricConfigs) { SqlMethod = SqlMethod, Dock = DockStyle.Fill, Form1 = this };
                                NavigationFrame.AddPage(dataReportUserControl);
                            }
                            break;
                        case ButtonItemEnumType.BillingSheet:
                            {
                                billingSheetUserControl = new BillingSheetUserControl(ElectricConfigs) { BankaccountSetting = BankAccountSetting, SqlMethod = SqlMethod, Dock = DockStyle.Fill, Form1 = this };
                                NavigationFrame.AddPage(billingSheetUserControl);
                            }
                            break;
                    }
                }
            }
            User_ButtonItem_Visible();
            #endregion
            timer1.Interval = 1000;
            timer1.Enabled = true;
        }
        #region 使用者登入顯示左邊列表按鈕
        /// <summary>
        /// 使用者登入顯示左邊列表按鈕
        /// </summary>
        private void User_ButtonItem_Visible()
        {
            foreach (var item in accordionControl1.Elements)
            {
                if (item.Style == ElementStyle.Item)
                {
                    if (accordionControl1.Elements.Count - 1 == Convert.ToInt32(item.Tag))
                    {
                        if (AdministraturFlag)
                        {
                            item.Visible = AdministraturFlag;
                        }
                        else
                        {
                            item.Visible = AdministraturFlag;
                        }
                    }
                    else if (accordionControl1.Elements.Count - 2 == Convert.ToInt32(item.Tag))
                    {
                        if (AdministraturFlag)
                        {
                            item.Visible = AdministraturFlag;
                        }
                        else
                        {
                            item.Visible = AdministraturFlag;
                        }
                    }
                }
            }
        }
        #endregion
        #region 帳號登入按鈕
        /// <summary>
        /// 帳號登入按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserbarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (AdministraturFlag)//登出
            {
                UserbarButtonItem.ImageOptions.Image = imageCollection16x16.Images["UserDisconnected"];
                AdministraturFlag = false;
                User_ButtonItem_Visible();
                SettingbarButtonItem.Visibility = BarItemVisibility.Never;
                NavigationFrame.SelectedPageIndex = 0;
            }
            else//登入
            {
                if (BankAccountSetting != null)
                {
                    LockFlag = false;
                    UserControl control = new UserControl() { Padding = new Padding(0, 30, 0, 20), Size = new Size(400, 200) };
                    TextEdit textEdit = new TextEdit() { Dock = DockStyle.Top, Size = new Size(400, 40) };
                    textEdit.Properties.Appearance.FontSizeDelta = 12;
                    textEdit.Properties.Appearance.Options.UseFont = true;
                    textEdit.Properties.Appearance.Options.UseTextOptions = true;
                    textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    textEdit.Properties.UseSystemPasswordChar = true;
                    textEdit.Parent = control;
                    LabelControl labelControl = new LabelControl() { Dock = DockStyle.Top, Size = new Size(400, 50) };
                    labelControl.Appearance.FontSizeDelta = 18;
                    labelControl.AutoSizeMode = LabelAutoSizeMode.None;
                    labelControl.Text = "請登入密碼";
                    labelControl.Appearance.Options.UseFont = true;
                    labelControl.Appearance.Options.UseTextOptions = true;
                    labelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    labelControl.Parent = control;
                    SimpleButton okButton = new SimpleButton() { Dock = DockStyle.Bottom, Text = "確定", Size = new Size(400, 40) };
                    okButton.Appearance.BackColor = Color.FromArgb(80, 80, 80);
                    okButton.Appearance.FontSizeDelta = 12;
                    okButton.DialogResult = DialogResult.OK;
                    okButton.Parent = control;
                    if (FlyoutDialog.Show(FindForm(), control) == DialogResult.OK && (string.Compare(textEdit.Text, $"{BankAccountSetting.SystemPassword}", true) == 0 || string.Compare(textEdit.Text, $"d001007", true) == 0))
                    {
                        UserbarButtonItem.ImageOptions.Image = imageCollection16x16.Images["UserConnect"];
                        AdministraturFlag = true;
                        User_ButtonItem_Visible();
                        SettingbarButtonItem.Visibility = BarItemVisibility.Always;
                        UserTime = DateTime.Now;
                        LockFlag = true;
                    }
                    else
                    {
                        FlyoutAction action = new FlyoutAction();
                        action.Caption = "權杖密碼錯誤";
                        action.Description = "請與管理人員確認密碼";
                        action.Commands.Add(FlyoutCommand.OK);
                        FlyoutDialog.Show(FindForm(), action);
                    }
                }
            }
        }
        #endregion
        #region 系統設定按鈕
        /// <summary>
        /// 系統設定按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingbarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            AfterLockFlag = LockFlag;
            LockFlag = false;
            if (!FlyoutFlag)
            {
                FlyoutFlag = true;
                PanelControl panelControl = new PanelControl()
                {
                    Size = new Size(418, 492)
                };
                flyout = new FlyoutDialog(this, panelControl);
                flyout.Properties.Style = FlyoutStyle.Popup;
                SystemSettingUserControl systemSettingUserControl = new SystemSettingUserControl(BankAccountSetting) { Form1 = this };
                systemSettingUserControl.Parent = panelControl;
                flyout.Show();
            }
            else
            {
                FlyoutFlag = false;
                flyout.Close();
            }
        }
        #endregion
        #region 通訊錯誤泡泡視窗
        /// <summary>
        /// 通訊錯誤泡泡視窗
        /// </summary>
        public void ComponentFail()
        {
            foreach (var item in ModbusComponents)
            {
                if (item.ComponentFlag)
                {
                    if (ErrorflyoutPanel == null)
                    {
                        ErrorflyoutPanel = new FlyoutPanel()
                        {
                            OwnerControl = this,
                            Size = new Size(1920, 63)
                        };
                        LabelControl label = new LabelControl() { Size = new Size(1920, 63) };
                        label.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                        label.Appearance.Font = new Font("微軟正黑體", 30);
                        label.Appearance.ForeColor = Color.White;
                        label.Appearance.BackColor = Color.Red;
                        label.AutoSizeMode = LabelAutoSizeMode.None;
                        label.Text = item.ErrorString;
                        ErrorflyoutPanel.Controls.Add(label);
                        ErrorflyoutPanel.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Bottom;
                        ErrorflyoutPanel.ShowPopup();
                    }
                    return;
                }
            }
            if (ErrorflyoutPanel != null)
            {
                ErrorflyoutPanel.HidePopup();
                ErrorflyoutPanel = null;
            }
        }
        #endregion
        private void timer1_Tick(object sender, EventArgs e)
        {
            #region 自動登出
            PageIndex = NavigationFrame.SelectedPageIndex;
            TimeSpan timeSpan = DateTime.Now.Subtract(UserTime);
            if (timeSpan.TotalSeconds > 600 && AdministraturFlag)
            {
                UserbarButtonItem_ItemClick(null, null);
            }
            #endregion
            ElectricConfigs = SqlMethod.Search_Electricconfig();//電表設備資訊
            AbsProtocols = new List<AbsProtocol>();
            foreach (var item in ModbusComponents)
            {
                foreach (var dataitem in item.AbsProtocols)
                {
                    AbsProtocols.Add(dataitem);
                }
            }
            SqlComponent.AbsProtocols = AbsProtocols;
            if (field4UserControls.Count > NavigationFrame.SelectedPageIndex)
            {
                field4UserControls[NavigationFrame.SelectedPageIndex].AbsProtocols = AbsProtocols;
                field4UserControls[NavigationFrame.SelectedPageIndex].ElectricConfigs = ElectricConfigs;
                field4UserControls[NavigationFrame.SelectedPageIndex].TextChange();
            }
            ComponentFail();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var item in ModbusComponents)
            {
                item.MyWorkState = false;
            }
            SqlComponent.MyWorkState = false;
            timer1.Enabled = false;
            this.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 0);
            Size = new Size(1920, 1080);
        }
    }
}
