// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Wallbox.cs" company="DTV-Online">
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
    using HomeControlLib.Wallbox.Models;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    internal class Wallbox : ServiceData, IServiceData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public Wallbox(string address = "http://localhost:8009")
        {
            Name = "BMW Wallbox";
            Address = address;

            Rows.Add(new RowData()
            {
                Gauge1 = new GaugeData() { Title = "Power", Format = "0.000 kW", StartValue = 0, EndValue = 10 },
                Gauge2 = new GaugeData() { Title = "Charging", Format = "0.00 kWh", StartValue = 0, EndValue = 30 },
                Gauge3 = new GaugeData() { Title = "Energy", Format = "0 kWh", StartValue = 0, EndValue = 2500 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Current",
                Gauge1 = new GaugeData() { Title = "Current L1", Format = "0.00 A", StartValue = 0, EndValue = 20 },
                Gauge2 = new GaugeData() { Title = "Current L2", Format = "0.00 A", StartValue = 0, EndValue = 20 },
                Gauge3 = new GaugeData() { Title = "Current L3", Format = "0.00 A", StartValue = 0, EndValue = 20 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Voltage",
                Gauge1 = new GaugeData() { Title = "Voltage L1", Format = "0.0 V", StartValue = 0, EndValue = 250 },
                Gauge2 = new GaugeData() { Title = "Voltage L2", Format = "0.0 V", StartValue = 0, EndValue = 250 },
                Gauge3 = new GaugeData() { Title = "Voltage L3", Format = "0.0 V", StartValue = 0, EndValue = 250 }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool UpdateValues()
        {
            try
            {
                var json = _client.GetStringAsync("api/wallbox/report3").Result;
                var result = JsonConvert.DeserializeObject<Report3Data>(json);

                if (result.Status.IsGood)
                {
                    Rows[0].Gauge1.Value = result.Power;
                    Rows[0].Gauge2.Value = result.EnergyCharging;
                    Rows[0].Gauge3.Value = result.EnergyTotal;
                    Rows[1].Gauge1.Value = result.CurrentL1;
                    Rows[1].Gauge2.Value = result.CurrentL2;
                    Rows[1].Gauge3.Value = result.CurrentL3;
                    Rows[2].Gauge1.Value = result.VoltageL1N;
                    Rows[2].Gauge2.Value = result.VoltageL2N;
                    Rows[2].Gauge3.Value = result.VoltageL3N;

                    Rows[0].Gauge1.Header = Rows[0].Gauge1.Value.ToString(Rows[0].Gauge1.Format);
                    Rows[0].Gauge2.Header = Rows[0].Gauge2.Value.ToString(Rows[0].Gauge2.Format);
                    Rows[0].Gauge3.Header = Rows[0].Gauge3.Value.ToString(Rows[0].Gauge3.Format);
                    Rows[1].Gauge1.Header = Rows[1].Gauge1.Value.ToString(Rows[1].Gauge1.Format);
                    Rows[1].Gauge2.Header = Rows[1].Gauge2.Value.ToString(Rows[1].Gauge2.Format);
                    Rows[1].Gauge3.Header = Rows[1].Gauge3.Value.ToString(Rows[1].Gauge3.Format);
                    Rows[2].Gauge1.Header = Rows[2].Gauge1.Value.ToString(Rows[2].Gauge1.Format);
                    Rows[2].Gauge2.Header = Rows[2].Gauge2.Value.ToString(Rows[2].Gauge2.Format);
                    Rows[2].Gauge3.Header = Rows[2].Gauge3.Value.ToString(Rows[2].Gauge3.Format);

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
                    Rows[2].Gauge1.Value = 0;
                    Rows[2].Gauge2.Value = 0;
                    Rows[2].Gauge3.Value = 0;

                    Rows[0].Gauge1.Header = Rows[0].Gauge1.Value.ToString(Rows[0].Gauge1.Format);
                    Rows[0].Gauge2.Header = Rows[0].Gauge2.Value.ToString(Rows[0].Gauge2.Format);
                    Rows[0].Gauge3.Header = Rows[0].Gauge3.Value.ToString(Rows[0].Gauge3.Format);
                    Rows[1].Gauge1.Header = Rows[1].Gauge1.Value.ToString(Rows[1].Gauge1.Format);
                    Rows[1].Gauge2.Header = Rows[1].Gauge2.Value.ToString(Rows[1].Gauge2.Format);
                    Rows[1].Gauge3.Header = Rows[1].Gauge3.Value.ToString(Rows[1].Gauge3.Format);
                    Rows[2].Gauge1.Header = Rows[2].Gauge1.Value.ToString(Rows[2].Gauge1.Format);
                    Rows[2].Gauge2.Header = Rows[2].Gauge2.Value.ToString(Rows[2].Gauge2.Format);
                    Rows[2].Gauge3.Header = Rows[2].Gauge3.Value.ToString(Rows[2].Gauge3.Format);

                    Message = $"Error updating Wallbox: {result.Status.Explanation}.";
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
                Rows[2].Gauge1.Header = "???";
                Rows[2].Gauge2.Header = "???";
                Rows[2].Gauge3.Header = "???";

                Message = $"Exception updating Wallbox: {ex.Message}.";
                return false;
            }
        }
    }
}
