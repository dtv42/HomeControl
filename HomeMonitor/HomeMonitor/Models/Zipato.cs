// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zipato.cs" company="DTV-Online">
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
    using HomeControlLib.Zipato.Models;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    internal class Zipato : ServiceData, IServiceData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public Zipato(string address = "http://localhost:8007")
        {
            Name = "Zipato Home Control";
            Address = address;

            Rows.Add(new RowData()
            {
                Caption = "Plugs Power",
                Gauge1 = new GaugeData() { Title = "Wallplug 1", Format = "0 W", StartValue = 0, EndValue = 2500 },
                Gauge2 = new GaugeData() { Title = "Wallplug 2", Format = "0 W", StartValue = 0, EndValue = 2500 },
                Gauge3 = new GaugeData() { Title = "Wallplug 3", Format = "0 W", StartValue = 0, EndValue = 2500 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Plugs Power",
                Gauge1 = new GaugeData() { Title = "Wallplug 4", Format = "0 W", StartValue = 0, EndValue = 2500 },
                Gauge2 = new GaugeData() { Title = "Wallplug 5", Format = "0 W", StartValue = 0, EndValue = 2500 },
                Gauge3 = new GaugeData() { Title = "Wallplug 6", Format = "0 W", StartValue = 0, EndValue = 2500 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Plugs Power",
                Gauge1 = new GaugeData() { Title = "Wallplug 7", Format = "0 W", StartValue = 0, EndValue = 2500 },
                Gauge2 = new GaugeData() { Title = "Wallplug 8", Format = "0 W", StartValue = 0, EndValue = 2500 },
                Gauge3 = new GaugeData() { Title = "Heavy Duty", Format = "0 W", StartValue = 0, EndValue = 3500 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Temperatures",
                Gauge1 = new GaugeData() { Title = "Thermostat 1", Format = "0.0 °C", StartValue = 0, EndValue = 40 },
                Gauge2 = new GaugeData() { Title = "Thermostat 2", Format = "0.0 °C", StartValue = 0, EndValue = 40 },
                Gauge3 = new GaugeData() { Title = "Thermostat 3", Format = "0.0 °C", StartValue = 0, EndValue = 40 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Temperatures",
                Gauge1 = new GaugeData() { Title = "Thermostat 4", Format = "0.0 °C", StartValue = 0, EndValue = 40 },
                Gauge2 = new GaugeData() { Title = "Smoke Sensor", Format = "0.0 °C", StartValue = 0, EndValue = 40 },
                Gauge3 = new GaugeData() { Title = "Flood Sensor", Format = "0.0 °C", StartValue = 0, EndValue = 40 }
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
                var json = _client.GetStringAsync("api/sensors").Result;
                var result = JsonConvert.DeserializeObject<ZipatoSensors>(json);

                if (result.Status.IsGood)
                {
                    Rows[0].Gauge1.Value = result.ConsumptionMeters[0].CurrentConsumption.Value;  // Plug 1
                    Rows[0].Gauge2.Value = result.ConsumptionMeters[1].CurrentConsumption.Value;  // Plug 2
                    Rows[0].Gauge3.Value = result.ConsumptionMeters[2].CurrentConsumption.Value;  // Plug 3
                    Rows[1].Gauge1.Value = result.ConsumptionMeters[3].CurrentConsumption.Value;  // Plug 4
                    Rows[1].Gauge2.Value = result.ConsumptionMeters[4].CurrentConsumption.Value;  // Plug 5
                    Rows[1].Gauge3.Value = result.ConsumptionMeters[5].CurrentConsumption.Value;  // Plug 6
                    Rows[2].Gauge1.Value = result.ConsumptionMeters[6].CurrentConsumption.Value;  // Plug 7
                    Rows[2].Gauge2.Value = result.ConsumptionMeters[7].CurrentConsumption.Value;  // Plug 8
                    Rows[2].Gauge3.Value = result.ConsumptionMeters[8].CurrentConsumption.Value;  // Heavy Duty Switch
                    Rows[3].Gauge1.Value = result.TemperatureSensors[5].Temperature.Value;        // Thermostat 1
                    Rows[3].Gauge2.Value = result.TemperatureSensors[6].Temperature.Value;        // Thermostat 2
                    Rows[3].Gauge3.Value = result.TemperatureSensors[7].Temperature.Value;        // Thermostat 3
                    Rows[4].Gauge1.Value = result.TemperatureSensors[8].Temperature.Value;        // Thermostat 4
                    Rows[4].Gauge2.Value = result.TemperatureSensors[2].Temperature.Value;        // Smoke Sensor
                    Rows[4].Gauge3.Value = result.TemperatureSensors[4].Temperature.Value;        // Flood Sensor

                    Rows[0].Gauge1.Header = Rows[0].Gauge1.Value.ToString(Rows[0].Gauge1.Format);
                    Rows[0].Gauge2.Header = Rows[0].Gauge2.Value.ToString(Rows[0].Gauge2.Format);
                    Rows[0].Gauge3.Header = Rows[0].Gauge3.Value.ToString(Rows[0].Gauge3.Format);
                    Rows[1].Gauge1.Header = Rows[1].Gauge1.Value.ToString(Rows[1].Gauge1.Format);
                    Rows[1].Gauge2.Header = Rows[1].Gauge2.Value.ToString(Rows[1].Gauge2.Format);
                    Rows[1].Gauge3.Header = Rows[1].Gauge3.Value.ToString(Rows[1].Gauge3.Format);
                    Rows[2].Gauge1.Header = Rows[2].Gauge1.Value.ToString(Rows[2].Gauge1.Format);
                    Rows[2].Gauge2.Header = Rows[2].Gauge2.Value.ToString(Rows[2].Gauge2.Format);
                    Rows[2].Gauge3.Header = Rows[2].Gauge3.Value.ToString(Rows[2].Gauge3.Format);
                    Rows[3].Gauge1.Header = Rows[3].Gauge1.Value.ToString(Rows[3].Gauge1.Format);
                    Rows[3].Gauge2.Header = Rows[3].Gauge2.Value.ToString(Rows[3].Gauge2.Format);
                    Rows[3].Gauge3.Header = Rows[3].Gauge3.Value.ToString(Rows[3].Gauge3.Format);
                    Rows[4].Gauge1.Header = Rows[4].Gauge1.Value.ToString(Rows[4].Gauge1.Format);
                    Rows[4].Gauge2.Header = Rows[4].Gauge2.Value.ToString(Rows[4].Gauge2.Format);
                    Rows[4].Gauge3.Header = Rows[4].Gauge3.Value.ToString(Rows[4].Gauge3.Format);

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
                    Rows[3].Gauge1.Value = 0;
                    Rows[3].Gauge2.Value = 0;
                    Rows[3].Gauge3.Value = 0;
                    Rows[4].Gauge1.Value = 0;
                    Rows[4].Gauge2.Value = 0;
                    Rows[4].Gauge3.Value = 0;

                    Rows[0].Gauge1.Header = Rows[0].Gauge1.Value.ToString(Rows[0].Gauge1.Format);
                    Rows[0].Gauge2.Header = Rows[0].Gauge2.Value.ToString(Rows[0].Gauge2.Format);
                    Rows[0].Gauge3.Header = Rows[0].Gauge3.Value.ToString(Rows[0].Gauge3.Format);
                    Rows[1].Gauge1.Header = Rows[1].Gauge1.Value.ToString(Rows[1].Gauge1.Format);
                    Rows[1].Gauge2.Header = Rows[1].Gauge2.Value.ToString(Rows[1].Gauge2.Format);
                    Rows[1].Gauge3.Header = Rows[1].Gauge3.Value.ToString(Rows[1].Gauge3.Format);
                    Rows[2].Gauge1.Header = Rows[2].Gauge1.Value.ToString(Rows[2].Gauge1.Format);
                    Rows[2].Gauge2.Header = Rows[2].Gauge2.Value.ToString(Rows[2].Gauge2.Format);
                    Rows[2].Gauge3.Header = Rows[2].Gauge3.Value.ToString(Rows[2].Gauge3.Format);
                    Rows[3].Gauge1.Header = Rows[3].Gauge1.Value.ToString(Rows[3].Gauge1.Format);
                    Rows[3].Gauge2.Header = Rows[3].Gauge2.Value.ToString(Rows[3].Gauge2.Format);
                    Rows[3].Gauge3.Header = Rows[3].Gauge3.Value.ToString(Rows[3].Gauge3.Format);
                    Rows[4].Gauge1.Header = Rows[4].Gauge1.Value.ToString(Rows[4].Gauge1.Format);
                    Rows[4].Gauge2.Header = Rows[4].Gauge2.Value.ToString(Rows[4].Gauge2.Format);
                    Rows[4].Gauge3.Header = Rows[4].Gauge3.Value.ToString(Rows[4].Gauge3.Format);

                    Message = $"Error updating Zipato: {result.Status.Explanation}.";
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
                Rows[3].Gauge1.Header = "???";
                Rows[3].Gauge2.Header = "???";
                Rows[3].Gauge3.Header = "???";
                Rows[4].Gauge1.Header = "???";
                Rows[4].Gauge2.Header = "???";
                Rows[4].Gauge3.Header = "???";

                Message = $"Exception updating Zipato: {ex.Message}.";
                return false;
            }
        }
    }
}
