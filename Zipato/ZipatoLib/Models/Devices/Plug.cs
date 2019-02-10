// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Plug.cs" company="DTV-Online">
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

    /// <summary>
    /// This class represents a Zipato plug device.
    /// </summary>
    public class Plug
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
        /// The boolean state of the plug.
        /// </summary>
        public ValueInfo<bool> State { get; private set; } = new ValueInfo<bool>();

        /// <summary>
        /// The cummulative consumption value of the plug.
        /// </summary>
        public ValueInfo<double> CummulativeConsumption { get; private set; } = new ValueInfo<double>();

        /// <summary>
        /// The current consumption value of the plug.
        /// </summary>
        public ValueInfo<double> CurrentConsumption { get; private set; } = new ValueInfo<double>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Plug"/> class.
        /// </summary>
        public Plug()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Plug"/> class.
        /// </summary>
        /// <remarks>Requires a valid list of endpoints, attributes, and values.</remarks>
        /// <param name="zipato"></param>
        /// <param name="uuid"></param>
        public Plug(IZipato zipato, Guid uuid)
        {
            _zipato = zipato;
            Uuid = uuid;
            Name = zipato.GetEndpoint(uuid)?.Name;
            State = zipato.GetAttributeByName(uuid, "STATE");
            CummulativeConsumption = zipato.GetAttributeByName(uuid, "CUMULATIVE_CONSUMPTION");
            CurrentConsumption = zipato.GetAttributeByName(uuid, "CURRENT_CONSUMPTION");
            Refresh();
        }

        #endregion

        #region Public Methods

        public bool TurnOn() => SetState(true);

        public bool TurnOff() => SetState(false);

        public bool Toggle() => SetState(!State.Value);

        /// <summary>
        /// Sets the state attribute value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetState(bool value)
        {
            var ok = _zipato.SetStateAsync(State.Uuid, value).Result;

            if (ok)
            {
                State.Value = value;
            }

            return ok;
        }

        /// <summary>
        /// Refreshes the property data using the list of values.
        /// </summary>
        /// <remarks>Requires a valid list of values.</remarks>
        public void Refresh()
        {
            State.Value = _zipato.GetBoolean(State.Uuid) ?? State.Value;
            CummulativeConsumption.Value = _zipato.GetDouble(CummulativeConsumption.Uuid) ?? CummulativeConsumption.Value;
            CurrentConsumption.Value = _zipato.GetDouble(CurrentConsumption.Uuid) ?? CurrentConsumption.Value;
        }

        #endregion
    }
}
