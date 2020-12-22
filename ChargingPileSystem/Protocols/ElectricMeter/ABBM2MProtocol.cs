using Modbus.Device;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingPileSystem.Protocols.ElectricMeter
{
    public class ABBM2MProtocol : ElectricMeterData
    {
        public override void DataReader(ModbusMaster master)
        {
            try
            {
                int Index = 0;
                ushort[] value = master.ReadHoldingRegisters(ID, 4096, 46);
                ushort[] value1 = master.ReadHoldingRegisters(ID, 4142, 20);
                _ = MathClass.work16to10(value[Index], value[Index + 1]); Index += 2;
                Rv = MathClass.work16to10(value[Index], value[Index + 1]); Index += 2;
                Sv = MathClass.work16to10(value[Index], value[Index + 1]); Index += 2;
                Tv = MathClass.work16to10(value[Index], value[Index + 1]); Index += 2;
                RSv = MathClass.work16to10(value[Index], value[Index + 1]); Index += 2;
                STv = MathClass.work16to10(value[Index], value[Index + 1]); Index += 2;
                TRv = MathClass.work16to10(value[Index], value[Index + 1]); Index += 2;
                _ = MathClass.work16to10(value[Index], value[Index + 1]) * 0.001F; Index += 2;
                RA = MathClass.work16to10(value[Index], value[Index + 1]) * 0.001F; Index += 2;
                SA = MathClass.work16to10(value[Index], value[Index + 1]) * 0.001F; Index += 2;
                TA = MathClass.work16to10(value[Index], value[Index + 1]) * 0.001F; Index += 2;
                PF = MathClass.work16to10(value[Index], value[Index + 1]) * 0.001F; Index += 2;
                PF_A = MathClass.work16to10(value[Index], value[Index + 1]) * 0.001F; Index += 2;        //PFE L1
                PF_B = MathClass.work16to10(value[Index], value[Index + 1]) * 0.001F; Index += 2;        //PFE L2
                PF_C = MathClass.work16to10(value[Index], value[Index + 1]) * 0.001F; Index += 2;        //PFE L3
                _ = MathClass.work16to10(value[Index], value[Index + 1]) * 0.001F; Index += 2;        //三相相位角
                RV_Angle = MathClass.work16to10(value[Index], value[Index + 1]) * 0.001F; Index += 2;        //三相相位角 L1
                SV_Angle = MathClass.work16to10(value[Index], value[Index + 1]) * 0.001F; Index += 2;        //三相相位角 L2
                TV_Angle = MathClass.work16to10(value[Index], value[Index + 1]) * 0.001F; Index += 2;        //三相相位角 L3
                kVA = MathClass.work16to10(value[Index], value[Index + 1]) * 0.001F; Index += 2;
                kVA_A = MathClass.work16to10(value[Index], value[Index + 1]) * 0.001F; Index += 2;        //三相視在功率 L1
                kVA_B = MathClass.work16to10(value[Index], value[Index + 1]) * 0.001F; Index += 2;        //三相視在功率 L2
                kVA_C = MathClass.work16to10(value[Index], value[Index + 1]) * 0.001F;                //三相視在功率 L3
                Index = 0;
                kW = MathClass.work16to10(value1[Index], value1[Index + 1]) * 0.001F; Index += 2;
                kW_A = MathClass.work16to10(value1[Index], value1[Index + 1]) * 0.001F; Index += 2;            //有效功率 L1
                kW_B = MathClass.work16to10(value1[Index], value1[Index + 1]) * 0.001F; Index += 2;            //有效功率 L2
                kW_C = MathClass.work16to10(value1[Index], value1[Index + 1]) * 0.001F; Index += 2;            //有效功率 L3
                kVAR = MathClass.work16to10(value1[Index], value1[Index + 1]) * 0.001F; Index += 2;
                kVAR_A = MathClass.work16to10(value1[Index], value1[Index + 1]) * 0.001F; Index += 2;            //無效功率 L1
                kVAR_B = MathClass.work16to10(value1[Index], value1[Index + 1]) * 0.001F; Index += 2;            //無效功率 L2
                kVAR_C = MathClass.work16to10(value1[Index], value1[Index + 1]) * 0.001F; Index += 2;            //無效功率 L3
                kWh = MathClass.work16to10(value1[Index], value1[Index + 1]) * 0.001F; Index += 2;
                kVARh = MathClass.work16to10(value1[Index], value1[Index + 1]) * 0.001F;
            }
            catch (Exception ex)
            {
                ConnectFlag = false;
                Log.Error(ex, $"ABBM2M解析異常、通訊編號: {GatewayIndex}、設備編號: {DeviceIndex}");
            }
        }
    }
}
