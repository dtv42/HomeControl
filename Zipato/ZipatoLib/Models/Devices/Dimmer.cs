// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dimmer.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Devices
{
    #region Using Directives

    using System;

    #endregion

    public class Dimmer
    {
        #region Private Data Members

        private IZipato _zipato;

        #endregion

        #region Public Properties

        /// <summary>
        /// The UUID of the dimmer.
        /// </summary>
        public Guid Uuid { get; }

        /// <summary>
        /// The name of the dimmer.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The INTENSITY value of the dimmer.
        /// </summary>
        public ValueInfo<int> Intensity { get; private set; } = new ValueInfo<int>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Dimmer"/> class.
        /// </summary>
        public Dimmer()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dimmer"/> class.
        /// </summary>
        /// <remarks>Requires a valid list of endpoints, attributes, and values.</remarks>
        /// <param name="zipato"></param>
        /// <param name="uuid"></param>
        public Dimmer(IZipato zipato, Guid uuid)
        {
            _zipato = zipato;
            Uuid = uuid;
            Name = zipato.GetEndpoint(uuid)?.Name;
            Intensity = zipato.GetAttributeByName(uuid, "INTENSITY");
            Refresh();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the intensity attribute value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetIntensity(int value)
        {
            if (value < 0) value = 0;
            if (value > 100) value = 100;

            var ok = _zipato.SetNumberAsync(Intensity.Uuid, value).Result;

            if (ok)
            {
                Intensity.Value = value;
            }

            return ok;
        }

        /// <summary>
        /// Refreshes the intesity using the list of values.
        /// </summary>
        /// <remarks>Requires a valid list of values.</remarks>
        public void Refresh()
        {
            Intensity.Value = (int?)_zipato.GetNumber(Intensity.Uuid) ?? Intensity.Value;
        }

        #endregion
    }
}
