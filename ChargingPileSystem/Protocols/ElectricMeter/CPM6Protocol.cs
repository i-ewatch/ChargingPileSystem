using Modbus.Device;
using Serilog;
using System;

namespace ChargingPileSystem.Protocols.ElectricMeter
{
    public class CPM6Protocol : ElectricMeterData
    {
        public override void DataReader(ModbusMaster master)
        {
            try
            {
                ushort[] value = master.ReadInputRegisters(ID, 0, 82);
                ushort[] value1 = master.ReadInputRegisters(ID, 200, 8);
                ushort[] value2 = master.ReadInputRegisters(ID, 342, 40);
                int Index = 0;
                Rv = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                Sv = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                Tv = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                RA = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                SA = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                TA = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                kW_A = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//R相有效功率
                kW_B = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//S相有效功率
                kW_C = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//T相有效功率
                kVA_A = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//R相視在功率
                kVA_B = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//S相視在功率
                kVA_C = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//T相視在功率
                kVAR_A = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//R相虛功率
                kVAR_B = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//S相虛功率
                kVAR_C = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//T相虛功率
                PF_A = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//R相功率因數
                PF_B = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//S相功率因數
                PF_C = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//T相功率因數
                RV_Angle = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//R相位角
                SV_Angle = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//S相位角
                TV_Angle = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//T相位角
                _ = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//平均線電壓
                _ = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//平均線電流
                _ = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//加總線電流
                kW = MathClass.work16to754(value[Index + 1], value[Index]) / 1000; Index += 2;//總系統有效功率
                kVA = MathClass.work16to754(value[Index + 1], value[Index]) / 1000; Index += 2;//總系統視在功率
                kVAR = MathClass.work16to754(value[Index + 1], value[Index]) / 1000; Index += 2;//總系統虛功率
                PF = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//總系統功率因數
                _ = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//總系統相位角
                HZ = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//頻率
                _ = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//總輸入累積用電度
                _ = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//總輸出累積用電度
                _ = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//總輸入累積視在功率
                kVA = MathClass.work16to754(value[Index + 1], value[Index]); //總輸出累積視在功率
                Index = 0;
                RSv = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                STv = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                TRv = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                _ = MathClass.work16to754(value1[Index + 1], value1[Index]);//平均相電壓
                Index = 0;
                kWh = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//總累積用電度數
                kVARh = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//總累積虛功率
                _ = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//R相輸入累積用電度數
                _ = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//S相輸入累積用電度數
                _ = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//T相輸入累積用電度數
                _ = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//R相輸出累積用電度數
                _ = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//S相輸出累積用電度數
                _ = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//T相輸出累積用電度數
                kWh_A = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//R相總累積用電度數
                kWh_B = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//S相總累積用電度數
                kWh_C = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//T相總累積用電度數
                _ = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//R相輸入累積視在功率
                _ = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//S相輸入累積視在功率
                _ = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//T相輸入累積視在功率
                _ = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//R相輸出累積視在功率
                _ = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//S相輸出累積視在功率
                _ = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//T相輸出累積視在功率
                kVAR_A = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//R相總累積視在功率
                kVAR_B = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//S相總累積視在功率
                kVAR_C = MathClass.work16to754(value2[Index + 1], value2[Index]); Index += 2;//T相總累積視在功率
                ConnectFlag = true;
            }
            catch (Exception ex)
            {
                ConnectFlag = false;
                Log.Error(ex, $"CPM6解析異常、通訊編號: {GatewayIndex}、設備編號: {DeviceIndex}");
            }
        }
    }
}
