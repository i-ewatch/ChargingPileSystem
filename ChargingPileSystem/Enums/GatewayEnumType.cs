using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingPileSystem.Enums
{
    public enum GatewayEnumType
    {
        /// <summary>
        /// RS485
        /// </summary>
        ModbusRTU,
        /// <summary>
        /// TCP/IP
        /// </summary>
        ModbusTCP,
    }
}
