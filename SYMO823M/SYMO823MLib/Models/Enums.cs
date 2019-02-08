// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Enums.cs" company="DTV-Online">
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
    using global::SunSpecLib;

    #endregion

    #region Public Enums

    public enum OperatingStates
    {
        Unknown = 0,
        Off = 1,            // Inverter is off
        Sleeping = 2,       // Auto shutdown
        Starting = 3,       // Inverter starting
        Mppt = 4,           // Inverter working normally
        Throttled = 5,      // Power reduction active
        ShuttingDown = 6,   // Inverter shutting down
        Fault = 7,          // One or more faults present
        Standby = 8,        // Standby
        NotImplemented = -1
    }

    public enum VendorStates
    {
        Unknown = 0,
        Off = 1,                // Inverter is off
        Sleeping = 2,           // Auto shutdown
        Starting = 3,           // Inverter starting
        Mppt = 4,               // Inverter working normally
        Throttled = 5,          // Power reduction active
        ShuttingDown = 6,       // Inverter shutting down
        Fault = 7,              // One or more faults present
        Standby = 8,            // Standby
        NoBusinit = 9,          // No SolarNet communication
        NoCommInv = 10,         // No communication with inverter possible
        SnOvercurrent = 11,     // Overcurrent detected on SolarNet plug
        Bootload = 12,          // Inverter is currently being updated
        AFCI = 13,              // AFCI event (arc detection)
        NotImplemented = -1
    }

    public enum DERTypes
    {
        Unknown = 0,
        PV = 4,
        PVSTOR = 82,
        NotImplemented = -1
    }

    public enum VarActions
    {
        Unknown = 0,
        Switch = 1,
        Maintain = 2,
        NotImplemented = -1
    }

    public enum CalcTotalVAs
    {
        Unknown = 0,
        Vector = 1,
        Arithmetic = 2,
        NotImplemented = -1
    }

    public enum ConnectedPhases
    {
        Unknown = 0,
        L1 = 1,
        L2 = 2,
        L3 = 3,
        NotImplemented = -1
    }

    [Flags]
    public enum PVConns
    {
        None = 0x0,
        Connected = 1,
        Available = 2,
        Operating = 4,
        Test = 8,
        NotImplemented = -1
    }

    [Flags]
    public enum StorConns
    {
        None = 0x0,
        Connected = 1,
        Available = 2,
        Operating = 4,
        NotImplemented = -1
    }

    [Flags]
    public enum ECPConns
    {
        None = 0x0,
        Connected = 1,
        NotImplemented = -1
    }

    [Flags]
    public enum StActCtls
    {
        None = 0x0,
        FixedW = 1,
        FixedVAR = 2,
        FixedPF = 4,
        NotImplemented = -1
    }

    [Flags]
    public enum Events1
    {
        None = 0x0,
        GroundFault = 0x1,              // I_EVENT_GROUND_FAULT
        OverVoltageDC = 0x2,            // I_EVENT_DC_OVER_VOLT
        DisconnectOpenAC = 0x4,         // I_EVENT_AC_DISCONNECT
        DisconnectOpenDC = 0x8,         // I_EVENT_DC_DISCONNECT
        GridShutdown = 0x10,            // I_EVENT_GRID_DISCONNECT
        CabinetOpen = 0x20,             // I_EVENT_CABINET_OPEN
        ManualShutdown = 0x40,          // I_EVENT_MANUAL_SHUTDOWN
        OverTemperature = 0x80,         // I_EVENT_OVER_TEMP
        FrequencyAboveLimit = 0x100,    // I_EVENT_OVER_FREQUENCY
        FrequencyUnderLimit = 0x200,    // I_EVENT_UNDER_FREQUENCY
        VoltageAboveLimitAC = 0x400,    // I_EVENT_AC_OVER_VOLT
        VoltageUnderLimit = 0x800,      // I_EVENT_AC_UNDER_VOLT
        BlownStringFuse = 0x1000,       // I_EVENT_BLOWN_STRING_FUSE
        UnderTemperature = 0x2000,      // I_EVENT_UNDER_TEMP
        GenericMemoryError = 0x4000,    // I_EVENT_MEMORY_LOSS
        HardwareTestFailure = 0x8000,   // I_EVENT_HW_TEST_FAILURE
    }

    [Flags]
    public enum Events2
    {
        None = 0x0,
    }

    [Flags]
    public enum EventsVendor1
    {
        None = 0x0,
        GridError = 0x2,
        OverCurrentAC = 0x4,
        OverCurrentDC = 0x8,
        OverTemperature = 0x10,
        LowPower = 0x20,
        LowDC = 0x40,
        IntermediateCircuitError = 0x80,
        FrequencyTooHighAC = 0x100,
        FrequencyTooLowAC = 0x200,
        VoltageTooHighAC = 0x400,
        VoltageTooLowAC = 0x800,
        DirectCurrentFeedIn = 0x1000,
        RelayProblem = 0x2000,
        InternalPowerStageError = 0x4000,
        GuardControllerVoltageErrorAC = 0x10000,
        GuardControllerFrequencyErrorAC = 0x20000,
        ErrorDuringAntiIslandingTest = 0x100000,
        FixedVoltageLowerThanCurrentMPPVoltage = 0x200000,
        MemoryFault = 0x400000,
        InternalCommunicationError = 0x1000000,
        TemperatureSensorsDefective = 0x2000000,
        DCorACboardFault = 0x4000000,
        ENSerror = 0x8000000,
        DefectiveFuse = 0x20000000
    }

    [Flags]
    public enum EventsVendor2
    {
        None = 0x0,
        NoFeedIn24h = 0x4,
        IncompatibleOrOldSoftware = 0x40,
        PowerDeratingDueToOvertemperature = 0x80,
        IncompatibleFeature = 0x200,
        PowerReductionOnError = 0x800,
        ArcDetected = 0x1000,
        AFCI_SelfTestFailed = 0x2000,
        CurrentSensorError = 0x4000,
        AFCI_Defective = 0x10000,
        PowerStackSupplyMissing = 0x40000,
        PolarityReversedAC = 0x200000,
        FlashFault = 0x800000,
        GeneralError = 0x1000000,
        PowerLimitationFault = 0x4000000,
        InternalProcessorProgramStatus = 0x20000000,
        SolarNetIssue = 0x40000000
    }

    [Flags]
    public enum EventsVendor3
    {
        None = 0x0,
        TimeError = 0x1,
        USBError = 0x2,
        HighDC = 0x4,
    }

    [Flags]
    public enum EventsVendor4
    {
        None = 0x0,
    }

    #endregion
}
