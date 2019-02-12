// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoScenes.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using DataValueLib;
    using ZipatoLib.Models.Others;

    #endregion

    /// <summary>
    /// Class holding attribute data values from the Zipato home controller.
    /// </summary>
    public class ZipatoOthers : DataValue, IPropertyHelper
    {
        #region Private Data Members

        private IZipato _zipato;

        #endregion

        #region Public Properties

        public List<Camera> Cameras { get; set; } = new List<Camera> { };
        public List<Scene> Scenes { get; set; } = new List<Scene> { };

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipatoOthers"/> class.
        /// </summary>
        public ZipatoOthers()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipatoOthers"/> class.
        /// The list of UUIDs from the SettingsData is used to create the device instances.
        /// </summary>
        /// <param name="zipato"></param>
        public ZipatoOthers(IZipato zipato)
        {
            _zipato = zipato;

            foreach (var uuid in zipato.OthersInfo.Cameras)
            {
                Cameras.Add(new Camera(zipato, uuid));
            }

            foreach (var uuid in zipato.OthersInfo.Scenes)
            {
                Scenes.Add(new Scene(zipato, uuid));
            }

            Status = zipato?.Data.Status ?? Uncertain;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the other devices used in ZipatoOthers.
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < Cameras.Count; ++i)
            {
                var uuid = Cameras[i].Uuid;
                Cameras[i] = new Camera(_zipato, uuid);
            }

            for (int i = 0; i < Scenes.Count; ++i)
            {
                var uuid = Scenes[i].Uuid;
                Scenes[i] = new Scene(_zipato, uuid);
            }

            Status = _zipato?.Data?.Status ?? Uncertain;
        }


        /// <summary>
        /// Updates the data values used in ZipatoOthers.
        /// </summary>
        public void Refresh()
        {
            foreach (var item in Cameras)
            {
                item.Refresh();
            }

            foreach (var item in Scenes)
            {
                item.Refresh();
            }

            Status = _zipato?.Data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the InverterInfo class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static List<string> GetProperties()
            => typeof(ZipatoOthers).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToList();

        /// <summary>
        /// Returns true if property with the specified name is found in the InverterInfo class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property)
            => (PropertyValue.GetPropertyInfo(typeof(ZipatoOthers), property) != null) ? true : false;

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
