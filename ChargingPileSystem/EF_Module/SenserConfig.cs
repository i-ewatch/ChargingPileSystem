using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingPileSystem.EF_Modules
{
    public partial class SenserConfig
    {
        public int GatewayIndex { get; set; }
        public int DeviceIndex { get; set; }
        public int DiceID { get; set; }
        public int SenserEnumType { get; set; }
        public string DeviceName { get; set; }

    }
}
