// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VirtualMeter.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Sensors
{
    #region Using Directives

    using System;

    #endregion

    public class VirtualMeter
    {
        #region Public Properties

        /// <summary>
        /// The name of the meter.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The UUID of the meter.
        /// </summary>
        public Guid Uuid { get; }

        /// <summary>
        /// The cummulative consumption value of the meter.
        /// </summary>
        public ValueInfo<double> Value1 { get; private set; } = new ValueInfo<double>();

        /// <summary>
        /// The cummulative consumption value of the meter.
        /// </summary>
        public ValueInfo<double> Value2 { get; private set; } = new ValueInfo<double>();

        /// <summary>
        /// The cummulative consumption value of the meter.
        /// </summary>
        public ValueInfo<double> Value3 { get; private set; } = new ValueInfo<double>();

        /// <summary>
        /// The cummulative consumption value of the meter.
        /// </summary>
        public ValueInfo<double> Value4 { get; private set; } = new ValueInfo<double>();

        /// <summary>
        /// The cummulative consumption value of the meter.
        /// </summary>
        public ValueInfo<double> Value5 { get; private set; } = new ValueInfo<double>();

        /// <summary>
        /// The cummulative consumption value of the meter.
        /// </summary>
        public ValueInfo<double> Value6 { get; private set; } = new ValueInfo<double>();

        /// <summary>
        /// The cummulative consumption value of the meter.
        /// </summary>
        public ValueInfo<double> Value7 { get; private set; } = new ValueInfo<double>();

        /// <summary>
        /// The cummulative consumption value of the meter.
        /// </summary>
        public ValueInfo<double> Value8 { get; private set; } = new ValueInfo<double>();

        /// <summary>
        /// The cummulative consumption value of the meter.
        /// </summary>
        public ValueInfo<double> Value9 { get; private set; } = new ValueInfo<double>();

        /// <summary>
        /// The cummulative consumption value of the meter.
        /// </summary>
        public ValueInfo<double> Value10 { get; private set; } = new ValueInfo<double>();

        /// <summary>
        /// The cummulative consumption value of the meter.
        /// </summary>
        public ValueInfo<double> Value11 { get; private set; } = new ValueInfo<double>();

        /// <summary>
        /// The cummulative consumption value of the meter.
        /// </summary>
        public ValueInfo<double> Value12 { get; private set; } = new ValueInfo<double>();

        /// <summary>
        /// The cummulative consumption value of the meter.
        /// </summary>
        public ValueInfo<double> Value13 { get; private set; } = new ValueInfo<double>();

        /// <summary>
        /// The cummulative consumption value of the meter.
        /// </summary>
        public ValueInfo<double> Value14 { get; private set; } = new ValueInfo<double>();

        /// <summary>
        /// The cummulative consumption value of the meter.
        /// </summary>
        public ValueInfo<double> Value15 { get; private set; } = new ValueInfo<double>();

        /// <summary>
        /// The cummulative consumption value of the meter.
        /// </summary>
        public ValueInfo<double> Value16 { get; private set; } = new ValueInfo<double>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the attribute value.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public ValueInfo<double> GetValue(int index)
        {
            if ((index < 0) && (index > 15))
            {
                return new ValueInfo<double>();
            }

            switch (index)
            {
                case 0: return Value1;
                case 1: return Value2;
                case 2: return Value3;
                case 3: return Value4;
                case 4: return Value5;
                case 5: return Value6;
                case 6: return Value7;
                case 7: return Value8;
                case 8: return Value9;
                case 9: return Value10;
                case 10: return Value11;
                case 11: return Value12;
                case 12: return Value13;
                case 13: return Value14;
                case 14: return Value15;
                case 15: return Value16;
            }

            return new ValueInfo<double>();
        }

        public Guid? GetAttributeUuid(int index)
        {
            if ((index < 0) || (index > 15))
            {
                return null;
            }

            switch (index)
            {
                case 0:  return Value1.Uuid;
                case 1:  return Value2.Uuid;
                case 2:  return Value3.Uuid;
                case 3:  return Value4.Uuid;
                case 4:  return Value5.Uuid;
                case 5:  return Value6.Uuid;
                case 6:  return Value7.Uuid;
                case 7:  return Value8.Uuid;
                case 8:  return Value9.Uuid;
                case 9:  return Value10.Uuid;
                case 10: return Value11.Uuid;
                case 11: return Value12.Uuid;
                case 12: return Value13.Uuid;
                case 13: return Value14.Uuid;
                case 14: return Value15.Uuid;
                case 15: return Value16.Uuid;
                default: return null;
            }
        }

        #endregion
    }
}
