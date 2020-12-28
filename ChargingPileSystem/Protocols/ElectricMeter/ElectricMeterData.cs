namespace ChargingPileSystem.Protocols.ElectricMeter
{
    public abstract class ElectricMeterData:AbsProtocol
    {
        /// <summary>
        /// R線電壓
        /// </summary>
        public double Rv { get; set; }
        /// <summary>
        /// S線電壓
        /// </summary>
        public double Sv { get; set; }
        /// <summary>
        /// T線電壓
        /// </summary>
        public double Tv { get; set; }
        /// <summary>
        /// RS相電壓
        /// </summary>
        public double RSv { get; set; }
        /// <summary>
        /// ST相電壓
        /// </summary>
        public double STv { get; set; }
        /// <summary>
        /// TR相電壓
        /// </summary>
        public double TRv { get; set; }
        /// <summary>
        /// R相電流
        /// </summary>
        public double RA { get; set; }
        /// <summary>
        /// S相電流 
        /// </summary>
        public double SA { get; set; }
        /// <summary>
        /// T相電流
        /// </summary>
        public double TA { get; set; }
        /// <summary>
        /// 頻率
        /// </summary>
        public double HZ { get; set; }
        /// <summary>
        /// 即時功率_A
        /// </summary>
        public double kW_A { get; set; }
        /// <summary>
        /// 即時功率_B
        /// </summary>
        public double kW_B { get; set; }
        /// <summary>
        /// 即時功率_C
        /// </summary>
        public double kW_C { get; set; }
        /// <summary>
        /// 即時功率
        /// </summary>
        public double kW { get; set; }
        /// <summary>
        /// 虛功率_A
        /// </summary>
        public double kVAR_A { get; set; }
        /// <summary>
        /// 虛功率_B
        /// </summary>
        public double kVAR_B { get; set; }
        /// <summary>
        /// 虛功率_C
        /// </summary>
        public double kVAR_C { get; set; }
        /// <summary>
        /// 虛功率
        /// </summary>
        public double kVAR { get; set; }
        /// <summary>
        /// 視在功率_A
        /// </summary>
        public double kVA_A { get; set; }
        /// <summary>
        /// 視在功率_B
        /// </summary>
        public double kVA_B { get; set; }
        /// <summary>
        /// 視在功率_C
        /// </summary>
        public double kVA_C { get; set; }
        /// <summary>
        /// 視在功率
        /// </summary>
        public double kVA { get; set; }
        /// <summary>
        /// 功率因數_A
        /// </summary>
        public double PF_A { get; set; }
        /// <summary>
        /// 功率因數_B
        /// </summary>
        public double PF_B { get; set; }
        /// <summary>
        /// 功率因數_C
        /// </summary>
        public double PF_C { get; set; }
        /// <summary>
        /// 平均功率因數
        /// </summary>
        public double PF { get; set; }
        /// <summary>
        /// R相電壓角度
        /// </summary>
        public double RV_Angle { get; set; }
        /// <summary>
        /// S相電壓角度
        /// </summary>
        public double SV_Angle { get; set; }
        /// <summary>
        /// T相電壓角度
        /// </summary>
        public double TV_Angle { get; set; }
        /// <summary>
        /// R相電流角度
        /// </summary>
        public double RA_Angle { get; set; }
        /// <summary>
        /// S相電流角度
        /// </summary>
        public double SA_Angle { get; set; }
        /// <summary>
        /// T相電流角度
        /// </summary>
        public double TA_Angle { get; set; }
        /// <summary>
        /// 累積功率_A
        /// </summary>
        public double kWh_A { get; set; }
        /// <summary>
        /// 累積功率_B
        /// </summary>
        public double kWh_B { get; set; }
        /// <summary>
        /// 累積功率_C
        /// </summary>
        public double kWh_C { get; set; }
        /// <summary>
        /// 累積功率
        /// </summary>
        public double kWh { get; set; }
        /// <summary>
        /// 累積虛功率_A
        /// </summary>
        public double kVARh_A { get; set; }
        /// <summary>
        /// 累積虛功率_B
        /// </summary>
        public double kVARh_B { get; set; }
        /// <summary>
        /// 累積虛功率_C
        /// </summary>
        public double kVARh_C { get; set; }
        /// <summary>
        /// 累積虛功率
        /// </summary>
        public double kVARh { get; set; }
        /// <summary>
        /// 累積視在功率_A
        /// </summary>
        public double kVAh_A { get; set; }
        /// <summary>
        /// 累積視在功率_B
        /// </summary>
        public double kVAh_B { get; set; }
        /// <summary>
        /// 累積視在功率_C
        /// </summary>
        public double kVAh_C { get; set; }
        /// <summary>
        /// 累積視在功率
        /// </summary>
        public double kVAh { get; set; }
    }
}
