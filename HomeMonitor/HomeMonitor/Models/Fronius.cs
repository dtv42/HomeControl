// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Fronius.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeMonitor.Models
{
    #region Using Directives

    using System;
    using Newtonsoft.Json;
    using HomeControlLib.Fronius.Models;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    internal class Fronius : ServiceData, IServiceData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public Fronius(string address = "http://localhost:8006")
        {
            Name = "Fronius Symo 8.2-3-M";
            Address = address;

            Rows.Add(new RowData()
            {
                Caption = "Power & Energy",
                Gauge1 = new GaugeData() { Title = "Current Power", Format = "0.000 kW", StartValue = 0, EndValue = 8 },
                Gauge2 = new GaugeData() { Title = "Daily Energy", Format = "0.0 kWh", StartValue = 0, EndValue = 50 },
                Gauge3 = new GaugeData() { Title = "Yearly Energy", Format = "0.0 kWh", StartValue = 0, EndValue = 8000 }
            });

            Rows.Add(new RowData()
            {
                Caption = "DC & AC Data",
                Gauge1 = new GaugeData() { Title = "Current", Format = "0.000 A", StartValue = 0, EndValue = 10 },
                Gauge2 = new GaugeData() { Title = "Voltage", Format = "0.0 V", StartValue = 0, EndValue = 500 },
                Gauge3 = new GaugeData() { Title = "Frequency", Format = "0.00 Hz", StartValue = 48, EndValue = 52 }
            });
        }

        public bool UpdateValues()
        {
            try
            {
                var json = _client.GetStringAsync("api/fronius/common").Result;
                var result = JsonConvert.DeserializeObject<CommonData>(json);

                if (result.Status.IsGood)
                {
                    Rows[0].Gauge1.Value = double.IsNaN(result.PowerAC) ? 0 : result.PowerAC / 1000.0;
                    Rows[0].Gauge2.Value = double.IsNaN(result.DailyEnergy) ? 0 : result.DailyEnergy / 1000.0;
                    Rows[0].Gauge3.Value = double.IsNaN(result.YearlyEnergy) ? 0 : result.YearlyEnergy / 1000.0;
                    Rows[1].Gauge1.Value = double.IsNaN(result.CurrentDC) ? 0 : result.CurrentDC;
                    Rows[1].Gauge2.Value = double.IsNaN(result.VoltageDC) ? 0 : result.VoltageDC;
                    Rows[1].Gauge3.Value = double.IsNaN(result.Frequency) ? 0 : result.Frequency;

                    Rows[0].Gauge1.Header = Rows[0].Gauge1.Value.ToString(Rows[0].Gauge1.Format);
                    Rows[0].Gauge2.Header = Rows[0].Gauge2.Value.ToString(Rows[0].Gauge2.Format);
                    Rows[0].Gauge3.Header = Rows[0].Gauge3.Value.ToString(Rows[0].Gauge3.Format);
                    Rows[1].Gauge1.Header = Rows[1].Gauge1.Value.ToString(Rows[1].Gauge1.Format);
                    Rows[1].Gauge2.Header = Rows[1].Gauge2.Value.ToString(Rows[1].Gauge2.Format);
                    Rows[1].Gauge3.Header = Rows[1].Gauge3.Value.ToString(Rows[1].Gauge3.Format);

                    return true;
                }
                else
                {
                    Rows[0].Gauge1.Value = 0;
                    Rows[0].Gauge2.Value = 0;
                    Rows[0].Gauge3.Value = 0;
                    Rows[1].Gauge1.Value = 0;
                    Rows[1].Gauge2.Value = 0;
                    Rows[1].Gauge3.Value = 0;

                    Rows[0].Gauge1.Header = Rows[0].Gauge1.Value.ToString(Rows[0].Gauge1.Format);
                    Rows[0].Gauge2.Header = Rows[0].Gauge2.Value.ToString(Rows[0].Gauge2.Format);
                    Rows[0].Gauge3.Header = Rows[0].Gauge3.Value.ToString(Rows[0].Gauge3.Format);
                    Rows[1].Gauge1.Header = Rows[1].Gauge1.Value.ToString(Rows[1].Gauge1.Format);
                    Rows[1].Gauge2.Header = Rows[1].Gauge2.Value.ToString(Rows[1].Gauge2.Format);
                    Rows[1].Gauge3.Header = Rows[1].Gauge3.Value.ToString(Rows[1].Gauge3.Format);

                    Message = $"Error updating Fronius: {result.Status.Explanation}.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Rows[0].Gauge1.Header = "???";
                Rows[0].Gauge2.Header = "???";
                Rows[0].Gauge3.Header = "???";
                Rows[1].Gauge1.Header = "???";
                Rows[1].Gauge2.Header = "???";
                Rows[1].Gauge3.Header = "???";

                Message = $"Exception updating Fronius: {ex.Message}.";
                return false;
            }
        }
    }
}
