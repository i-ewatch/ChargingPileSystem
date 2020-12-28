using System;

namespace ChargingPileSystem.EF_Modules
{
    public partial class SenserLog
    {
        public string ttime { get; set; }
        public DateTime ttimen { get; set; }
        public int GatewayIndex { get; set; }
        public int DeviceIndex { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal PM1 { get; set; }
        public decimal PM25 { get; set; }
        public decimal PM10 { get; set; }
        public decimal CO2 { get; set; }
        public decimal TVOC { get; set; }
        public decimal HCHO { get; set; }
        public decimal O3 { get; set; }
        public decimal CO { get; set; }
        public decimal Mold { get; set; }
        public decimal IAQ { get; set; }

    }
}
