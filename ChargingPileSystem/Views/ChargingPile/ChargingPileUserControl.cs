using DevExpress.XtraEditors;
using ChargingPileSystem.EF_Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChargingPileSystem.Methods;

namespace ChargingPileSystem.Views.ChargingPile
{
    public partial class ChargingPileUserControl : Field4UserControl
    {
        private List<Field4UserControl> SubMeters { get; set; } = new List<Field4UserControl>();
        private int SubMeterIndex { get; set; } = 0;
        public ChargingPileUserControl(List<ElectricConfig> electricConfigs, SqlMethod sqlMethod,Form1 form1)
        {
            InitializeComponent();
            foreach (var item in electricConfigs)
            {
                if (!item.TotalMeterFlag)//子電表
                {
                    SubMeterUserControl subMeter = new SubMeterUserControl(item, sqlMethod, form1) { SqlMethod = sqlMethod, Location = new Point(2 + 370 * (SubMeterIndex % 5), 2 + 230 * (SubMeterIndex / 5))}; SubMeterIndex++;
                    SubMeters.Add(subMeter);
                    xtraScrollableControl1.Controls.Add(subMeter);
                }
            }
        }
        public override void TextChange()
        {
            if (ElectricConfigs != null)
            {
                var electricconfigs = ElectricConfigs.Where(g => g.TotalMeterFlag == false).ToList();
                if (electricconfigs.Count == SubMeterIndex)
                {
                    SubMeterIndex = 0;
                    foreach (var item in ElectricConfigs)
                    {
                        if (!item.TotalMeterFlag)
                        {
                            SubMeters[SubMeterIndex].AbsProtocols = AbsProtocols;
                            SubMeters[SubMeterIndex].ElectricConfig = item; 
                            SubMeters[SubMeterIndex].TextChange();
                            SubMeterIndex++;
                        }
                    }
                }
            }
        }
    }
}
