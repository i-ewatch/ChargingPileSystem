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
                ushort[] data = master.ReadHoldingRegisters(ID, 100, 44);
                ushort[] data1 = master.ReadHoldingRegisters(ID, 324, 13);
                ushort[] data2 = master.ReadHoldingRegisters(ID, 368, 6);
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
                if (data1[Index] > 32767)
                {
                    HZ = (data1[Index] - 65536) * 0.002f; Index++;
                }
                else
                {
                    HZ = (data1[Index] * 0.002f); Index++;
                }
                if (data1[Index] > 32767)
                {
                    kW_A = (data1[Index] - 65536) * 0.2f; Index++;
                }
                else
                {
                    kW_A = (data1[Index] * 0.2f); Index++;
                }
                if (data1[Index] > 32767)
                {
                    kVAR_A = (data1[Index] - 65536) * 0.2f; Index++;
                }
                else
                {
                    kVAR_A = (data1[Index] * 0.2f); Index++;
                }
                if (data1[Index] > 32767)
                {
                    kVA_A = (data1[Index] - 65536) * 0.2f; Index++;
                }
                else
                {
                    kVA_A = (data1[Index] * 0.2f); Index++;
                }
                if (data1[Index] > 32767)
                {
                    PF_A = (data1[Index] - 65536) * 0.0001f; Index++;
                }
                else
                {
                    PF_A = (data1[Index] * 0.0001f); Index++;
                }
                if (data1[Index] > 32767)
                {
                    kW_B = (data1[Index] - 65536) * 0.2f; Index++;
                }
                else
                {
                    kW_B = (data1[Index] * 0.2f); Index++;
                }
                if (data1[Index] > 32767)
                {
                    kVAR_B = (data1[Index] - 65536) * 0.2f; Index++;
                }
                else
                {
                    kVAR_B = (data1[Index] * 0.2f); Index++;
                }
                if (data1[Index] > 32767)
                {
                    kVA_B = (data1[Index] - 65536) * 0.2f; Index++;
                }
                else
                {
                    kVA_B = (data1[Index] * 0.2f); Index++;
                }
                if (data1[Index] > 32767)
                {
                    PF_B = (data1[Index] - 65536) * 0.0001f; Index++;
                }
                else
                {
                    PF_B = (data1[Index] * 0.0001f); Index++;
                }
                if (data1[Index] > 32767)
                {
                    kW_C = (data1[Index] - 65536) * 0.2f; Index++;
                }
                else
                {
                    kW_C = (data1[Index] * 0.2f); Index++;
                }
                if (data1[Index] > 32767)
                {
                    kVAR_C = (data1[Index] - 65536) * 0.2f; Index++;
                }
                else
                {
                    kVAR_C = (data1[Index] * 0.2f); Index++;
                }
                if (data1[Index] > 32767)
                {
                    kVA_C = (data1[Index] - 65536) * 0.2f; Index++;
                }
                else
                {
                    kVA_C = (data1[Index] * 0.2f); Index++;
                }
                if (data1[Index] > 32767)
                {
                    PF_C = (data1[Index] - 65536) * 0.0001f; Index++;
                }
                else
                {
                    PF_C = (data1[Index] * 0.0001f); Index++;
                }
                Index = 0;
                kWh_A = MathClass.work16to10(data2[Index], data2[Index + 1]) * 0.001; Index += 2;
                kWh_B = MathClass.work16to10(data2[Index], data2[Index + 1]) * 0.001; Index += 2;
                kWh_C = MathClass.work16to10(data2[Index], data2[Index + 1]) * 0.001;
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
