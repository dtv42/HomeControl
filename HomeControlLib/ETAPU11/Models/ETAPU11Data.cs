// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ETAPU11Data.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.ETAPU11.Models
{
    #region Using Directives

    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using DataValueLib;

    #endregion

    /// <summary>
    /// This class holds all ETA PU 11 pellet boiler properties.
    /// </summary>
    public class ETAPU11Data : DataValue
    {
        #region Public Enums

        public enum DemandValues
        {
            Off = 1020,
            On = 1021,
        }

        public enum DemandValuesEx
        {
            Stop = 1023,
            Fwd = 1024,
            Back = 1025,
        }

        public enum VacuumStates
        {
            Off = 1040,
            On = 1041,
            Locked = 1042,
            FaultyFuse = 1043,
            NoTerminalAssigned = 1044,
            NoAnswerFromCANnode = 1045,
        }

        public enum FirebedStates
        {
            High = 1100,
            OK = 1101,
            Locked = 1102,
            FaultyFuse = 1103,
            NoTerminalAssigned = 1104,
            NoAnswerFromCANnode = 1105,
        }

        public enum ScrewStates
        {
            Off1 = 1220,
            Stop1 = 1221,
            Fwd = 1222,
            Stop2 = 1223,
            ReturnFwd = 1224,
            Back = 1225,
            ReturnBack = 1226,
            CurrentDrawTooHigh = 1227,
            DriveProtection = 1228,
            MinCurrent1 = 1229,
            MinCurrent2 = 1230,
            Off2 = 1231,
            ResidCurr = 1232,
            StopLocked = 1233,
            StopFaultyFuse = 1234,
            NoTerminalAssigned = 1235,
            NoAnswerFromCANnode = 1236,
        }

        public enum FlowMixValveStates
        {
            Off = 1240,
            Open1 = 1241,
            Open2 = 1242,
            Close1 = 1243,
            Closed = 1244,
            Stop = 1245,
            Open3 = 1246,
            Close2 = 1247,
            Locked = 1248,
            FaultyFuse = 1259,
            NoTerminalAssigned = 1250,
            NoAnswerFromCANnode = 1251,
        }

        public enum StartValues
        {
            No = 1800,
            Yes = 1801,
        }

        public enum OnOffStates
        {
            Off = 1802,
            On = 1803,
        }

        public enum MeasurementTypes
        {
            NomLoad = 1960,
            PartLoad = 1961,
            Combination = 1962,
        }

        public enum BoilerStates
        {
            SwitchedOff = 2000,
            FlapOpen = 2001,
            FillUpPelletBin = 2002,
            FillingStoppedForIgnition = 2003,
            WarmStart = 2004,
            Igniting = 2005,
            Heating1 = 2006,
            EmberBurnout = 2007,
            EmberBurnoutDueToDeAshing = 2008,
            EmberBurnoutSwitchedOff = 2009,
            EmberBurnoutAshBoxMissing = 2010,
            FillingStoppedForDeAshing = 2011,
            Ready = 2012,
            AshboxMissing = 2013,
            DeAsh = 2014,
            MalfunctionDuringAshRemoval = 2015,
            Malfunction1 = 2016,
            EmberBurnoutDueToMalfunction = 2017,
            EmberBurnoutDueToExternalLocking = 2018,
            Locked = 2019,
            CalibratingLambdaProbe = 2020,
            Heating2 = 2021,
            Preheat = 2022,
            EmptyingStoker = 2023,
            Fill = 2024,
            InsulatorDoorOpen1 = 2025,
            Ignition = 2026,
            WaitForDelayTime = 2027,
            InsulatorDoorOpen2 = 2028,
            Overtemperature = 2029,
            TWINoperation = 2030,
            Malfunction2 = 2031,
            Preparation = 2032,
            EmptyingStoker1 = 2033,
            EmptyingStoker2 = 2034,
            EmptyingStoker3 = 2035,
            EmptyingStoker4 = 2036,
            HeatingPreparingToMeasure = 2037,
            HeatingPerformingPartialLoadMeasurement = 2038,
            HeatingPerformingFullLoadMeasurement = 2039,
        }

        public enum ConveyingSystemStates
        {
            PowerSupplyError = 2057,
            SelfCheck = 2058,
            Ready = 2059,
            Off = 2060,
            StartVacuumMotor = 2061,
            VacuumMotorRunning = 2062,
            Convey = 2063,
            EmptyHoses = 2064,
            SuctionTimeExceeded = 2065,
            NotEnoughPelletsConv = 2066,
            DelayDueToError = 2067,
            DischargeScrewError = 2068,
            SelfCheckError = 2069,
        }

        public enum FlowControlStates
        {
            ClosedStart = 2070,
            Off = 2071,
            Heating = 2072,
            Ctrl = 2073,
            Delay = 2074,
            Malfunction = 2075,
            FrostProtection = 2076,
            Open1 = 2077,
            Open2 = 2078,
            Open3 = 2079,
        }

        public enum DiverterValveStates
        {
            Value_2090 = 2090,
            Stop = 2091,
            Hotwater = 2092,
            Heating = 2093,
            Locked = 2094,
            FaultyFuse = 2095,
            NoTerminalAssigned = 2096,
            NoAnswerFromCANnode = 2097,
        }

        public enum AshRemovalStates
        {
            GrateOpen = 2100,
            Close = 2101,
            Ready1 = 2102,
            DeAsh = 2103,
            Stop1 = 2104,
            Ready2 = 2105,
            ShortBack1 = 2106,
            ShortFwd1 = 2107,
            WaitingShortBackward = 2108,
            WaitingShortForward = 2109,
            StopAshBox = 2110,
            Back = 2111,
            EmergencyTilting = 2112,
            ShortBack2 = 2113,
            Close1 = 2114,
            ShortFwd2 = 2115,
            ShortMoveError = 2116,
            Error = 2117,
            Ready = 2118,
            Open = 2119,
            Stop2 = 2120,
            Close2 = 2121,
            Closed = 2122,
            ErrorOpening = 2123,
            ErrorClosing = 2124,
            ColdStart = 2125,
        }

        public enum HeatingCircuitStates
        {
            Off = 2200,
            DayOn = 2201,
            NightOn = 2202,
            HolidayOn = 2203,
            Value2204 = 2204,
            EnableOff = 2205,
            DayTgtOff = 2206,
            NightTgtOff = 2207,
            HDayTgtOff = 2208,
            RoomDayOff = 2209,
            RoomNightOff = 2210,
            HDayRoomOff = 2211,
            DayHeatLimOff = 2212,
            NightHeatLimOff = 2213,
            HDayHeatLimOff = 2214,
            SummerOff = 2215,
            HWOff = 2216,
            RoomFreezeProtOn = 2217,
            FlowFreezeProtOn1 = 2218,
            ResidHeatOn = 2219,
            HeatDissOn = 2220,
            ScreedOn = 2221,
            SensorErrorOn = 2222,
            FlowFreezeProtOn2 = 2223,
            SolarHeatDiss = 2224,
            LockedOff = 2225,
        }

        public enum HWTankStates
        {
            Off = 2260,
            Demand1 = 2261,
            Demand2 = 2262,
            Chrg = 2263,
            ExtraCharge1 = 2264,
            ResidHeat = 2265,
            Charged = 2266,
            HeatDissipation = 2267,
            FreezeProt = 2268,
            SensorError = 2269,
            TimerOff = 2270,
            LoadingWithProducer = 2271,
            ExtraCharge2 = 2272,
            SolarPriority = 2273,
            Working = 2274,
            SolarHeatDiss = 2275,
        }

        public enum HWRunningStates
        {
            Day1 = 2301,
            Night1 = 2302,
            Day2 = 2303,
            Night2 = 2304,
            Off = 2305,
            Holiday = 2306,
            Screed = 2307,
        }

        public enum EmissionMeasurements
        {
            Off = 3110,
            Standby = 3111,
            DeAshBoiler = 3112,
            LockBoiler = 3113,
            PreheatBoiler1 = 3114,
            PreheatBoiler2 = 3115,
            CapacityMeasurement = 3116,
            PartialLoadMeasurement = 3117,
            DurationReached = 3118,
            Terminate1 = 3119,
            Terminate2 = 3120,
        }

        public enum HopperStates
        {
            NotFull = 3640,
            Demand = 3641,
            FillUp = 3642,
            ConveyorDelay = 3643,
            VacuumMotorDelay = 3644,
            Full = 3645,
            StandbyDelay = 3646,
            BoilerStandby = 3647,
            ConveyorStandby = 3648,
            ConveyorError = 3649,
            ErrorFillTimeMax = 3650,
        }

        #endregion Enums

        #region Public Properties

        /// <summary>
        /// ETA PU 11 pellet boiler properties (data fields).
        /// The Modbus attributes are used to define offset, acces mode and numer of registers used.
        /// </summary>

        // <register id = "1000" >< variable name="PE-C 0: Boiler - Full load hours" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12153" scale="1" unit="" min="" def="" max="" />
        public TimeSpan FullLoadHours { get; set; }

        // <register id = "1002" >< variable name="PE-C 0: Boiler - Total consumed" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12016" scale="10" unit="kg" min="" def="" max="" />
        public double TotalConsumed { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1004" >< variable name="PE-C 0: Boiler - Boiler" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12000" scale="1" unit="" min="" def="" max="" />
        public BoilerStates BoilerState { get; set; }

        // <register id = "1006" >< variable name="PE-C 0: Boiler - Boiler pressure" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12180" scale="100" unit="bar" min="" def="" max="" />
        public double BoilerPressure { get; set; }

        // <register id = "1008" >< variable name="PE-C 0: Boiler - Boiler" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12161" scale="10" unit="°C" min="" def="" max="" />
        public double BoilerTemperature { get; set; }

        // <register id = "1010" >< variable name="PE-C 0: Boiler - Boiler tgt." nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12001" scale="10" unit="°C" min="" def="" max="" />
        public double BoilerTarget { get; set; }

        // <register id = "1012" >< variable name="PE-C 0: Boiler - Boiler bottom" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12300" scale="10" unit="°C" min="" def="" max="" />
        public double BoilerBottom { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1014" >< variable name="PE-C 0: Boiler - Flow control 1" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12078" scale="1" unit="" min="" def="" max="" />
        public FlowControlStates FlowControlState { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1016" >< variable name="PE-C 0: Boiler - Diverter valve" nodeId="40" fubId="10021" fktId="0" ioId="11128" varId="0" scale="1" unit="" min="" def="" max="" />
        public DiverterValveStates DiverterValveState { get; set; }
        // <register id = "1018" >< variable name="PE-C 0: Boiler - Diverter valve - Demand" nodeId="40" fubId="10021" fktId="0" ioId="11128" varId="2001" scale="1" unit="" min="" def="" max="" />
        [JsonConverter(typeof(StringEnumConverter))]
        public DemandValues DiverterValveDemand { get; set; }

        // <register id = "1020" >< variable name="PE-C 0: Boiler - Demanded output" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12077" scale="10" unit="kW" min="" def="" max="" />
        public double DemandedOutput { get; set; }

        // <register id = "1022" >< variable name="PE-C 0: Boiler - FL mix. valve 1 - Target temp." nodeId="40" fubId="10021" fktId="0" ioId="11121" varId="2120" scale="10" unit="°C" min="" def="" max="" />
        public double FlowMixValveTarget { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1024" >< variable name="PE-C 0: Boiler - FL mix. valve 1 - State" nodeId="40" fubId="10021" fktId="0" ioId="11121" varId="2002" scale="1" unit="" min="" def="" max="" />
        public FlowMixValveStates FlowMixValveState { get; set; }

        // <register id = "1026" >< variable name="PE-C 0: Boiler - FL mix. valve 1 - Curr. temp." nodeId="40" fubId="10021" fktId="0" ioId="11121" varId="2121" scale="10" unit="°C" min="" def="" max="" />
        public double FlowMixValveCurrTemp { get; set; }

        // <register id = "1028" >< variable name="PE-C 0: Boiler - FL mix. valve 1 - Position" nodeId="40" fubId="10021" fktId="0" ioId="11121" varId="2127" scale="10" unit="%" min="" def="" max="" />
        public double FlowMixValvePosition { get; set; }

        // <register id = "1030" >< variable name="PE-C 0: Boiler - Boiler pump" nodeId="40" fubId="10021" fktId="0" ioId="11123" varId="0" scale="10" unit="%" min="" def="" max="" />
        public double BoilerPumpOutput { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1032" >< variable name="PE-C 0: Boiler - Boiler pump - Demand" nodeId="40" fubId="10021" fktId="0" ioId="11123" varId="2001" scale="1" unit="" min="" def="" max="" />
        public DemandValues BoilerPumpDemand { get; set; }

        // <register id = "1034" >< variable name="PE-C 0: Boiler - Flue gas" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12162" scale="10" unit="°C" min="" def="" max="" />
        public double FlueGasTemperature { get; set; }

        // <register id = "1036" >< variable name="PE-C 0: Boiler - Draught fan" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12165" scale="1" unit="rpm" min="" def="" max="" />
        public double DraughtFanSpeed { get; set; }

        // <register id = "1038" >< variable name="PE-C 0: Boiler - Residual O2" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12164" scale="100" unit="" min="" def="" max="" />
        public double ResidualO2 { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1040" >< variable name="PE-C 0: Boiler - Stoker screw - Demand" nodeId="40" fubId="10021" fktId="0" ioId="11030" varId="2001" scale="1" unit="" min="" def="" max="" />
        public DemandValuesEx StokerScrewDemand { get; set; }

        // <register id = "1042" >< variable name="PE-C 0: Boiler - Stoker screw - Clock rate" nodeId="40" fubId="10021" fktId="0" ioId="11030" varId="2090" scale="10" unit="%" min="" def="" max="" />
        public double StokerScrewClockRate { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1044" >< variable name="PE-C 0: Boiler - Stoker screw - State" nodeId="40" fubId="10021" fktId="0" ioId="11030" varId="2002" scale="1" unit="" min="" def="" max="" />
        public ScrewStates StokerScrewState { get; set; }

        // <register id = "1046" >< variable name="PE-C 0: Boiler - Stoker screw - Motor curr." nodeId="40" fubId="10021" fktId="0" ioId="11030" varId="2091" scale="1" unit="mA" min="" def="" max="" />
        public double StokerScrewMotorCurr { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1048" >< variable name="PE-C 0: Boiler - Ash removal" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12050" scale="1" unit="" min="" def="" max="" />
        public AshRemovalStates AshRemovalState { get; set; }

        // <register id = "1050" >< variable name="PE-C 0: Boiler - Start idle time" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12248" scale="1" unit="" min="0" def="1260" max="1439" />
        public TimeSpan AshRemovalStartIdleTime { get; set; }

        // <register id = "1052" >< variable name="PE-C 0: Boiler - Duration idle time" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12249" scale="1" unit="" min="0" def="36000" max="43200" />
        public TimeSpan AshRemovalDurationIdleTime { get; set; }

        // <register id = "1054" >< variable name="PE-C 0: Boiler - Consumption since de-ash" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12012" scale="10" unit="kg" min="" def="" max="" />
        public double ConsumptionSinceDeAsh { get; set; }

        // <register id = "1056" >< variable name="PE-C 0: Boiler - Consump. since ash box emptied" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12013" scale="10" unit="kg" min="" def="" max="" />
        public double ConsumptionSinceAshBoxEmptied { get; set; }

        // <register id = "1058" >< variable name="PE-C 0: Boiler - Empty ash box after" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12120" scale="10" unit="kg" min="0" def="10000" max="50000" />
        public double EmptyAshBoxAfter { get; set; }

        // <register id = "1060" >< variable name="PE-C 0: Boiler - Consump. since maint." nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12014" scale="10" unit="kg" min="" def="" max="" />
        public double ConsumptionSinceMaintainence { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1062" >< variable name="PE-C 0: Boiler - Hopper" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12005" scale="1" unit="" min="" def="" max="" />
        public HopperStates HopperState { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1064" >< variable name="PE-C 0: Boiler - Fill up pellet bin" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12071" scale="1" unit="" min="1800" def="1800" max="1801" />
        public StartValues HopperFillUpPelletBin { get; set; }

        // <register id = "1066" >< variable name="PE-C 0: Boiler - Pellet bin contents" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12011" scale="10" unit="kg" min="0" def="0" max="0" />
        public double HopperPelletBinContents { get; set; }

        // <register id = "1068" >< variable name="PE-C 0: Boiler - Fill-up time" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12152" scale="1" unit="" min="0" def="1140" max="1439" />
        public TimeSpan HopperFillUpTime { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1070" >< variable name="PE-C 0: Boiler - Aspirator" nodeId="40" fubId="10021" fktId="0" ioId="11042" varId="0" scale="1" unit="" min="" def="" max="" />
        public VacuumStates HopperVacuumState { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1072" >< variable name="PE-C 0: Boiler - Aspirator - Demand" nodeId="40" fubId="10021" fktId="0" ioId="11042" varId="2001" scale="1" unit="" min="" def="" max="" />
        public DemandValues HopperVacuumDemand { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1074" >< variable name="PE-C 0: Boiler - On/off button" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12080" scale="1" unit="" min="1802" def="1802" max="1803" />
        public OnOffStates OnOffButton { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1076" >< variable name="PE-C 0: Boiler - De-ash button" nodeId="40" fubId="10021" fktId="0" ioId="0" varId="12112" scale="1" unit="" min="1802" def="1802" max="1803" />
        public OnOffStates DeAshButton { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1078" >< variable name="GM-C 0: HW - Hot water tank" nodeId="120" fubId="10111" fktId="0" ioId="0" varId="12129" scale="1" unit="" min="" def="" max="" />
        public HWTankStates HotwaterTankState { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1080" >< variable name="GM-C 0: HW - Charging Times" nodeId="120" fubId="10111" fktId="12130" ioId="0" varId="0" scale="1" unit="" min="" def="" max="" />
        public OnOffStates ChargingTimesState { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1082" >< variable name="GM-C 0: HW - Charging Times - SwitchStatus" nodeId="120" fubId="10111" fktId="12130" ioId="0" varId="1109" scale="1" unit="" min="" def="" max="" />
        public OnOffStates ChargingTimesSwitchStatus { get; set; }

        // <register id = "1084" >< variable name="GM-C 0: HW - Charging Times - Temperature" nodeId="120" fubId="10111" fktId="12130" ioId="0" varId="1110" scale="10" unit="°C" min="" def="" max="" />
        public double ChargingTimesTemperature { get; set; }

        // <register id = "1086" >< variable name="GM-C 0: HW - Switch-on diff." nodeId="120" fubId="10111" fktId="0" ioId="0" varId="12133" scale="10" unit="°C" min="0" def="150" max="300" />
        public double HotwaterSwitchonDiff { get; set; }

        // <register id = "1088" >< variable name="GM-C 0: HW - Hot water tank target" nodeId="120" fubId="10111" fktId="0" ioId="0" varId="12132" scale="10" unit="°C" min="" def="" max="" />
        public double HotwaterTarget { get; set; }

        // <register id = "1090" >< variable name="GM-C 0: HW - Hot water tank" nodeId="120" fubId="10111" fktId="0" ioId="0" varId="12271" scale="10" unit="°C" min="" def="" max="" />
        public double HotwaterTemperature { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1092" >< variable name="GM-C 0: HW - Chrg. button" nodeId="120" fubId="10111" fktId="0" ioId="0" varId="12134" scale="1" unit="" min="1802" def="1802" max="1803" />
        public OnOffStates ChargeButton { get; set; }

        // <register id = "1094" >< variable name="GM-C 0: HC - Room sensor" nodeId="120" fubId="10101" fktId="0" ioId="11237" varId="0" scale="10" unit="°C" min="" def="" max="" />
        public double RoomSensor { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1096" >< variable name="GM-C 0: HC - Heating circuit" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12090" scale="1" unit="" min="" def="" max="" />
        public HeatingCircuitStates HeatingCircuitState { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1098" >< variable name="GM-C 0: HC - Running" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12092" scale="1" unit="" min="" def="" max="" />
        public HWRunningStates RunningState { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1100" >< variable name="GM-C 0: HC - Heat times" nodeId="120" fubId="10101" fktId="12113" ioId="0" varId="0" scale="1" unit="" min="" def="" max="" />
        public OnOffStates HeatingTimes { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1102" >< variable name="GM-C 0: HC - Heat times - SwitchStatus" nodeId="120" fubId="10101" fktId="12113" ioId="0" varId="1109" scale="1" unit="" min="" def="" max="" />
        public OnOffStates HeatingSwitchStatus { get; set; }

        // <register id = "1104" >< variable name="GM-C 0: HC - Heat times - Temperature" nodeId="120" fubId="10101" fktId="12113" ioId="0" varId="1110" scale="10" unit="°C" min="" def="" max="" />
        public double HeatingTemperature { get; set; }

        // <register id = "1106" >< variable name="GM-C 0: HC - Room" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12634" scale="10" unit="°C" min="" def="" max="" />
        public double RoomTemperature { get; set; }

        // <register id = "1108" >< variable name="GM-C 0: HC - RoomTarg" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12127" scale="10" unit="°C" min="" def="" max="" />
        public double RoomTarget { get; set; }

        // <register id = "1110" >< variable name="GM-C 0: HC - Flow" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12241" scale="10" unit="" min="" def="" max="" />
        public double Flow { get; set; }

        // <register id = "1112" >< variable name="GM-C 0: HC - Heating curve" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12111" scale="10" unit="°C" min="" def="" max="" />
        public double HeatingCurve { get; set; }

        // <register id = "1114" >< variable name="GM-C 0: HC - Flow at -10°C" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12104" scale="10" unit="°C" min="0" def="550" max="1000" />
        public double FlowAtMinus10 { get; set; }

        // <register id = "1116" >< variable name="GM-C 0: HC - Flow at +10°C" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12103" scale="10" unit="°C" min="0" def="350" max="1000" />
        public double FlowAtPlus10 { get; set; }

        // <register id = "1118" >< variable name="GM-C 0: HC - Set-back" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12107" scale="10" unit="°C" min="0" def="150" max="500" />
        public double FlowSetBack { get; set; }

        // <register id = "1120" >< variable name="GM-C 0: HC - Outside temp. delay" nodeId="120" fubId="10101" fktId="12095" ioId="0" varId="0" scale="10" unit="°C" min="" def="" max="" />
        public double OutsideTemperatureDelayed { get; set; }

        // <register id = "1122" >< variable name="GM-C 0: HC - Day Heating threshold" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12096" scale="10" unit="°C" min="-500" def="180" max="500" />
        public double DayHeatingThreshold { get; set; }

        // <register id = "1124" >< variable name="GM-C 0: HC - Night Heating threshold" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12097" scale="10" unit="°C" min="-500" def="20" max="500" />
        public double NightHeatingThreshold { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1126" >< variable name="GM-C 0: HC - Day button" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12125" scale="1" unit="" min="1802" def="1802" max="1803" />
        public OnOffStates HeatingDayButton { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1128" >< variable name="GM-C 0: HC - Auto button" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12126" scale="1" unit="" min="1802" def="1803" max="1803" />
        public OnOffStates HeatingAutoButton { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1130" >< variable name="GM-C 0: HC - Night button" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12230" scale="1" unit="" min="1802" def="1802" max="1803" />
        public OnOffStates HeatingNightButton { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1132" >< variable name="GM-C 0: HC - On/off button" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12080" scale="1" unit="" min="1802" def="1803" max="1803" />
        public OnOffStates HeatingOnOffButton { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1134" >< variable name="GM-C 0: HC - Home button" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12218" scale="1" unit="" min="1802" def="1802" max="1803" />
        public OnOffStates HeatingHomeButton { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1136" >< variable name="GM-C 0: HC - Away button" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12231" scale="1" unit="" min="1802" def="1802" max="1803" />
        public OnOffStates HeatingAwayButton { get; set; }

        // <register id = "1138" >< variable name="GM-C 0: HC - Holiday start" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12232" scale="1" unit="" min="0" def="0" max="0" />
        public DateTimeOffset HeatingHolidayStart { get; set; }

        // <register id = "1140" >< variable name="GM-C 0: HC - Holiday end" nodeId="120" fubId="10101" fktId="0" ioId="0" varId="12239" scale="1" unit="" min="0" def="0" max="0" />
        public DateTimeOffset HeatingHolidayEnd { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1142" >< variable name="PE-C 0: Store - Discharge screw - Demand" nodeId="40" fubId="10201" fktId="0" ioId="11029" varId="2001" scale="1" unit="" min="" def="" max="" />
        public DemandValuesEx DischargeScrewDemand { get; set; }

        // <register id = "1144" >< variable name="PE-C 0: Store - Discharge screw - Clock rate" nodeId="40" fubId="10201" fktId="0" ioId="11029" varId="2090" scale="10" unit="%" min="" def="" max="" />
        public double DischargeScrewClockRate { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1146" >< variable name="PE-C 0: Store - Discharge screw - State" nodeId="40" fubId="10201" fktId="0" ioId="11029" varId="2002" scale="1" unit="" min="" def="" max="" />
        public ScrewStates DischargeScrewState { get; set; }

        // <register id = "1148" >< variable name="PE-C 0: Store - Discharge screw - Motor curr." nodeId="40" fubId="10201" fktId="0" ioId="11029" varId="2091" scale="1" unit="mA" min="" def="" max="" />
        public double DischargeScrewMotorCurr { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1150" >< variable name="PE-C 0: Store - Conveying system" nodeId="40" fubId="10201" fktId="0" ioId="0" varId="12058" scale="1" unit="" min="" def="" max="" />
        public ConveyingSystemStates ConveyingSystem { get; set; }

        // <register id = "1152" >< variable name="PE-C 0: Store - Stock" nodeId="40" fubId="10201" fktId="0" ioId="0" varId="12015" scale="10" unit="kg" min="-1e+06" def="0" max="1e+06" />
        public double Stock { get; set; }

        // <register id = "1154" >< variable name="PE-C 0: Store - Warning limit for pellet stock" nodeId="40" fubId="10201" fktId="0" ioId="0" varId="12042" scale="10" unit="kg" min="0" def="0" max="1e+06" />
        public double StockWarningLimit { get; set; }

        // <register id = "1156" >< variable name="PE-C 0: Sys - Outside temp." nodeId="40" fubId="10241" fktId="0" ioId="0" varId="12197" scale="10" unit="°C" min="" def="" max="" />
        public double OutsideTemperature { get; set; }
        // Other (Boiler)

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1158" >< variable name="PE-C 0: Boiler - Firebed" nodeId="40" fubId="10021" fktId="0" ioId="11036" varId="0" scale="1" unit="" min="" def="" max="" />
        public FirebedStates FirebedState { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        // <register id = "1160" >< variable name="PE-C 0: Boiler - Supply - Demand" nodeId="40" fubId="10021" fktId="0" ioId="11181" varId="2001" scale="1" unit="" min="" def="" max="" />
        public DemandValuesEx SupplyDemand { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]

        // <register id = "1162" >< variable name="PE-C 0: Boiler - Ignition - Demand" nodeId="40" fubId="10021" fktId="0" ioId="11041" varId="2001" scale="1" unit="" min="" def="" max="" />
        public DemandValues IgnitionDemand { get; set; }

        // <register id = "1164" >< variable name="PE-C 0: Boiler - FL mix. valve 1" nodeId="40" fubId="10021" fktId="0" ioId="11121" varId="0" scale="10" unit="°C" min="" def="" max="" />
        public double FlowMixValveTemperature { get; set; }

        // <register id = "1166" >< variable name="PE-C 0: Boiler - Air valve - Set position" nodeId="40" fubId="10021" fktId="0" ioId="11115" varId="2070" scale="10" unit="%" min="" def="" max="" />
        public double AirValveSetPosition { get; set; }

        // <register id = "1168" >< variable name="PE-C 0: Boiler - Air valve - Curr. pos." nodeId="40" fubId="10021" fktId="0" ioId="11115" varId="2071" scale="10" unit="%" min="" def="" max="" />
        public double AirValveCurrPosition { get; set; }

        #endregion Public Properties
    }
}
