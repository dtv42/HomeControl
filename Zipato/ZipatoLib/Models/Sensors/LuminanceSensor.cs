// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LuminanceSensor.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Sensors
{
    #region Using Directives

    using System;

    #endregion

    /// <summary>
    /// This class represents a Zipato luminance sensor.
    /// </summary>
    public class LuminanceSensor
    {
        #region Private Data Members

        private IZipato _zipato;

        #endregion

        #region Public Properties

        /// <summary>
        /// The name of the plug.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The UUID of the plug.
        /// </summary>
        public Guid Uuid { get; }

        /// <summary>
        /// The cummulative consumption value of the plug.
        /// </summary>
        public ValueInfo<double> Luminance { get; private set; } = new ValueInfo<double>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LuminanceSensor"/> class.
        /// </summary>
        public LuminanceSensor()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LuminanceSensor"/> class.
        /// </summary>
        /// <remarks>Requires a valid list of endpoints, attributes, and values.</remarks>
        /// <param name="zipato"></param>
        /// <param name="uuid"></param>
        public LuminanceSensor(IZipato zipato, Guid uuid)
        {
            _zipato = zipato;
            Uuid = uuid;
            Name = zipato.GetEndpoint(uuid)?.Name;
            Luminance = zipato.GetAttributeByName(uuid, "LUMINANCE");
            Refresh();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Refreshes the property data using the list of values.
        /// </summary>
        /// <remarks>Requires a valid list of values.</remarks>
        public void Refresh()
        {
            Luminance.Value = _zipato.GetDouble(Luminance.Uuid) ?? Luminance.Value;
        }

        #endregion
    }
}
