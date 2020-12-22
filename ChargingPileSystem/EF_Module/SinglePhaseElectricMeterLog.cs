using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingPileSystem.EF_Modules
{
    public partial class SinglePhaseElectricMeterLog
    {
        public string ttime { get; set; }
        public DateTime ttimen { get; set; }
        public int GatewayIndex { get; set; }
        public int DeviceIndex { get; set; }
        public decimal v { get; set; }
        public decimal a { get; set; }
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
