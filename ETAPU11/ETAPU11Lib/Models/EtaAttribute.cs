// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EtaAttribute.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ETAPU11Lib.Models
{
    #region Using Directives

    using System;
    using System.Reflection;

    #endregion

    /// <summary>
    /// This attribute allows to mark a property with a ETA specific scaling factor.
    ///
    /// class EtaClass
    /// {
    ///     [Eta(1)]
    ///     public ushort Value { get; set; }
    ///
    ///     [Eta(scale: 100)]
    ///     public double Value { get; set; }
    /// }
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class EtaAttribute : Attribute
    {
        #region Private Data Members

        private readonly ushort _scale = 1;
        private readonly string _unit = "";
        private readonly int? _min;
        private readonly int? _def;
        private readonly int? _max;

        #endregion

        #region Public Properties

        /// <summary>
        /// The Eta scaling factor.
        /// </summary>
        public ushort Scale { get { return _scale; } }

        /// <summary>
        /// The Eta unit string.
        /// </summary>
        public string Unit { get { return _unit; } }

        /// <summary>
        /// The Eta minimum value.
        /// </summary>
        public int? Minimum { get { return _min; } }

        /// <summary>
        /// The Eta default value.
        /// </summary>
        public int? Default { get { return _def; } }

        /// <summary>
        /// The Eta maximum value.
        /// </summary>
        public int? Maximum { get { return _max; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EtaAttribute"/> class.
        /// </summary>
        public EtaAttribute(ushort scale = 1, string unit = "")
        {
            _scale = scale;
            _unit = unit;
            _min = null;
            _def = null;
            _max = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EtaAttribute"/> class.
        /// </summary>
        public EtaAttribute(ushort scale, string unit, int min, int def, int max)
        {
            _scale = scale;
            _unit = unit;
            _min = min;
            _def = def;
            _max = max;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Helper function to return the ETA custom attribute using a PropertyInfo.
        /// </summary>
        /// <param name="info">The property info.</param>
        /// <returns>The Modbus offset.</returns>
        public static EtaAttribute GetEtaAttribute(PropertyInfo info)
        {
            if (info != null)
            {
                EtaAttribute attribute = info.GetCustomAttribute<EtaAttribute>();

                if (attribute != null)
                {
                    return attribute;
                }
                else
                {
                    throw new InvalidOperationException($"Property '{info.Name}' has no ETA attribute set");
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
