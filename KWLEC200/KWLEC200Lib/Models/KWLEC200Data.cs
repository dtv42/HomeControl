// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KWLEC200Data.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200Lib.Models
{
    #region Using Directives

    using System;
    using System.Reflection;
    using System.Linq;
    using DataValueLib;

    #endregion

    public class KWLEC200Data : DataValue, IPropertyHelper
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
        [Helios("v00000", 31, 20)] public string ItemDescription { get; set; } = string.Empty;
        [Helios("v00001", 16, 12)] public string OrderNumber { get; set; } = string.Empty;
        [Helios("v00002", 18, 13)] public string MacAddress { get; set; } = string.Empty;
        [Helios("v00003", 2, 5)] public string Language { get; set; } = string.Empty;
        [Helios("v00004", 10, 9)] public DateTime Date { get; set; } = new DateTime();
        [Helios("v00005", 10, 9)] public TimeSpan Time { get; set; } = new TimeSpan();
        [Helios("v00006", 1, 5)] public DaylightSaving DayLightSaving { get; set; } = new DaylightSaving();
        [Helios("v00007", 1, 5)] public AutoSoftwareUpdates AutoUpdateEnabled { get; set; } = new AutoSoftwareUpdates();
        [Helios("v00008", 1, 5)] public HeliosPortalAccess PortalAccessEnabled { get; set; } = new HeliosPortalAccess();
        [Helios("v00012", 3, 6)] public double ExhaustVentilatorVoltageLevel1 { get; set; }
        [Helios("v00013", 3, 6)] public double SupplyVentilatorVoltageLevel1 { get; set; }
        [Helios("v00014", 3, 6)] public double ExhaustVentilatorVoltageLevel2 { get; set; }
        [Helios("v00015", 3, 6)] public double SupplyVentilatorVoltageLevel2 { get; set; }
        [Helios("v00016", 3, 6)] public double ExhaustVentilatorVoltageLevel3 { get; set; }
        [Helios("v00017", 3, 6)] public double SupplyVentilatorVoltageLevel3 { get; set; }
        [Helios("v00018", 3, 6)] public double ExhaustVentilatorVoltageLevel4 { get; set; }
        [Helios("v00019", 3, 6)] public double SupplyVentilatorVoltageLevel4 { get; set; }
        [Helios("v00020", 1, 5)] public MinimumFanLevels MinimumVentilationLevel { get; set; } = new MinimumFanLevels();
        [Helios("v00021", 1, 5)] public StatusTypes KwlBeEnabled { get; set; } = new StatusTypes();
        [Helios("v00022", 1, 5)] public StatusTypes KwlBecEnabled { get; set; } = new StatusTypes();
        [Helios("v00023", 1, 5)] public ConfigOptions DeviceConfiguration { get; set; } = new ConfigOptions();
        [Helios("v00024", 1, 5)] public StatusTypes PreheaterStatus { get; set; } = new StatusTypes();
        [Helios("v00025", 1, 5)] public KwlFTFConfig KwlFTFConfig0 { get; set; } = new KwlFTFConfig();
        [Helios("v00026", 1, 5)] public KwlFTFConfig KwlFTFConfig1 { get; set; } = new KwlFTFConfig();
        [Helios("v00027", 1, 5)] public KwlFTFConfig KwlFTFConfig2 { get; set; } = new KwlFTFConfig();
        [Helios("v00028", 1, 5)] public KwlFTFConfig KwlFTFConfig3 { get; set; } = new KwlFTFConfig();
        [Helios("v00029", 1, 5)] public KwlFTFConfig KwlFTFConfig4 { get; set; } = new KwlFTFConfig();
        [Helios("v00030", 1, 5)] public KwlFTFConfig KwlFTFConfig5 { get; set; } = new KwlFTFConfig();
        [Helios("v00031", 1, 5)] public KwlFTFConfig KwlFTFConfig6 { get; set; } = new KwlFTFConfig();
        [Helios("v00032", 1, 5)] public KwlFTFConfig KwlFTFConfig7 { get; set; } = new KwlFTFConfig();
        [Helios("v00033", 1, 5)] public SensorStatus HumidityControlStatus { get; set; } = new SensorStatus();
        [Helios("v00034", 2, 5)] public int HumidityControlTarget { get; set; }
        [Helios("v00035", 2, 5)] public int HumidityControlStep { get; set; }
        [Helios("v00036", 2, 5)] public int HumidityControlStop { get; set; }
        [Helios("v00037", 1, 5)] public SensorStatus CO2ControlStatus { get; set; } = new SensorStatus();
        [Helios("v00038", 4, 6)] public int CO2ControlTarget { get; set; }
        [Helios("v00039", 3, 6)] public int CO2ControlStep { get; set; }
        [Helios("v00040", 1, 5)] public SensorStatus VOCControlStatus { get; set; } = new SensorStatus();
        [Helios("v00041", 4, 6)] public int VOCControlTarget { get; set; }
        [Helios("v00042", 3, 6)] public int VOCControlStep { get; set; }
        [Helios("v00043", 4, 6)] public double ThermalComfortTemperature { get; set; }
        [Helios("v00051", 3, 6)] public int TimeZoneOffset { get; set; }
        [Helios("v00052", 1, 5)] public DateFormats DateFormat { get; set; } = new DateFormats();
        [Helios("v00053", 1, 5)] public HeatExchangerTypes HeatExchangerType { get; set; } = new HeatExchangerTypes();
        [Helios("v00091", 3, 6)] public int PartyOperationDuration { get; set; }
        [Helios("v00092", 3, 5)] public FanLevels PartyVentilationLevel { get; set; } = new FanLevels();
        [Helios("v00093", 3, 6)] public int PartyOperationRemaining { get; set; }
        [Helios("v00094", 1, 5)] public StatusTypes PartyOperationActivate { get; set; } = new StatusTypes();
        [Helios("v00096", 3, 6)] public int StandbyOperationDuration { get; set; }
        [Helios("v00097", 3, 5)] public FanLevels StandbyVentilationLevel { get; set; } = new FanLevels();
        [Helios("v00098", 3, 6)] public int StandbyOperationRemaining { get; set; }
        [Helios("v00099", 1, 5)] public StatusTypes StandbyOperationActivate { get; set; } = new StatusTypes();
        [Helios("v00101", 1, 5)] public OperationModes OperationMode { get; set; } = new OperationModes();
        [Helios("v00102", 1, 5)] public FanLevels VentilationLevel { get; set; } = new FanLevels();
        [Helios("v00103", 3, 6)] public int VentilationPercentage { get; set; }
        [Helios("v00104", 7, 8)] public double TemperatureOutdoor { get; set; }
        [Helios("v00105", 7, 8)] public double TemperatureSupply { get; set; }
        [Helios("v00106", 7, 8)] public double TemperatureExhaust { get; set; }
        [Helios("v00107", 7, 8)] public double TemperatureExtract { get; set; }
        [Helios("v00108", 7, 8)] public double TemperaturePreHeater { get; set; }
        [Helios("v00110", 7, 8)] public double TemperaturePostHeater { get; set; }
        [Helios("v00111", 7, 8)] public double ExternalHumiditySensor1 { get; set; }
        [Helios("v00112", 7, 8)] public double ExternalHumiditySensor2 { get; set; }
        [Helios("v00113", 7, 8)] public double ExternalHumiditySensor3 { get; set; }
        [Helios("v00114", 7, 8)] public double ExternalHumiditySensor4 { get; set; }
        [Helios("v00115", 7, 8)] public double ExternalHumiditySensor5 { get; set; }
        [Helios("v00116", 7, 8)] public double ExternalHumiditySensor6 { get; set; }
        [Helios("v00117", 7, 8)] public double ExternalHumiditySensor7 { get; set; }
        [Helios("v00118", 7, 8)] public double ExternalHumiditySensor8 { get; set; }
        [Helios("v00119", 7, 8)] public double ExternalHumidityTemperature1 { get; set; }
        [Helios("v00120", 7, 8)] public double ExternalHumidityTemperature2 { get; set; }
        [Helios("v00121", 7, 8)] public double ExternalHumidityTemperature3 { get; set; }
        [Helios("v00122", 7, 8)] public double ExternalHumidityTemperature4 { get; set; }
        [Helios("v00123", 7, 8)] public double ExternalHumidityTemperature5 { get; set; }
        [Helios("v00124", 7, 8)] public double ExternalHumidityTemperature6 { get; set; }
        [Helios("v00125", 7, 8)] public double ExternalHumidityTemperature7 { get; set; }
        [Helios("v00126", 7, 8)] public double ExternalHumidityTemperature8 { get; set; }
        [Helios("v00128", 4, 6)] public double ExternalCO2Sensor1 { get; set; }
        [Helios("v00129", 4, 6)] public double ExternalCO2Sensor2 { get; set; }
        [Helios("v00130", 4, 6)] public double ExternalCO2Sensor3 { get; set; }
        [Helios("v00131", 4, 6)] public double ExternalCO2Sensor4 { get; set; }
        [Helios("v00132", 4, 6)] public double ExternalCO2Sensor5 { get; set; }
        [Helios("v00133", 4, 6)] public double ExternalCO2Sensor6 { get; set; }
        [Helios("v00134", 4, 6)] public double ExternalCO2Sensor7 { get; set; }
        [Helios("v00135", 4, 6)] public double ExternalCO2Sensor8 { get; set; }
        [Helios("v00136", 4, 6)] public double ExternalVOCSensor1 { get; set; }
        [Helios("v00137", 4, 6)] public double ExternalVOCSensor2 { get; set; }
        [Helios("v00138", 4, 6)] public double ExternalVOCSensor3 { get; set; }
        [Helios("v00139", 4, 6)] public double ExternalVOCSensor4 { get; set; }
        [Helios("v00140", 4, 6)] public double ExternalVOCSensor5 { get; set; }
        [Helios("v00141", 4, 6)] public double ExternalVOCSensor6 { get; set; }
        [Helios("v00142", 4, 6)] public double ExternalVOCSensor7 { get; set; }
        [Helios("v00143", 4, 6)] public double ExternalVOCSensor8 { get; set; }
        [Helios("v00146", 7, 8)] public double TemperatureChannel { get; set; }
        [Helios("v00201", 1, 5)] public WeeklyProfiles WeeklyProfile { get; set; } = new WeeklyProfiles();
        [Helios("v00303", 16, 12)] public string SerialNumber { get; set; } = string.Empty;
        [Helios("v00304", 13, 11)] public string ProductionCode { get; set; } = string.Empty;
        [Helios("v00348", 4, 6)] public int SupplyFanSpeed { get; set; }
        [Helios("v00349", 4, 6)] public int ExhaustFanSpeed { get; set; }
        [Helios("v00403", 1, 5)] public bool Logout { get; set; }
        [Helios("v00601", 1, 5)] public VacationOperations VacationOperation { get; set; } = new VacationOperations();
        [Helios("v00602", 1, 5)] public FanLevels VacationVentilationLevel { get; set; } = new FanLevels();
        [Helios("v00603", 10, 9)] public DateTime VacationStartDate { get; set; } = new DateTime();
        [Helios("v00604", 10, 9)] public DateTime VacationEndDate { get; set; } = new DateTime();
        [Helios("v00605", 2, 5)] public int VacationInterval { get; set; }
        [Helios("v00606", 3, 6)] public int VacationDuration { get; set; }
        [Helios("v01010", 1, 5)] public PreheaterTypes PreheaterType { get; set; } = new PreheaterTypes();
        [Helios("v01017", 1, 5)] public FunctionTypes KwlFunctionType { get; set; } = new FunctionTypes();
        [Helios("v01019", 3, 6)] public int HeaterAfterRunTime { get; set; }
        [Helios("v01020", 1, 5)] public ContactTypes ExternalContact { get; set; } = new ContactTypes();
        [Helios("v01021", 1, 5)] public FaultTypes FaultTypeOutput { get; set; } = new FaultTypes();
        [Helios("v01031", 1, 5)] public StatusTypes FilterChange { get; set; } = new StatusTypes();
        [Helios("v01032", 2, 5)] public int FilterChangeInterval { get; set; }
        [Helios("v01033", 10, 9)] public int FilterChangeRemaining { get; set; }
        [Helios("v01035", 2, 5)] public int BypassRoomTemperature { get; set; }
        [Helios("v01036", 2, 5)] public int BypassOutdoorTemperature { get; set; }
        [Helios("v01037", 2, 5)] public int BypassOutdoorTemperature2 { get; set; }
        [Helios("v01041", 1, 5)] public bool StartReset { get; set; }
        [Helios("v01042", 1, 5)] public bool FactoryReset { get; set; }
        [Helios("v01050", 1, 5)] public FanLevels SupplyLevel { get; set; } = new FanLevels();
        [Helios("v01051", 1, 5)] public FanLevels ExhaustLevel { get; set; } = new FanLevels();
        [Helios("v01061", 1, 5)] public FanLevels FanLevelRegion02 { get; set; } = new FanLevels();
        [Helios("v01062", 1, 5)] public FanLevels FanLevelRegion24 { get; set; } = new FanLevels();
        [Helios("v01063", 1, 5)] public FanLevels FanLevelRegion46 { get; set; } = new FanLevels();
        [Helios("v01064", 1, 5)] public FanLevels FanLevelRegion68 { get; set; } = new FanLevels();
        [Helios("v01065", 1, 5)] public FanLevels FanLevelRegion80 { get; set; } = new FanLevels();
        [Helios("v01066", 10, 9)] public double OffsetExhaust { get; set; }
        [Helios("v01068", 1, 5)] public FanLevelConfig FanLevelConfiguration { get; set; } = new FanLevelConfig();
        [Helios("v01071", 15, 12)] public string SensorName1 { get; set; } = string.Empty;
        [Helios("v01072", 15, 12)] public string SensorName2 { get; set; } = string.Empty;
        [Helios("v01073", 15, 12)] public string SensorName3 { get; set; } = string.Empty;
        [Helios("v01074", 15, 12)] public string SensorName4 { get; set; } = string.Empty;
        [Helios("v01075", 15, 12)] public string SensorName5 { get; set; } = string.Empty;
        [Helios("v01076", 15, 12)] public string SensorName6 { get; set; } = string.Empty;
        [Helios("v01077", 15, 12)] public string SensorName7 { get; set; } = string.Empty;
        [Helios("v01078", 15, 12)] public string SensorName8 { get; set; } = string.Empty;
        [Helios("v01081", 15, 12)] public string CO2SensorName1 { get; set; } = string.Empty;
        [Helios("v01082", 15, 12)] public string CO2SensorName2 { get; set; } = string.Empty;
        [Helios("v01083", 15, 12)] public string CO2SensorName3 { get; set; } = string.Empty;
        [Helios("v01084", 15, 12)] public string CO2SensorName4 { get; set; } = string.Empty;
        [Helios("v01085", 15, 12)] public string CO2SensorName5 { get; set; } = string.Empty;
        [Helios("v01086", 15, 12)] public string CO2SensorName6 { get; set; } = string.Empty;
        [Helios("v01087", 15, 12)] public string CO2SensorName7 { get; set; } = string.Empty;
        [Helios("v01088", 15, 12)] public string CO2SensorName8 { get; set; } = string.Empty;
        [Helios("v01091", 15, 12)] public string VOCSensorName1 { get; set; } = string.Empty;
        [Helios("v01092", 15, 12)] public string VOCSensorName2 { get; set; } = string.Empty;
        [Helios("v01093", 15, 12)] public string VOCSensorName3 { get; set; } = string.Empty;
        [Helios("v01094", 15, 12)] public string VOCSensorName4 { get; set; } = string.Empty;
        [Helios("v01095", 15, 12)] public string VOCSensorName5 { get; set; } = string.Empty;
        [Helios("v01096", 15, 12)] public string VOCSensorName6 { get; set; } = string.Empty;
        [Helios("v01097", 15, 12)] public string VOCSensorName7 { get; set; } = string.Empty;
        [Helios("v01098", 15, 12)] public string VOCSensorName8 { get; set; } = string.Empty;
        [Helios("v01101", 5, 7)] public string SoftwareVersion { get; set; } = string.Empty;
        [Helios("v01103", 10, 9)] public int OperationMinutesSupply { get; set; }
        [Helios("v01104", 10, 9)] public int OperationMinutesExhaust { get; set; }
        [Helios("v01105", 10, 9)] public int OperationMinutesPreheater { get; set; }
        [Helios("v01106", 10, 9)] public int OperationMinutesAfterheater { get; set; }
        [Helios("v01108", 10, 9)] public double PowerPreheater { get; set; }
        [Helios("v01109", 10, 9)] public double PowerAfterheater { get; set; }
        [Helios("v01120", 1, 5)] public bool ResetFlag { get; set; }
        [Helios("v01123", 10, 9)] public int ErrorCode { get; set; }
        [Helios("v01124", 3, 6)] public int WarningCode { get; set; }
        [Helios("v01125", 3, 6)] public int InfoCode { get; set; }
        [Helios("v01300", 2, 5)] public int NumberOfErrors { get; set; }
        [Helios("v01301", 1, 5)] public int NumberOfWarnings { get; set; }
        [Helios("v01302", 1, 5)] public int NumberOfInfos { get; set; }
        [Helios("v01303", 32, 20)] public string Errors { get; set; } = string.Empty;
        [Helios("v01304", 8, 8)] public int Warnings { get; set; }
        [Helios("v01305", 8, 8)] public string Infos { get; set; } = string.Empty;
        [Helios("v01306", 32, 20)] public string StatusFlags { get; set; } = string.Empty;
        //[Helios("v02013", 1, 5)] public GlobalUpdates GlobalUpdate { get; set; } = new GlobalUpdates();
        //[Helios("v02014", 3, 6)] public int LastError { get; set; }
        [Helios("v02015", 1, 5)] public bool ClearError { get; set; }
        [Helios("v02020", 1, 5)] public KwlSensorConfig SensorConfig1 { get; set; } = new KwlSensorConfig();
        [Helios("v02021", 1, 5)] public KwlSensorConfig SensorConfig2 { get; set; } = new KwlSensorConfig();
        [Helios("v02022", 1, 5)] public KwlSensorConfig SensorConfig3 { get; set; } = new KwlSensorConfig();
        [Helios("v02023", 1, 5)] public KwlSensorConfig SensorConfig4 { get; set; } = new KwlSensorConfig();
        [Helios("v02024", 1, 5)] public KwlSensorConfig SensorConfig5 { get; set; } = new KwlSensorConfig();
        [Helios("v02025", 1, 5)] public KwlSensorConfig SensorConfig6 { get; set; } = new KwlSensorConfig();
        [Helios("v02026", 1, 5)] public KwlSensorConfig SensorConfig7 { get; set; } = new KwlSensorConfig();
        [Helios("v02027", 1, 5)] public KwlSensorConfig SensorConfig8 { get; set; } = new KwlSensorConfig();

        #endregion Public Properties

        #region Public Property Helper

        /// <summary>
        /// Returns the Helios attribute of the property.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The Helios attribute.</returns>
        public static HeliosAttribute GetHeliosAttribute(string property) =>
            HeliosAttribute.GetHeliosAttribute(PropertyValue.GetPropertyInfo(typeof(KWLEC200Data), property));

        /// <summary>
        /// Gets the Helios value name attribute of the property.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The Helios value name.</returns>
        public static string GetName(string property) => GetHeliosAttribute(property).Name;

        /// <summary>
        /// Gets the Helios value size attribute of the property.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The Helios value size.</returns>
        public static ushort GetSize(string property) => GetHeliosAttribute(property).Size;

        /// <summary>
        /// Gets the Helios value count attribute of the property.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The Helios count size.</returns>
        public static ushort GetCount(string property) => GetHeliosAttribute(property).Count;

        /// <summary>
        /// Returns true if the property is readable.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>True if readable.</returns>
        public static bool IsReadable(string property)
        {
            switch (property)
            {
                case "ItemDescription": return true;
                case "OrderNumber": return true;
                case "MacAddress": return true;
                case "Language": return true;
                case "Date": return true;
                case "Time": return true;
                case "DayLightSaving": return true;
                case "AutoUpdateEnabled": return true;
                case "PortalAccessEnabled": return true;
                case "ExhaustVentilatorVoltageLevel1": return true;
                case "SupplyVentilatorVoltageLevel1": return true;
                case "ExhaustVentilatorVoltageLevel2": return true;
                case "SupplyVentilatorVoltageLevel2": return true;
                case "ExhaustVentilatorVoltageLevel3": return true;
                case "SupplyVentilatorVoltageLevel3": return true;
                case "ExhaustVentilatorVoltageLevel4": return true;
                case "SupplyVentilatorVoltageLevel4": return true;
                case "MinimumVentilationLevel": return true;
                case "KwlBeEnabled": return true;
                case "KwlBecEnabled": return true;
                case "DeviceConfiguration": return true;
                case "PreheaterStatus": return true;
                case "KwlFTFConfig0": return true;
                case "KwlFTFConfig1": return true;
                case "KwlFTFConfig2": return true;
                case "KwlFTFConfig3": return true;
                case "KwlFTFConfig4": return true;
                case "KwlFTFConfig5": return true;
                case "KwlFTFConfig6": return true;
                case "KwlFTFConfig7": return true;
                case "HumidityControlStatus": return true;
                case "HumidityControlTarget": return true;
                case "HumidityControlStep": return true;
                case "HumidityControlStop": return true;
                case "CO2ControlStatus": return true;
                case "CO2ControlTarget": return true;
                case "CO2ControlStep": return true;
                case "VOCControlStatus": return true;
                case "VOCControlTarget": return true;
                case "VOCControlStep": return true;
                case "ThermalComfortTemperature": return true;
                case "TimeZoneOffset": return true;
                case "DateFormat": return true;
                case "HeatExchangerType": return true;
                case "PartyOperationDuration": return true;
                case "PartyVentilationLevel": return true;
                case "PartyOperationRemaining": return true;
                case "PartyOperationActivate": return true;
                case "StandbyOperationDuration": return true;
                case "StandbyVentilationLevel": return true;
                case "StandbyOperationRemaining": return true;
                case "StandbyOperationActivate": return true;
                case "OperationMode": return true;
                case "VentilationLevel": return true;
                case "VentilationPercentage": return true;
                case "TemperatureOutdoor": return true;
                case "TemperatureSupply": return true;
                case "TemperatureExhaust": return true;
                case "TemperatureExtract": return true;
                case "TemperaturePreHeater": return true;
                case "TemperaturePostHeater": return true;
                case "ExternalHumiditySensor1": return true;
                case "ExternalHumiditySensor2": return true;
                case "ExternalHumiditySensor3": return true;
                case "ExternalHumiditySensor4": return true;
                case "ExternalHumiditySensor5": return true;
                case "ExternalHumiditySensor6": return true;
                case "ExternalHumiditySensor7": return true;
                case "ExternalHumiditySensor8": return true;
                case "ExternalHumidityTemperature1": return true;
                case "ExternalHumidityTemperature2": return true;
                case "ExternalHumidityTemperature3": return true;
                case "ExternalHumidityTemperature4": return true;
                case "ExternalHumidityTemperature5": return true;
                case "ExternalHumidityTemperature6": return true;
                case "ExternalHumidityTemperature7": return true;
                case "ExternalHumidityTemperature8": return true;
                case "ExternalCO2Sensor1": return true;
                case "ExternalCO2Sensor2": return true;
                case "ExternalCO2Sensor3": return true;
                case "ExternalCO2Sensor4": return true;
                case "ExternalCO2Sensor5": return true;
                case "ExternalCO2Sensor6": return true;
                case "ExternalCO2Sensor7": return true;
                case "ExternalCO2Sensor8": return true;
                case "ExternalVOCSensor1": return true;
                case "ExternalVOCSensor2": return true;
                case "ExternalVOCSensor3": return true;
                case "ExternalVOCSensor4": return true;
                case "ExternalVOCSensor5": return true;
                case "ExternalVOCSensor6": return true;
                case "ExternalVOCSensor7": return true;
                case "ExternalVOCSensor8": return true;
                case "TemperatureChannel": return true;
                case "WeeklyProfile": return true;
                case "SerialNumber": return true;
                case "ProductionCode": return true;
                case "SupplyFanSpeed": return true;
                case "ExhaustFanSpeed": return true;
                // Logout is not readable.
                case "VacationOperation": return true;
                case "VacationVentilationLevel": return true;
                case "VacationStartDate": return true;
                case "VacationEndDate": return true;
                case "VacationInterval": return true;
                case "VacationDuration": return true;
                case "PreheaterType": return true;
                case "KwlFunctionType": return true;
                case "HeaterAfterRunTime": return true;
                case "ExternalContact": return true;
                case "FaultTypeOutput": return true;
                case "FilterChange": return true;
                case "FilterChangeInterval": return true;
                case "FilterChangeRemaining": return true;
                case "BypassRoomTemperature": return true;
                case "BypassOutdoorTemperature": return true;
                case "BypassOutdoorTemperature2": return true;
                // StartReset is not readable.
                // FactoryReset is not readable.
                case "SupplyLevel": return true;
                case "ExhaustLevel": return true;
                case "FanLevelRegion02": return true;
                case "FanLevelRegion24": return true;
                case "FanLevelRegion46": return true;
                case "FanLevelRegion68": return true;
                case "FanLevelRegion80": return true;
                case "OffsetExhaust": return true;
                case "FanLevelConfiguration": return true;
                case "SensorName1": return true;
                case "SensorName2": return true;
                case "SensorName3": return true;
                case "SensorName4": return true;
                case "SensorName5": return true;
                case "SensorName6": return true;
                case "SensorName7": return true;
                case "SensorName8": return true;
                case "CO2SensorName1": return true;
                case "CO2SensorName2": return true;
                case "CO2SensorName3": return true;
                case "CO2SensorName4": return true;
                case "CO2SensorName5": return true;
                case "CO2SensorName6": return true;
                case "CO2SensorName7": return true;
                case "CO2SensorName8": return true;
                case "VOCSensorName1": return true;
                case "VOCSensorName2": return true;
                case "VOCSensorName3": return true;
                case "VOCSensorName4": return true;
                case "VOCSensorName5": return true;
                case "VOCSensorName6": return true;
                case "VOCSensorName7": return true;
                case "VOCSensorName8": return true;
                case "SoftwareVersion": return true;
                case "OperationMinutesSupply": return true;
                case "OperationMinutesExhaust": return true;
                case "OperationMinutesPreheater": return true;
                case "OperationMinutesAfterheater": return true;
                case "PowerPreheater": return true;
                case "PowerAfterheater": return true;
                //case "ResetFlag": return true;        // returns all zeros.
                case "ErrorCode": return true;
                case "WarningCode": return true;
                case "InfoCode": return true;
                case "NumberOfErrors": return true;
                case "NumberOfWarnings": return true;
                case "NumberOfInfos": return true;
                case "Errors": return true;
                case "Warnings": return true;
                case "Infos": return true;
                case "StatusFlags": return true;
                //case "GlobalUpdate": return true;     // returns all zeros.
                //case "LastError": return true;        // returns all zeros.
                // ClearError is not readable.
                case "SensorConfig1": return true;
                case "SensorConfig2": return true;
                case "SensorConfig3": return true;
                case "SensorConfig4": return true;
                case "SensorConfig5": return true;
                case "SensorConfig6": return true;
                case "SensorConfig7": return true;
                case "SensorConfig8": return true;

                default: return false;
            }
        }

        /// <summary>
        /// Returns true if the property is writable.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>True if writable.</returns>
        public static bool IsWritable(string property)
        {
            switch (property)
            {
                case "ItemDescription": return true;
                case "OrderNumber": return true;
                // MacAddress is not writable.
                case "Language": return true;
                case "Date": return true;
                case "Time": return true;
                case "DayLightSaving": return true;
                case "AutoUpdateEnabled": return true;
                case "PortalAccessEnabled": return true;
                case "ExhaustVentilatorVoltageLevel1": return true;
                case "SupplyVentilatorVoltageLevel1": return true;
                case "ExhaustVentilatorVoltageLevel2": return true;
                case "SupplyVentilatorVoltageLevel2": return true;
                case "ExhaustVentilatorVoltageLevel3": return true;
                case "SupplyVentilatorVoltageLevel3": return true;
                case "ExhaustVentilatorVoltageLevel4": return true;
                case "SupplyVentilatorVoltageLevel4": return true;
                case "MinimumVentilationLevel": return true;
                case "KwlBeEnabled": return true;
                case "KwlBecEnabled": return true;
                case "DeviceConfiguration": return true;
                case "PreheaterStatus": return true;
                case "KwlFTFConfig0": return true;
                case "KwlFTFConfig1": return true;
                case "KwlFTFConfig2": return true;
                case "KwlFTFConfig3": return true;
                case "KwlFTFConfig4": return true;
                case "KwlFTFConfig5": return true;
                case "KwlFTFConfig6": return true;
                case "KwlFTFConfig7": return true;
                case "HumidityControlStatus": return true;
                case "HumidityControlTarget": return true;
                case "HumidityControlStep": return true;
                case "HumidityControlStop": return true;
                case "CO2ControlStatus": return true;
                case "CO2ControlTarget": return true;
                case "CO2ControlStep": return true;
                case "VOCControlStatus": return true;
                case "VOCControlTarget": return true;
                case "VOCControlStep": return true;
                case "ThermalComfortTemperature": return true;
                case "TimeZoneOffset": return true;
                case "DateFormat": return true;
                case "HeatExchangerType": return true;
                case "PartyOperationDuration": return true;
                case "PartyVentilationLevel": return true;
                // PartyOperationRemaining is not writable.
                case "PartyOperationActivate": return true;
                case "StandbyOperationDuration": return true;
                case "StandbyVentilationLevel": return true;
                // StandbyOperationRemaining is not writable.
                case "StandbyOperationActivate": return true;
                case "OperationMode": return true;
                case "VentilationLevel": return true;
                // VentilationPercentage is not writable.
                // TemperatureOutdoor is not writable.
                // TemperatureSupply is not writable.
                // TemperatureExhaust is not writable.
                // TemperatureExtract is not writable.
                // TemperaturePreHeater is not writable.
                // TemperaturePostHeater is not writable.
                // ExternalHumiditySensor1 is not writable.
                // ExternalHumiditySensor2 is not writable.
                // ExternalHumiditySensor3 is not writable.
                // ExternalHumiditySensor4 is not writable.
                // ExternalHumiditySensor5 is not writable.
                // ExternalHumiditySensor6 is not writable.
                // ExternalHumiditySensor7 is not writable.
                // ExternalHumiditySensor8 is not writable.
                // ExternalHumidityTemperature1 is not writable.
                // ExternalHumidityTemperature2 is not writable.
                // ExternalHumidityTemperature3 is not writable.
                // ExternalHumidityTemperature4 is not writable.
                // ExternalHumidityTemperature5 is not writable.
                // ExternalHumidityTemperature6 is not writable.
                // ExternalHumidityTemperature7 is not writable.
                // ExternalHumidityTemperature8 is not writable.
                // ExternalCO2Sensor1 is not writable.
                // ExternalCO2Sensor2 is not writable.
                // ExternalCO2Sensor3 is not writable.
                // ExternalCO2Sensor4 is not writable.
                // ExternalCO2Sensor5 is not writable.
                // ExternalCO2Sensor6 is not writable.
                // ExternalCO2Sensor7 is not writable.
                // ExternalCO2Sensor8 is not writable.
                // ExternalVOCSensor1 is not writable.
                // ExternalVOCSensor2 is not writable.
                // ExternalVOCSensor3 is not writable.
                // ExternalVOCSensor4 is not writable.
                // ExternalVOCSensor5 is not writable.
                // ExternalVOCSensor6 is not writable.
                // ExternalVOCSensor7 is not writable.
                // ExternalVOCSensor8 is not writable.
                // TemperatureChannel is not writable.
                case "WeeklyProfile": return true;
                case "SerialNumber": return true;
                case "ProductionCode": return true;
                // SupplyFanSpeed is not writable.
                // ExhaustFanSpeed is not writable.
                case "Logout": return true;
                case "VacationOperation": return true;
                case "VacationVentilationLevel": return true;
                case "VacationStartDate": return true;
                case "VacationEndDate": return true;
                case "VacationInterval": return true;
                case "VacationDuration": return true;
                case "PreheaterType": return true;
                case "KwlFunctionType": return true;
                case "HeaterAfterRunTime": return true;
                case "ExternalContact": return true;
                case "FaultTypeOutput": return true;
                case "FilterChange": return true;
                case "FilterChangeInterval": return true;
                // FilterChangeRemaining is not writable.
                case "BypassRoomTemperature": return true;
                case "BypassOutdoorTemperature": return true;
                case "BypassOutdoorTemperature2": return true;
                case "StartReset": return true;
                case "FactoryReset": return true;
                case "SupplyLevel": return true;
                case "ExhaustLevel": return true;
                case "FanLevelRegion02": return true;
                case "FanLevelRegion24": return true;
                case "FanLevelRegion46": return true;
                case "FanLevelRegion68": return true;
                case "FanLevelRegion80": return true;
                case "OffsetExhaust": return true;
                case "FanLevelConfiguration": return true;
                case "SensorName1": return true;
                case "SensorName2": return true;
                case "SensorName3": return true;
                case "SensorName4": return true;
                case "SensorName5": return true;
                case "SensorName6": return true;
                case "SensorName7": return true;
                case "SensorName8": return true;
                case "CO2SensorName1": return true;
                case "CO2SensorName2": return true;
                case "CO2SensorName3": return true;
                case "CO2SensorName4": return true;
                case "CO2SensorName5": return true;
                case "CO2SensorName6": return true;
                case "CO2SensorName7": return true;
                case "CO2SensorName8": return true;
                case "VOCSensorName1": return true;
                case "VOCSensorName2": return true;
                case "VOCSensorName3": return true;
                case "VOCSensorName4": return true;
                case "VOCSensorName5": return true;
                case "VOCSensorName6": return true;
                case "VOCSensorName7": return true;
                case "VOCSensorName8": return true;
                // SoftwareVersion is not writable.
                // OperationMinutesSupply is not writable.
                // OperationMinutesExhaust is not writable.
                // OperationMinutesPreheater is not writable.
                // OperationMinutesAfterheater is not writable.
                // PowerPreheater is not writable.
                // PowerAfterheater is not writable.
                case "ResetFlag": return true;
                // ErrorCode is not writable.
                // WarningCode is not writable.
                // InfoCode is not writable.
                // NumberOfErrors is not writable.
                // NumberOfWarnings is not writable.
                // NumberOfInfos is not writable.
                // Errors is not writable.
                // Warnings is not writable.
                // Infos is not writable.
                // StatusFlags is not writable.
                //case "GlobalUpdate": return true;
                // LastError is not writable.
                case "ClearError": return true;
                // SensorConfig1 is not writable.
                // SensorConfig2 is not writable.
                // SensorConfig3 is not writable.
                // SensorConfig4 is not writable.
                // SensorConfig5 is not writable.
                // SensorConfig6 is not writable.
                // SensorConfig7 is not writable.
                // SensorConfig8 is not writable.

                default: return false;
            }
        }

        /// <summary>
        /// Updates all the Properties used in KWLEC200wData.
        /// </summary>
        /// <param name="data">The KWLEC200 data.</param>
        public void Refresh(KWLEC200Data data)
        {
            if (data != null)
            {
                ItemDescription = data.ItemDescription;
                OrderNumber = data.OrderNumber;
                MacAddress = data.MacAddress;
                Language = data.Language;
                Date = data.Date;
                Time = data.Time;
                DayLightSaving = data.DayLightSaving;
                AutoUpdateEnabled = data.AutoUpdateEnabled;
                PortalAccessEnabled = data.PortalAccessEnabled;
                ExhaustVentilatorVoltageLevel1 = data.ExhaustVentilatorVoltageLevel1;
                SupplyVentilatorVoltageLevel1 = data.SupplyVentilatorVoltageLevel1;
                ExhaustVentilatorVoltageLevel2 = data.ExhaustVentilatorVoltageLevel2;
                SupplyVentilatorVoltageLevel2 = data.SupplyVentilatorVoltageLevel2;
                ExhaustVentilatorVoltageLevel3 = data.ExhaustVentilatorVoltageLevel3;
                SupplyVentilatorVoltageLevel3 = data.SupplyVentilatorVoltageLevel3;
                ExhaustVentilatorVoltageLevel4 = data.ExhaustVentilatorVoltageLevel4;
                SupplyVentilatorVoltageLevel4 = data.SupplyVentilatorVoltageLevel4;
                MinimumVentilationLevel = data.MinimumVentilationLevel;
                KwlBeEnabled = data.KwlBeEnabled;
                KwlBecEnabled = data.KwlBecEnabled;
                DeviceConfiguration = data.DeviceConfiguration;
                PreheaterStatus = data.PreheaterStatus;
                KwlFTFConfig0 = data.KwlFTFConfig0;
                KwlFTFConfig1 = data.KwlFTFConfig1;
                KwlFTFConfig2 = data.KwlFTFConfig2;
                KwlFTFConfig3 = data.KwlFTFConfig3;
                KwlFTFConfig4 = data.KwlFTFConfig4;
                KwlFTFConfig5 = data.KwlFTFConfig5;
                KwlFTFConfig6 = data.KwlFTFConfig6;
                KwlFTFConfig7 = data.KwlFTFConfig7;
                HumidityControlStatus = data.HumidityControlStatus;
                HumidityControlTarget = data.HumidityControlTarget;
                HumidityControlStep = data.HumidityControlStep;
                HumidityControlStop = data.HumidityControlStop;
                CO2ControlStatus = data.CO2ControlStatus;
                CO2ControlTarget = data.CO2ControlTarget;
                CO2ControlStep = data.CO2ControlStep;
                VOCControlStatus = data.VOCControlStatus;
                VOCControlTarget = data.VOCControlTarget;
                VOCControlStep = data.VOCControlStep;
                ThermalComfortTemperature = data.ThermalComfortTemperature;
                TimeZoneOffset = data.TimeZoneOffset;
                DateFormat = data.DateFormat;
                HeatExchangerType = data.HeatExchangerType;
                PartyOperationDuration = data.PartyOperationDuration;
                PartyVentilationLevel = data.PartyVentilationLevel;
                PartyOperationRemaining = data.PartyOperationRemaining;
                PartyOperationActivate = data.PartyOperationActivate;
                StandbyOperationDuration = data.StandbyOperationDuration;
                StandbyVentilationLevel = data.StandbyVentilationLevel;
                StandbyOperationRemaining = data.StandbyOperationRemaining;
                StandbyOperationActivate = data.StandbyOperationActivate;
                OperationMode = data.OperationMode;
                VentilationLevel = data.VentilationLevel;
                VentilationPercentage = data.VentilationPercentage;
                TemperatureOutdoor = data.TemperatureOutdoor;
                TemperatureSupply = data.TemperatureSupply;
                TemperatureExhaust = data.TemperatureExhaust;
                TemperatureExtract = data.TemperatureExtract;
                TemperaturePreHeater = data.TemperaturePreHeater;
                TemperaturePostHeater = data.TemperaturePostHeater;
                ExternalHumiditySensor1 = data.ExternalHumiditySensor1;
                ExternalHumiditySensor2 = data.ExternalHumiditySensor2;
                ExternalHumiditySensor3 = data.ExternalHumiditySensor3;
                ExternalHumiditySensor4 = data.ExternalHumiditySensor4;
                ExternalHumiditySensor5 = data.ExternalHumiditySensor5;
                ExternalHumiditySensor6 = data.ExternalHumiditySensor6;
                ExternalHumiditySensor7 = data.ExternalHumiditySensor7;
                ExternalHumiditySensor8 = data.ExternalHumiditySensor8;
                ExternalHumidityTemperature1 = data.ExternalHumidityTemperature1;
                ExternalHumidityTemperature2 = data.ExternalHumidityTemperature2;
                ExternalHumidityTemperature3 = data.ExternalHumidityTemperature3;
                ExternalHumidityTemperature4 = data.ExternalHumidityTemperature4;
                ExternalHumidityTemperature5 = data.ExternalHumidityTemperature5;
                ExternalHumidityTemperature6 = data.ExternalHumidityTemperature6;
                ExternalHumidityTemperature7 = data.ExternalHumidityTemperature7;
                ExternalHumidityTemperature8 = data.ExternalHumidityTemperature8;
                ExternalCO2Sensor1 = data.ExternalCO2Sensor1;
                ExternalCO2Sensor2 = data.ExternalCO2Sensor2;
                ExternalCO2Sensor3 = data.ExternalCO2Sensor3;
                ExternalCO2Sensor4 = data.ExternalCO2Sensor4;
                ExternalCO2Sensor5 = data.ExternalCO2Sensor5;
                ExternalCO2Sensor6 = data.ExternalCO2Sensor6;
                ExternalCO2Sensor7 = data.ExternalCO2Sensor7;
                ExternalCO2Sensor8 = data.ExternalCO2Sensor8;
                ExternalVOCSensor1 = data.ExternalVOCSensor1;
                ExternalVOCSensor2 = data.ExternalVOCSensor2;
                ExternalVOCSensor3 = data.ExternalVOCSensor3;
                ExternalVOCSensor4 = data.ExternalVOCSensor4;
                ExternalVOCSensor5 = data.ExternalVOCSensor5;
                ExternalVOCSensor6 = data.ExternalVOCSensor6;
                ExternalVOCSensor7 = data.ExternalVOCSensor7;
                ExternalVOCSensor8 = data.ExternalVOCSensor8;
                TemperatureChannel = data.TemperatureChannel;
                WeeklyProfile = data.WeeklyProfile;
                SerialNumber = data.SerialNumber;
                ProductionCode = data.ProductionCode;
                SupplyFanSpeed = data.SupplyFanSpeed;
                ExhaustFanSpeed = data.ExhaustFanSpeed;
                Logout = data.Logout;
                VacationOperation = data.VacationOperation;
                VacationVentilationLevel = data.VacationVentilationLevel;
                VacationStartDate = data.VacationStartDate;
                VacationEndDate = data.VacationEndDate;
                VacationInterval = data.VacationInterval;
                VacationDuration = data.VacationDuration;
                PreheaterType = data.PreheaterType;
                KwlFunctionType = data.KwlFunctionType;
                HeaterAfterRunTime = data.HeaterAfterRunTime;
                ExternalContact = data.ExternalContact;
                FaultTypeOutput = data.FaultTypeOutput;
                FilterChange = data.FilterChange;
                FilterChangeInterval = data.FilterChangeInterval;
                FilterChangeRemaining = data.FilterChangeRemaining;
                BypassRoomTemperature = data.BypassRoomTemperature;
                BypassOutdoorTemperature = data.BypassOutdoorTemperature;
                BypassOutdoorTemperature2 = data.BypassOutdoorTemperature2;
                StartReset = data.StartReset;
                FactoryReset = data.FactoryReset;
                SupplyLevel = data.SupplyLevel;
                ExhaustLevel = data.ExhaustLevel;
                FanLevelRegion02 = data.FanLevelRegion02;
                FanLevelRegion24 = data.FanLevelRegion24;
                FanLevelRegion46 = data.FanLevelRegion46;
                FanLevelRegion68 = data.FanLevelRegion68;
                FanLevelRegion80 = data.FanLevelRegion80;
                OffsetExhaust = data.OffsetExhaust;
                FanLevelConfiguration = data.FanLevelConfiguration;
                SensorName1 = data.SensorName1;
                SensorName2 = data.SensorName2;
                SensorName3 = data.SensorName3;
                SensorName4 = data.SensorName4;
                SensorName5 = data.SensorName5;
                SensorName6 = data.SensorName6;
                SensorName7 = data.SensorName7;
                SensorName8 = data.SensorName8;
                CO2SensorName1 = data.CO2SensorName1;
                CO2SensorName2 = data.CO2SensorName2;
                CO2SensorName3 = data.CO2SensorName3;
                CO2SensorName4 = data.CO2SensorName4;
                CO2SensorName5 = data.CO2SensorName5;
                CO2SensorName6 = data.CO2SensorName6;
                CO2SensorName7 = data.CO2SensorName7;
                CO2SensorName8 = data.CO2SensorName8;
                VOCSensorName1 = data.VOCSensorName1;
                VOCSensorName2 = data.VOCSensorName2;
                VOCSensorName3 = data.VOCSensorName3;
                VOCSensorName4 = data.VOCSensorName4;
                VOCSensorName5 = data.VOCSensorName5;
                VOCSensorName6 = data.VOCSensorName6;
                VOCSensorName7 = data.VOCSensorName7;
                VOCSensorName8 = data.VOCSensorName8;
                SoftwareVersion = data.SoftwareVersion;
                OperationMinutesSupply = data.OperationMinutesSupply;
                OperationMinutesExhaust = data.OperationMinutesExhaust;
                OperationMinutesPreheater = data.OperationMinutesPreheater;
                OperationMinutesAfterheater = data.OperationMinutesAfterheater;
                PowerPreheater = data.PowerPreheater;
                PowerAfterheater = data.PowerAfterheater;
                ResetFlag = data.ResetFlag;
                ErrorCode = data.ErrorCode;
                WarningCode = data.WarningCode;
                InfoCode = data.InfoCode;
                NumberOfErrors = data.NumberOfErrors;
                NumberOfWarnings = data.NumberOfWarnings;
                NumberOfInfos = data.NumberOfInfos;
                Errors = data.Errors;
                Warnings = data.Warnings;
                Infos = data.Infos;
                StatusFlags = data.StatusFlags;
                //GlobalUpdate = data.GlobalUpdate;
                //LastError = data.LastError;
                ClearError = data.ClearError;
                SensorConfig1 = data.SensorConfig1;
                SensorConfig2 = data.SensorConfig2;
                SensorConfig3 = data.SensorConfig3;
                SensorConfig4 = data.SensorConfig4;
                SensorConfig5 = data.SensorConfig5;
                SensorConfig6 = data.SensorConfig6;
                SensorConfig7 = data.SensorConfig7;
                SensorConfig8 = data.SensorConfig8;
            }

            Status = data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the KWLEC200Data class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static string[] GetProperties()
            => typeof(KWLEC200Data).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToArray();

        /// <summary>
        /// Returns true if property with the specified name is found in the KWLEC200Data class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property) => PropertyValue.GetPropertyInfo(typeof(KWLEC200Data), property) != null;

        /// <summary>
        /// Returns the <see cref="PropertyInfo"/> data for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The property info.</returns>
        public static PropertyInfo GetPropertyInfo(string property) => PropertyValue.GetPropertyInfo(typeof(KWLEC200Data), property);

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
