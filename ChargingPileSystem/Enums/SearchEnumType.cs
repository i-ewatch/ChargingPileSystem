using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingPileSystem.Enums
{
    /// <summary>
    /// 資料庫查詢類型
    /// </summary>
    public enum SearchEnumType
    {
        /// <summary>
        /// 本日累積用電
        /// </summary>
        NowDay,
        /// <summary>
        /// 昨日累積用電
        /// </summary>
        AfterDay,
        /// <summary>
        /// 本月累積用電
        /// </summary>
        NowMonth,
        /// <summary>
        /// 總累積用電
        /// </summary>
        Total
    }
}
