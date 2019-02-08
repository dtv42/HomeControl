// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KWLEC200Data.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.KWLEC200.Models
{
    #region Using Directives

    using System;
    using DataValueLib;

    #endregion

    public class KWLEC200Data : DataValue
    {
        #region Enums

        /// <summary>
        /// 
        /// </summary>
        public enum AutoSoftwareUpdates
        {
            Disabled = 0,
            Enabled = 1
        }

        /// <summary>
        /// 
        /// </summary>
        public enum ConfigOptions
        {
            DiBt = 1,
            Passive = 2
        }

        /// <summary>
        /// 
        /// </summary>
        public enum ContactTypes
        {
            Function1 = 1,
            Function2 = 2,
            Function3 = 3,
            Function4 = 4,
            Function5 = 5,
            Function6 = 6
        }

        /// <summary>
        /// 
        /// </summary>
        public enum DateFormats
        {
            DDMMYY = 0,
            MMDDYYYY = 1,
            YYYYMMDD = 2
        }

        /// <summary>
        /// 
        /// </summary>
        public enum DaylightSaving
        {
            Winter = 0,
            Summer = 1
        }

        /// <summary>
        /// 
        /// </summary>
        public enum FanLevelConfig
        {
            Continuous = 0,
            Discrete = 1
        }

        /// <summary>
        /// 
        /// </summary>
        public enum FanLevels
        {
            Level0 = 0,
            Level1 = 1,
            Level2 = 2,
            Level3 = 3,
            Level4 = 4
        }

        /// <summary>
        /// 
        /// </summary>
        public enum FaultTypes
        {
            MultipleFaults = 1,
            SingleFault = 2
        }

        /// <summary>
        /// 
        /// </summary>
        public enum FunctionTypes
        {
            Function1 = 1,
            Function2 = 2
        }

        /// <summary>
        /// 
        /// </summary>
        public enum GlobalUpdates
        {
            NotUpdated = 0,
            Manual = 1
        }

        /// <summary>
        /// 
        /// </summary>
        public enum HeatExchangerTypes
        {
            Plastic = 1,
            Aluminum = 2,
            Enthalpie = 3
        }

        /// <summary>
        /// 
        /// </summary>
        public enum HeliosPortalAccess
        {
            Disabled = 0,
            Enabled = 1
        }

        /// <summary>
        /// 
        /// </summary>
        public enum KwlFTFConfig
        {
            RF = 1,
            Temp = 2,
            Combined = 3
        }

        /// <summary>
        /// 
        /// </summary>
        public enum KwlSensorConfig
        {
            None = 0,
            Imstalled = 1
        }

        /// <summary>
        /// 
        /// </summary>
        public enum MinimumFanLevels
        {
            Level0 = 0,
            Level1 = 1
        }

        /// <summary>
        /// 
        /// </summary>
        public enum OperationModes
        {
            Automatic = 0,
            Manual = 1
        }

        /// <summary>
        /// 
        /// </summary>
        public enum PreheaterTypes
        {
            Basis = 1,
            ERW = 2,
            SEWT = 3,
            Other = 4
        }

        /// <summary>
        /// 
        /// </summary>
        public enum SensorStatus
        {
            Off = 0,
            Steps = 2,
            Smooth = 3
        }

        /// <summary>
        /// 
        /// </summary>
        public enum StatusTypes
        {
            Off = 0,
            On = 1
        }

        /// <summary>
        /// 
        /// </summary>
        public enum VacationOperations
        {
            Off = 0,
            Interval = 1,
            Constant = 2
        }

        /// <summary>
        /// 
        /// </summary>
        public enum WeeklyProfiles
        {
            Standard1 = 0,
            Standard2 = 1,
            Fixed = 2,
            Individual1 = 3,
            Individual2 = 4,
            NA = 5,
            Off = 6
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Helios KWLEC200 properties (data fields).
        /// The Helios attributes are used to define name, size and count of the property.
        /// </summary>
        public string ItemDescription { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        public string MacAddress { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public DateTime Date { get; set; } = new DateTime();
        public TimeSpan Time { get; set; } = new TimeSpan();
        public DaylightSaving DayLightSaving { get; set; } = new DaylightSaving();
        public AutoSoftwareUpdates AutoUpdateEnabled { get; set; } = new AutoSoftwareUpdates();
        public HeliosPortalAccess PortalAccessEnabled { get; set; } = new HeliosPortalAccess();
        public double ExhaustVentilatorVoltageLevel1 { get; set; }
        public double SupplyVentilatorVoltageLevel1 { get; set; }
        public double ExhaustVentilatorVoltageLevel2 { get; set; }
        public double SupplyVentilatorVoltageLevel2 { get; set; }
        public double ExhaustVentilatorVoltageLevel3 { get; set; }
        public double SupplyVentilatorVoltageLevel3 { get; set; }
        public double ExhaustVentilatorVoltageLevel4 { get; set; }
        public double SupplyVentilatorVoltageLevel4 { get; set; }
        public MinimumFanLevels MinimumVentilationLevel { get; set; } = new MinimumFanLevels();
        public StatusTypes KwlBeEnabled { get; set; } = new StatusTypes();
        public StatusTypes KwlBecEnabled { get; set; } = new StatusTypes();
        public ConfigOptions DeviceConfiguration { get; set; } = new ConfigOptions();
        public StatusTypes PreheaterStatus { get; set; } = new StatusTypes();
        public KwlFTFConfig KwlFTFConfig0 { get; set; } = new KwlFTFConfig();
        public KwlFTFConfig KwlFTFConfig1 { get; set; } = new KwlFTFConfig();
        public KwlFTFConfig KwlFTFConfig2 { get; set; } = new KwlFTFConfig();
        public KwlFTFConfig KwlFTFConfig3 { get; set; } = new KwlFTFConfig();
        public KwlFTFConfig KwlFTFConfig4 { get; set; } = new KwlFTFConfig();
        public KwlFTFConfig KwlFTFConfig5 { get; set; } = new KwlFTFConfig();
        public KwlFTFConfig KwlFTFConfig6 { get; set; } = new KwlFTFConfig();
        public KwlFTFConfig KwlFTFConfig7 { get; set; } = new KwlFTFConfig();
        public SensorStatus HumidityControlStatus { get; set; } = new SensorStatus();
        public int HumidityControlTarget { get; set; }
        public int HumidityControlStep { get; set; }
        public int HumidityControlStop { get; set; }
        public SensorStatus CO2ControlStatus { get; set; } = new SensorStatus();
        public int CO2ControlTarget { get; set; }
        public int CO2ControlStep { get; set; }
        public SensorStatus VOCControlStatus { get; set; } = new SensorStatus();
        public int VOCControlTarget { get; set; }
        public int VOCControlStep { get; set; }
        public double ThermalComfortTemperature { get; set; }
        public int TimeZoneOffset { get; set; }
        public DateFormats DateFormat { get; set; } = new DateFormats();
        public HeatExchangerTypes HeatExchangerType { get; set; } = new HeatExchangerTypes();
        public int PartyOperationDuration { get; set; }
        public FanLevels PartyVentilationLevel { get; set; } = new FanLevels();
        public int PartyOperationRemaining { get; set; }
        public StatusTypes PartyOperationActivate { get; set; } = new StatusTypes();
        public int StandbyOperationDuration { get; set; }
        public FanLevels StandbyVentilationLevel { get; set; } = new FanLevels();
        public int StandbyOperationRemaining { get; set; }
        public StatusTypes StandbyOperationActivate { get; set; } = new StatusTypes();
        public OperationModes OperationMode { get; set; } = new OperationModes();
        public FanLevels VentilationLevel { get; set; } = new FanLevels();
        public int VentilationPercentage { get; set; }
        public double TemperatureOutdoor { get; set; }
        public double TemperatureSupply { get; set; }
        public double TemperatureExhaust { get; set; }
        public double TemperatureExtract { get; set; }
        public double TemperaturePreHeater { get; set; }
        public double TemperaturePostHeater { get; set; }
        public double ExternalHumiditySensor1 { get; set; }
        public double ExternalHumiditySensor2 { get; set; }
        public double ExternalHumiditySensor3 { get; set; }
        public double ExternalHumiditySensor4 { get; set; }
        public double ExternalHumiditySensor5 { get; set; }
        public double ExternalHumiditySensor6 { get; set; }
        public double ExternalHumiditySensor7 { get; set; }
        public double ExternalHumiditySensor8 { get; set; }
        public double ExternalHumidityTemperature1 { get; set; }
        public double ExternalHumidityTemperature2 { get; set; }
        public double ExternalHumidityTemperature3 { get; set; }
        public double ExternalHumidityTemperature4 { get; set; }
        public double ExternalHumidityTemperature5 { get; set; }
        public double ExternalHumidityTemperature6 { get; set; }
        public double ExternalHumidityTemperature7 { get; set; }
        public double ExternalHumidityTemperature8 { get; set; }
        public double ExternalCO2Sensor1 { get; set; }
        public double ExternalCO2Sensor2 { get; set; }
        public double ExternalCO2Sensor3 { get; set; }
        public double ExternalCO2Sensor4 { get; set; }
        public double ExternalCO2Sensor5 { get; set; }
        public double ExternalCO2Sensor6 { get; set; }
        public double ExternalCO2Sensor7 { get; set; }
        public double ExternalCO2Sensor8 { get; set; }
        public double ExternalVOCSensor1 { get; set; }
        public double ExternalVOCSensor2 { get; set; }
        public double ExternalVOCSensor3 { get; set; }
        public double ExternalVOCSensor4 { get; set; }
        public double ExternalVOCSensor5 { get; set; }
        public double ExternalVOCSensor6 { get; set; }
        public double ExternalVOCSensor7 { get; set; }
        public double ExternalVOCSensor8 { get; set; }
        public double TemperatureChannel { get; set; }
        public WeeklyProfiles WeeklyProfile { get; set; } = new WeeklyProfiles();
        public string SerialNumber { get; set; } = string.Empty;
        public string ProductionCode { get; set; } = string.Empty;
        public int SupplyFanSpeed { get; set; }
        public int ExhaustFanSpeed { get; set; }
        public bool Logout { get; set; }
        public VacationOperations VacationOperation { get; set; } = new VacationOperations();
        public FanLevels VacationVentilationLevel { get; set; } = new FanLevels();
        public DateTime VacationStartDate { get; set; } = new DateTime();
        public DateTime VacationEndDate { get; set; } = new DateTime();
        public int VacationInterval { get; set; }
        public int VacationDuration { get; set; }
        public PreheaterTypes PreheaterType { get; set; } = new PreheaterTypes();
        public FunctionTypes KwlFunctionType { get; set; } = new FunctionTypes();
        public int HeaterAfterRunTime { get; set; }
        public ContactTypes ExternalContact { get; set; } = new ContactTypes();
        public FaultTypes FaultTypeOutput { get; set; } = new FaultTypes();
        public StatusTypes FilterChange { get; set; } = new StatusTypes();
        public int FilterChangeInterval { get; set; }
        public int FilterChangeRemaining { get; set; }
        public int BypassRoomTemperature { get; set; }
        public int BypassOutdoorTemperature { get; set; }
        public int BypassOutdoorTemperature2 { get; set; }
        public bool StartReset { get; set; }
        public bool FactoryReset { get; set; }
        public FanLevels SupplyLevel { get; set; } = new FanLevels();
        public FanLevels ExhaustLevel { get; set; } = new FanLevels();
        public FanLevels FanLevelRegion02 { get; set; } = new FanLevels();
        public FanLevels FanLevelRegion24 { get; set; } = new FanLevels();
        public FanLevels FanLevelRegion46 { get; set; } = new FanLevels();
        public FanLevels FanLevelRegion68 { get; set; } = new FanLevels();
        public FanLevels FanLevelRegion80 { get; set; } = new FanLevels();
        public double OffsetExhaust { get; set; }
        public FanLevelConfig FanLevelConfiguration { get; set; } = new FanLevelConfig();
        public string SensorName1 { get; set; } = string.Empty;
        public string SensorName2 { get; set; } = string.Empty;
        public string SensorName3 { get; set; } = string.Empty;
        public string SensorName4 { get; set; } = string.Empty;
        public string SensorName5 { get; set; } = string.Empty;
        public string SensorName6 { get; set; } = string.Empty;
        public string SensorName7 { get; set; } = string.Empty;
        public string SensorName8 { get; set; } = string.Empty;
        public string CO2SensorName1 { get; set; } = string.Empty;
        public string CO2SensorName2 { get; set; } = string.Empty;
        public string CO2SensorName3 { get; set; } = string.Empty;
        public string CO2SensorName4 { get; set; } = string.Empty;
        public string CO2SensorName5 { get; set; } = string.Empty;
        public string CO2SensorName6 { get; set; } = string.Empty;
        public string CO2SensorName7 { get; set; } = string.Empty;
        public string CO2SensorName8 { get; set; } = string.Empty;
        public string VOCSensorName1 { get; set; } = string.Empty;
        public string VOCSensorName2 { get; set; } = string.Empty;
        public string VOCSensorName3 { get; set; } = string.Empty;
        public string VOCSensorName4 { get; set; } = string.Empty;
        public string VOCSensorName5 { get; set; } = string.Empty;
        public string VOCSensorName6 { get; set; } = string.Empty;
        public string VOCSensorName7 { get; set; } = string.Empty;
        public string VOCSensorName8 { get; set; } = string.Empty;
        public string SoftwareVersion { get; set; } = string.Empty;
        public int OperationMinutesSupply { get; set; }
        public int OperationMinutesExhaust { get; set; }
        public int OperationMinutesPreheater { get; set; }
        public int OperationMinutesAfterheater { get; set; }
        public double PowerPreheater { get; set; }
        public double PowerAfterheater { get; set; }
        public bool ResetFlag { get; set; }
        public int ErrorCode { get; set; }
        public int WarningCode { get; set; }
        public int InfoCode { get; set; }
        public int NumberOfErrors { get; set; }
        public int NumberOfWarnings { get; set; }
        public int NumberOfInfos { get; set; }
        public string Errors { get; set; } = string.Empty;
        public int Warnings { get; set; }
        public string Infos { get; set; } = string.Empty;
        public string StatusFlags { get; set; } = string.Empty;
        //public GlobalUpdates GlobalUpdate { get; set; } = new GlobalUpdates();
        //public int LastError { get; set; }
        public bool ClearError { get; set; }
        public KwlSensorConfig SensorConfig1 { get; set; } = new KwlSensorConfig();
        public KwlSensorConfig SensorConfig2 { get; set; } = new KwlSensorConfig();
        public KwlSensorConfig SensorConfig3 { get; set; } = new KwlSensorConfig();
        public KwlSensorConfig SensorConfig4 { get; set; } = new KwlSensorConfig();
        public KwlSensorConfig SensorConfig5 { get; set; } = new KwlSensorConfig();
        public KwlSensorConfig SensorConfig6 { get; set; } = new KwlSensorConfig();
        public KwlSensorConfig SensorConfig7 { get; set; } = new KwlSensorConfig();
        public KwlSensorConfig SensorConfig8 { get; set; } = new KwlSensorConfig();

        #endregion Public Properties
    }
}
