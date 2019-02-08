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
        /// </summary>
        /// <param name="zipato"></param>
        public ZipatoSensors(IZipato zipato)
        {
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
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in ZipatoSensors.
        /// </summary>
        /// <param name="data">The Zipato data.</param>
        public void Refresh(ZipatoData data)
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

            Status = data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the InverterInfo class.
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
