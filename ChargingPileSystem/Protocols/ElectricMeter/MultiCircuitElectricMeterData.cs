namespace ChargingPileSystem.Protocols.ElectricMeter
{
    public abstract class MultiCircuitElectricMeterData : AbsProtocol
    {
        /// <summary>
        /// R線電壓
        /// </summary>
        public double[] Rv { get; set; } = new double[4];
        /// <summary>
        /// S線電壓
        /// </summary>
        public double[] Sv { get; set; } = new double[4];
        /// <summary>
        /// T線電壓
        /// </summary>
        public double[] Tv { get; set; } = new double[4];
        /// <summary>
        /// 平均線電壓
        /// </summary>
        public double[] vn { get; set; } = new double[4];
        /// <summary>
        /// RS相電壓
        /// </summary>
        public double[] RSv { get; set; } = new double[4];
        /// <summary>
        /// ST相電壓
        /// </summary>
        public double[] STv { get; set; } = new double[4];
        /// <summary>
        /// TR相電壓
        /// </summary>
        public double[] TRv { get; set; } = new double[4];
        /// <summary>
        /// 平均相電壓
        /// </summary>
        public double[] v { get; set; } = new double[4];
        /// <summary>
        /// R相電流
        /// </summary>
        public double[] RA { get; set; } = new double[4];
        /// <summary>
        /// S相電流 
        /// </summary>
        public double[] SA { get; set; } = new double[4];
        /// <summary>
        /// T相電流
        /// </summary>
        public double[] TA { get; set; } = new double[4];
        /// <summary>
        /// 頻均相電流
        /// </summary>
        public double[] A { get; set; } = new double[4];
        /// <summary>
        /// 頻率
        /// </summary>
        public double[] HZ { get; set; } = new double[4];
        /// <summary>
        /// 平均功率因數
        /// </summary>
        public double[] R_PF { get; set; } = new double[4];
        /// <summary>
        /// 平均功率因數
        /// </summary>
        public double[] S_PF { get; set; } = new double[4];
        /// <summary>
        /// 平均功率因數
        /// </summary>
        public double[] T_PF { get; set; } = new double[4];
        /// <summary>
        /// 平均功率因數
        /// </summary>
        public double[] PF { get; set; } = new double[4];
        /// <summary>
        /// 虛功率
        /// </summary>
        public double[] R_kVAR { get; set; } = new double[4];
        /// <summary>
        /// 虛功率
        /// </summary>
        public double[] S_kVAR { get; set; } = new double[4];
        /// <summary>
        /// 虛功率
        /// </summary>
        public double[] T_kVAR { get; set; } = new double[4];
        /// <summary>
        /// 虛功率
        /// </summary>
        public double[] kVAR { get; set; } = new double[4];
        /// <summary>
        /// 累積虛功率
        /// </summary>
        public double[] R_kVARh { get; set; } = new double[4];
        /// <summary>
        /// 累積虛功率
        /// </summary>
        public double[] S_kVARh { get; set; } = new double[4];
        /// <summary>
        /// 累積虛功率
        /// </summary>
        public double[] T_kVARh { get; set; } = new double[4];
        /// <summary>
        /// 累積虛功率
        /// </summary>
        public double[] kVARh { get; set; } = new double[4];
        /// <summary>
        /// 視在功率
        /// </summary>
        public double[] R_kVA { get; set; } = new double[4];
        /// <summary>
        /// 視在功率
        /// </summary>
        public double[] S_kVA { get; set; } = new double[4];
        /// <summary>
        /// 視在功率
        /// </summary>
        public double[] T_kVA { get; set; } = new double[4];
        /// <summary>
        /// 視在功率
        /// </summary>
        public double[] kVA { get; set; } = new double[4];
        /// <summary>
        /// 累積視在功率
        /// </summary>
        public double[] R_kVAh { get; set; } = new double[4];
        /// <summary>
        /// 累積視在功率
        /// </summary>
        public double[] S_kVAh { get; set; } = new double[4];
        /// <summary>
        /// 累積視在功率
        /// </summary>
        public double[] T_kVAh { get; set; } = new double[4];
        /// <summary>
        /// 累積視在功率
        /// </summary>
        public double[] kVAh { get; set; } = new double[4];
        /// <summary>
        /// 即時功率
        /// </summary>
        public double[] R_kW { get; set; } = new double[4];
        /// <summary>
        /// 即時功率
        /// </summary>
        public double[] S_kW { get; set; } = new double[4];
        /// <summary>
        /// 即時功率
        /// </summary>
        public double[] T_kW { get; set; } = new double[4];
        /// <summary>
        /// 即時功率
        /// </summary>
        public double[] kW { get; set; } = new double[4];
        /// <summary>
        /// 累積功率
        /// </summary>
        public double[] R_kWh { get; set; } = new double[4];
        /// <summary>
        /// 累積功率
        /// </summary>
        public double[] S_kWh { get; set; } = new double[4];
        /// <summary>
        /// 累積功率
        /// </summary>
        public double[] T_kWh { get; set; } = new double[4];
        /// <summary>
        /// 累積功率
        /// </summary>
        public double[] kWh { get; set; } = new double[4];
    }
}
