// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModbusAttribute.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbus.Extensions
{
    #region Using Directives

    using System;
    using System.Reflection;

    #endregion

    /// <summary>
    /// This attribute allows to mark a property with a Modbus specific offset, length, and access mode.
    ///
    /// class ModbusClass
    /// {
    ///     [Modbus(1)]
    ///     public ushort Value { get; set; }
    ///
    ///     [Modbus(1, 4)]
    ///     public ushort Value { get; set; }
    ///
    ///     [Modbus(1, 4, AccessModes.ReadOnly)]
    ///     public long Value { get; set; }
    ///
    ///     [Modbus(offset: 1, length: 4, access: AccessModes.ReadOnly)]
    ///     public double Value { get; set; }
    /// }
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ModbusAttribute : Attribute
    {
        #region Public Enums

        /// <summary>
        /// The access mode (RO, RW, WO)
        /// </summary>
        public enum AccessModes
        {
            ReadOnly,
            ReadWrite,
            WriteOnly
        }

        #endregion

        #region Private Data Members

        private readonly AccessModes _access = AccessModes.ReadWrite;
        private readonly ushort _offset = 0;
        private readonly ushort _length = 1;

        #endregion

        #region Public Properties

        /// <summary>
        /// The Modbus address (offset).
        /// </summary>
        public ushort Offset { get { return _offset; } }

        /// <summary>
        /// The Modbus number of array data items.
        /// </summary>
        public ushort Length { get { return _length; } }

        /// <summary>
        /// The Modbus access mode.
        /// </summary>
        public AccessModes Access { get => _access; }

        /// <summary>
        /// Returns true if the access mode is not write only.
        /// </summary>
        public bool IsReadable { get => !IsWriteOnly; }

        /// <summary>
        /// Returns true if the access mode is not read only.
        /// </summary>
        public bool IsWritable { get => !IsReadOnly; }

        /// <summary>
        /// Returns true if the access mode is read only.
        /// </summary>
        public bool IsReadOnly { get => _access == AccessModes.ReadOnly; }

        /// <summary>
        /// Returns true if the access mode is write only.
        /// </summary>
        public bool IsWriteOnly { get => _access == AccessModes.WriteOnly; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ModbusAttribute"/> class.
        /// </summary>
        public ModbusAttribute(ushort offset = 0, ushort length = 1, AccessModes access = AccessModes.ReadWrite)
        {
            _offset = offset;
            _length = length;
            _access = access;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Helper function to return the Modbus custom attribute using a PropertyInfo.
        /// </summary>
        /// <param name="info">The property info.</param>
        /// <returns>The Modbus offset.</returns>
        public static ModbusAttribute GetModbusAttribute(PropertyInfo info)
        {
            if (info != null)
            {
                ModbusAttribute attribute = info.GetCustomAttribute<ModbusAttribute>();

                if (attribute != null)
                {
                    return attribute;
                }
                else
                {
                    throw new InvalidOperationException($"Property '{info.Name}' has no Modbus attribute set");
                }
            }
            else
            {
                throw new ArgumentException($"Specified PropertyInfo is null!");
            }
        }

        #endregion
    }
}
