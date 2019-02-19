// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VirtualMeter.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Sensors
{
    #region Using Directives

    using System;
    using System.Linq;

    #endregion

    /// <summary>
    /// This class represents a Zipato virtual meter.
    /// </summary>
    public class VirtualMeter
    {
        #region Private Data Members

        private IZipato _zipato;

        #endregion

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

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualMeter"/> class.
        /// </summary>
        public VirtualMeter()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualMeter"/> class.
        /// </summary>
        /// <remarks>Requires a valid list of endpoints, attributes, and values.</remarks>
        /// <param name="zipato"></param>
        /// <param name="uuid"></param>
        public VirtualMeter(IZipato zipato, Guid uuid)
        {
            _zipato = zipato;
            Uuid = uuid;
            Name = zipato.GetEndpoint(uuid)?.Name;

            Value1 = zipato.GetAttributeByDefinition(uuid, "value1");
            Value2 = zipato.GetAttributeByDefinition(uuid, "value2");
            Value3 = zipato.GetAttributeByDefinition(uuid, "value3");
            Value4 = zipato.GetAttributeByDefinition(uuid, "value4");
            Value5 = zipato.GetAttributeByDefinition(uuid, "value5");
            Value6 = zipato.GetAttributeByDefinition(uuid, "value6");
            Value7 = zipato.GetAttributeByDefinition(uuid, "value7");
            Value8 = zipato.GetAttributeByDefinition(uuid, "value8");
            Value9 = zipato.GetAttributeByDefinition(uuid, "value9");
            Value10 = zipato.GetAttributeByDefinition(uuid, "value10");
            Value11 = zipato.GetAttributeByDefinition(uuid, "value11");
            Value12 = zipato.GetAttributeByDefinition(uuid, "value12");
            Value13 = zipato.GetAttributeByDefinition(uuid, "value13");
            Value14 = zipato.GetAttributeByDefinition(uuid, "value14");
            Value15 = zipato.GetAttributeByDefinition(uuid, "value15");
            Value16 = zipato.GetAttributeByDefinition(uuid, "value16");
            Refresh();
        }

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

        /// <summary>
        /// Sets the attribute value.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValue(int index, double value)
        {
            if ((index < 0) && (index > 15))
            {
                return false;
            }

            bool ok = false;
            Guid? uuid = GetAttributeUuid(index);

            if (uuid.HasValue)
            {
                ok = _zipato.SetDoubleAsync(uuid.Value, value).Result;

                if (ok)
                {
                    SetMeterValue(index, _zipato.GetDouble(uuid.Value));
                }
            }

            return ok;
        }

        /// <summary>
        /// Sets the attribute value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValues(double?[] values)
        {
            bool ok = false;

            for (int index = 0; index < 16; ++index)
            {
                if (values[index].HasValue)
                {
                    Guid? uuid = GetAttributeUuid(index);

                    if (uuid.HasValue)
                    {
                        ok = _zipato.SetDoubleAsync(uuid.Value, values[index].Value).Result;

                        if (ok)
                        {
                            SetMeterValue(index, _zipato.GetDouble(uuid.Value));
                        }
                    }
                }
            }

            return ok;
        }

        /// <summary>
        /// Sets the attribute value1.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValue1(double value) => SetValue(0, value);

        /// <summary>
        /// Sets the attribute value2.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValue2(double value) => SetValue(1, value);

        /// <summary>
        /// Sets the attribute value3.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValue3(double value) => SetValue(2, value);

        /// <summary>
        /// Sets the attribute value4.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValue4(double value) => SetValue(3, value);

        /// <summary>
        /// Sets the attribute value5.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValue5(double value) => SetValue(4, value);

        /// <summary>
        /// Sets the attribute value6.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValue6(double value) => SetValue(5, value);

        /// <summary>
        /// Sets the attribute value7.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValue7(double value) => SetValue(6, value);

        /// <summary>
        /// Sets the attribute value8.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValue8(double value) => SetValue(7, value);

        /// <summary>
        /// Sets the attribute value9.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValue9(double value) => SetValue(8, value);

        /// <summary>
        /// Sets the attribute value10.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValue10(double value) => SetValue(9, value);

        /// <summary>
        /// Sets the attribute value11.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValue11(double value) => SetValue(10, value);

        /// <summary>
        /// Sets the attribute value12.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValue12(double value) => SetValue(11, value);

        /// <summary>
        /// Sets the attribute value13.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValue13(double value) => SetValue(12, value);

        /// <summary>
        /// Sets the attribute value14.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValue14(double value) => SetValue(13, value);

        /// <summary>
        /// Sets the attribute value15.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValue15(double value) => SetValue(14, value);

        /// <summary>
        /// Sets the attribute value16.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public bool SetValue16(double value) => SetValue(15, value);

        /// <summary>
        /// Refreshes the property data using the list of values.
        /// </summary>
        /// <remarks>Requires a valid list of values.</remarks>
        public void Refresh()
        {
            Value1.Value = _zipato.GetDouble(Value1.Uuid) ?? Value1.Value;
            Value2.Value = _zipato.GetDouble(Value2.Uuid) ?? Value2.Value;
            Value3.Value = _zipato.GetDouble(Value3.Uuid) ?? Value3.Value;
            Value4.Value = _zipato.GetDouble(Value4.Uuid) ?? Value4.Value;
            Value5.Value = _zipato.GetDouble(Value5.Uuid) ?? Value5.Value;
            Value6.Value = _zipato.GetDouble(Value6.Uuid) ?? Value6.Value;
            Value7.Value = _zipato.GetDouble(Value7.Uuid) ?? Value7.Value;
            Value8.Value = _zipato.GetDouble(Value8.Uuid) ?? Value8.Value;
            Value9.Value = _zipato.GetDouble(Value9.Uuid) ?? Value9.Value;
            Value10.Value = _zipato.GetDouble(Value10.Uuid) ?? Value10.Value;
            Value11.Value = _zipato.GetDouble(Value11.Uuid) ?? Value11.Value;
            Value12.Value = _zipato.GetDouble(Value12.Uuid) ?? Value12.Value;
            Value13.Value = _zipato.GetDouble(Value13.Uuid) ?? Value13.Value;
            Value14.Value = _zipato.GetDouble(Value14.Uuid) ?? Value14.Value;
            Value15.Value = _zipato.GetDouble(Value15.Uuid) ?? Value15.Value;
            Value16.Value = _zipato.GetDouble(Value16.Uuid) ?? Value16.Value;
        }

        #endregion

        #region Private Methods

        private Guid? GetAttributeUuid(int index)
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

        private void SetMeterValue(int index, double? data)
        {
            if ((index > 0) && (index < 16) && data.HasValue)
            {
                switch (index)
                {
                    case 0: Value1.Value = data.Value; break;
                    case 1: Value2.Value = data.Value; break;
                    case 2: Value3.Value = data.Value; break;
                    case 3: Value4.Value = data.Value; break;
                    case 4: Value5.Value = data.Value; break;
                    case 5: Value6.Value = data.Value; break;
                    case 6: Value7.Value = data.Value; break;
                    case 7: Value8.Value = data.Value; break;
                    case 8: Value9.Value = data.Value; break;
                    case 9: Value10.Value = data.Value; break;
                    case 10: Value11.Value = data.Value; break;
                    case 11: Value12.Value = data.Value; break;
                    case 12: Value13.Value = data.Value; break;
                    case 13: Value14.Value = data.Value; break;
                    case 14: Value15.Value = data.Value; break;
                    case 15: Value16.Value = data.Value; break;
                    default: break;
                }
            }
        }

        #endregion
    }
}
