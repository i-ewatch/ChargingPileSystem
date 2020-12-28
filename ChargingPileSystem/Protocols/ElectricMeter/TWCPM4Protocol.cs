using Modbus.Device;
using Serilog;
using System;

namespace ChargingPileSystem.Protocols.ElectricMeter
{
    public class TWCPM4Protocol : ElectricMeterData
    {
        public override void DataReader(ModbusMaster master)
        {
            try
            {
                ushort[] Voltage_Current_Unit = master.ReadHoldingRegisters(ID, 21, 1);
                ushort[] Master_Unit = master.ReadHoldingRegisters(ID, 34, 1);
                int Voltage_Unit = MathClass.work2to10(MathClass.work10to2(Voltage_Current_Unit[0]).Substring(0, 8));//電壓單位換算
                int Current_Unit = MathClass.work2to10(MathClass.work10to2(Voltage_Current_Unit[0]).Substring(8, 8));//電流單位換算
                int Unit = MathClass.work2to10(MathClass.work10to2(Master_Unit[0]).Substring(8, 8)); //kVA、kVAR、kW、kWh
                ushort[] kwhValue = master.ReadHoldingRegisters(ID, 1, 2);//kWh
                ushort[] Value = master.ReadHoldingRegisters(ID, 12, 9); //電壓電流
                ushort[] Value1 = master.ReadHoldingRegisters(ID, 22, 12);//kVA、kVAR、kW
                ushort[] HzValue = master.ReadHoldingRegisters(ID, 35, 1);//頻率
                ushort[] PFValue = master.ReadHoldingRegisters(ID, 43, 1);//功率因數
                #region 電壓單位換算
                int Index = 0;
                switch (Voltage_Unit)//電壓單位換算
                {
                    case 1://(0.1mV)
                        {
                            Rv = Value[Index] * 0.0001; Index++;
                            Sv = Value[Index] * 0.0001; Index++;
                            Tv = Value[Index] * 0.0001; Index += 4;
                            RSv = Value[Index] * 0.0001; Index++;
                            STv = Value[Index] * 0.0001; Index++;
                            TRv = Value[Index] * 0.0001;
                        }
                        break;
                    case 2:
                        {
                            Rv = Value[Index] * 0.001; Index++;
                            Sv = Value[Index] * 0.001; Index++;
                            Tv = Value[Index] * 0.001; Index += 4;
                            RSv = Value[Index] * 0.001; Index++;
                            STv = Value[Index] * 0.001; Index++;
                            TRv = Value[Index] * 0.001;
                        }
                        break;
                    case 4:
                        {
                            Rv = Value[Index] * 0.01; Index++;
                            Sv = Value[Index] * 0.01; Index++;
                            Tv = Value[Index] * 0.01; Index += 4;
                            RSv = Value[Index] * 0.01; Index++;
                            STv = Value[Index] * 0.01; Index++;
                            TRv = Value[Index] * 0.01;
                        }
                        break;
                    case 8:
                        {
                            Rv = Value[Index] * 0.1; Index++;
                            Sv = Value[Index] * 0.1; Index++;
                            Tv = Value[Index] * 0.1; Index += 4;
                            RSv = Value[Index] * 0.1; Index++;
                            STv = Value[Index] * 0.1; Index++;
                            TRv = Value[Index] * 0.1;
                        }
                        break;
                    case 16:
                        {
                            Rv = Value[Index] * 1; Index++;
                            Sv = Value[Index] * 1; Index++;
                            Tv = Value[Index] * 1; Index += 4;
                            RSv = Value[Index] * 1; Index++;
                            STv = Value[Index] * 1; Index++;
                            TRv = Value[Index] * 1;
                        }
                        break;
                    case 32:
                        {
                            Rv = Value[Index] * 10; Index++;
                            Sv = Value[Index] * 10; Index++;
                            Tv = Value[Index] * 10; Index += 4;
                            RSv = Value[Index] * 10; Index++;
                            STv = Value[Index] * 10; Index++;
                            TRv = Value[Index] * 10;
                        }
                        break;
                }
                #endregion
                #region 電流單位換算
                Index = 3;
                switch (Current_Unit)//電流單位換算
                {
                    case 1:
                        {
                            RA = Value[Index] * 0.0001; Index++;
                            SA = Value[Index] * 0.0001; Index++;
                            TA = Value[Index] * 0.0001;
                        }
                        break;
                    case 2:
                        {
                            RA = Value[Index] * 0.001; Index++;
                            SA = Value[Index] * 0.001; Index++;
                            TA = Value[Index] * 0.001;
                        }
                        break;
                    case 4:
                        {
                            RA = Value[Index] * 0.01; Index++;
                            SA = Value[Index] * 0.01; Index++;
                            TA = Value[Index] * 0.01;
                        }
                        break;
                    case 8:
                        {
                            RA = Value[Index] * 0.1; Index++;
                            SA = Value[Index] * 0.1; Index++;
                            TA = Value[Index] * 0.1;
                        }
                        break;
                    case 16:
                        {
                            RA = Value[Index] * 1; Index++;
                            SA = Value[Index] * 1; Index++;
                            TA = Value[Index] * 1;
                        }
                        break;
                    case 32:
                        {
                            RA = Value[Index] * 10; Index++;
                            SA = Value[Index] * 10; Index++;
                            TA = Value[Index] * 10;
                        }
                        break;
                }
                #endregion
                #region kVA、kVAR、kW、kWh單位換算
                Index = 0;
                switch (Unit)
                {
                    case 1:
                        {
                            kVA_A = Value1[Index] * 0.0001; Index++;
                            kVA_B = Value1[Index] * 0.0001; Index++;
                            kVA_C = Value1[Index] * 0.0001; Index++;
                            kVA = Value1[Index] * 0.0001; Index++;
                            kVAR_A = Value1[Index] * 0.0001; Index++;
                            kVAR_B = Value1[Index] * 0.0001; Index++;
                            kVAR_C = Value1[Index] * 0.0001; Index++;
                            kVAR = Value1[Index] * 0.0001; Index++;
                            kW_A = Value1[Index] * 0.0001; Index++;
                            kW_B = Value1[Index] * 0.0001; Index++;
                            kW_C = Value1[Index] * 0.0001; Index++;
                            kW = Value1[Index] * 0.0001;
                            kWh = MathClass.work16to10(kwhValue[0], kwhValue[1]) * 0.0001;
                        }
                        break;
                    case 2:
                        {
                            kVA_A = Value1[Index] * 0.001; Index++;
                            kVA_B = Value1[Index] * 0.001; Index++;
                            kVA_C = Value1[Index] * 0.001; Index++;
                            kVA = Value1[Index] * 0.001; Index++;
                            kVAR_A = Value1[Index] * 0.001; Index++;
                            kVAR_B = Value1[Index] * 0.001; Index++;
                            kVAR_C = Value1[Index] * 0.001; Index++;
                            kVAR = Value1[Index] * 0.001; Index++;
                            kW_A = Value1[Index] * 0.001; Index++;
                            kW_B = Value1[Index] * 0.001; Index++;
                            kW_C = Value1[Index] * 0.001; Index++;
                            kW = Value1[Index] * 0.001;
                            kWh = MathClass.work16to10(kwhValue[0], kwhValue[1]) * 0.001;
                        }
                        break;
                    case 4:
                        {
                            kVA_A = Value1[Index] * 0.01; Index++;
                            kVA_B = Value1[Index] * 0.01; Index++;
                            kVA_C = Value1[Index] * 0.01; Index++;
                            kVA = Value1[Index] * 0.01; Index++;
                            kVAR_A = Value1[Index] * 0.01; Index++;
                            kVAR_B = Value1[Index] * 0.01; Index++;
                            kVAR_C = Value1[Index] * 0.01; Index++;
                            kVAR = Value1[Index] * 0.01; Index++;
                            kW_A = Value1[Index] * 0.01; Index++;
                            kW_B = Value1[Index] * 0.01; Index++;
                            kW_C = Value1[Index] * 0.01; Index++;
                            kW = Value1[Index] * 0.01;
                            kWh = MathClass.work16to10(kwhValue[0], kwhValue[1]) * 0.01;
                        }
                        break;
                    case 8:
                        {
                            kVA_A = Value1[Index] * 0.1; Index++;
                            kVA_B = Value1[Index] * 0.1; Index++;
                            kVA_C = Value1[Index] * 0.1; Index++;
                            kVA = Value1[Index] * 0.1; Index++;
                            kVAR_A = Value1[Index] * 0.1; Index++;
                            kVAR_B = Value1[Index] * 0.1; Index++;
                            kVAR_C = Value1[Index] * 0.1; Index++;
                            kVAR = Value1[Index] * 0.1; Index++;
                            kW_A = Value1[Index] * 0.1; Index++;
                            kW_B = Value1[Index] * 0.1; Index++;
                            kW_C = Value1[Index] * 0.1; Index++;
                            kW = Value1[Index] * 0.1;
                            kWh = MathClass.work16to10(kwhValue[0], kwhValue[1]) * 0.1;
                        }
                        break;
                    case 16:
                        {
                            kVA_A = Value1[Index] * 1; Index++;
                            kVA_B = Value1[Index] * 1; Index++;
                            kVA_C = Value1[Index] * 1; Index++;
                            kVA = Value1[Index] * 1; Index++;
                            kVAR_A = Value1[Index] * 1; Index++;
                            kVAR_B = Value1[Index] * 1; Index++;
                            kVAR_C = Value1[Index] * 1; Index++;
                            kVAR = Value1[Index] * 1; Index++;
                            kW_A = Value1[Index] * 1; Index++;
                            kW_B = Value1[Index] * 1; Index++;
                            kW_C = Value1[Index] * 1; Index++;
                            kW = Value1[Index] * 1;
                            kWh = MathClass.work16to10(kwhValue[0], kwhValue[1]) * 1;
                        }
                        break;
                    case 32:
                        {
                            kVA_A = Value1[Index] * 10; Index++;
                            kVA_B = Value1[Index] * 10; Index++;
                            kVA_C = Value1[Index] * 10; Index++;
                            kVA = Value1[Index] * 10; Index++;
                            kVAR_A = Value1[Index] * 10; Index++;
                            kVAR_B = Value1[Index] * 10; Index++;
                            kVAR_C = Value1[Index] * 10; Index++;
                            kVAR = Value1[Index] * 10; Index++;
                            kW_A = Value1[Index] * 10; Index++;
                            kW_B = Value1[Index] * 10; Index++;
                            kW_C = Value1[Index] * 10; Index++;
                            kW = Value1[Index] * 10;
                            kWh = MathClass.work16to10(kwhValue[0], kwhValue[1]) * 10;
                        }
                        break;
                    case 64:
                        {
                            kVA_A = Value1[Index] * 100; Index++;
                            kVA_B = Value1[Index] * 100; Index++;
                            kVA_C = Value1[Index] * 100; Index++;
                            kVA = Value1[Index] * 100; Index++;
                            kVAR_A = Value1[Index] * 100; Index++;
                            kVAR_B = Value1[Index] * 100; Index++;
                            kVAR_C = Value1[Index] * 100; Index++;
                            kVAR = Value1[Index] * 100; Index++;
                            kW_A = Value1[Index] * 100; Index++;
                            kW_B = Value1[Index] * 100; Index++;
                            kW_C = Value1[Index] * 100; Index++;
                            kW = Value1[Index] * 100;
                            kWh = MathClass.work16to10(kwhValue[0], kwhValue[1]) * 100;
                        }
                        break;
                }
                #endregion
                HZ = HzValue[0] * 01;
                PF = PFValue[0] * 0.001;
                ConnectFlag = true;
            }
            catch (Exception ex)
            {
                ConnectFlag = false;
                Log.Error(ex, $"TWC-CPM4解析異常、通訊編號: {GatewayIndex}、設備編號: {DeviceIndex}");
            }
        }
    }
}
