using Modbus.Device;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingPileSystem.Protocols.Senser
{
    public class BlackSenserProtocol:SenserData
    {
        public override void DataReader(ModbusMaster master)
        {
            try
            {
                ushort[] Value = master.ReadHoldingRegisters(ID, 0, 2);
                ConnectFlag = true;
                if (Value[0] > 32767)
                {
                    Temperature = (Value[0] - 65536) / 10F;
                }
                else
                {
                    Temperature = Value[0] / 10F;
                }
                Humidity = Value[1] / 10F;
            }
            catch (Exception ex)
            {
                ConnectFlag = false;
                Log.Error(ex, $"黑色溫濕度感測器解析異常、通訊編號: {GatewayIndex}、設備編號: {DeviceIndex}");
            }
        }
    }
}
