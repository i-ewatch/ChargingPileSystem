using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingPileSystem.Configuration
{
    #region  按鈕設定Json
    /// <summary>
    /// 按鈕設定
    /// </summary>
    public class ButtonSetting
    {
        public List<ButtonGroupSetting> ButtonGroupSettings = new List<ButtonGroupSetting>();
    }
    /// <summary>
    /// 按鈕群組Data
    /// </summary>
    public class ButtonGroupSetting
    {
        /// <summary>
        /// 按鈕類型
        /// <para>1 = 不建立群組</para>
        /// <para>0 = 建立群組</para>
        /// </summary>
        public int ButtonEnumType { get; set; }
        /// <summary>
        /// 群組名稱
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 群組類型
        /// </summary>
        public int GroupTag { get; set; }
        /// <summary>
        /// 列表名稱
        /// </summary>
        public List<ButtonItemSetting> ButtonItemSettings { get; set; } = new List<ButtonItemSetting>();
    }
    /// <summary>
    /// 按鈕列表Data
    /// </summary>
    public class ButtonItemSetting
    {
        /// <summary>
        /// 列表名稱
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 按鈕列表類型
        /// </summary>
        public int ButtonItemEnumType { get; set; }
    }
    #endregion
}
