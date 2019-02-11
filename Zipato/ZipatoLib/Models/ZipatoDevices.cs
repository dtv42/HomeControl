// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoDevices.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models
{
    using System;
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using DataValueLib;
    using ZipatoLib.Models.Devices;

    #endregion

    /// <summary>
    /// Class holding attribute data values from the Zipato home controller.
    /// </summary>
    public class ZipatoDevices : DataValue, IPropertyHelper
    {
        #region Private Data Members

        private IZipato _zipato;

        #endregion

        #region Public Properties

        public List<Switch> Switches { get; set; } = new List<Switch> { };
        public List<OnOff> OnOffSwitches { get; set; } = new List<OnOff> { };
        public List<Plug> Wallplugs { get; set; } = new List<Plug> { };
        public List<Dimmer> Dimmers { get; set; } = new List<Dimmer> { };
        public List<RGBControl> RGBControls { get; set; } = new List<RGBControl> { };

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipatoDevices"/> class.
        /// </summary>
        public ZipatoDevices()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipatoDevices"/> class.
        /// The list of UUIDs from the SettingsData is used to create the device instances.
        /// </summary>
        /// <param name="zipato"></param>
        public ZipatoDevices(IZipato zipato)
        {
            _zipato = zipato;

            foreach (var uuid in zipato.DevicesInfo.Switches)
            {
                Switches.Add(new Switch(zipato, uuid));
            }

            foreach (var uuid in zipato.DevicesInfo.OnOffSwitches)
            {
                OnOffSwitches.Add(new OnOff(zipato, uuid));
            }

            foreach (var uuid in zipato.DevicesInfo.Wallplugs)
            {
                Wallplugs.Add(new Plug(zipato, uuid));
            }

            foreach (var uuid in zipato.DevicesInfo.Dimmers)
            {
                Dimmers.Add(new Dimmer(zipato, uuid));
            }

            foreach (var uuid in zipato.DevicesInfo.RGBControls)
            {
                RGBControls.Add(new RGBControl(zipato, uuid));
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the devices used in ZipatoDevices.
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < Switches.Count; ++i)
            {
                var uuid = Switches[i].Uuid;
                Switches[i] = new Switch(_zipato, uuid);
            }

            for (int i = 0; i < OnOffSwitches.Count; ++i)
            {
                var uuid = OnOffSwitches[i].Uuid;
                OnOffSwitches[i] = new OnOff(_zipato, uuid);
            }

            for (int i = 0; i < Wallplugs.Count; ++i)
            {
                var uuid = Wallplugs[i].Uuid;
                Wallplugs[i] = new Plug(_zipato, uuid);
            }

            for (int i = 0; i < Dimmers.Count; ++i)
            {
                var uuid = Dimmers[i].Uuid;
                Dimmers[i] = new Dimmer(_zipato, uuid);
            }

            for (int i = 0; i < RGBControls.Count; ++i)
            {
                var uuid = RGBControls[i].Uuid;
                RGBControls[i] = new RGBControl(_zipato, uuid);
            }

            Status = _zipato.Data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Updates the data values used in ZipatoDevices.
        /// </summary>
        public void Refresh()
        {
            foreach (var item in Switches)
            {
                item.Refresh();
            }

            foreach (var item in OnOffSwitches)
            {
                item.Refresh();
            }

            foreach (var item in Wallplugs)
            {
                item.Refresh();
            }

            foreach (var item in Dimmers)
            {
                item.Refresh();
            }

            foreach (var item in RGBControls)
            {
                item.Refresh();
            }

            Status = _zipato.Data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the ZipatoDevices class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static List<string> GetProperties()
            => typeof(ZipatoDevices).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToList();

        /// <summary>
        /// Returns true if property with the specified name is found in the InverterInfo class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property)
            => (PropertyValue.GetPropertyInfo(typeof(ZipatoDevices), property) != null) ? true : false;

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
