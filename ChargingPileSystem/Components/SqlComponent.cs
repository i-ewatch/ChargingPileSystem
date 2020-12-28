using ChargingPileSystem.Protocols.ElectricMeter;
using Serilog;
using System;
using System.ComponentModel;
using System.Threading;
using ChargingPileSystem.Enums;

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
                                    if (item.ElectricEnumType != -1)
                                    {
                                        ElectricEnumType = (Enums.ElectricEnumType)item.ElectricEnumType;
                                        switch (ElectricEnumType)
                                        {
                                            case Enums.ElectricEnumType.PA310:
                                                {
                                                    PA310Protocol protocol = (PA310Protocol)item;
                                                    PhaseEnumType PhaseEnumType = (PhaseEnumType)item.PhaseEnumType;
                                                    switch (PhaseEnumType)
                                                    {
                                                        case Enums.PhaseEnumType.ThreePhase:
                                                            SqlMethod.Insert_ThreePhaseElectricMeter(protocol);
                                                            break;
                                                        case Enums.PhaseEnumType.SinglePhase:
                                                            SqlMethod.Insert_SinglePhaseElectricMeter(protocol);
                                                            break;
                                                    }
                                                }
                                                break;
                                            case Enums.ElectricEnumType.HC660:
                                                {
                                                    HC6600Protocol protocol = (HC6600Protocol)item;
                                                    PhaseEnumType PhaseEnumType = (PhaseEnumType)item.PhaseEnumType;
                                                    switch (PhaseEnumType)
                                                    {
                                                        case Enums.PhaseEnumType.ThreePhase:
                                                            SqlMethod.Insert_ThreePhaseElectricMeter(protocol);
                                                            break;
                                                        case Enums.PhaseEnumType.SinglePhase:
                                                            SqlMethod.Insert_SinglePhaseElectricMeter(protocol);
                                                            break;
                                                    }
                                                }
                                                break;
                                            case Enums.ElectricEnumType.CPM6:
                                                {
                                                    CPM6Protocol protocol = (CPM6Protocol)item;
                                                    PhaseEnumType PhaseEnumType = (PhaseEnumType)item.PhaseEnumType;
                                                    switch (PhaseEnumType)
                                                    {
                                                        case Enums.PhaseEnumType.ThreePhase:
                                                            SqlMethod.Insert_ThreePhaseElectricMeter(protocol);
                                                            break;
                                                        case Enums.PhaseEnumType.SinglePhase:
                                                            SqlMethod.Insert_SinglePhaseElectricMeter(protocol);
                                                            break;
                                                    }
                                                }
                                                break;
                                            case Enums.ElectricEnumType.PA60:
                                                {
                                                    PA60Protocol protocol = (PA60Protocol)item;
                                                    PhaseEnumType PhaseEnumType = (PhaseEnumType)item.PhaseEnumType;
                                                    switch (PhaseEnumType)
                                                    {
                                                        case Enums.PhaseEnumType.ThreePhase:
                                                            SqlMethod.Insert_ThreePhaseElectricMeter(protocol);
                                                            break;
                                                        case Enums.PhaseEnumType.SinglePhase:
                                                            SqlMethod.Insert_SinglePhaseElectricMeter(protocol);
                                                            break;
                                                    }
                                                }
                                                break;
                                            case Enums.ElectricEnumType.ABBM2M:
                                                {
                                                    ABBM2MProtocol protocol = (ABBM2MProtocol)item;
                                                    PhaseEnumType PhaseEnumType = (PhaseEnumType)item.PhaseEnumType;
                                                    switch (PhaseEnumType)
                                                    {
                                                        case Enums.PhaseEnumType.ThreePhase:
                                                            SqlMethod.Insert_ThreePhaseElectricMeter(protocol);
                                                            break;
                                                        case Enums.PhaseEnumType.SinglePhase:
                                                            SqlMethod.Insert_SinglePhaseElectricMeter(protocol);
                                                            break;
                                                    }
                                                }
                                                break;
                                            case Enums.ElectricEnumType.PM200:
                                                {
                                                    PM200Protocol protocol = (PM200Protocol)item;
                                                    SqlMethod.Insert_ThreePhaseElectricMeter(protocol);
                                                }
                                                break;
                                            case Enums.ElectricEnumType.TWCPM4:
                                                {
                                                    TWCPM4Protocol protocol = (TWCPM4Protocol)item;
                                                    PhaseEnumType PhaseEnumType = (PhaseEnumType)item.PhaseEnumType;
                                                    switch (PhaseEnumType)
                                                    {
                                                        case Enums.PhaseEnumType.ThreePhase:
                                                            SqlMethod.Insert_ThreePhaseElectricMeter(protocol);
                                                            break;
                                                        case Enums.PhaseEnumType.SinglePhase:
                                                            SqlMethod.Insert_SinglePhaseElectricMeter(protocol);
                                                            break;
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                            ComponentFlag = false;
                            ReadTime = DateTime.Now;
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
