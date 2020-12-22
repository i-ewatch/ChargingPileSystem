using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingPileSystem.EF_Modules
{
    public partial class ThreePhaseElectricMeter
    {
        public string ttime { get; set; }
        public DateTime ttimen { get; set; }
        public int GatewayIndex { get; set; }
        public int DeviceIndex { get; set; }
        public decimal rv { get; set; }
        public decimal sv { get; set; }
        public decimal tv { get; set; }
        public decimal rsv { get; set; }
        public decimal stv { get; set; }
        public decimal trv { get; set; }
        public decimal ra { get; set; }
        public decimal sa { get; set; }
        public decimal ta { get; set; }
        public decimal kw { get; set; }
        public decimal kwh { get; set; }
        public decimal kvar { get; set; }
        public decimal kvarh { get; set; }
        public decimal pfe { get; set; }
        public decimal kva { get; set; }
        public decimal kvah { get; set; }
        public decimal hz { get; set; }

    }
}
