using Modbus.Device;
using Serilog;
using System;

namespace ChargingPileSystem.Protocols.ElectricMeter
{
    public class PA310Protocol : ElectricMeterData
    {
        public override void DataReader(ModbusMaster master)
        {
            try
            {
                int Index = 0;
                ushort[] value = master.ReadInputRegisters(ID, 1024, 72);
                ushort[] value1 = master.ReadInputRegisters(ID, 1152, 40);
                Rv = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                Sv = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                Tv = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                _ = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2; //平均
                RSv = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                STv = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                TRv = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                _ = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//平均
                RA = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                SA = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                TA = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                _ = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//平均
                HZ = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                kW_A = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//功率
                kW_B = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//功率
                kW_C = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//功率
                kW = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                kVAR_A = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//虛功率
                kVAR_B = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//虛功率
                kVAR_C = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//虛功率
                kVAR = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                kVA_A = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//視在功率
                kVA_B = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//視在功率
                kVA_C = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//視在功率
                kVA = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                PF_A = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//功率因數
                PF_B = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//功率因數
                PF_C = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;//功率因數
                PF = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                RV_Angle = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                SV_Angle = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                TV_Angle = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                RA_Angle = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                SA_Angle = MathClass.work16to754(value[Index + 1], value[Index]); Index += 2;
                TA_Angle = MathClass.work16to754(value[Index + 1], value[Index]);
                Index = 0;
                kWh_A = MathClass.work16to754(value1[Index + 1], value1[Index]); Index +=2;
                _ = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                kVARh_A = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                _ = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                kVAh_A = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                kWh_B = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                _ = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                kVARh_B = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                _ = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                kVAh_B = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                kWh_C = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                _ = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                kVARh_C = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                _ = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                kVAh_C = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                kWh = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                _ = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                kVARh = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                _ = MathClass.work16to754(value1[Index + 1], value1[Index]); Index += 2;
                kVAh = MathClass.work16to754(value1[Index + 1], value1[Index]);
                ConnectFlag = true;
            }
            catch (Exception ex)
            {
                ConnectFlag = false;
                Log.Error(ex, $"PA310解析異常、通訊編號: {GatewayIndex}、設備編號: {DeviceIndex}");
            }
        }
    }
}
