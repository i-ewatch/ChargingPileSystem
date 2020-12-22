using ChargingPileSystem.Configuration;
using ChargingPileSystem.Enums;
using DevExpress.XtraBars.Navigation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChargingPileSystem.Controllers
{
    public class ButtonControl
    {
        /// <summary>
        /// 切換畫面使用
        /// </summary>
        public NavigationFrame navigationFrame { get; set; }
        /// <summary>
        /// 目前畫面編號
        /// </summary>
        public int ViewIndex { get; set; }
        /// <summary>
        /// 繼承主視窗
        /// </summary>
        public Form1 Form1 { get; set; }
        /// <summary>
        /// 所有項目
        /// </summary>
        private List<AccordionControlElement> AccordionControlElements = new List<AccordionControlElement>();
        /// <summary>
        /// 按鈕編號
        /// </summary>
        private int ButtonIndex = 0;
        #region 按鈕建立
        /// <summary>
        /// 按鈕建立
        /// </summary>
        /// <param name="buttonSetting">按鈕設定檔</param>
        public void AccordionLoad(AccordionControl accordionControl1, ButtonSetting buttonSetting)
        {
            accordionControl1.Clear();
            accordionControl1.AllowItemSelection = true;//按鈕聚焦
            foreach (var item in buttonSetting.ButtonGroupSettings)
            {
                ButtonEnumType ButtonEnumType = (ButtonEnumType)item.ButtonEnumType;
                switch (ButtonEnumType)
                {
                    case ButtonEnumType.Group: // 群組
                        {
                            //
                            //列表
                            //
                            AccordionControlElement[] ButtonItem = new AccordionControlElement[item.ButtonItemSettings.Count];//建立列表
                            for (int i = 0; i < item.ButtonItemSettings.Count; i++)
                            {
                                ButtonItem[i] = new AccordionControlElement()
                                {
                                    Style = ElementStyle.Item, //設定列表類型
                                    Name = item.ButtonItemSettings[i].ItemName, // 設定顯示名稱
                                    Tag = ButtonIndex,//自訂TAG
                                    Text = item.ButtonItemSettings[i].ItemName,//縮小名稱
                                };
                                AccordionControlElements.Add(ButtonItem[i]);
                                ButtonIndex++;
                            }
                            //
                            // 群組
                            //
                            AccordionControlElement Group = new AccordionControlElement();//建立群組
                            Group.Expanded = false;//不顯示列表
                            Group.Name = item.GroupName;// 設定顯示名稱
                            Group.Text = item.GroupName;//縮小名稱
                                                        //Group.ImageOptions.Image= System.Drawing.Bitmap//加入ICON
                            /*設定字體*/
                            Group.Appearance.Normal.FontStyleDelta = FontStyle.Bold;
                            Group.Appearance.Normal.Name = "微軟正黑體";
                            Group.Appearance.Normal.FontSizeDelta = 10;
                            Group.Appearance.Hovered.FontStyleDelta = FontStyle.Bold;
                            Group.Appearance.Hovered.Name = "微軟正黑體";
                            Group.Appearance.Hovered.FontSizeDelta = 10;
                            Group.Appearance.Pressed.FontStyleDelta = FontStyle.Bold;
                            Group.Appearance.Pressed.Name = "微軟正黑體";
                            Group.Appearance.Pressed.FontSizeDelta = 10;
                            Group.Elements.AddRange(ButtonItem);//將列表加入至群組
                            accordionControl1.Elements.Add(Group);//將群組加入至控制物件
                                                                  //
                                                                  //列表
                                                                  //
                            for (int i = 0; i < item.ButtonItemSettings.Count; i++)
                            {
                                /*設定字體*/
                                ButtonItem[i].Appearance.Normal.FontStyleDelta = FontStyle.Bold;
                                ButtonItem[i].Appearance.Normal.Name = "微軟正黑體";
                                ButtonItem[i].Appearance.Normal.FontSizeDelta = 12;
                                ButtonItem[i].Appearance.Hovered.FontStyleDelta = FontStyle.Bold;
                                ButtonItem[i].Appearance.Hovered.Name = "微軟正黑體";
                                ButtonItem[i].Appearance.Hovered.FontSizeDelta = 12;
                                ButtonItem[i].Appearance.Pressed.FontStyleDelta = FontStyle.Bold;
                                ButtonItem[i].Appearance.Pressed.Name = "微軟正黑體";
                                ButtonItem[i].Appearance.Pressed.FontSizeDelta = 12;
                            }
                        }
                        break;
                    case ButtonEnumType.Item: //列表
                        {
                            AccordionControlElement ButtonItem = new AccordionControlElement()
                            {
                                Style = ElementStyle.Item,
                                Name = item.ButtonItemSettings[0].ItemName,
                                Tag = ButtonIndex,
                                Text = item.ButtonItemSettings[0].ItemName,
                            };
                            ButtonItem.Appearance.Normal.FontStyleDelta = FontStyle.Bold;
                            ButtonItem.Appearance.Normal.Name = "微軟正黑體";
                            ButtonItem.Appearance.Normal.FontSizeDelta = 12;
                            ButtonItem.Appearance.Hovered.FontStyleDelta = FontStyle.Bold;
                            ButtonItem.Appearance.Hovered.Name = "微軟正黑體";
                            ButtonItem.Appearance.Hovered.FontSizeDelta = 12;
                            ButtonItem.Appearance.Pressed.FontStyleDelta = FontStyle.Bold;
                            ButtonItem.Appearance.Pressed.Name = "微軟正黑體";
                            ButtonItem.Appearance.Pressed.FontSizeDelta = 12;
                            accordionControl1.Elements.Add(ButtonItem);
                            AccordionControlElements.Add(ButtonItem);
                            ButtonIndex++;
                        }
                        break;
                }
            }
            accordionControl1.OptionsMinimizing.AllowMinimizeMode = DevExpress.Utils.DefaultBoolean.False; // 鎖定視窗
            accordionControl1.ElementClick += new ElementClickEventHandler(this.accordionControl1_ElementClick);//按鈕事件
            accordionControl1.SelectedElement = AccordionControlElements[0];//觸發初始按鈕
        }
        #endregion

        #region 按鈕事件
        /// <summary>
        /// 按鈕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accordionControl1_ElementClick(object sender, ElementClickEventArgs e)
        {
            if (e.Element.Style == DevExpress.XtraBars.Navigation.ElementStyle.Group) return;
            if (e.Element.Tag == null) return;
            ViewIndex = Convert.ToInt32(e.Element.Tag);
            if (navigationFrame.Pages.Count > Convert.ToInt32(e.Element.Tag))
            {
                navigationFrame.SelectedPageIndex = Convert.ToInt32(e.Element.Tag);
            }
        }
        #endregion
    }
}
