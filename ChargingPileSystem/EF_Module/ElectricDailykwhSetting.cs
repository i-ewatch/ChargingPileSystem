namespace ChargingPileSystem.EF_Modules
{
    public partial class ElectricDailykwhSetting
    {
        public decimal SummerOftenContract { get; set; }
        public decimal NonSummerOftenContract { get; set; }
        public decimal SummerHalfPeakContract { get; set; }
        public decimal NonSummerHalfPeakContract { get; set; }
        public decimal SummerSatHalfPeakContract { get; set; }
        public decimal NonSummerSatHalfPeakContract { get; set; }
        public decimal SummerOffPeakContract { get; set; }
        public decimal NonSummerOffPeakContract { get; set; }
        public decimal SummerPeakPrice { get; set; }
        public decimal SummerHalfPeakPrice { get; set; }
        public decimal NonSummerHalfPeakPrice { get; set; }
        public decimal SummerSatHalfPeakPrice { get; set; }
        public decimal NonSummerSatHalfPeakPrice { get; set; }
        public decimal SummerOffPeakPrice { get; set; }
        public decimal NonSummerOffPeakPrice { get; set; }

    }
}
