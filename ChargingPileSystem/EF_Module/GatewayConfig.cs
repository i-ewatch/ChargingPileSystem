using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingPileSystem.EF_Module
{
    public partial class GatewayConfig
    {
        public int GatewayIndex { get; set; }
        public int GatewayEnumType { get; set; }
        public string Location { get; set; }
        public int Rate { get; set; }
        public string GatewayName { get; set; }
    }
}
