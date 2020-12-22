using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingPileSystem.Enums
{
    /// <summary>
    /// 環境數值類型
    /// </summary>
    public enum EnvironmentalValueEnumType
    {
        /// <summary>
        /// 溫度
        /// </summary>
        Temperature,
        /// <summary>
        /// 濕度
        /// </summary>
        Humidity,
        /// <summary>
        /// 天氣現象
        /// </summary>
        WxName,
        /// <summary>
        /// 降雨機率
        /// </summary>
        PoP,
        /// <summary>
        /// 舒適度
        /// </summary>
        CIName
    }
}
