using ChargingPileSystem.Protocols.ElectricMeter;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChargingPileSystem.Components
{
    public partial class SqlComponent : Field4Component
    {
        public SqlComponent()
        {
            InitializeComponent();
        }

        public SqlComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void AfterMyWorkStateChanged(object sender, EventArgs e)
        {
            if (myWorkState)
            {
                ReadThread = new Thread(SqlRecord);
                ReadThread.Start();
            }
            else
            {
                if (ReadThread != null)
                {
                    ReadThread.Abort();
                }
            }
        }
        public void SqlRecord()
        {
            while (myWorkState)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(ReadTime);
                if (timeSpan.TotalSeconds >= 30)
                {
                    try
                    {
                        if (AbsProtocols.Count > 0)
                        {
                            foreach (var item in AbsProtocols)
                            {
                                if (item.ConnectFlag)
                                {
                                    PhaseEnumType = (Enums.PhaseEnumType)item.PhaseEnumType;
                                    switch (PhaseEnumType)
                                    {
                                        case Enums.PhaseEnumType.ThreePhase:
                                            {
                                                ElectricMeterData data = (ElectricMeterData)item;
                                                SqlMethod.Insert_ThreePhaseElectricMeter(data);
                                            }
                                            break;
                                        case Enums.PhaseEnumType.SinglePhase:
                                            {
                                                MultiCircuitElectricMeterData data = (MultiCircuitElectricMeterData)item;
                                                SqlMethod.Insert_SinglePhaseElectricMeter(data);
                                            }
                                            break;
                                    }
                                }
                            }
                            ComponentFlag = false;
                        }
                    }
                    catch (ThreadAbortException) { }
                    catch (Exception ex) { ComponentFlag = true; ErrorString = "資料庫連結失敗!"; Log.Error(ex, "資料庫紀錄失敗"); }
                }
                else
                {
                    Thread.Sleep(80);
                }
            }
        }
    }
}
