// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BControlData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace BControlLib.Models
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

    public class BControlData : DataValue, IPropertyHelper
    {
        #region Constants

        const ModbusAttribute.AccessModes RO = ModbusAttribute.AccessModes.ReadOnly;
        const ModbusAttribute.AccessModes RW = ModbusAttribute.AccessModes.ReadWrite;
        const ModbusAttribute.AccessModes WO = ModbusAttribute.AccessModes.WriteOnly;

        #endregion

        #region Public Properties

        /// <summary>
        /// TQ Energy Manager Modbus Slave properties (data fields).
        /// The Modbus attributes are used to define offset, acces mode and numer of registers used.
        /// </summary>

        // Internal immediate registers
        [Modbus(0, 2, RO)] public uint32 ActivePowerPlus { get; set; }
        [Modbus(2, 2, RO)] public uint32 ActivePowerMinus { get; set; }
        [Modbus(4, 2, RO)] public uint32 ReactivePowerPlus { get; set; }
        [Modbus(6, 2, RO)] public uint32 ReactivePowerMinus { get; set; }
        [Modbus(16, 2, RO)] public uint32 ApparentPowerPlus { get; set; }
        [Modbus(18, 2, RO)] public uint32 ApparentPowerMinus { get; set; }
        [Modbus(24, 2, RO)] public int32 PowerFactor { get; set; }
        [Modbus(26, 2, RO)] public uint32 LineFrequency { get; set; }
        [Modbus(40, 2, RO)] public uint32 ActivePowerPlusL1 { get; set; }
        [Modbus(42, 2, RO)] public uint32 ActivePowerMinusL1 { get; set; }
        [Modbus(44, 2, RO)] public uint32 ReactivePowerPlusL1 { get; set; }
        [Modbus(46, 2, RO)] public uint32 ReactivePowerMinusL1 { get; set; }
        [Modbus(56, 2, RO)] public uint32 ApparentPowerPlusL1 { get; set; }
        [Modbus(58, 2, RO)] public uint32 ApparentPowerMinusL1 { get; set; }
        [Modbus(60, 2, RO)] public uint32 CurrentL1 { get; set; }
        [Modbus(62, 2, RO)] public uint32 VoltageL1 { get; set; }
        [Modbus(64, 2, RO)] public int32 PowerFactorL1 { get; set; }
        [Modbus(80, 2, RO)] public uint32 ActivePowerPlusL2 { get; set; }
        [Modbus(82, 2, RO)] public uint32 ActivePowerMinusL2 { get; set; }
        [Modbus(84, 2, RO)] public uint32 ReactivePowerPlusL2 { get; set; }
        [Modbus(86, 2, RO)] public uint32 ReactivePowerMinusL2 { get; set; }
        [Modbus(96, 2, RO)] public uint32 ApparentPowerPlusL2 { get; set; }
        [Modbus(98, 2, RO)] public uint32 ApparentPowerMinusL2 { get; set; }
        [Modbus(100, 2, RO)] public uint32 CurrentL2 { get; set; }
        [Modbus(102, 2, RO)] public uint32 VoltageL2 { get; set; }
        [Modbus(104, 2, RO)] public int32 PowerFactorL2 { get; set; }
        [Modbus(120, 2, RO)] public uint32 ActivePowerPlusL3 { get; set; }
        [Modbus(122, 2, RO)] public uint32 ActivePowerMinusL3 { get; set; }
        [Modbus(124, 2, RO)] public uint32 ReactivePowerPlusL3 { get; set; }
        [Modbus(126, 2, RO)] public uint32 ReactivePowerMinusL3 { get; set; }
        [Modbus(136, 2, RO)] public uint32 ApparentPowerPlusL3 { get; set; }
        [Modbus(138, 2, RO)] public uint32 ApparentPowerMinusL3 { get; set; }
        [Modbus(140, 2, RO)] public uint32 CurrentL3 { get; set; }
        [Modbus(142, 2, RO)] public uint32 VoltageL3 { get; set; }
        [Modbus(144, 2, RO)] public int32 PowerFactorL3 { get; set; }

        //Internal energy registers(meters)
        [Modbus(512, 4, RO)] public uint64 ActiveEnergyPlus { get; set; }
        [Modbus(516, 4, RO)] public uint64 ActiveEnergyMinus { get; set; }
        [Modbus(520, 4, RO)] public uint64 ReactiveEnergyPlus { get; set; }
        [Modbus(524, 4, RO)] public uint64 ReactiveEnergyMinus { get; set; }
        [Modbus(544, 4, RO)] public uint64 ApparentEnergyPlus { get; set; }
        [Modbus(548, 4, RO)] public uint64 ApparentEnergyMinus { get; set; }
        [Modbus(592, 4, RO)] public uint64 ActiveEnergyPlusL1 { get; set; }
        [Modbus(596, 4, RO)] public uint64 ActiveEnergyMinusL1 { get; set; }
        [Modbus(600, 4, RO)] public uint64 ReactiveEnergyPlusL1 { get; set; }
        [Modbus(604, 4, RO)] public uint64 ReactiveEnergyMinusL1 { get; set; }
        [Modbus(624, 4, RO)] public uint64 ApparentEnergyPlusL1 { get; set; }
        [Modbus(628, 4, RO)] public uint64 ApparentEnergyMinusL1 { get; set; }
        [Modbus(672, 4, RO)] public uint64 ActiveEnergyPlusL2 { get; set; }
        [Modbus(676, 4, RO)] public uint64 ActiveEnergyMinusL2 { get; set; }
        [Modbus(680, 4, RO)] public uint64 ReactiveEnergyPlusL2 { get; set; }
        [Modbus(684, 4, RO)] public uint64 ReactiveEnergyMinusL2 { get; set; }
        [Modbus(704, 4, RO)] public uint64 ApparentEnergyPlusL2 { get; set; }
        [Modbus(708, 4, RO)] public uint64 ApparentEnergyMinusL2 { get; set; }
        [Modbus(752, 4, RO)] public uint64 ActiveEnergyPlusL3 { get; set; }
        [Modbus(756, 4, RO)] public uint64 ActiveEnergyMinusL3 { get; set; }
        [Modbus(760, 4, RO)] public uint64 ReactiveEnergyPlusL3 { get; set; }
        [Modbus(764, 4, RO)] public uint64 ReactiveEnergyMinusL3 { get; set; }
        [Modbus(784, 4, RO)] public uint64 ApparentEnergyPlusL3 { get; set; }
        [Modbus(788, 4, RO)] public uint64 ApparentEnergyMinusL3 { get; set; }

        // PnP registers
        [Modbus(8192, 1, RO)] public uint16 ManufacturerID { get; set; }
        [Modbus(8193, 1, RO)] public uint16 ProductID { get; set; }
        [Modbus(8194, 1, RO)] public uint16 ProductVersion { get; set; }
        [Modbus(8195, 1, RO)] public uint16 FirmwareVersion { get; set; }
        [Modbus(8196, 16, RO)] public string VendorName { get; set; } = string.Empty;
        [Modbus(8212, 16, RO)] public string ProductName { get; set; } = string.Empty;
        [Modbus(8228, 16, RO)] public string SerialNumber { get; set; } = string.Empty;

        // SunSpec registers
        [Modbus(40000, 2, RO)] public uint32 C_SunSpec_ID { get; set; } = 0x53756e53;
        [Modbus(40002, 1, RO)] public uint16 C_SunSpec_DID1 { get; set; } = 0x0001;
        [Modbus(40003, 1, RO)] public uint16 C_SunSpec_Length1 { get; set; } = 65;
        [Modbus(40004, 16, RO)] public string C_Manufacturer { get; set; } = string.Empty;
        [Modbus(40020, 16, RO)] public string C_Model { get; set; } = string.Empty;
        [Modbus(40036, 8, RO)] public string C_Options { get; set; } = string.Empty;
        [Modbus(40044, 8, RO)] public string C_Version { get; set; } = string.Empty;
        [Modbus(40052, 16, RO)] public string C_SerialNumber { get; set; } = string.Empty;
        [Modbus(40068, 1, RO)] public uint16 C_DeviceAddress { get; set; }
        [Modbus(40069, 1, RO)] public uint16 C_SunSpec_DID2 { get; set; } = 203;
        [Modbus(40070, 1, RO)] public uint16 C_SunSpec_Length2 { get; set; } = 105;
        [Modbus(40071, 1, RO)] public int16 M_AC_Current { get; set; } = 0x8000;
        [Modbus(40072, 1, RO)] public int16 M_AC_Current_A { get; set; }
        [Modbus(40073, 1, RO)] public int16 M_AC_Current_B { get; set; }
        [Modbus(40074, 1, RO)] public int16 M_AC_Current_C { get; set; }
        [Modbus(40075, 1, RO)] public sunssf M_AC_Current_SF { get; set; }
        [Modbus(40076, 1, RO)] public int16 M_AC_Voltage_LN { get; set; } = 0x8000;
        [Modbus(40077, 1, RO)] public int16 M_AC_Voltage_AN { get; set; }
        [Modbus(40078, 1, RO)] public int16 M_AC_Voltage_BN { get; set; }
        [Modbus(40079, 1, RO)] public int16 M_AC_Voltage_CN { get; set; }
        [Modbus(40080, 1, RO)] public int16 M_AC_Voltage_LL { get; set; } = 0x8000;
        [Modbus(40081, 1, RO)] public int16 M_AC_Voltage_AB { get; set; } = 0x8000;
        [Modbus(40082, 1, RO)] public int16 M_AC_Voltage_BC { get; set; } = 0x8000;
        [Modbus(40083, 1, RO)] public int16 M_AC_Voltage_CA { get; set; } = 0x8000;
        [Modbus(40084, 1, RO)] public sunssf M_AC_Voltage_SF { get; set; }
        [Modbus(40085, 1, RO)] public int16 M_AC_Freq { get; set; }
        [Modbus(40086, 1, RO)] public sunssf M_AC_Freq_SF { get; set; }
        [Modbus(40087, 1, RO)] public int16 M_AC_Power { get; set; }
        [Modbus(40088, 1, RO)] public int16 M_AC_Power_A { get; set; }
        [Modbus(40089, 1, RO)] public int16 M_AC_Power_B { get; set; }
        [Modbus(40090, 1, RO)] public int16 M_AC_Power_C { get; set; }
        [Modbus(40091, 1, RO)] public sunssf M_AC_Power_SF { get; set; }
        [Modbus(40092, 1, RO)] public int16 M_AC_VA { get; set; }
        [Modbus(40093, 1, RO)] public int16 M_AC_VA_A { get; set; }
        [Modbus(40094, 1, RO)] public int16 M_AC_VA_B { get; set; }
        [Modbus(40095, 1, RO)] public int16 M_AC_VA_C { get; set; }
        [Modbus(40096, 1, RO)] public sunssf M_AC_VA_SF { get; set; }
        [Modbus(40097, 1, RO)] public int16 M_AC_VAR { get; set; }
        [Modbus(40098, 1, RO)] public int16 M_AC_VAR_A { get; set; }
        [Modbus(40099, 1, RO)] public int16 M_AC_VAR_B { get; set; }
        [Modbus(40100, 1, RO)] public int16 M_AC_VAR_C { get; set; }
        [Modbus(40101, 1, RO)] public sunssf M_AC_VAR_SF { get; set; }
        [Modbus(40102, 1, RO)] public int16 M_AC_PF { get; set; }
        [Modbus(40103, 1, RO)] public int16 M_AC_PF_A { get; set; }
        [Modbus(40104, 1, RO)] public int16 M_AC_PF_B { get; set; }
        [Modbus(40105, 1, RO)] public int16 M_AC_PF_C { get; set; }
        [Modbus(40106, 1, RO)] public sunssf M_AC_PF_SF { get; set; }
        [Modbus(40107, 2, RO)] public uint32 M_Exported { get; set; }
        [Modbus(40109, 2, RO)] public uint32 M_Exported_A { get; set; }
        [Modbus(40111, 2, RO)] public uint32 M_Exported_B { get; set; }
        [Modbus(40113, 2, RO)] public uint32 M_Exported_C { get; set; }
        [Modbus(40115, 2, RO)] public uint32 M_Imported { get; set; }
        [Modbus(40117, 2, RO)] public uint32 M_Imported_A { get; set; }
        [Modbus(40119, 2, RO)] public uint32 M_Imported_B { get; set; }
        [Modbus(40121, 2, RO)] public uint32 M_Imported_C { get; set; }
        [Modbus(40123, 1, RO)] public sunssf M_Energy_W_SF { get; set; }
        [Modbus(40124, 2, RO)] public uint32 M_Exported_VA { get; set; }
        [Modbus(40126, 2, RO)] public uint32 M_Exported_VA_A { get; set; }
        [Modbus(40128, 2, RO)] public uint32 M_Exported_VA_B { get; set; }
        [Modbus(40130, 2, RO)] public uint32 M_Exported_VA_C { get; set; }
        [Modbus(40132, 2, RO)] public uint32 M_Imported_VA { get; set; }
        [Modbus(40134, 2, RO)] public uint32 M_Imported_VA_A { get; set; }
        [Modbus(40136, 2, RO)] public uint32 M_Imported_VA_B { get; set; }
        [Modbus(40138, 2, RO)] public uint32 M_Imported_VA_C { get; set; }
        [Modbus(40140, 1, RO)] public sunssf M_Energy_VA_SF { get; set; }
        [Modbus(40141, 2, RO)] public uint32 M_Import_VARh_Q1 { get; set; } = 0x80000000;
        [Modbus(40143, 2, RO)] public uint32 M_Import_VARh_Q1A { get; set; } = 0x80000000;
        [Modbus(40145, 2, RO)] public uint32 M_Import_VARh_Q1B { get; set; } = 0x80000000;
        [Modbus(40147, 2, RO)] public uint32 M_Import_VARh_Q1C { get; set; } = 0x80000000;
        [Modbus(40149, 2, RO)] public uint32 M_Import_VARh_Q2 { get; set; } = 0x80000000;
        [Modbus(40151, 2, RO)] public uint32 M_Import_VARh_Q2A { get; set; } = 0x80000000;
        [Modbus(40153, 2, RO)] public uint32 M_Import_VARh_Q2B { get; set; } = 0x80000000;
        [Modbus(40155, 2, RO)] public uint32 M_Import_VARh_Q2C { get; set; } = 0x80000000;
        [Modbus(40157, 2, RO)] public uint32 M_Import_VARh_Q3 { get; set; } = 0x80000000;
        [Modbus(40159, 2, RO)] public uint32 M_Import_VARh_Q3A { get; set; } = 0x80000000;
        [Modbus(40161, 2, RO)] public uint32 M_Import_VARh_Q3B { get; set; } = 0x80000000;
        [Modbus(40163, 2, RO)] public uint32 M_Import_VARh_Q3C { get; set; } = 0x80000000;
        [Modbus(40165, 2, RO)] public uint32 M_Import_VARh_Q4 { get; set; } = 0x80000000;
        [Modbus(40167, 2, RO)] public uint32 M_Import_VARh_Q4A { get; set; } = 0x80000000;
        [Modbus(40169, 2, RO)] public uint32 M_Import_VARh_Q4B { get; set; } = 0x80000000;
        [Modbus(40171, 2, RO)] public uint32 M_Import_VARh_Q4C { get; set; } = 0x80000000;
        [Modbus(40173, 1, RO)] public sunssf M_Energy_VAR_SF { get; set; } = (short)0;
        [Modbus(40174, 2, RO)] public uint32 M_Events { get; set; } = 0;
        [Modbus(40176, 1, RO)] public uint16 C_SunSpec_DID3 { get; set; } = 0xffff;
        [Modbus(40177, 1, RO)] public uint16 C_SunSpec_Length3 { get; set; } = 0;

        #endregion

        #region Block Properties

        /// <summary>
        /// TQ Energy Manager Modbus Slave block area (internal, immediate register).
        /// The Modbus attributes are used to define offset, acces mode and numer of registers used.
        /// </summary>
        [JsonIgnore]
        [Modbus(0, 50, RO)]
        public uint[] InternalDataBlock1
        {
            get {
                var array = new uint[50];
                array[0] = ActivePowerPlus;
                array[1] = ActivePowerMinus;
                array[2] = ReactivePowerPlus;
                array[3] = ReactivePowerMinus;
                array[8] = ApparentPowerPlus;
                array[9] = ApparentPowerMinus;
                array[12] = (uint)(int)PowerFactor;
                array[13] = LineFrequency;
                array[20] = ActivePowerPlusL1;
                array[21] = ActivePowerMinusL1;
                array[22] = ReactivePowerPlusL1;
                array[23] = ReactivePowerMinusL1;
                array[28] = ApparentPowerPlusL1;
                array[29] = ApparentPowerMinusL1;
                array[30] = CurrentL1;
                array[31] = VoltageL1;
                array[32] = (uint)(int)PowerFactorL1;
                array[40] = ActivePowerPlusL2;
                array[41] = ActivePowerMinusL2;
                array[42] = ReactivePowerPlusL2;
                array[43] = ReactivePowerMinusL2;
                array[48] = ApparentPowerPlusL2;
                array[49] = ApparentPowerMinusL2;
                return array;
            }
            set
            {
                if (value.Length == 50)
                {
                    ActivePowerPlus = value[0];
                    ActivePowerMinus = value[1];
                    ReactivePowerPlus = value[2];
                    ReactivePowerMinus = value[3];
                    ApparentPowerPlus = value[8];
                    ApparentPowerMinus = value[9];
                    PowerFactor = (int)value[12];
                    LineFrequency = value[13];
                    ActivePowerPlusL1 = value[20];
                    ActivePowerMinusL1 = value[21];
                    ReactivePowerPlusL1 = value[22];
                    ReactivePowerMinusL1 = value[23];
                    ApparentPowerPlusL1 = value[28];
                    ApparentPowerMinusL1 = value[29];
                    CurrentL1 = value[30];
                    VoltageL1 = value[31];
                    PowerFactorL1 = (int)value[32];
                    ActivePowerPlusL2 = value[40];
                    ActivePowerMinusL2 = value[41];
                    ReactivePowerPlusL2 = value[42];
                    ReactivePowerMinusL2 = value[43];
                    ApparentPowerPlusL2 = value[48];
                    ApparentPowerMinusL2 = value[49];
                }
            }
        }

        [JsonIgnore]
        [Modbus(100, 23, RO)]
        public uint[] InternalDataBlock2
        {
            get
            {
                var array = new uint[23];
                array[0] = CurrentL2;
                array[1] = VoltageL2;
                array[2] = (uint)(int)PowerFactorL2;
                array[10] = ActivePowerPlusL3;
                array[11] = ActivePowerMinusL3;
                array[12] = ReactivePowerPlusL3;
                array[13] = ReactivePowerMinusL3;
                array[18] = ApparentPowerPlusL3;
                array[19] = ApparentPowerMinusL3;
                array[20] = CurrentL3;
                array[21] = VoltageL3;
                array[22] = (uint)(int)PowerFactorL3;
                return array;
            }
            set
            {
                if (value.Length == 23)
                {
                    CurrentL2 = value[0];
                    VoltageL2 = value[1];
                    PowerFactorL2 = (int)value[2];
                    ActivePowerPlusL3 = value[10];
                    ActivePowerMinusL3 = value[11];
                    ReactivePowerPlusL3 = value[12];
                    ReactivePowerMinusL3 = value[13];
                    ApparentPowerPlusL3 = value[18];
                    ApparentPowerMinusL3 = value[19];
                    CurrentL3 = value[20];
                    VoltageL3 = value[21];
                    PowerFactorL3 = (int)value[22];
                }
            }
        }

        /// <summary>
        /// TQ Energy Manager Modbus Slave block area (internal energy registers).
        /// The Modbus attributes are used to define offset, acces mode and numer of registers used.
        /// </summary>
        [JsonIgnore]
        [Modbus(512, 25, RO)]
        public ulong[] EnergyDataBlock1
        {
            get
            {
                var array = new ulong[25];
                array[0] = ActiveEnergyPlus;
                array[1] = ActiveEnergyMinus;
                array[2] = ReactiveEnergyPlus;
                array[3] = ReactiveEnergyMinus;
                array[8] = ApparentEnergyPlus;
                array[9] = ApparentEnergyMinus;
                array[20] = ActiveEnergyPlusL1;
                array[21] = ActiveEnergyMinusL1;
                array[22] = ReactiveEnergyPlusL1;
                array[23] = ReactiveEnergyMinusL1;
                return array;
            }
            set
            {
                if (value.Length == 25)
                {
                    ActiveEnergyPlus = value[0];
                    ActiveEnergyMinus = value[1];
                    ReactiveEnergyPlus = value[2];
                    ReactiveEnergyMinus = value[3];
                    ApparentEnergyPlus = value[8];
                    ApparentEnergyMinus = value[9];
                    ActiveEnergyPlusL1 = value[20];
                    ActiveEnergyMinusL1 = value[21];
                    ReactiveEnergyPlusL1 = value[22];
                    ReactiveEnergyMinusL1 = value[23];
                }
            }
        }

        [JsonIgnore]
        [Modbus(612, 25, RO)]
        public ulong[] EnergyDataBlock2
        {
            get
            {
                var array = new ulong[25];
                array[3] = ApparentEnergyPlusL1;
                array[4] = ApparentEnergyMinusL1;
                array[15] = ActiveEnergyPlusL2;
                array[16] = ActiveEnergyMinusL2;
                array[17] = ReactiveEnergyPlusL2;
                array[18] = ReactiveEnergyMinusL2;
                array[23] = ApparentEnergyPlusL2;
                array[24] = ApparentEnergyMinusL2;
                return array;
            }
            set
            {
                if (value.Length == 25)
                {
                    ApparentEnergyPlusL1 = value[3];
                    ApparentEnergyMinusL1 = value[4];
                    ActiveEnergyPlusL2 = value[15];
                    ActiveEnergyMinusL2 = value[16];
                    ReactiveEnergyPlusL2 = value[17];
                    ReactiveEnergyMinusL2 = value[18];
                    ApparentEnergyPlusL2 = value[23];
                    ApparentEnergyMinusL2 = value[24];
                }
            }
        }

        [JsonIgnore]
        [Modbus(712, 20, RO)]
        public ulong[] EnergyDataBlock3
        {
            get
            {
                var array = new ulong[20];
                array[10] = ActiveEnergyPlusL3;
                array[11] = ActiveEnergyMinusL3;
                array[12] = ReactiveEnergyPlusL3;
                array[13] = ReactiveEnergyMinusL3;
                array[18] = ApparentEnergyPlusL3;
                array[19] = ApparentEnergyMinusL3;
                return array;
            }
            set
            {
                if (value.Length == 20)
                {
                    ActiveEnergyPlusL3 = value[10];
                    ActiveEnergyMinusL3 = value[11];
                    ReactiveEnergyPlusL3 = value[12];
                    ReactiveEnergyMinusL3 = value[13];
                    ApparentEnergyPlusL3 = value[18];
                    ApparentEnergyMinusL3 = value[19];
                }
            }
        }

        /// <summary>
        /// TQ Energy Manager Modbus Slave block area (PnP registers).
        /// The Modbus attributes are used to define offset, acces mode and numer of registers used.
        /// </summary>
        [JsonIgnore]
        [Modbus(8192, 52, RO)]
        public ushort[] PnPDataBlock1
        {
            get
            {
                var array = new ushort[52];
                array[0] = ManufacturerID;
                array[1] = ProductID;
                array[2] = ProductVersion;
                array[3] = FirmwareVersion;
                Array.Copy(VendorName.PadRight(32, ' ').ToRegisters(), 0, array, 4, 16);
                Array.Copy(ProductName.PadRight(32, ' ').ToRegisters(), 0, array, 20, 16);
                Array.Copy(SerialNumber.PadRight(32, ' ').ToRegisters(), 0, array, 36, 16);
                return array;
            }
            set
            {
                if (value.Length == 52)
                {
                    ManufacturerID = value[0];
                    ProductID = value[1];
                    ProductVersion = value[2];
                    FirmwareVersion = value[3];
                    VendorName = value.Skip(4).Take(16).ToArray().ToASCII();
                    ProductName = value.Skip(20).Take(16).ToArray().ToASCII();
                    SerialNumber = value.Skip(36).Take(16).ToArray().ToASCII();
                }
            }
        }

        /// <summary>
        /// TQ Energy Manager Modbus Slave block area (SunSpec registers).
        /// The Modbus attributes are used to define offset, acces mode and numer of registers used.
        /// </summary>
        [JsonIgnore]
        [Modbus(40000, 100, RO)]
        public ushort[] SunSpecDataBlock1
        {
            get {
                var array = new ushort[100];
                Array.Copy(C_SunSpec_ID.ToRegisters(), 0, array, 0, 2);
                array[2] = C_SunSpec_DID1;
                array[3] = C_SunSpec_Length1;
                Array.Copy(C_Manufacturer.PadRight(32, ' ').ToRegisters(), 0, array, 4, 16);
                Array.Copy(C_Model.PadRight(32, ' ').ToRegisters(), 0, array, 20, 16);
                Array.Copy(C_Options.PadRight(16, ' ').ToRegisters(), 0, array, 36, 8);
                Array.Copy(C_Version.PadRight(16, ' ').ToRegisters(), 0, array, 44, 8);
                Array.Copy(C_SerialNumber.PadRight(32, ' ').ToRegisters(), 0, array, 52, 16);
                array[68] = C_DeviceAddress;
                array[69] = C_SunSpec_DID2;
                array[70] = C_SunSpec_Length2;
                array[71] = (ushort)(short)M_AC_Current;
                array[72] = (ushort)(short)M_AC_Current_A;
                array[73] = (ushort)(short)M_AC_Current_B;
                array[74] = (ushort)(short)M_AC_Current_C;
                array[75] = (ushort)(short)M_AC_Current_SF;
                array[76] = (ushort)(short)M_AC_Voltage_LN;
                array[77] = (ushort)(short)M_AC_Voltage_AN;
                array[78] = (ushort)(short)M_AC_Voltage_BN;
                array[79] = (ushort)(short)M_AC_Voltage_CN;
                array[80] = (ushort)(short)M_AC_Voltage_LL;
                array[81] = (ushort)(short)M_AC_Voltage_AB;
                array[82] = (ushort)(short)M_AC_Voltage_BC;
                array[83] = (ushort)(short)M_AC_Voltage_CA;
                array[84] = (ushort)(short)M_AC_Voltage_SF;
                array[85] = (ushort)(short)M_AC_Freq;
                array[86] = (ushort)(short)M_AC_Freq_SF;
                array[87] = (ushort)(short)M_AC_Power;
                array[88] = (ushort)(short)M_AC_Power_A;
                array[89] = (ushort)(short)M_AC_Power_B;
                array[90] = (ushort)(short)M_AC_Power_C;
                array[91] = (ushort)(short)M_AC_Power_SF;
                array[92] = (ushort)(short)M_AC_VA;
                array[93] = (ushort)(short)M_AC_VA_A;
                array[94] = (ushort)(short)M_AC_VA_B;
                array[95] = (ushort)(short)M_AC_VA_C;
                array[96] = (ushort)(short)M_AC_VA_SF;
                array[97] = (ushort)(short)M_AC_VAR;
                array[98] = (ushort)(short)M_AC_VAR_A;
                array[99] = (ushort)(short)M_AC_VAR_B;
                return array;
            }
            set
            {
                if (value.Length == 100)
                {
                    C_SunSpec_ID = value.Take(2).ToArray();
                    C_SunSpec_DID1 = value[2];
                    C_SunSpec_Length1 = value[3];
                    C_Manufacturer = value.Skip(4).Take(16).ToArray().ToASCII();
                    C_Model = value.Skip(20).Take(16).ToArray().ToASCII();
                    C_Options = value.Skip(36).Take(8).ToArray().ToASCII();
                    C_Version = value.Skip(44).Take(8).ToArray().ToASCII();
                    C_SerialNumber = value.Skip(52).Take(16).ToArray().ToASCII();
                    C_DeviceAddress = value[68];
                    C_SunSpec_DID2 = value[69];
                    C_SunSpec_Length2 = value[70];
                    M_AC_Current = value[71];
                    M_AC_Current_A = value[72];
                    M_AC_Current_B = value[73];
                    M_AC_Current_C = value[74];
                    M_AC_Current_SF = value[75];
                    M_AC_Voltage_LN = value[76];
                    M_AC_Voltage_AN = value[77];
                    M_AC_Voltage_BN = value[78];
                    M_AC_Voltage_CN = value[79];
                    M_AC_Voltage_LL = value[80];
                    M_AC_Voltage_AB = value[81];
                    M_AC_Voltage_BC = value[82];
                    M_AC_Voltage_CA = value[83];
                    M_AC_Voltage_SF = value[84];
                    M_AC_Freq = value[85];
                    M_AC_Freq_SF = value[86];
                    M_AC_Power = value[87];
                    M_AC_Power_A = value[88];
                    M_AC_Power_B = value[89];
                    M_AC_Power_C = value[90];
                    M_AC_Power_SF = value[91];
                    M_AC_VA = value[92];
                    M_AC_VA_A = value[93];
                    M_AC_VA_B = value[94];
                    M_AC_VA_C = value[95];
                    M_AC_VA_SF = value[96];
                    M_AC_VAR = value[97];
                    M_AC_VAR_A = value[98];
                    M_AC_VAR_B = value[99];
                }
            }
        }

        [JsonIgnore]
        [Modbus(40100, 78, RO)]
        public ushort[] SunSpecDataBlock2
        {
            get
            {
                var array = new ushort[78];
                array[0] = (ushort)(short)M_AC_VAR_C;
                array[1] = (ushort)(short)M_AC_VAR_SF;
                array[2] = (ushort)(short)M_AC_PF;
                array[3] = (ushort)(short)M_AC_PF_A;
                array[4] = (ushort)(short)M_AC_PF_B;
                array[5] = (ushort)(short)M_AC_PF_C;
                array[6] = (ushort)(short)M_AC_PF_SF;
                Array.Copy(M_Exported.ToRegisters(), 0, array, 7, 2);
                Array.Copy(M_Exported_A.ToRegisters(), 0, array, 9, 2);
                Array.Copy(M_Exported_B.ToRegisters(), 0, array, 11, 2);
                Array.Copy(M_Exported_C.ToRegisters(), 0, array, 13, 2);
                Array.Copy(M_Imported.ToRegisters(), 0, array, 15, 2);
                Array.Copy(M_Imported_A.ToRegisters(), 0, array, 17, 2);
                Array.Copy(M_Imported_B.ToRegisters(), 0, array, 19, 2);
                Array.Copy(M_Imported_C.ToRegisters(), 0, array, 21, 2);
                array[23] = (ushort)(short)M_Energy_W_SF;
                Array.Copy(M_Exported_VA.ToRegisters(), 0, array, 24, 2);
                Array.Copy(M_Exported_VA_A.ToRegisters(), 0, array, 26, 2);
                Array.Copy(M_Exported_VA_B.ToRegisters(), 0, array, 28, 2);
                Array.Copy(M_Exported_VA_C.ToRegisters(), 0, array, 30, 2);
                Array.Copy(M_Imported_VA.ToRegisters(), 0, array, 32, 2);
                Array.Copy(M_Imported_VA_A.ToRegisters(), 0, array, 34, 2);
                Array.Copy(M_Imported_VA_B.ToRegisters(), 0, array, 36, 2);
                Array.Copy(M_Imported_VA_C.ToRegisters(), 0, array, 38, 2);
                array[40] = (ushort)(short)M_Energy_VA_SF;
                Array.Copy(M_Import_VARh_Q1.ToRegisters(), 0, array, 41, 2);
                Array.Copy(M_Import_VARh_Q1A.ToRegisters(), 0, array, 43, 2);
                Array.Copy(M_Import_VARh_Q1B.ToRegisters(), 0, array, 45, 2);
                Array.Copy(M_Import_VARh_Q1C.ToRegisters(), 0, array, 47, 2);
                Array.Copy(M_Import_VARh_Q2.ToRegisters(), 0, array, 49, 2);
                Array.Copy(M_Import_VARh_Q2A.ToRegisters(), 0, array, 51, 2);
                Array.Copy(M_Import_VARh_Q2B.ToRegisters(), 0, array, 53, 2);
                Array.Copy(M_Import_VARh_Q2C.ToRegisters(), 0, array, 55, 2);
                Array.Copy(M_Import_VARh_Q3.ToRegisters(), 0, array, 57, 2);
                Array.Copy(M_Import_VARh_Q3A.ToRegisters(), 0, array, 59, 2);
                Array.Copy(M_Import_VARh_Q3B.ToRegisters(), 0, array, 61, 2);
                Array.Copy(M_Import_VARh_Q3C.ToRegisters(), 0, array, 63, 2);
                Array.Copy(M_Import_VARh_Q4.ToRegisters(), 0, array, 65, 2);
                Array.Copy(M_Import_VARh_Q4A.ToRegisters(), 0, array, 67, 2);
                Array.Copy(M_Import_VARh_Q4B.ToRegisters(), 0, array, 69, 2);
                Array.Copy(M_Import_VARh_Q4C.ToRegisters(), 0, array, 71, 2);
                array[73] = (ushort)(short)M_Energy_VAR_SF;
                Array.Copy(M_Events.ToRegisters(), 0, array, 74, 2);
                array[76] = C_SunSpec_DID3;
                array[77] = C_SunSpec_Length3;
                return array;
            }
            set
            {
                if (value.Length == 78)
                {
                    M_AC_VAR_C = value[0];
                    M_AC_VAR_SF = value[1];
                    M_AC_PF = value[2];
                    M_AC_PF_A = value[3];
                    M_AC_PF_B = value[4];
                    M_AC_PF_C = value[5];
                    M_AC_PF_SF = value[6];
                    M_Exported = value.Skip(7).Take(2).ToArray();
                    M_Exported_A = value.Skip(9).Take(2).ToArray();
                    M_Exported_B = value.Skip(11).Take(2).ToArray();
                    M_Exported_C = value.Skip(13).Take(2).ToArray();
                    M_Imported = value.Skip(15).Take(2).ToArray();
                    M_Imported_A = value.Skip(17).Take(2).ToArray();
                    M_Imported_B = value.Skip(19).Take(2).ToArray();
                    M_Imported_C = value.Skip(21).Take(2).ToArray();
                    M_Energy_W_SF = value.Skip(23).Take(2).ToArray();
                    M_Exported_VA = value.Skip(24).Take(2).ToArray();
                    M_Exported_VA_A = value.Skip(26).Take(2).ToArray();
                    M_Exported_VA_B = value.Skip(28).Take(2).ToArray();
                    M_Exported_VA_C = value.Skip(30).Take(2).ToArray();
                    M_Imported_VA = value.Skip(32).Take(2).ToArray();
                    M_Imported_VA_A = value.Skip(34).Take(2).ToArray();
                    M_Imported_VA_B = value.Skip(36).Take(2).ToArray();
                    M_Imported_VA_C = value.Skip(38).Take(2).ToArray();
                    M_Energy_VA_SF = value[40];
                    M_Import_VARh_Q1 = value.Skip(41).Take(2).ToArray();
                    M_Import_VARh_Q1A = value.Skip(43).Take(2).ToArray();
                    M_Import_VARh_Q1B = value.Skip(45).Take(2).ToArray();
                    M_Import_VARh_Q1C = value.Skip(47).Take(2).ToArray();
                    M_Import_VARh_Q2 = value.Skip(49).Take(2).ToArray();
                    M_Import_VARh_Q2A = value.Skip(51).Take(2).ToArray();
                    M_Import_VARh_Q2B = value.Skip(53).Take(2).ToArray();
                    M_Import_VARh_Q2C = value.Skip(55).Take(2).ToArray();
                    M_Import_VARh_Q3 = value.Skip(57).Take(2).ToArray();
                    M_Import_VARh_Q3A = value.Skip(59).Take(2).ToArray();
                    M_Import_VARh_Q3B = value.Skip(61).Take(2).ToArray();
                    M_Import_VARh_Q3C = value.Skip(63).Take(2).ToArray();
                    M_Import_VARh_Q4 = value.Skip(65).Take(2).ToArray();
                    M_Import_VARh_Q4A = value.Skip(67).Take(2).ToArray();
                    M_Import_VARh_Q4B = value.Skip(69).Take(2).ToArray();
                    M_Import_VARh_Q4C = value.Skip(71).Take(2).ToArray();
                    M_Energy_VAR_SF = value[73];
                    M_Events = value.Skip(74).Take(2).ToArray();
                    C_SunSpec_DID3 = value[76];
                    C_SunSpec_Length3 = value[77];
                }
            }
        }

        #endregion Block Properties

        #region Public Methods

        /// <summary>
        /// Updates all the Properties.
        /// </summary>
        /// <param name="data">The BControl data.</param>
        public void Refresh(BControlData data)
        {
            if (data != null)
            {
                // Internal immediate registers
                ActivePowerPlus = data.ActivePowerPlus;
                ActivePowerMinus = data.ActivePowerMinus;
                ReactivePowerPlus = data.ReactivePowerPlus;
                ReactivePowerMinus = data.ReactivePowerMinus;
                ApparentPowerPlus = data.ApparentPowerPlus;
                ApparentPowerMinus = data.ApparentPowerMinus;
                PowerFactor = data.PowerFactor;
                LineFrequency = data.LineFrequency;
                ActivePowerPlusL1 = data.ActivePowerPlusL1;
                ActivePowerMinusL1 = data.ActivePowerMinusL1;
                ReactivePowerPlusL1 = data.ReactivePowerPlusL1;
                ReactivePowerMinusL1 = data.ReactivePowerMinusL1;
                ApparentPowerPlusL1 = data.ApparentPowerPlusL1;
                ApparentPowerMinusL1 = data.ApparentPowerMinusL1;
                CurrentL1 = data.CurrentL1;
                VoltageL1 = data.VoltageL1;
                PowerFactorL1 = data.PowerFactorL1;
                ActivePowerPlusL2 = data.ActivePowerPlusL2;
                ActivePowerMinusL2 = data.ActivePowerMinusL2;
                ReactivePowerPlusL2 = data.ReactivePowerPlusL2;
                ReactivePowerMinusL2 = data.ReactivePowerMinusL2;
                ApparentPowerPlusL2 = data.ApparentPowerPlusL2;
                ApparentPowerMinusL2 = data.ApparentPowerMinusL2;
                CurrentL2 = data.CurrentL2;
                VoltageL2 = data.VoltageL2;
                PowerFactorL2 = data.PowerFactorL2;
                ActivePowerPlusL3 = data.ActivePowerPlusL3;
                ActivePowerMinusL3 = data.ActivePowerMinusL3;
                ReactivePowerPlusL3 = data.ReactivePowerPlusL3;
                ReactivePowerMinusL3 = data.ReactivePowerMinusL3;
                ApparentPowerPlusL3 = data.ApparentPowerPlusL3;
                ApparentPowerMinusL3 = data.ApparentPowerMinusL3;
                CurrentL3 = data.CurrentL3;
                VoltageL3 = data.VoltageL3;
                PowerFactorL3 = data.PowerFactorL3;

                //Internal energy registers(meters)
                ActiveEnergyPlus = data.ActiveEnergyPlus;
                ActiveEnergyMinus = data.ActiveEnergyMinus;
                ReactiveEnergyPlus = data.ReactiveEnergyPlus;
                ReactiveEnergyMinus = data.ReactiveEnergyMinus;
                ApparentEnergyPlus = data.ApparentEnergyPlus;
                ApparentEnergyMinus = data.ApparentEnergyMinus;
                ActiveEnergyPlusL1 = data.ActiveEnergyPlusL1;
                ActiveEnergyMinusL1 = data.ActiveEnergyMinusL1;
                ReactiveEnergyPlusL1 = data.ReactiveEnergyPlusL1;
                ReactiveEnergyMinusL1 = data.ReactiveEnergyMinusL1;
                ApparentEnergyPlusL1 = data.ApparentEnergyPlusL1;
                ApparentEnergyMinusL1 = data.ApparentEnergyMinusL1;
                ActiveEnergyPlusL2 = data.ActiveEnergyPlusL2;
                ActiveEnergyMinusL2 = data.ActiveEnergyMinusL2;
                ReactiveEnergyPlusL2 = data.ReactiveEnergyPlusL2;
                ReactiveEnergyMinusL2 = data.ReactiveEnergyMinusL2;
                ApparentEnergyPlusL2 = data.ApparentEnergyPlusL2;
                ApparentEnergyMinusL2 = data.ApparentEnergyMinusL2;
                ActiveEnergyPlusL3 = data.ActiveEnergyPlusL3;
                ActiveEnergyMinusL3 = data.ActiveEnergyMinusL3;
                ReactiveEnergyPlusL3 = data.ReactiveEnergyPlusL3;
                ReactiveEnergyMinusL3 = data.ReactiveEnergyMinusL3;
                ApparentEnergyPlusL3 = data.ApparentEnergyPlusL3;
                ApparentEnergyMinusL3 = data.ApparentEnergyMinusL3;

                // PnP registers
                ManufacturerID = data.ManufacturerID;
                ProductID = data.ProductID;
                ProductVersion = data.ProductVersion;
                FirmwareVersion = data.FirmwareVersion;
                VendorName = data.VendorName;
                ProductName = data.ProductName;
                SerialNumber = data.SerialNumber;

                // SunSpec registers
                C_SunSpec_ID = data.C_SunSpec_ID;
                C_SunSpec_DID1 = data.C_SunSpec_DID1;
                C_SunSpec_Length1 = data.C_SunSpec_Length1;
                C_Manufacturer = data.C_Manufacturer;
                C_Model = data.C_Model;
                C_Options = data.C_Options;
                C_Version = data.C_Version;
                C_SerialNumber = data.C_SerialNumber;
                C_DeviceAddress = data.C_DeviceAddress;
                C_SunSpec_DID2 = data.C_SunSpec_DID2;
                C_SunSpec_Length2 = data.C_SunSpec_Length2;
                M_AC_Current = data.M_AC_Current;
                M_AC_Current_A = data.M_AC_Current_A;
                M_AC_Current_B = data.M_AC_Current_B;
                M_AC_Current_C = data.M_AC_Current_C;
                M_AC_Current_SF = data.M_AC_Current_SF;
                M_AC_Voltage_LN = data.M_AC_Voltage_LN;
                M_AC_Voltage_AN = data.M_AC_Voltage_AN;
                M_AC_Voltage_BN = data.M_AC_Voltage_BN;
                M_AC_Voltage_CN = data.M_AC_Voltage_CN;
                M_AC_Voltage_LL = data.M_AC_Voltage_LL;
                M_AC_Voltage_AB = data.M_AC_Voltage_AB;
                M_AC_Voltage_BC = data.M_AC_Voltage_BC;
                M_AC_Voltage_CA = data.M_AC_Voltage_CA;
                M_AC_Voltage_SF = data.M_AC_Voltage_SF;
                M_AC_Freq = data.M_AC_Freq;
                M_AC_Freq_SF = data.M_AC_Freq_SF;
                M_AC_Power = data.M_AC_Power;
                M_AC_Power_A = data.M_AC_Power_A;
                M_AC_Power_B = data.M_AC_Power_B;
                M_AC_Power_C = data.M_AC_Power_C;
                M_AC_Power_SF = data.M_AC_Power_SF;
                M_AC_VA = data.M_AC_VA;
                M_AC_VA_A = data.M_AC_VA_A;
                M_AC_VA_B = data.M_AC_VA_B;
                M_AC_VA_C = data.M_AC_VA_C;
                M_AC_VA_SF = data.M_AC_VA_SF;
                M_AC_VAR = data.M_AC_VAR;
                M_AC_VAR_A = data.M_AC_VAR_A;
                M_AC_VAR_B = data.M_AC_VAR_B;
                M_AC_VAR_C = data.M_AC_VAR_C;
                M_AC_VAR_SF = data.M_AC_VAR_SF;
                M_AC_PF = data.M_AC_PF;
                M_AC_PF_A = data.M_AC_PF_A;
                M_AC_PF_B = data.M_AC_PF_B;
                M_AC_PF_C = data.M_AC_PF_C;
                M_AC_PF_SF = data.M_AC_PF_SF;
                M_Exported = data.M_Exported;
                M_Exported_A = data.M_Exported_A;
                M_Exported_B = data.M_Exported_B;
                M_Exported_C = data.M_Exported_C;
                M_Imported = data.M_Imported;
                M_Imported_A = data.M_Imported_A;
                M_Imported_B = data.M_Imported_B;
                M_Imported_C = data.M_Imported_C;
                M_Energy_W_SF = data.M_Energy_W_SF;
                M_Exported_VA = data.M_Exported_VA;
                M_Exported_VA_A = data.M_Exported_VA_A;
                M_Exported_VA_B = data.M_Exported_VA_B;
                M_Exported_VA_C = data.M_Exported_VA_C;
                M_Imported_VA = data.M_Imported_VA;
                M_Imported_VA_A = data.M_Imported_VA_A;
                M_Imported_VA_B = data.M_Imported_VA_B;
                M_Imported_VA_C = data.M_Imported_VA_C;
                M_Energy_VA_SF = data.M_Energy_VA_SF;
                M_Import_VARh_Q1 = data.M_Import_VARh_Q1;
                M_Import_VARh_Q1A = data.M_Import_VARh_Q1A;
                M_Import_VARh_Q1B = data.M_Import_VARh_Q1B;
                M_Import_VARh_Q1C = data.M_Import_VARh_Q1C;
                M_Import_VARh_Q2 = data.M_Import_VARh_Q2;
                M_Import_VARh_Q2A = data.M_Import_VARh_Q2A;
                M_Import_VARh_Q2B = data.M_Import_VARh_Q2B;
                M_Import_VARh_Q2C = data.M_Import_VARh_Q2C;
                M_Import_VARh_Q3 = data.M_Import_VARh_Q3;
                M_Import_VARh_Q3A = data.M_Import_VARh_Q3A;
                M_Import_VARh_Q3B = data.M_Import_VARh_Q3B;
                M_Import_VARh_Q3C = data.M_Import_VARh_Q3C;
                M_Import_VARh_Q4 = data.M_Import_VARh_Q4;
                M_Import_VARh_Q4A = data.M_Import_VARh_Q4A;
                M_Import_VARh_Q4B = data.M_Import_VARh_Q4B;
                M_Import_VARh_Q4C = data.M_Import_VARh_Q4C;
                M_Energy_VAR_SF = data.M_Energy_VAR_SF;
                M_Events = data.M_Events;
                C_SunSpec_DID3 = data.C_SunSpec_DID3;
                C_SunSpec_Length3 = data.C_SunSpec_Length3;
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
            ModbusAttribute.GetModbusAttribute(PropertyValue.GetPropertyInfo(typeof(BControlData), property));

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
        /// Gets the property list for the BControlData class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static string[] GetProperties()
            => typeof(BControlData).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Select(p => p.Name).ToArray();

        /// <summary>
        /// Returns true if property with the specified name is found in the ETAPU11Data class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property) => PropertyValue.GetPropertyInfo(typeof(BControlData), property) != null;

        /// <summary>
        /// Returns the <see cref="PropertyInfo"/> data for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The property info.</returns>
        public static PropertyInfo GetPropertyInfo(string property) => PropertyValue.GetPropertyInfo(typeof(BControlData), property);

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

        #endregion
    }
}
