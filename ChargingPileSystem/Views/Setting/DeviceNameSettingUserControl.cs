﻿using ChargingPileSystem.Methods;
using System;

namespace ChargingPileSystem.Views.Setting
{
    public partial class DeviceNameSettingUserControl : Field4UserControl
    {
        /// <summary>
        /// 通訊編號
        /// </summary>
        private int GatewayIndex { get; set; }
        /// <summary>
        /// 設備編號
        /// </summary>
        private int DeviceIndex { get; set; }
        public DeviceNameSettingUserControl(string GatewayName, int DeviceID, string OldDeviceName, int gatewayIndex, int deviceIndex)
        {
            InitializeComponent();
            GatewayNamelabelControl.Text = GatewayName;
            DeviceIDlabelControl.Text = DeviceID.ToString();
            OldDeviceNamelabelControl.Text = OldDeviceName;
            GatewayIndex = gatewayIndex;
            DeviceIndex = deviceIndex;
        }

        private void SavesimpleButton_Click(object sender, EventArgs e)
        {
            if (Form1.ConnectionFlag)
            {
                if (SqlMethod.Updata_ElectricConfig(NewDeviceNametextEdit.Text, GatewayIndex, DeviceIndex))
                {
                    ElectricConfigs = SqlMethod.Search_Electricconfig();
                    Form1.billingSheetUserControl.Create_CheckedListBoxItem(ElectricConfigs);
                    Form1.dataReportUserControl.DeviceItemRefresh(ElectricConfigs);
                }
            }
            else
            {
                foreach (var item in ElectricConfigs)
                {
                    if (item.DeviceID == Convert.ToInt32(DeviceIDlabelControl.Text))
                    {
                        item.DeviceName = NewDeviceNametextEdit.Text;
                        break;
                    }
                }
                Form1.billingSheetUserControl.Create_CheckedListBoxItem(ElectricConfigs);
                Form1.dataReportUserControl.DeviceItemRefresh(ElectricConfigs);
            }
            /*復原GIA切換畫面旗標*/
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
