// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SYMO823MData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MLib.Models
{
    #region Using Directives

    using System;
    using System.Linq;
    using System.Reflection;

    using NModbus.Extensions;
    using Newtonsoft.Json;

    using DataValueLib;
    using SunSpecLib;

    #endregion

    /// <summary>
    /// This class holds all Fronius Symo 8.2-3-M properties.
    /// Each property corresponds to a Modbus data value. Some data values are writable, some are readable.
    /// They can be retrieved using the various Modbus extension functions. Special handling is provided
    /// for the compound data related to the device information parameters where the complete data block
    /// is read or written. The block areas allow reading of multiple data values minimizing the number
    /// of Modbus read operations. The block size is determined by data type and consecutive registers.
    /// </summary>
    public class SYMO823MData : DataValue, IPropertyHelper
    {
        #region Constants

        const ModbusAttribute.AccessModes RO = ModbusAttribute.AccessModes.ReadOnly;
        const ModbusAttribute.AccessModes RW = ModbusAttribute.AccessModes.ReadWrite;
        const ModbusAttribute.AccessModes WO = ModbusAttribute.AccessModes.WriteOnly;

        #endregion

        #region Private Data Members

        #endregion

        #region Public Properties

        /// <summary>
        /// Fronius Symo 8.2-3-M inverter properties (data fields).
        /// The Modbus attributes are used to define offset, acces mode and numer of registers used.
        /// </summary>

        // Common Model (C001)
        [Modbus(40000, 2, RO)] public uint32 SunSpecID { get; set; } = 0x53756e53;
        [Modbus(40002, 1, RO)] public uint16 C001ID { get; set; } = 1;
        [Modbus(40003, 1, RO)] public uint16 C001Length { get; set; } = 65;
        [Modbus(40004, 16, RO)] public string Manufacturer { get; set; } = string.Empty;
        [Modbus(40020, 16, RO)] public string Model { get; set; } = string.Empty;
        [Modbus(40036, 8, RO)] public string Options { get; set; } = string.Empty;
        [Modbus(40044, 8, RO)] public string Version { get; set; } = string.Empty;
        [Modbus(40052, 16, RO)] public string SerialNumber { get; set; } = string.Empty;
        [Modbus(40068, 1, RO)] public uint16 DeviceAddress { get; set; }
        // Inverter Model (I113)
        [Modbus(40069, 1, RO)] public uint16 I113ID { get; set; } = 113;
        [Modbus(40070, 1, RO)] public uint16 I113Length { get; set; } = 60;
        [Modbus(40071, 2, RO)] public float TotalCurrentAC { get; set; }
        [Modbus(40073, 2, RO)] public float CurrentL1 { get; set; }
        [Modbus(40075, 2, RO)] public float CurrentL2 { get; set; }
        [Modbus(40077, 2, RO)] public float CurrentL3 { get; set; }
        [Modbus(40079, 2, RO)] public float VoltageL1L2 { get; set; }
        [Modbus(40081, 2, RO)] public float VoltageL2L3 { get; set; }
        [Modbus(40083, 2, RO)] public float VoltageL3L1 { get; set; }
        [Modbus(40085, 2, RO)] public float VoltageL1N { get; set; }
        [Modbus(40087, 2, RO)] public float VoltageL2N { get; set; }
        [Modbus(40089, 2, RO)] public float VoltageL3N { get; set; }
        [Modbus(40091, 2, RO)] public float PowerAC { get; set; }
        [Modbus(40093, 2, RO)] public float Frequency { get; set; }
        [Modbus(40095, 2, RO)] public float ApparentPower { get; set; }
        [Modbus(40097, 2, RO)] public float ReactivePower { get; set; }
        [Modbus(40099, 2, RO)] public float PowerFactor { get; set; }
        [Modbus(40101, 2, RO)] public float LifeTimeEnergy { get; set; }
        [Modbus(40103, 2, RO)] public float CurrentDC { get; set; }
        [Modbus(40105, 2, RO)] public float VoltageDC { get; set; }
        [Modbus(40107, 2, RO)] public float PowerDC { get; set; }
        [Modbus(40109, 2, RO)] public float CabinetTemperature { get; set; }
        [Modbus(40111, 2, RO)] public float HeatsinkTemperature { get; set; }
        [Modbus(40113, 2, RO)] public float TransformerTemperature { get; set; }
        [Modbus(40115, 2, RO)] public float OtherTemperature { get; set; }
        [Modbus(40117, 1, RO)] public OperatingState OperatingState { get; set; }
        [Modbus(40118, 1, RO)] public VendorState VendorState { get; set; }
        [Modbus(40119, 2, RO)] public Evt1 Evt1 { get; set; }
        [Modbus(40121, 2, RO)] public Evt2 Evt2 { get; set; }
        [Modbus(40123, 2, RO)] public EvtVnd1 EvtVnd1 { get; set; }
        [Modbus(40125, 2, RO)] public EvtVnd2 EvtVnd2 { get; set; }
        [Modbus(40127, 2, RO)] public EvtVnd3 EvtVnd3 { get; set; }
        [Modbus(40129, 2, RO)] public EvtVnd4 EvtVnd4 { get; set; }
        // Nameplate Model (IC120)
        [Modbus(40131, 1, RO)] public uint16 IC120ID { get; set; } = 120;
        [Modbus(40132, 1, RO)] public uint16 IC120Length { get; set; } = 26;
        [Modbus(40133, 1, RO)] public DERType DERType { get; set; }
        [Modbus(40134, 1, RO)] public uint16 OutputW { get; set; }
        [Modbus(40135, 1, RO)] public sunssf ScaleFactorOutputW { get; set; }
        [Modbus(40136, 1, RO)] public uint16 OutputVA { get; set; }
        [Modbus(40137, 1, RO)] public sunssf ScaleFactorOutputVA { get; set; }
        [Modbus(40138, 1, RO)] public int16 OutputVArQ1 { get; set; }
        [Modbus(40139, 1, RO)] public int16 OutputVArQ2 { get; set; }
        [Modbus(40140, 1, RO)] public int16 OutputVArQ3 { get; set; }
        [Modbus(40141, 1, RO)] public int16 OutputVArQ4 { get; set; }
        [Modbus(40142, 1, RO)] public sunssf ScaleFactorOutputVAr { get; set; }
        [Modbus(40143, 1, RO)] public uint16 MaxRMS { get; set; }
        [Modbus(40144, 1, RO)] public sunssf ScaleFactorMaxRMS { get; set; }
        [Modbus(40145, 1, RO)] public int16 MinimumPFQ1 { get; set; }
        [Modbus(40146, 1, RO)] public int16 MinimumPFQ2 { get; set; }
        [Modbus(40147, 1, RO)] public int16 MinimumPFQ3 { get; set; }
        [Modbus(40148, 1, RO)] public int16 MinimumPFQ4 { get; set; }
        [Modbus(40149, 1, RO)] public sunssf ScaleFactorMinimumPF { get; set; }
        [Modbus(40150, 1, RO)] public uint16 EnergyRating { get; set; }
        [Modbus(40151, 1, RO)] public sunssf ScaleFactorEnergyRating { get; set; }
        [Modbus(40152, 1, RO)] public uint16 BatteryCapacity { get; set; }
        [Modbus(40153, 1, RO)] public sunssf ScaleFactorBatteryCapacity { get; set; }
        [Modbus(40154, 1, RO)] public uint16 MaxCharge { get; set; }
        [Modbus(40155, 1, RO)] public sunssf ScaleFactorMaxCharge { get; set; }
        [Modbus(40156, 1, RO)] public uint16 MaxDischarge { get; set; }
        [Modbus(40157, 1, RO)] public sunssf ScaleFactorMaxDischarge { get; set; }
        [Modbus(40158, 1, RO)] public ushort Pad { get; set; }
        // Basic Settings Model(IC121)
        [Modbus(40159, 1, RO)] public uint16 IC121ID { get; set; } = 121;
        [Modbus(40160, 1, RO)] public uint16 IC121Length { get; set; } = 30;
        [Modbus(40161, 1, RO)] public uint16 WMax { get; set; }
        [Modbus(40162, 1, RW)] public uint16 VRef { get; set; }
        [Modbus(40163, 1, RW)] public int16 VRefOfs { get; set; }
        [Modbus(40164, 1, RO)] public uint16 VMax { get; set; }
        [Modbus(40165, 1, RO)] public uint16 VMin { get; set; }
        [Modbus(40166, 1, RO)] public uint16 VAMax { get; set; }
        [Modbus(40167, 1, RO)] public int16 VARMaxQ1 { get; set; }
        [Modbus(40168, 1, RO)] public int16 VARMaxQ2 { get; set; }
        [Modbus(40169, 1, RO)] public int16 VARMaxQ3 { get; set; }
        [Modbus(40170, 1, RO)] public int16 VARMaxQ4 { get; set; }
        [Modbus(40171, 1, RO)] public uint16 WGra { get; set; }
        [Modbus(40172, 1, RO)] public enum16 VArAct { get; set; }
        [Modbus(40173, 1, RO)] public enum16 ClcTotVA { get; set; }
        [Modbus(40174, 1, RO)] public int16 PFMinQ1 { get; set; }
        [Modbus(40175, 1, RO)] public int16 PFMinQ2 { get; set; }
        [Modbus(40176, 1, RO)] public int16 PFMinQ3 { get; set; }
        [Modbus(40177, 1, RO)] public int16 PFMinQ4 { get; set; }
        [Modbus(40178, 1, RO)] public uint16 MaxRmpRte { get; set; }
        [Modbus(40179, 1, RO)] public uint16 ECPNomHz { get; set; }
        [Modbus(40180, 1, RO)] public enum16 ConnectedPhase { get; set; }
        [Modbus(40181, 1, RO)] public sunssf ScaleFactorWMax { get; set; }
        [Modbus(40182, 1, RO)] public sunssf ScaleFactorVRef { get; set; }
        [Modbus(40183, 1, RO)] public sunssf ScaleFactorVRefOfs { get; set; }
        [Modbus(40184, 1, RO)] public sunssf ScaleFactorVMinMax { get; set; }
        [Modbus(40185, 1, RO)] public sunssf ScaleFactorVAMax { get; set; }
        [Modbus(40186, 1, RO)] public sunssf ScaleFactorVARMax { get; set; }
        [Modbus(40187, 1, RO)] public sunssf ScaleFactorWGra { get; set; }
        [Modbus(40188, 1, RO)] public sunssf ScaleFactorPFMin { get; set; }
        [Modbus(40189, 1, RO)] public sunssf ScaleFactorMaxRmpRte { get; set; }
        [Modbus(40190, 1, RO)] public sunssf ScaleFactorECPNomHz { get; set; }
        // Extended Measurements & Status Model (IC122)
        [Modbus(40191, 1, RO)] public uint16 IC122ID { get; set; } = 122;
        [Modbus(40192, 1, RO)] public uint16 IC122Length { get; set; } = 44;
        [Modbus(40193, 1, RO)] public PVConn PVConn { get; set; }
        [Modbus(40194, 1, RO)] public StorConn StorConn { get; set; }
        [Modbus(40195, 1, RO)] public ECPConn ECPConn { get; set; }
        [Modbus(40196, 4, RO)] public uint64 ActWh { get; set; }
        [Modbus(40200, 4, RO)] public uint64 ActVAh { get; set; }
        [Modbus(40204, 4, RO)] public uint64 ActVArhQ1 { get; set; }
        [Modbus(40208, 4, RO)] public uint64 ActVArhQ2 { get; set; }
        [Modbus(40212, 4, RO)] public uint64 ActVArhQ3 { get; set; }
        [Modbus(40216, 4, RO)] public uint64 ActVArhQ4 { get; set; }
        [Modbus(40220, 1, RO)] public uint16 AvailableVAr { get; set; }
        [Modbus(40221, 1, RO)] public sunssf ScaleFactorAvailableVAr { get; set; }
        [Modbus(40222, 1, RO)] public uint16 AvailableW { get; set; }
        [Modbus(40223, 1, RO)] public sunssf ScaleFactorAvailableW { get; set; }
        [Modbus(40224, 2, RO)] public uint32 StSetLimMsk { get; set; }
        [Modbus(40226, 2, RO)] public StActCtls StActCtl { get; set; }
        [Modbus(40228, 4, RO)] public string TmSrc { get; set; } = string.Empty;
        [Modbus(40232, 2, RO)] public uint32 Tms { get; set; }
        [Modbus(40234, 1, RO)] public uint16 RtSt { get; set; }
        [Modbus(40235, 1, RO)] public uint16 Riso { get; set; }
        [Modbus(40236, 1, RO)] public sunssf ScaleFactorRiso { get; set; }
        // Immediate Control Model (IC123)
        [Modbus(40237, 1, RO)] public uint16 IC123ID { get; set; } = 123;
        [Modbus(40238, 1, RO)] public uint16 IC123Length { get; set; } = 24;
        [Modbus(40239, 1, RW)] public uint16 ConnWinTms { get; set; }
        [Modbus(40240, 1, RW)] public uint16 ConnRvrtTms { get; set; }
        [Modbus(40241, 1, RW)] public uint16 Conn { get; set; }
        [Modbus(40242, 1, RW)] public uint16 WMaxLimPct { get; set; }
        [Modbus(40243, 1, RW)] public uint16 WMaxLimPctWinTms { get; set; }
        [Modbus(40244, 1, RW)] public uint16 WMaxLimPctRvrtTms { get; set; }
        [Modbus(40245, 1, RO)] public uint16 WMaxLimPctRmpTms { get; set; }
        [Modbus(40246, 1, RW)] public uint16 WMaxLimEna { get; set; }
        [Modbus(40247, 1, RW)] public int16 OutPFSet { get; set; }
        [Modbus(40248, 1, RW)] public uint16 OutPFSetWinTms { get; set; }
        [Modbus(40249, 1, RW)] public uint16 OutPFSetRvrtTms { get; set; }
        [Modbus(40250, 1, RO)] public uint16 OutPFSetRmpTms { get; set; }
        [Modbus(40251, 1, RW)] public uint16 OutPFSetEna { get; set; }
        [Modbus(40252, 1, RO)] public int16 VArWMaxPct { get; set; }
        [Modbus(40253, 1, RW)] public int16 VArMaxPct { get; set; }
        [Modbus(40254, 1, RO)] public int16 VArAvalPct { get; set; }
        [Modbus(40255, 1, RW)] public uint16 VArPctWinTms { get; set; }
        [Modbus(40256, 1, RO)] public uint16 VArPctRvrtTms { get; set; }
        [Modbus(40257, 1, RW)] public uint16 VArPctRmpTms { get; set; }
        [Modbus(40258, 1, RO)] public uint16 VArPctMod { get; set; }
        [Modbus(40259, 1, RW)] public uint16 VArPctEna { get; set; }
        [Modbus(40260, 1, RO)] public sunssf ScaleFactorWMaxLimPct { get; set; }
        [Modbus(40261, 1, RO)] public sunssf ScaleFactorOutPFSet { get; set; }
        [Modbus(40262, 1, RO)] public sunssf ScaleFactorVArPct { get; set; }
        // Multiple MPPT Inverter Extension Model (I160)
        [Modbus(40263, 1, RO)] public uint16 I160ID { get; set; } = 160;
        [Modbus(40264, 1, RO)] public uint16 I160Length { get; set; } = 48;
        [Modbus(40265, 1, RO)] public sunssf ScaleFactorCurrent { get; set; }
        [Modbus(40266, 1, RO)] public sunssf ScaleFactorVoltage { get; set; }
        [Modbus(40267, 1, RO)] public sunssf ScaleFactorPower { get; set; }
        [Modbus(40268, 1, RO)] public sunssf ScaleFactorEnergy { get; set; }
        [Modbus(40269, 2, RO)] public uint32 GlobalEvents { get; set; }
        [Modbus(40271, 1, RO)] public uint16 NumberOfModules { get; set; }
        [Modbus(40272, 1, RO)] public uint16 TimestampPeriod { get; set; }
        [Modbus(40273, 1, RO)] public uint16 InputID1 { get; set; }
        [Modbus(40274, 8, RO)] public string InputIDString1 { get; set; } = string.Empty;
        [Modbus(40282, 1, RO)] public uint16 CurrentDC1 { get; set; }
        [Modbus(40283, 1, RO)] public uint16 VoltageDC1 { get; set; }
        [Modbus(40284, 1, RO)] public uint16 PowerDC1 { get; set; }
        [Modbus(40285, 2, RO)] public uint32 LifetimeEnergy1 { get; set; }
        [Modbus(40287, 2, RO)] public uint32 Timestamp1 { get; set; }
        [Modbus(40289, 1, RO)] public uint16 Temperature1 { get; set; }
        [Modbus(40290, 1, RO)] public OperatingState OperatingState1 { get; set; }
        [Modbus(40291, 2, RO)] public uint32 ModuleEvents1 { get; set; }
        [Modbus(40293, 1, RO)] public uint16 InputID2 { get; set; }
        [Modbus(40294, 8, RO)] public string InputIDString2 { get; set; } = string.Empty;
        [Modbus(40302, 1, RO)] public uint16 CurrentDC2 { get; set; }
        [Modbus(40303, 1, RO)] public uint16 VoltageDC2 { get; set; }
        [Modbus(40304, 1, RO)] public uint16 PowerDC2 { get; set; }
        [Modbus(40305, 2, RO)] public uint32 LifetimeEnergy2 { get; set; }
        [Modbus(40307, 2, RO)] public uint32 Timestamp2 { get; set; }
        [Modbus(40309, 1, RO)] public uint16 Temperature2 { get; set; }
        [Modbus(40310, 1, RO)] public OperatingState OperatingState2 { get; set; }
        [Modbus(40311, 2, RO)] public uint32 ModuleEvents2 { get; set; }
        // Fronius registers
        [Modbus(211, 1, RW)] public ushort DeleteData { get; set; }
        [Modbus(212, 1, RW)] public ushort StoreData { get; set; }
        [Modbus(213, 1, RO)] public ushort ActiveStateCode { get; set; }
        [Modbus(214, 1, RW)] public ushort ResetAllEventFlags { get; set; }
        [Modbus(215, 1, RW)] public ushort ModelType { get; set; }
        [Modbus(499, 2, RO)] public uint SitePower { get; set; }
        [Modbus(501, 4, RO)] public ulong SiteEnergyDay { get; set; }
        [Modbus(505, 4, RO)] public ulong SiteEnergyYear { get; set; }
        [Modbus(509, 4, RO)] public ulong SiteEnergyTotal { get; set; }

        #endregion

        #region Block Properties

        /// <summary>
        /// Fronius Symo 8.2-3-M inverter common model block area.
        /// The Modbus attributes are used to define offset, acces mode and numer of registers used.
        /// </summary>
        [JsonIgnore]
        [Modbus(40004, 65, RO)]
        public ushort[] C001Block
        {
            get { return Array.Empty<ushort>(); }
            set
            {
                if (value.Length == 65)
                {
                    Manufacturer = value.Take(16).ToArray().ToASCII();
                    Model = value.Skip(16).Take(16).ToArray().ToASCII();
                    Options = value.Skip(32).Take(8).ToArray().ToASCII();
                    Version = value.Skip(40).Take(8).ToArray().ToASCII();
                    SerialNumber = value.Skip(48).Take(16).ToArray().ToASCII();
                    DeviceAddress = value[64];
                }
            }
        }

        [JsonIgnore]
        [Modbus(40071, 60, RO)]
        public ushort[] I113Block
        {
            get { return Array.Empty<ushort>(); }
            set
            {
                if (value.Length == 60)
                {
                    TotalCurrentAC = value.Take(2).ToArray().ToFloat();
                    CurrentL1 = value.Skip(2).Take(2).ToArray().ToFloat();
                    CurrentL2 = value.Skip(4).Take(2).ToArray().ToFloat();
                    CurrentL3 = value.Skip(6).Take(2).ToArray().ToFloat();
                    VoltageL1L2 = value.Skip(8).Take(2).ToArray().ToFloat();
                    VoltageL2L3 = value.Skip(10).Take(2).ToArray().ToFloat();
                    VoltageL3L1 = value.Skip(12).Take(2).ToArray().ToFloat();
                    VoltageL1N = value.Skip(14).Take(2).ToArray().ToFloat();
                    VoltageL2N = value.Skip(16).Take(2).ToArray().ToFloat();
                    VoltageL3N = value.Skip(18).Take(2).ToArray().ToFloat();
                    PowerAC = value.Skip(20).Take(2).ToArray().ToFloat();
                    Frequency = value.Skip(22).Take(2).ToArray().ToFloat();
                    ApparentPower = value.Skip(24).Take(2).ToArray().ToFloat();
                    ReactivePower = value.Skip(26).Take(2).ToArray().ToFloat();
                    PowerFactor = value.Skip(28).Take(2).ToArray().ToFloat();
                    LifeTimeEnergy = value.Skip(30).Take(2).ToArray().ToFloat();
                    CurrentDC = value.Skip(32).Take(2).ToArray().ToFloat();
                    VoltageDC = value.Skip(34).Take(2).ToArray().ToFloat();
                    PowerDC = value.Skip(36).Take(2).ToArray().ToFloat();
                    CabinetTemperature = value.Skip(38).Take(2).ToArray().ToFloat();
                    HeatsinkTemperature = value.Skip(40).Take(2).ToArray().ToFloat();
                    TransformerTemperature = value.Skip(42).Take(2).ToArray().ToFloat();
                    OtherTemperature = value.Skip(44).Take(2).ToArray().ToFloat();
                    OperatingState = (OperatingStates)value[46];
                    VendorState = (VendorStates)value[47];
                    Evt1 = value.Skip(48).Take(2).ToArray().ToInt32();
                    Evt2 = value.Skip(50).Take(2).ToArray().ToInt32();
                    EvtVnd1 = value.Skip(52).Take(2).ToArray().ToInt32();
                    EvtVnd2 = value.Skip(54).Take(2).ToArray().ToInt32();
                    EvtVnd3 = value.Skip(56).Take(2).ToArray().ToInt32();
                    EvtVnd4 = value.Skip(58).Take(2).ToArray().ToInt32();
                }
            }
        }

        [JsonIgnore]
        [Modbus(40133, 26, RO)]
        public ushort[] IC120Block
        {
            get { return Array.Empty<ushort>(); }
            set
            {
                if (value.Length == 26)
                {
                    DERType = (DERTypes)value[0];
                    OutputW = value[1];
                    ScaleFactorOutputW = value[2];
                    OutputVA = value[3];
                    ScaleFactorOutputVA = value[4];
                    OutputVArQ1 = value[5];
                    OutputVArQ2 = value[6];
                    OutputVArQ3 = value[7];
                    OutputVArQ4 = value[8];
                    ScaleFactorOutputVAr = value[9];
                    MaxRMS = value[10];
                    ScaleFactorMaxRMS = value[11];
                    MinimumPFQ1 = value[12];
                    MinimumPFQ2 = value[13];
                    MinimumPFQ3 = value[14];
                    MinimumPFQ4 = value[15];
                    ScaleFactorMinimumPF = value[16];
                    EnergyRating = value[17];
                    ScaleFactorEnergyRating = value[18];
                    BatteryCapacity = value[19];
                    ScaleFactorBatteryCapacity = value[20];
                    MaxCharge = value[21];
                    ScaleFactorMaxCharge = value[22];
                    MaxDischarge = value[23];
                    ScaleFactorMaxDischarge = value[24];
                }
            }
        }

        [JsonIgnore]
        [Modbus(40161, 30, RO)]
        public ushort[] IC121Block
        {
            get { return Array.Empty<ushort>(); }
            set
            {
                if (value.Length == 30)
                {
                    WMax = value[0];
                    VRef = value[1];
                    VRefOfs = value[2];
                    VMax = value[3];
                    VMin = value[4];
                    VAMax = value[5];
                    VARMaxQ1 = value[6];
                    VARMaxQ2 = value[7];
                    VARMaxQ3 = value[8];
                    VARMaxQ4 = value[9];
                    WGra = value[10];
                    VArAct = value[11];
                    ClcTotVA = value[12];
                    PFMinQ1 = value[13];
                    PFMinQ2 = value[14];
                    PFMinQ3 = value[15];
                    PFMinQ4 = value[16];
                    MaxRmpRte = value[17];
                    ECPNomHz = value[18];
                    ConnectedPhase = value[19];
                    ScaleFactorWMax = value[20];
                    ScaleFactorVRef = value[21];
                    ScaleFactorVRefOfs = value[22];
                    ScaleFactorVMinMax = value[23];
                    ScaleFactorVAMax = value[24];
                    ScaleFactorVARMax = value[25];
                    ScaleFactorWGra = value[26];
                    ScaleFactorPFMin = value[27];
                    ScaleFactorMaxRmpRte = value[28];
                    ScaleFactorECPNomHz = value[29];
                }
            }
        }

        [JsonIgnore]
        [Modbus(40193, 44, RO)]
        public ushort[] IC122Block
        {
            get { return Array.Empty<ushort>(); }
            set
            {
                if (value.Length == 44)
                {
                    PVConn = (PVConns)value[0];
                    StorConn = (StorConns)value[1];
                    ECPConn = (ECPConns)value[2];
                    ActWh = value.Skip(3).Take(4).ToArray().ToULong();
                    ActVAh = value.Skip(7).Take(4).ToArray().ToULong();
                    ActVArhQ1 = value.Skip(11).Take(4).ToArray().ToULong();
                    ActVArhQ2 = value.Skip(15).Take(4).ToArray().ToULong();
                    ActVArhQ3 = value.Skip(19).Take(4).ToArray().ToULong();
                    ActVArhQ4 = value.Skip(23).Take(4).ToArray().ToULong();
                    AvailableVAr = value[27];
                    ScaleFactorAvailableVAr = value[28];
                    AvailableW = value[29];
                    ScaleFactorAvailableW = value[30];
                    StSetLimMsk = value.Skip(31).Take(2).ToArray().ToUInt32();
                    StActCtl = (StActCtls)value.Skip(33).Take(2).ToArray().ToUInt32();
                    TmSrc = value.Skip(35).Take(4).ToArray().ToASCII();
                    Tms = value.Skip(39).Take(2).ToArray().ToUInt32();
                    RtSt = value[41];
                    Riso = value[42];
                    ScaleFactorRiso = value[43];
                }
            }
        }

        [JsonIgnore]
        [Modbus(40239, 24, RO)]
        public ushort[] IC123Block
        {
            get { return Array.Empty<ushort>(); }
            set
            {
                if (value.Length == 24)
                {
                    ConnWinTms = value[0];
                    ConnRvrtTms = value[1];
                    Conn = value[2];
                    WMaxLimPct = value[3];
                    WMaxLimPctWinTms = value[4];
                    WMaxLimPctRvrtTms = value[5];
                    WMaxLimPctRmpTms = value[6];
                    WMaxLimEna = value[7];
                    OutPFSet = value[8];
                    OutPFSetWinTms = value[9];
                    OutPFSetRvrtTms = value[10];
                    OutPFSetRmpTms = value[11];
                    OutPFSetEna = value[12];
                    VArWMaxPct = value[13];
                    VArMaxPct = value[14];
                    VArAvalPct = value[15];
                    VArPctWinTms = value[16];
                    VArPctRvrtTms = value[17];
                    VArPctRmpTms = value[18];
                    VArPctMod = value[19];
                    VArPctEna = value[20];
                    ScaleFactorWMaxLimPct = value[21];
                    ScaleFactorOutPFSet = value[22];
                    ScaleFactorVArPct = value[23];
                }
            }
        }

        [JsonIgnore]
        [Modbus(40265, 48, RO)]
        public ushort[] I160Block
        {
            get { return Array.Empty<ushort>(); }
            set
            {
                if (value.Length == 48)
                {
                    ScaleFactorCurrent = value[0];
                    ScaleFactorVoltage = value[1];
                    ScaleFactorPower = value[2];
                    ScaleFactorEnergy = value[3];
                    GlobalEvents = value.Skip(4).Take(2).ToArray().ToUInt32();
                    NumberOfModules = value[6];
                    TimestampPeriod = value[7];
                    InputID1 = value[8];
                    InputIDString1 = value.Skip(9).Take(8).ToArray().ToASCII();
                    CurrentDC1 = value[17];
                    VoltageDC1 = value[18];
                    PowerDC1 = value[19];
                    LifetimeEnergy1 = value.Skip(20).Take(2).ToArray().ToUInt32();
                    Timestamp1 = value.Skip(22).Take(2).ToArray().ToUInt32();
                    Temperature1 = value[24];
                    OperatingState1 = (OperatingStates)value[25];
                    ModuleEvents1 = value.Skip(26).Take(2).ToArray().ToUInt32();
                    InputID2 = value[28];
                    InputIDString2 = value.Skip(29).Take(8).ToArray().ToASCII();
                    CurrentDC2 = value[37];
                    VoltageDC2 = value[38];
                    PowerDC2 = value[39];
                    LifetimeEnergy2 = value.Skip(40).Take(2).ToArray().ToUInt32();
                    Timestamp2 = value.Skip(42).Take(2).ToArray().ToUInt32();
                    Temperature2 = value[44];
                    OperatingState2 = (OperatingStates)value[45];
                    ModuleEvents2 = value.Skip(46).Take(2).ToArray().ToUInt32();
                }
            }
        }

        /// <summary>
        /// Fronius Symo 8.2-3-M inverter common model block area.
        /// The Modbus attributes are used to define offset, acces mode and numer of registers used.
        /// </summary>
        [JsonIgnore]
        [Modbus(212, 5, RO)]
        public ushort[] Register1
        {
            get { return Array.Empty<ushort>(); }
            set
            {
                if (value.Length == 5)
                {
                    DeleteData = value[0];
                    StoreData = value[1];
                    ActiveStateCode = value[2];
                    ResetAllEventFlags = value[3];
                    ModelType = value[4];
                }
            }
        }

        /// <summary>
        /// Fronius Symo 8.2-3-M inverter common model block area.
        /// The Modbus attributes are used to define offset, acces mode and numer of registers used.
        /// </summary>
        [JsonIgnore]
        [Modbus(499, 14, RO)]
        public ushort[] Register2
        {
            get { return Array.Empty<ushort>(); }
            set
            {
                if (value.Length == 14)
                {
                    SitePower = value.Take(2).ToArray().ToUInt32();
                    SiteEnergyDay = value.Skip(2).Take(4).ToArray().ToULong();
                    SiteEnergyYear = value.Skip(6).Take(4).ToArray().ToULong();
                    SiteEnergyTotal = value.Skip(10).Take(4).ToArray().ToULong();
                }
            }
        }

        #endregion Block Properties

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in OverviewData.
        /// </summary>
        /// <param name="data">The SYMO823M data.</param>
        public void Refresh(SYMO823MData data)
        {
            if (data != null)
            {
                // Common Model (C001)
                SunSpecID = data.SunSpecID;
                C001ID = data.C001ID;
                C001Length = data.C001Length;
                Manufacturer = data.Manufacturer;
                Model = data.Model;
                Options = data.Options;
                Version = data.Version;
                SerialNumber = data.SerialNumber;
                DeviceAddress = data.DeviceAddress;
                // Inverter Model (I113)
                I113ID = data.I113ID;
                I113Length = data.I113Length;
                TotalCurrentAC = data.TotalCurrentAC;
                CurrentL1 = data.CurrentL1;
                CurrentL2 = data.CurrentL2;
                CurrentL3 = data.CurrentL3;
                VoltageL1L2 = data.VoltageL1L2;
                VoltageL2L3 = data.VoltageL2L3;
                VoltageL3L1 = data.VoltageL3L1;
                VoltageL1N = data.VoltageL1N;
                VoltageL2N = data.VoltageL2N;
                VoltageL3N = data.VoltageL3N;
                PowerAC = data.PowerAC;
                Frequency = data.Frequency;
                ApparentPower = data.ApparentPower;
                ReactivePower = data.ReactivePower;
                PowerFactor = data.PowerFactor;
                LifeTimeEnergy = data.LifeTimeEnergy;
                CurrentDC = data.CurrentDC;
                VoltageDC = data.VoltageDC;
                PowerDC = data.PowerDC;
                CabinetTemperature = data.CabinetTemperature;
                HeatsinkTemperature = data.HeatsinkTemperature;
                TransformerTemperature = data.TransformerTemperature;
                OtherTemperature = data.OtherTemperature;
                OperatingState = data.OperatingState;
                VendorState = data.VendorState;
                Evt1 = data.Evt1;
                Evt2 = data.Evt2;
                EvtVnd1 = data.EvtVnd1;
                EvtVnd2 = data.EvtVnd2;
                EvtVnd3 = data.EvtVnd3;
                EvtVnd4 = data.EvtVnd4;
                // Nameplate Model (IC120)
                IC120ID = data.IC120ID;
                IC120Length = data.IC120Length;
                DERType = data.DERType;
                OutputW = data.OutputW;
                ScaleFactorOutputW = data.ScaleFactorOutputW;
                OutputVA = data.OutputVA;
                ScaleFactorOutputVA = data.ScaleFactorOutputVA;
                OutputVArQ1 = data.OutputVArQ1;
                OutputVArQ2 = data.OutputVArQ2;
                OutputVArQ3 = data.OutputVArQ3;
                OutputVArQ4 = data.OutputVArQ4;
                ScaleFactorOutputVAr = data.ScaleFactorOutputVAr;
                MaxRMS = data.MaxRMS;
                ScaleFactorMaxRMS = data.ScaleFactorMaxRMS;
                MinimumPFQ1 = data.MinimumPFQ1;
                MinimumPFQ2 = data.MinimumPFQ2;
                MinimumPFQ3 = data.MinimumPFQ3;
                MinimumPFQ4 = data.MinimumPFQ4;
                ScaleFactorMinimumPF = data.ScaleFactorMinimumPF;
                EnergyRating = data.EnergyRating;
                ScaleFactorEnergyRating = data.ScaleFactorEnergyRating;
                BatteryCapacity = data.BatteryCapacity;
                ScaleFactorBatteryCapacity = data.ScaleFactorBatteryCapacity;
                MaxCharge = data.MaxCharge;
                ScaleFactorMaxCharge = data.ScaleFactorMaxCharge;
                MaxDischarge = data.MaxDischarge;
                ScaleFactorMaxDischarge = data.ScaleFactorMaxDischarge;
                // Basic Settings Model(IC121)
                IC121ID = data.IC121ID;
                IC121Length = data.IC121Length;
                WMax = data.WMax;
                VRef = data.VRef;
                VRefOfs = data.VRefOfs;
                VMax = data.VMax;
                VMin = data.VMin;
                VAMax = data.VAMax;
                VARMaxQ1 = data.VARMaxQ1;
                VARMaxQ2 = data.VARMaxQ2;
                VARMaxQ3 = data.VARMaxQ3;
                VARMaxQ4 = data.VARMaxQ4;
                WGra = data.WGra;
                VArAct = data.VArAct;
                ClcTotVA = data.ClcTotVA;
                PFMinQ1 = data.PFMinQ1;
                PFMinQ2 = data.PFMinQ2;
                PFMinQ3 = data.PFMinQ3;
                PFMinQ4 = data.PFMinQ4;
                MaxRmpRte = data.MaxRmpRte;
                ECPNomHz = data.ECPNomHz;
                ConnectedPhase = data.ConnectedPhase;
                ScaleFactorWMax = data.ScaleFactorWMax;
                ScaleFactorVRef = data.ScaleFactorVRef;
                ScaleFactorVRefOfs = data.ScaleFactorVRefOfs;
                ScaleFactorVMinMax = data.ScaleFactorVMinMax;
                ScaleFactorVAMax = data.ScaleFactorVAMax;
                ScaleFactorVARMax = data.ScaleFactorVARMax;
                ScaleFactorWGra = data.ScaleFactorWGra;
                ScaleFactorPFMin = data.ScaleFactorPFMin;
                ScaleFactorMaxRmpRte = data.ScaleFactorMaxRmpRte;
                ScaleFactorECPNomHz = data.ScaleFactorECPNomHz;
                // Extended Measurements & Status Model (IC122)
                IC122ID = data.IC122ID;
                IC122Length = data.IC122Length;
                PVConn = data.PVConn;
                StorConn = data.StorConn;
                ECPConn = data.ECPConn;
                ActWh = data.ActWh;
                ActVAh = data.ActVAh;
                ActVArhQ1 = data.ActVArhQ1;
                ActVArhQ2 = data.ActVArhQ2;
                ActVArhQ3 = data.ActVArhQ3;
                ActVArhQ4 = data.ActVArhQ4;
                AvailableVAr = data.AvailableVAr;
                ScaleFactorAvailableVAr = data.ScaleFactorAvailableVAr;
                AvailableW = data.AvailableW;
                ScaleFactorAvailableW = data.ScaleFactorAvailableW;
                StSetLimMsk = data.StSetLimMsk;
                StActCtl = data.StActCtl;
                TmSrc = data.TmSrc;
                Tms = data.Tms;
                RtSt = data.RtSt;
                Riso = data.Riso;
                ScaleFactorRiso = data.ScaleFactorRiso;
                // Immediate Control Model (IC123)
                IC123ID = data.IC123ID;
                IC123Length = data.IC123Length;
                ConnWinTms = data.ConnWinTms;
                ConnRvrtTms = data.ConnRvrtTms;
                Conn = data.Conn;
                WMaxLimPct = data.WMaxLimPct;
                WMaxLimPctWinTms = data.WMaxLimPctWinTms;
                WMaxLimPctRvrtTms = data.WMaxLimPctRvrtTms;
                WMaxLimPctRmpTms = data.WMaxLimPctRmpTms;
                WMaxLimEna = data.WMaxLimEna;
                OutPFSet = data.OutPFSet;
                OutPFSetWinTms = data.OutPFSetWinTms;
                OutPFSetRvrtTms = data.OutPFSetRvrtTms;
                OutPFSetRmpTms = data.OutPFSetRmpTms;
                OutPFSetEna = data.OutPFSetEna;
                VArWMaxPct = data.VArWMaxPct;
                VArMaxPct = data.VArMaxPct;
                VArAvalPct = data.VArAvalPct;
                VArPctWinTms = data.VArPctWinTms;
                VArPctRvrtTms = data.VArPctRvrtTms;
                VArPctRmpTms = data.VArPctRmpTms;
                VArPctMod = data.VArPctMod;
                VArPctEna = data.VArPctEna;
                ScaleFactorWMaxLimPct = data.ScaleFactorWMaxLimPct;
                ScaleFactorOutPFSet = data.ScaleFactorOutPFSet;
                ScaleFactorVArPct = data.ScaleFactorVArPct;
                // Multiple MPPT Inverter Extension Model (I160)
                I160ID = data.I160ID;
                I160Length = data.I160Length;
                ScaleFactorCurrent = data.ScaleFactorCurrent;
                ScaleFactorVoltage = data.ScaleFactorVoltage;
                ScaleFactorPower = data.ScaleFactorPower;
                ScaleFactorEnergy = data.ScaleFactorEnergy;
                GlobalEvents = data.GlobalEvents;
                NumberOfModules = data.NumberOfModules;
                TimestampPeriod = data.TimestampPeriod;
                InputID1 = data.InputID1;
                InputIDString1 = data.InputIDString1;
                CurrentDC1 = data.CurrentDC1;
                VoltageDC1 = data.VoltageDC1;
                PowerDC1 = data.PowerDC1;
                LifetimeEnergy1 = data.LifetimeEnergy1;
                Timestamp1 = data.Timestamp1;
                Temperature1 = data.Temperature1;
                OperatingState1 = data.OperatingState1;
                ModuleEvents1 = data.ModuleEvents1;
                InputID2 = data.InputID2;
                InputIDString2 = data.InputIDString2;
                CurrentDC2 = data.CurrentDC2;
                VoltageDC2 = data.VoltageDC2;
                PowerDC2 = data.PowerDC2;
                LifetimeEnergy2 = data.LifetimeEnergy2;
                Timestamp2 = data.Timestamp2;
                Temperature2 = data.Temperature2;
                OperatingState2 = data.OperatingState2;
                ModuleEvents2 = data.ModuleEvents2;
                // Fronius registers
                DeleteData = data.DeleteData;
                StoreData = data.StoreData;
                ActiveStateCode = data.ActiveStateCode;
                ResetAllEventFlags = data.ResetAllEventFlags;
                ModelType = data.ModelType;
                SitePower = data.SitePower;
                SiteEnergyDay = data.SiteEnergyDay;
                SiteEnergyYear = data.SiteEnergyYear;
                SiteEnergyTotal = data.SiteEnergyTotal;
            }

            Status = data?.Status ?? Uncertain;
        }

        #endregion

        #region Public Property Helper

        /// <summary>
        /// Returns the Modbus attribute of the property.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The Modbus attribute.</returns>
        public static ModbusAttribute GetModbusAttribute(string property) =>
                ModbusAttribute.GetModbusAttribute(PropertyValue.GetPropertyInfo(typeof(SYMO823MData), property));

        /// <summary>
        /// Gets the Modbus access mode attribute of the property.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The Modbus access mode.</returns>
        public static ModbusAttribute.AccessModes GetAccess(string property) => GetModbusAttribute(property).Access;

        /// <summary>
        /// Gets the Modbus address (offset) attribute of the property.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The Modbus offset.</returns>
        public static ushort GetOffset(string property) => GetModbusAttribute(property).Offset;

        /// <summary>
        /// Gets the data length attribute of the property.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The Modbus data length.</returns>
        public static ushort GetLength(string property) => GetModbusAttribute(property).Length;

        /// <summary>
        /// Returns true if the property is readable.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>True if readable.</returns>
        public static bool IsReadable(string property) => GetModbusAttribute(property).IsReadable;

        /// <summary>
        /// Returns true if the property is writable.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>True if writable.</returns>
        public static bool IsWritable(string property) => GetModbusAttribute(property).IsWritable;

        /// <summary>
        /// Gets the property list for the ETAPU11Data class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static string[] GetProperties()
            => typeof(SYMO823MData).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Select(p => p.Name).ToArray();

        /// <summary>
        /// Returns true if property with the specified name is found in the ETAPU11Data class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property) => PropertyValue.GetPropertyInfo(typeof(SYMO823MData), property) != null;

        /// <summary>
        /// Returns the <see cref="PropertyInfo"/> data for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The property info.</returns>
        public static PropertyInfo GetPropertyInfo(string property) => PropertyValue.GetPropertyInfo(typeof(SYMO823MData), property);

        /// <summary>
        /// Returns the value for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The property value.</returns>
        public object GetPropertyValue(string property) => PropertyValue.GetPropertyValue(this, property);

        /// <summary>
        /// Sets the value for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <param name="value">The property value.</param>
        public void SetPropertyValue(string property, object value) => PropertyValue.SetPropertyValue(this, property, value);

        #endregion Public Property Helper
    }
}
