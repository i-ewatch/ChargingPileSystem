using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingPileSystem.EF_Modules
{
    public partial class ElectricDailykwh
    {
        public string ttime { get; set; }
        public DateTime ttimen { get; set; }
        public int GatewayIndex { get; set; }
        public int DeviceIndex { get; set; }
        public int weekIndex { get; set; }
        public decimal PeakTimeStart1 { get; set; }
        public decimal PeakTimeEnd1 { get; set; }
        public decimal PeakTimeStart2 { get; set; }
        public decimal PeakTimeEnd2 { get; set; }
        public decimal PeakTimeStart3 { get; set; }
        public decimal PeakTimeEnd3 { get; set; }
        public decimal PeakTimeTotal { get; set; }
        public decimal PeakMoneyTotal { get; set; }
        public decimal HalfPeakTimeStart1 { get; set; }
        public decimal HalfPeakTimeEnd1 { get; set; }
        public decimal HalfPeakTimeStart2 { get; set; }
        public decimal HalfPeakTimeEnd2 { get; set; }
        public decimal HalfPeakTimeStart3 { get; set; }
        public decimal HalfPeakTimeEnd3 { get; set; }
        public decimal HalfPeakTimeTotal { get; set; }
        public decimal HalfPeakMoneyTotal { get; set; }
        public decimal OffPeakTimeStart1 { get; set; }
        public decimal OffPeakTimeEnd1 { get; set; }
        public decimal OffPeakTimeStart2 { get; set; }
        public decimal OffPeakTimeEnd2 { get; set; }
        public decimal OffPeakTimeStart3 { get; set; }
        public decimal OffPeakTimeEnd3 { get; set; }
        public decimal OffPeakTimeTotal { get; set; }
        public decimal OffPeakMoneyTotal { get; set; }
        public decimal Total { get; set; }
        public decimal MoneyTotal { get; set; }

    }
}
