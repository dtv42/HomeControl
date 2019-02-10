// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RGBControl.cs" company="DTV-Online">
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
    using Newtonsoft.Json;
    using ZipatoLib.Models.Data;
    using ZipatoLib.Models.Data.Color;

    #endregion

    /// <summary>
    /// This class represents a Zipato RGB control device.
    /// </summary>
    public class RGBControl
    {
        #region Private Data Members

        private IZipato _zipato;

        #endregion

        #region Public Properties

        /// <summary>
        /// The UUID of the RGB controller.
        /// </summary>
        public Guid Uuid { get; }

        /// <summary>
        /// The name of the RGB controller.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The INTENSITY value of the RGB controller.
        /// </summary>
        public ValueInfo<int> Intensity { get; private set; } = new ValueInfo<int>();

        /// <summary>
        /// The RED value of the RGB controller.
        /// </summary>
        public ValueInfo<int> Red { get; set; } = new ValueInfo<int>();

        /// <summary>
        /// The GREEN value of the RGB controller.
        /// </summary>
        public ValueInfo<int> Green { get; set; } = new ValueInfo<int>();

        /// <summary>
        /// The BLUE value of the RGB controller.
        /// </summary>
        public ValueInfo<int> Blue { get; set; } = new ValueInfo<int>();

        /// <summary>
        /// The ColdWhite value of the RGB controller.
        /// </summary>
        public ValueInfo<int> ColdWhite { get; set; } = new ValueInfo<int>();

        /// <summary>
        /// The WarmWhite value of the RGB controller.
        /// </summary>
        public ValueInfo<int> WarmWhite { get; set; } = new ValueInfo<int>();

        /// <summary>
        /// The RGB value of the RGB controller.
        /// </summary>
        [JsonIgnore]
        public ValueInfoString RGB { get; set; } = new ValueInfoString();

        /// <summary>
        /// The RGBW value of the RGB controller.
        /// </summary>
        [JsonIgnore]
        public ValueInfoString RGBW { get; set; } = new ValueInfoString();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBControl"/> class.
        /// </summary>
        public RGBControl()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBControl"/> class.
        /// </summary>
        /// <remarks>Requires a valid list of endpoints, attributes, and values.</remarks>
        /// <param name="zipato"></param>
        /// <param name="uuid"></param>
        public RGBControl(IZipato zipato, Guid uuid)
        {
            _zipato = zipato;
            Uuid = uuid;
            Name = zipato.GetEndpoint(uuid)?.Name;
            Intensity = zipato.GetAttributeByName(uuid, "INTENSITY");
            Red = zipato.GetAttributeByName(uuid, "red");
            Green = zipato.GetAttributeByName(uuid, "green");
            Blue = zipato.GetAttributeByName(uuid, "blue");
            ColdWhite = zipato.GetAttributeByName(uuid, "coldWhite");
            WarmWhite = zipato.GetAttributeByName(uuid, "warmWhite");
            RGB = zipato.GetAttributeByName(uuid, "rgb");
            RGBW = zipato.GetAttributeByName(uuid, "rgbw");
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
        /// Sets the cold white attribute value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetColdWhite(int value)
        {
            if (value < 0) value = 0;
            if (value > 255) value = 255;

            var ok = _zipato.SetNumberAsync(ColdWhite.Uuid, value).Result;

            if (ok)
            {
                ColdWhite.Value = value;
            }

            return ok;
        }

        /// <summary>
        /// Sets the warm white attribute value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetWarmWhite(int value)
        {
            if (value < 0) value = 0;
            if (value > 255) value = 255;

            var ok = _zipato.SetNumberAsync(WarmWhite.Uuid, value).Result;

            if (ok)
            {
                WarmWhite.Value = value;
            }

            return ok;
        }

        /// <summary>
        /// Sets the red attribute value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetRed(int value)
        {
            if (value < 0) value = 0;
            if (value > 255) value = 255;

            var ok = _zipato.SetNumberAsync(Red.Uuid, value).Result;

            if (ok)
            {
                Red.Value = (int?)_zipato.GetNumber(Red.Uuid) ?? Red.Value;
            }

            return ok;
        }

        /// <summary>
        /// Sets the green attribute value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetGreen(int value)
        {
            if (value < 0) value = 0;
            if (value > 255) value = 255;

            var ok = _zipato.SetNumberAsync(Green.Uuid, value).Result;

            if (ok)
            {
                Green.Value = (int?)_zipato.GetNumber(Green.Uuid) ?? Green.Value;
            }

            return ok;
        }

        /// <summary>
        /// Sets the red attribute value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetBlue(int value)
        {
            if (value < 0) value = 0;
            if (value > 255) value = 255;

            var ok = _zipato.SetNumberAsync(Blue.Uuid, value).Result;

            if (ok)
            {
                Blue.Value = (int?)_zipato.GetNumber(Blue.Uuid) ?? Blue.Value;
            }

            return ok;
        }

        /// <summary>
        /// Sets the RGB attribute value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetRGB(string value)
        {
            var ok = _zipato.SetColorAsync(RGB.Uuid, value).Result;

            if (ok)
            {
                _zipato.DataReadValueAsync(Red.Uuid).Wait();
                _zipato.DataReadValueAsync(Green.Uuid).Wait();
                _zipato.DataReadValueAsync(Blue.Uuid).Wait();
                Refresh();
            }

            return ok;
        }

        /// <summary>
        /// Sets the RGBW attribute value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetRGBW(string value)
        {
            var ok = _zipato.SetColorAsync(RGBW.Uuid, value).Result;

            if (ok)
            {
                _zipato.DataReadValueAsync(Red.Uuid).Wait();
                _zipato.DataReadValueAsync(Green.Uuid).Wait();
                _zipato.DataReadValueAsync(Blue.Uuid).Wait();
                Refresh();
            }

            return ok;
        }

        /// <summary>
        /// Refreshes the property data using the list of values.
        /// </summary>
        /// <remarks>Requires a valid list of values.</remarks>
        public void Refresh()
        {
            Intensity.Value = (int?)_zipato.GetNumber(Intensity.Uuid) ?? Intensity.Value;
            ColdWhite.Value = (int?)_zipato.GetNumber(ColdWhite.Uuid) ?? ColdWhite.Value;
            WarmWhite.Value = (int?)_zipato.GetNumber(WarmWhite.Uuid) ?? WarmWhite.Value;
            Red.Value = (int?)_zipato.GetNumber(Red.Uuid) ?? Red.Value;
            Green.Value = (int?)_zipato.GetNumber(Green.Uuid) ?? Green.Value;
            Blue.Value = (int?)_zipato.GetNumber(Blue.Uuid) ?? Blue.Value;
        }

        #endregion
    }
}
