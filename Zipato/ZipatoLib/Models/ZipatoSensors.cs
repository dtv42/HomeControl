// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoSensors.cs" company="DTV-Online">
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
    using ZipatoLib.Models.Sensors;

    #endregion

    /// <summary>
    /// Class holding attribute data values from the Zipato home controller.
    /// </summary>
    public class ZipatoSensors : DataValue, IPropertyHelper
    {
        #region Private Data Members

        private IZipato _zipato;

        #endregion

        #region Public Properties

        public List<VirtualMeter> VirtualMeters { get; set; } = new List<VirtualMeter> { };
        public List<ConsumptionMeter> ConsumptionMeters { get; set; } = new List<ConsumptionMeter> { };
        public List<TemperatureSensor> TemperatureSensors { get; set; } = new List<TemperatureSensor> { };
        public List<HumiditySensor> HumiditySensors { get; set; } = new List<HumiditySensor> { };
        public List<LuminanceSensor> LuminanceSensors { get; set; } = new List<LuminanceSensor> { };

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipatoSensors"/> class.
        /// </summary>
        public ZipatoSensors()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipatoSensors"/> class.
        /// The list of UUIDs from the SettingsData is used to create the device instances.
        /// </summary>
        /// <param name="zipato"></param>
        public ZipatoSensors(IZipato zipato)
        {
            _zipato = zipato;

            foreach (var uuid in zipato.SensorsInfo.VirtualMeters)
            {
                VirtualMeters.Add(new VirtualMeter(zipato, uuid));
            }

            foreach (var uuid in zipato.SensorsInfo.ConsumptionMeters)
            {
                ConsumptionMeters.Add(new ConsumptionMeter(zipato, uuid));
            }

            foreach (var uuid in zipato.SensorsInfo.TemperatureSensors)
            {
                TemperatureSensors.Add(new TemperatureSensor(zipato, uuid));
            }

            foreach (var uuid in zipato.SensorsInfo.HumiditySensors)
            {
                HumiditySensors.Add(new HumiditySensor(zipato, uuid));
            }

            foreach (var uuid in zipato.SensorsInfo.LuminanceSensors)
            {
                LuminanceSensors.Add(new LuminanceSensor(zipato, uuid));
            }

            Status = _zipato?.Data?.Status ?? Uncertain;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the sensors used in ZipatoSensors.
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < VirtualMeters.Count; ++i)
            {
                var uuid = VirtualMeters[i].Uuid;
                VirtualMeters[i] = new VirtualMeter(_zipato, uuid);
            }

            for (int i = 0; i < ConsumptionMeters.Count; ++i)
            {
                var uuid = ConsumptionMeters[i].Uuid;
                ConsumptionMeters[i] = new ConsumptionMeter(_zipato, uuid);
            }

            for (int i = 0; i < TemperatureSensors.Count; ++i)
            {
                var uuid = TemperatureSensors[i].Uuid;
                TemperatureSensors[i] = new TemperatureSensor(_zipato, uuid);
            }

            for (int i = 0; i < HumiditySensors.Count; ++i)
            {
                var uuid = HumiditySensors[i].Uuid;
                HumiditySensors[i] = new HumiditySensor(_zipato, uuid);
            }

            for (int i = 0; i < LuminanceSensors.Count; ++i)
            {
                var uuid = LuminanceSensors[i].Uuid;
                LuminanceSensors[i] = new LuminanceSensor(_zipato, uuid);
            }

            Status = _zipato?.Data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Updates the data values used in ZipatoSensors.
        /// </summary>
        public void Refresh()
        {
            foreach (var item in VirtualMeters)
            {
                item.Refresh();
            }

            foreach (var item in ConsumptionMeters)
            {
                item.Refresh();
            }

            foreach (var item in TemperatureSensors)
            {
                item.Refresh();
            }

            foreach (var item in HumiditySensors)
            {
                item.Refresh();
            }

            foreach (var item in LuminanceSensors)
            {
                item.Refresh();
            }

            Status = _zipato.Data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the ZipatoSensors class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static List<string> GetProperties()
            => typeof(ZipatoSensors).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToList();

        /// <summary>
        /// Returns true if property with the specified name is found in the InverterInfo class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property)
            => (PropertyValue.GetPropertyInfo(typeof(ZipatoSensors), property) != null) ? true : false;

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
