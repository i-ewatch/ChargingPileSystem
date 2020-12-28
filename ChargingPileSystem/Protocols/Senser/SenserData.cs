using System;
using System.Collections.Generic;

namespace ChargingPileSystem.Protocols.Senser
{
    public abstract class SenserData : AbsProtocol
    {
        /// <summary>
        /// 溫度
        /// </summary>
        public float Temperature { get; set; }
        /// <summary>
        /// 濕度
        /// </summary>
        public float Humidity { get; set; }
        /// <summary>
        /// 超細懸浮微粒
        /// </summary>
        public float PM1 { get; set; }
        /// <summary>
        /// 懸浮微粒
        /// </summary>
        public float PM25 { get; set; }
        /// <summary>
        /// 細懸浮微粒
        /// </summary>
        public float PM10 { get; set; }
        /// <summary>
        /// 二氧化碳
        /// </summary>
        public float CO2 { get; set; }
        /// <summary>
        /// 揮發性有機物
        /// </summary>
        public float TVOC { get; set; }
        /// <summary>
        /// 甲醛
        /// </summary>
        public float HCHO { get; set; }
        /// <summary>
        /// 臭氧
        /// </summary>
        public float O3 { get; set; }
        /// <summary>
        /// 一氧化碳
        /// </summary>
        public float CO { get; set; }
        /// <summary>
        /// 黴菌
        /// </summary>
        public float Mold { get; set; }
        /// <summary>
        /// 室內指數
        /// </summary>
        public float IAQ { get; set; }
        /// <summary>
        /// 地區名稱
        /// </summary>
        public string LocationName { get; set; }
        /// <summary>
        /// 天氣現象
        /// </summary>
        public string WxName { get; set; }
        /// <summary>
        /// 天氣現象編號
        /// </summary>
        public string WxIndex { get; set; }
        /// <summary>
        /// 降雨機率
        /// </summary>
        public string PoP { get; set; }
        /// <summary>
        /// 舒適度
        /// </summary>
        public string CIName { get; set; }
        /// <summary>
        /// 最低溫度
        /// </summary>
        public string MinT { get; set; }
        /// <summary>
        /// 最高溫度
        /// </summary>
        public string MaxT { get; set; }
    }
    #region 天氣資訊回傳值
    /// <summary>
    /// 天氣資訊回傳值
    /// </summary>
    public class WeatherAnalysis
    {
        /// <summary>
        /// 查詢內容名稱
        /// </summary>
        public string datasetDescription;

        public List<Location> location = new List<Location>();
    }
    /// <summary>
    /// 地區名稱
    /// </summary>
    public class Location
    {
        /// <summary>
        /// 地區名稱
        /// </summary>
        public string locationName;
        /// <summary>
        /// 資訊
        /// </summary>
        public List<WeatherElement> weatherElement = new List<WeatherElement>();

    }
    public class WeatherElement
    {
        /// <summary>
        /// 狀態名稱
        /// </summary>
        public string elementName;
        /// <summary>
        /// 時間
        /// </summary>
        public List<Time> time = new List<Time>();
    }
    /// <summary>
    /// 時間
    /// </summary>
    public class Time
    {
        /// <summary>
        /// 起始時間
        /// </summary>
        public DateTime startTime;
        /// <summary>
        /// 結束時間
        /// </summary>
        public DateTime endTime;
        /// <summary>
        /// 內容
        /// </summary>
        public Parameter parameter;
    }
    /// <summary>
    /// 內容
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// 狀態名稱
        /// </summary>
        public string parameterName;
        /// <summary>
        /// 數值
        /// </summary>
        public string parameterValue;
        /// <summary>
        /// 單位
        /// </summary>
        public string parameterUnit;
    }
    #endregion
}
