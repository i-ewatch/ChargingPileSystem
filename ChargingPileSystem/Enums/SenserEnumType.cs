using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingPileSystem.Enums
{
    public  enum SenserEnumType
    {
        /// <summary>
        /// 溫濕度感測器(黑色485)
        /// </summary>
        BlackSenser,
        /// <summary>
        /// 溫濕度感測器(白色485)
        /// </summary>
        WhiteSenser,
        /// <summary>
        /// 氣象局API
        /// </summary>
        WeatherAPI
    }
}
