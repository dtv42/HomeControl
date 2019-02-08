// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Switch.cs" company="DTV-Online">
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

    public class Switch
    {
        #region Private Data Members

        private IZipato _zipato;

        #endregion

        #region Public Properties

        /// <summary>
        /// The UUID of the boolean device.
        /// </summary>
        public Guid Uuid { get; }

        /// <summary>
        /// The name the boolean device.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The STATE value of the boolean device.
        /// </summary>
        public ValueInfo<bool> State { get; private set; } = new ValueInfo<bool>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Switch"/> class.
        /// </summary>
        public Switch()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Switch"/> class.
        /// </summary>
        /// <remarks>Requires a valid list of endpoints, attributes, and values.</remarks>
        /// <param name="zipato"></param>
        /// <param name="uuid"></param>
        public Switch(IZipato zipato, Guid uuid)
        {
            _zipato = zipato;
            Uuid = uuid;
            Name = zipato.GetEndpoint(uuid)?.Name;
            State = zipato.GetAttributeByName(uuid, "STATE");
            Refresh();
        }

        #endregion

        #region Public Methods

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
