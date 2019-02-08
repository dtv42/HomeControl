// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeliosAttribute.cs" company="DTV-Online">
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

    #endregion

    /// <summary>
    /// This attribute allows to mark a property with a Helios specific offset, length, and readonly values.
    /// 
    /// class HeliosClass
    /// {
    ///     [Helios("v00006")]
    ///     public ushort Value { get; set; }
    ///
    ///     [Helios("v00006", 1)]
    ///     public ushort Value { get; set; }
    ///
    ///     [Helios("v00006", 1, 5)]
    ///     public ushort Value { get; set; }
    ///
    ///     [Helios(name: "v00006", size: 1, count: 5)]
    ///     public ushort Value { get; set; }
    ///
    /// }
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class HeliosAttribute : Attribute
    {
        #region Private Data Members

        private string _name = string.Empty;
        private ushort _size = 1;
        private ushort _count = 5;

        #endregion

        #region Public Properties

        /// <summary>
        /// The Helios value name.
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// The Helios value size ().
        /// </summary>
        public ushort Size { get { return _size; } }

        /// <summary>
        /// The Helios value number of .
        /// </summary>
        public ushort Count { get { return _count; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HeliosAttribute"/> class.
        /// </summary>
        public HeliosAttribute(string name, ushort size = 1, ushort count = 5)
        {
            _name = name;
            _size = size;
            _count = count;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Helper function to return the Modbus custom attribute using a PropertyInfo.
        /// </summary>
        /// <param name="info">The property info.</param>
        /// <returns>The Modbus offset.</returns>
        public static HeliosAttribute GetHeliosAttribute(PropertyInfo info)
        {
            if (info != null)
            {
                HeliosAttribute attribute = info.GetCustomAttribute<HeliosAttribute>();

                if (attribute != null)
                {
                    return attribute;
                }
                else
                {
                    throw new InvalidOperationException($"Property '{info.Name}' has no Helios attribute set");
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
