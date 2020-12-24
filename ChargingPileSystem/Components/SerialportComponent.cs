using ChargingPileSystem.Enums;
using ChargingPileSystem.Protocols.ElectricMeter;
using Modbus.Device;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChargingPileSystem.Components
{
    public partial class SerialportComponent : Field4Component
    {
        public SerialportComponent()
        {
            InitializeComponent();
        }

        public SerialportComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void AfterMyWorkStateChanged(object sender, EventArgs e)
        {
            if (myWorkState)
            {
                foreach (var item in ElectricConfigs)
                {
                    ElectricEnumType = (ElectricEnumType)item.ElectricEnumType;
                    switch (ElectricEnumType)
                    {
                        case ElectricEnumType.PA310:
                            {
                                PA310Protocol protocol = new PA310Protocol() { GatewayIndex = item.GatewayIndex, DeviceIndex = item.DeviceIndex, ID = (byte)item.DeviceID, LoopEnumType = item.LoopEnumType, PhaseAngleEnumType = item.PhaseAngleEnumType, PhaseEnumType = item.PhaseEnumType, ElectricEnumType = item.ElectricEnumType };
                                AbsProtocols.Add(protocol);
                            }
                            break;
                        case ElectricEnumType.HC660:
                            {
                                HC6600Protocol protocol = new HC6600Protocol() { GatewayIndex = item.GatewayIndex, DeviceIndex = item.DeviceIndex, ID = (byte)item.DeviceID, LoopEnumType = item.LoopEnumType, PhaseAngleEnumType = item.PhaseAngleEnumType, PhaseEnumType = item.PhaseEnumType, ElectricEnumType = item.ElectricEnumType };
                                AbsProtocols.Add(protocol);
                            }
                            break;
                        case ElectricEnumType.CPM6:
                            {
                                CPM6Protocol protocol = new CPM6Protocol() { GatewayIndex = item.GatewayIndex, DeviceIndex = item.DeviceIndex, ID = (byte)item.DeviceID, LoopEnumType = item.LoopEnumType, PhaseAngleEnumType = item.PhaseAngleEnumType, PhaseEnumType = item.PhaseEnumType, ElectricEnumType = item.ElectricEnumType };
                                AbsProtocols.Add(protocol);
                            }
                            break;
                        case ElectricEnumType.PA60:
                            {
                                PA60Protocol protocol = new PA60Protocol() { GatewayIndex = item.GatewayIndex, DeviceIndex = item.DeviceIndex, ID = (byte)item.DeviceID, LoopEnumType = item.LoopEnumType, PhaseAngleEnumType = item.PhaseAngleEnumType, PhaseEnumType = item.PhaseEnumType, ElectricEnumType = item.ElectricEnumType };
                                AbsProtocols.Add(protocol);
                            }
                            break;
                        case ElectricEnumType.ABBM2M:
                            {
                                ABBM2MProtocol protocol = new ABBM2MProtocol() { GatewayIndex = item.GatewayIndex, DeviceIndex = item.DeviceIndex, ID = (byte)item.DeviceID, LoopEnumType = item.LoopEnumType, PhaseAngleEnumType = item.PhaseAngleEnumType, PhaseEnumType = item.PhaseEnumType, ElectricEnumType = item.ElectricEnumType };
                                AbsProtocols.Add(protocol);
                            }
                            break;
                        case ElectricEnumType.PM200:
                            {
                                PM200Protocol protocol = new PM200Protocol() { GatewayIndex = item.GatewayIndex, DeviceIndex = item.DeviceIndex, ID = (byte)item.DeviceID, LoopEnumType = item.LoopEnumType, PhaseAngleEnumType = item.PhaseAngleEnumType, PhaseEnumType = item.PhaseEnumType, ElectricEnumType = item.ElectricEnumType };
                                AbsProtocols.Add(protocol);
                            }
                            break;
                        case ElectricEnumType.TWCCPM4:
                            {
                                TWCCPM4Protocol protocol = new TWCCPM4Protocol() { GatewayIndex = item.GatewayIndex, DeviceIndex = item.DeviceIndex, ID = (byte)item.DeviceID, LoopEnumType = item.LoopEnumType, PhaseAngleEnumType = item.PhaseAngleEnumType, PhaseEnumType = item.PhaseEnumType, ElectricEnumType = item.ElectricEnumType };
                                AbsProtocols.Add(protocol);
                            }
                            break;
                    }
                }
                ReadThread = new Thread(Analysis);
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
        private void Analysis()
        {
            while (myWorkState)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(ReadTime);
                if (timeSpan.TotalSeconds >= 5)
                {
                    try
                    {
                        #region Rs485通訊功能初始化
                        try
                        {
                            if (SerialPort == null)
                            {
                                SerialPort = new SerialPort();
                            }
                            if (!SerialPort.IsOpen)
                            {
                                SerialPort.PortName = Gatewayconfig.Location;
                                SerialPort.BaudRate = Gatewayconfig.Rate;
                                SerialPort.DataBits = 8;
                                SerialPort.StopBits = StopBits.One;
                                SerialPort.Parity = Parity.None;
                                SerialPort.Open();
                            }
                        }
                        catch (ArgumentException)
                        {
                            Log.Error("通訊埠設定有誤");
                            ComponentFlag = true;
                            ErrorString = "Modbus RTU通訊失敗!";
                        }
                        catch (InvalidOperationException)
                        {
                            Log.Error("通訊埠被占用");
                            ComponentFlag = true;
                            ErrorString = "Modbus RTU通訊失敗!";
                        }
                        catch (IOException)
                        {
                            Log.Error("通訊埠無效");
                            ComponentFlag = true;
                            ErrorString = "Modbus RTU通訊失敗!";
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, "通訊埠發生不可預期的錯誤。");
                            ComponentFlag = true;
                            ErrorString = "Modbus RTU通訊失敗!";
                        }
                        #endregion
                        if (SerialPort.IsOpen)
                        {
                            ModbusMaster ModbusMaster = ModbusSerialMaster.CreateRtu(SerialPort);
                            ModbusMaster.Transport.Retries = 3;
                            ModbusMaster.Transport.ReadTimeout = 500;
                            ModbusMaster.Transport.WriteTimeout = 500;
                            foreach (var item in AbsProtocols)
                            {
                                item.DataReader(ModbusMaster);
                                Thread.Sleep(10);
                            }
                            ReadTime = DateTime.Now;
                            ComponentFlag = false;
                        }
                    }
                    catch (ThreadAbortException) { }
                    catch (Exception ex)
                    {
                        ReadTime = DateTime.Now;
                        ComponentFlag = true;
                        ErrorString = "Modbus RTU通訊失敗!";
                        Log.Error(ex, $"通訊失敗 COM:{Gatewayconfig.Location} Rate:{Gatewayconfig.Rate} ");
                    }
                }
                else
                {
                    Thread.Sleep(80);
                }
            }
        }
    }
}
