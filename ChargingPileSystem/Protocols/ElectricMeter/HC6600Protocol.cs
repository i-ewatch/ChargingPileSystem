using Modbus.Device;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChargingPileSystem.Protocols.ElectricMeter
{
    public class HC6600Protocol : ElectricMeterData
    {
        public override void DataReader(ModbusMaster master)
        {
            try
            {
                int Index = 0;
                ushort[] data = master.ReadHoldingRegisters(ID, 0x64, 66);
                ushort[] data1 = master.ReadInputRegisters(ID, 324, 13);
                RSv = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                STv = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                TRv = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                RA = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                SA = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                TA = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                kVA = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                kW = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                kVAR = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                PF = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                kWh = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                kVARh = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                kVAh = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                RV_Angle = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                SV_Angle = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                TV_Angle = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                RA_Angle = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                SA_Angle = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                TA_Angle = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                Rv = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                Sv = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
                Tv = MathClass.work16to754(data[Index + 1], data[Index]);
                Index = 0;
                HZ = (data1[Index] * 0.1f); Index++;
                kW_A = (data1[Index] * 0.01f); Index++;
                kVAR_A = (data1[Index] * 0.01f); Index++;
                kVA_A = (data1[Index] * 0.1f); Index++;
                PF_A = (data1[Index] * 0.01f); Index++;
                kW_B = (data1[Index] * 0.01f); Index++;
                kVAR_B = (data1[Index] * 0.01f); Index++;
                kVA_B = (data1[Index] * 0.1f); Index++;
                PF_B = (data1[Index] * 0.01f); Index++;
                kW_C = (data1[Index] * 0.01f); Index++;
                kVAR_C = (data1[Index] * 0.01f); Index++;
                kVA_C = (data1[Index] * 0.1f); Index++;
                PF_C = (data1[Index] * 0.01f);
                kWh_A = kWh;
                kWh_B = kWh;
                kWh_C = kWh;
                ConnectFlag = true;
            }
            catch (Exception ex)
            {
                ConnectFlag = false;
                Log.Error(ex, $"HC6600解析異常、通訊編號: {GatewayIndex}、設備編號: {DeviceIndex}");
            }
        }
    }
}
