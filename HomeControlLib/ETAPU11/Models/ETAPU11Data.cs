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
        /// </summary>

        public TimeSpan FullLoadHours { get; set; }
        public double TotalConsumed { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public BoilerStates BoilerState { get; set; }
        public double BoilerPressure { get; set; }
        public double BoilerTemperature { get; set; }
        public double BoilerTarget { get; set; }
        public double BoilerBottom { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public FlowControlStates FlowControlState { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DiverterValveStates DiverterValveState { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DemandValues DiverterValveDemand { get; set; }
        public double DemandedOutput { get; set; }
        public double FlowMixValveTarget { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public FlowMixValveStates FlowMixValveState { get; set; }
        public double FlowMixValveCurrTemp { get; set; }
        public double FlowMixValvePosition { get; set; }
        public double BoilerPumpOutput { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DemandValues BoilerPumpDemand { get; set; }
        public double FlueGasTemperature { get; set; }
        public double DraughtFanSpeed { get; set; }
        public double ResidualO2 { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DemandValuesEx StokerScrewDemand { get; set; }
        public double StokerScrewClockRate { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ScrewStates StokerScrewState { get; set; }
        public double StokerScrewMotorCurr { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public AshRemovalStates AshRemovalState { get; set; }
        public TimeSpan AshRemovalStartIdleTime { get; set; }
        public TimeSpan AshRemovalDurationIdleTime { get; set; }
        public double ConsumptionSinceDeAsh { get; set; }
        public double ConsumptionSinceAshBoxEmptied { get; set; }
        public double EmptyAshBoxAfter { get; set; }
        public double ConsumptionSinceMaintainence { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public HopperStates HopperState { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public StartValues HopperFillUpPelletBin { get; set; }
        public double HopperPelletBinContents { get; set; }
        public TimeSpan HopperFillUpTime { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public VacuumStates HopperVacuumState { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DemandValues HopperVacuumDemand { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OnOffStates OnOffButton { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OnOffStates DeAshButton { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public HWTankStates HotwaterTankState { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OnOffStates ChargingTimesState { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OnOffStates ChargingTimesSwitchStatus { get; set; }
        public double ChargingTimesTemperature { get; set; }
        public double HotwaterSwitchonDiff { get; set; }
        public double HotwaterTarget { get; set; }
        public double HotwaterTemperature { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OnOffStates ChargeButton { get; set; }
        public double RoomSensor { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public HeatingCircuitStates HeatingCircuitState { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public HWRunningStates RunningState { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OnOffStates HeatingTimes { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OnOffStates HeatingSwitchStatus { get; set; }
        public double HeatingTemperature { get; set; }
        public double RoomTemperature { get; set; }
        public double RoomTarget { get; set; }
        public double Flow { get; set; }
        public double HeatingCurve { get; set; }
        public double FlowAtMinus10 { get; set; }
        public double FlowAtPlus10 { get; set; }
        public double FlowSetBack { get; set; }
        public double OutsideTemperatureDelayed { get; set; }
        public double DayHeatingThreshold { get; set; }
        public double NightHeatingThreshold { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OnOffStates HeatingDayButton { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OnOffStates HeatingAutoButton { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OnOffStates HeatingNightButton { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OnOffStates HeatingOnOffButton { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OnOffStates HeatingHomeButton { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OnOffStates HeatingAwayButton { get; set; }
        public DateTimeOffset HeatingHolidayStart { get; set; }
        public DateTimeOffset HeatingHolidayEnd { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DemandValuesEx DischargeScrewDemand { get; set; }
        public double DischargeScrewClockRate { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ScrewStates DischargeScrewState { get; set; }
        public double DischargeScrewMotorCurr { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ConveyingSystemStates ConveyingSystem { get; set; }
        public double Stock { get; set; }
        public double StockWarningLimit { get; set; }
        public double OutsideTemperature { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public FirebedStates FirebedState { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DemandValuesEx SupplyDemand { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DemandValues IgnitionDemand { get; set; }
        public double FlowMixValveTemperature { get; set; }
        public double AirValveSetPosition { get; set; }
        public double AirValveCurrPosition { get; set; }

        #endregion Public Properties
    }
}
