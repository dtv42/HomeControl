// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OnOff.cs" company="DTV-Online">
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
    /// This class represents a Zipato OnOff switch device.
    /// </summary>
    public class OnOff
    {
        #region Private Data Members

        private IZipato _zipato;

        #endregion

        #region Public Properties

        /// <summary>
        /// The UUID of the OnOff switch.
        /// </summary>
        public Guid Uuid { get; }

        /// <summary>
        /// The name the OnOff switch.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The STATE value of the OnOff switch.
        /// </summary>
        public ValueInfo<bool> State { get; private set; } = new ValueInfo<bool>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OnOff"/> class.
        /// </summary>
        public OnOff()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OnOff"/> class.
        /// </summary>
        /// <remarks>Requires a valid list of endpoints, attributes, and values.</remarks>
        /// <param name="zipato"></param>
        /// <param name="uuid"></param>
        public OnOff(IZipato zipato, Guid uuid)
        {
            _zipato = zipato;
            Uuid = uuid;
            Name = zipato.GetEndpoint(uuid)?.Name;
            State = zipato.GetAttributeByName(uuid, "STATE");
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
        /// Refreshes the state using the list of values.
        /// </summary>
        /// <remarks>Requires a valid list of values.</remarks>
        public void Refresh()
        {
            State.Value = _zipato.GetBoolean(State.Uuid) ?? State.Value;
        }

        #endregion
    }
}
