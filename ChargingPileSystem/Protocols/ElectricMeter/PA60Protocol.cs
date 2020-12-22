using Modbus.Device;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingPileSystem.Protocols.ElectricMeter
{
    public class PA60Protocol : MultiCircuitElectricMeterData
    {
        public override void DataReader(ModbusMaster master)
        {
            try
            {
                ushort[] Loop1 = master.ReadInputRegisters(ID, 4096, 82);
                ushort[] Loop2 = master.ReadInputRegisters(ID, 4352, 82);
                ushort[] Loop3 = master.ReadInputRegisters(ID, 4608, 82);
                ushort[] Loop4 = master.ReadInputRegisters(ID, 4864, 82);
                Analysis(Loop1, 0);
                Analysis(Loop2, 1);
                Analysis(Loop3, 2);
                Analysis(Loop4, 3);
                ConnectFlag = true;
            }
            catch (Exception ex)
            {
                ConnectFlag = false;
                Log.Error(ex, $"PA60解析異常、通訊編號: {GatewayIndex}、設備編號: {DeviceIndex}");
            }
        }
        private void Analysis(ushort[] data, int Loop)
        {
            int Index = 0;

            Rv[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            RSv[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            RA[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            R_kW[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            R_kVAR[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            R_kVA[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            R_PF[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            R_kWh[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            R_kVARh[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            R_kVAh[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;

            Sv[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;//20
            STv[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            SA[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            S_kW[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            S_kVAR[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            S_kVA[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            S_PF[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            S_kWh[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            S_kVARh[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            S_kVAh[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;

            Tv[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            TRv[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            TA[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            T_kW[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            T_kVAR[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            T_kVA[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            T_PF[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            T_kWh[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            T_kVARh[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            T_kVAh[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;

            vn[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            v[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            A[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            kW[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            kVAR[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            kVA[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            PF[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            kWh[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            kVARh[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            kVAh[Loop] = MathClass.work16to754(data[Index + 1], data[Index]); Index += 2;
            HZ[Loop] = MathClass.work16to754(data[Index + 1], data[Index]);
        }
    }
}
