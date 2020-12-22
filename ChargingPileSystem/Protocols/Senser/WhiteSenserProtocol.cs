using Modbus.Device;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingPileSystem.Protocols.Senser
{
   public class WhiteSenserProtocol:SenserData
    {
        public override void DataReader(ModbusMaster master)
        {
            try
            {
                ushort[] Value = master.ReadHoldingRegisters(ID, 0, 2);
                ConnectFlag = true;
                if (Value[1] > 32767)
                {
                    Temperature = (Value[1] - 65536) / 10F;
                }
                else
                {
                    Temperature = Value[1] / 10F;
                }
                Humidity = Value[0] / 10F;
            }
            catch (Exception ex)
            {
                ConnectFlag = false;
                Log.Error(ex, $"白色溫溼度感測器解析異常、通訊編號: {GatewayIndex}、設備編號: {DeviceIndex}");
            }
        }
    }
}
