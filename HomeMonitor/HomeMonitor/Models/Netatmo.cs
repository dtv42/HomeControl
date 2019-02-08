// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Netatmo.cs" company="DTV-Online">
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
    using HomeControlLib.Netatmo.Models;

    #endregion

    internal class Netatmo : ServiceData, IServiceData
    {
        public Netatmo(string address = "http://localhost:8002")
        {
            Name = "Netatmo Weather Station";
            Address = address;

            Rows.Add(new RowData()
            {
                Caption = "Outdoor",
                Gauge1 = new GaugeData() { Title = "Temperature", Format = "0.0 °C", StartValue = -20, EndValue = 40 },
                Gauge2 = new GaugeData() { Title = "Humidity", Format = @"0.0 \%", StartValue = 0, EndValue = 100 },
                Gauge3 = new GaugeData() { Title = "Pressure", Format = "0000 mbar", StartValue = 0, EndValue = 1200 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Indoor",
                Gauge1 = new GaugeData() { Title = "Temperature", Format = "0.0 °C", StartValue = 0, EndValue = 40 },
                Gauge2 = new GaugeData() { Title = "Humidity", Format = @"0.0 \%", StartValue = 0, EndValue = 100 },
                Gauge3 = new GaugeData() { Title = "CO2", Format = "0 ppm", StartValue = 0, EndValue = 2000 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Indoor 1",
                Gauge1 = new GaugeData() { Title = "Temperature", Format = "0.0 °C", StartValue = 0, EndValue = 40 },
                Gauge2 = new GaugeData() { Title = "Humidity", Format = @"0.0 \%", StartValue = 0, EndValue = 100 },
                Gauge3 = new GaugeData() { Title = "CO2", Format = "0 ppm", StartValue = 0, EndValue = 2000 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Indoor 2",
                Gauge1 = new GaugeData() { Title = "Temperature", Format = "0.0 °C", StartValue = 0, EndValue = 40 },
                Gauge2 = new GaugeData() { Title = "Humidity", Format = @"0.0 \%", StartValue = 0, EndValue = 100 },
                Gauge3 = new GaugeData() { Title = "CO2", Format = "0 ppm", StartValue = 0, EndValue = 2000 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Indoor 3",
                Gauge1 = new GaugeData() { Title = "Temperature", Format = "0.0 °C", StartValue = 0, EndValue = 40 },
                Gauge2 = new GaugeData() { Title = "Humidity", Format = @"0.0 \%", StartValue = 0, EndValue = 100 },
                Gauge3 = new GaugeData() { Title = "CO2", Format = "0 ppm", StartValue = 0, EndValue = 2000 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Rain Gauge",
                Gauge1 = new GaugeData() { Title = "Rain", Format = "0.0 mm", StartValue = 0, EndValue = 20 },
                Gauge2 = new GaugeData() { Title = "Sum 1h", Format = "0.0 mm", StartValue = 0, EndValue = 20 },
                Gauge3 = new GaugeData() { Title = "Sum 24h", Format = "0.0 mm", StartValue = 0, EndValue = 100 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Wind Gauge",
                Gauge1 = new GaugeData() { Title = "Wind", Format = "0.0 km/h", StartValue = 0, EndValue = 100 },
                Gauge2 = new GaugeData() { Title = "Gust", Format = "0.0 km/h", StartValue = 0, EndValue = 100 },
                Gauge3 = new GaugeData() { Title = "Max.", Format = "0.0 km/h", StartValue = 0, EndValue = 100 }
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
                var json = _client.GetStringAsync("api/netatmo/all").Result;
                var result = JsonConvert.DeserializeObject<NetatmoData>(json);

                if (result.Status.IsGood)
                {
                    Rows[0].Gauge1.Value = result.Device.OutdoorModule.DashboardData.Temperature;
                    Rows[0].Gauge2.Value = result.Device.OutdoorModule.DashboardData.Humidity;
                    Rows[0].Gauge3.Value = result.Device.DashboardData.Pressure;
                    Rows[1].Gauge1.Value = result.Device.DashboardData.Temperature;
                    Rows[1].Gauge2.Value = result.Device.DashboardData.Humidity;
                    Rows[1].Gauge3.Value = result.Device.DashboardData.CO2;
                    Rows[2].Gauge1.Value = result.Device.IndoorModule1.DashboardData.Temperature;
                    Rows[2].Gauge2.Value = result.Device.IndoorModule1.DashboardData.Humidity;
                    Rows[2].Gauge3.Value = result.Device.IndoorModule1.DashboardData.CO2;
                    Rows[3].Gauge1.Value = result.Device.IndoorModule2.DashboardData.Temperature;
                    Rows[3].Gauge2.Value = result.Device.IndoorModule2.DashboardData.Humidity;
                    Rows[3].Gauge3.Value = result.Device.IndoorModule2.DashboardData.CO2;
                    Rows[4].Gauge1.Value = result.Device.IndoorModule3.DashboardData.Temperature;
                    Rows[4].Gauge2.Value = result.Device.IndoorModule3.DashboardData.Humidity;
                    Rows[4].Gauge3.Value = result.Device.IndoorModule3.DashboardData.CO2;
                    Rows[5].Gauge1.Value = result.Device.RainGauge.DashboardData.Rain;
                    Rows[5].Gauge2.Value = result.Device.RainGauge.DashboardData.SumRain1;
                    Rows[5].Gauge3.Value = result.Device.RainGauge.DashboardData.SumRain24;
                    Rows[6].Gauge1.Value = result.Device.WindGauge.DashboardData.WindStrength;
                    Rows[6].Gauge2.Value = result.Device.WindGauge.DashboardData.GustStrength;
                    Rows[6].Gauge3.Value = result.Device.WindGauge.DashboardData.MaxWindStrength;

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
                    Rows[5].Gauge1.Header = Rows[5].Gauge1.Value.ToString(Rows[5].Gauge1.Format);
                    Rows[5].Gauge2.Header = Rows[5].Gauge2.Value.ToString(Rows[5].Gauge2.Format);
                    Rows[5].Gauge3.Header = Rows[5].Gauge3.Value.ToString(Rows[5].Gauge3.Format);
                    Rows[6].Gauge1.Header = Rows[6].Gauge1.Value.ToString(Rows[6].Gauge1.Format);
                    Rows[6].Gauge2.Header = Rows[6].Gauge2.Value.ToString(Rows[6].Gauge2.Format);
                    Rows[6].Gauge3.Header = Rows[6].Gauge3.Value.ToString(Rows[6].Gauge3.Format);

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
                    Rows[5].Gauge1.Value = 0;
                    Rows[5].Gauge2.Value = 0;
                    Rows[5].Gauge3.Value = 0;
                    Rows[6].Gauge1.Value = 0;
                    Rows[6].Gauge2.Value = 0;
                    Rows[6].Gauge3.Value = 0;

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
                    Rows[5].Gauge1.Header = Rows[5].Gauge1.Value.ToString(Rows[5].Gauge1.Format);
                    Rows[5].Gauge2.Header = Rows[5].Gauge2.Value.ToString(Rows[5].Gauge2.Format);
                    Rows[5].Gauge3.Header = Rows[5].Gauge3.Value.ToString(Rows[5].Gauge3.Format);
                    Rows[6].Gauge1.Header = Rows[6].Gauge1.Value.ToString(Rows[6].Gauge1.Format);
                    Rows[6].Gauge2.Header = Rows[6].Gauge2.Value.ToString(Rows[6].Gauge2.Format);
                    Rows[6].Gauge3.Header = Rows[6].Gauge3.Value.ToString(Rows[6].Gauge3.Format);

                    Message = $"Error updating Netatmo: {result.Status.Explanation}.";
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
                Rows[5].Gauge1.Header = "???";
                Rows[5].Gauge2.Header = "???";
                Rows[5].Gauge3.Header = "???";
                Rows[6].Gauge1.Header = "???";
                Rows[6].Gauge2.Header = "???";
                Rows[6].Gauge3.Header = "???";

                Message = $"Exception updating Netatmo: {ex.Message}.";
                return false;
            }
        }
    }
}
