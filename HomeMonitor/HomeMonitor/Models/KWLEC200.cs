// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KWLEC200.cs" company="DTV-Online">
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
    using HomeControlLib.KWLEC200.Models;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    internal class KWLEC200 : ServiceData, IServiceData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public KWLEC200(string address = "http://localhost:8003")
        {
            Name = "Helios KWL EC 200 L";
            Address = address;

            Rows.Add(new RowData()
            {
                Caption = "Temperatures",
                Gauge1 = new GaugeData() { Title = "Outdoor", Format = "0.0 °C", StartValue = -20, EndValue = 40 },
                Gauge2 = new GaugeData() { Title = "Exhaust", Format = "0.0 °C", StartValue = -20, EndValue = 40 },
                Gauge3 = new GaugeData() { Title = "Extract", Format = "0.0 °C", StartValue = -20, EndValue = 40 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Temperature & Ventilation",
                Gauge1 = new GaugeData() { Title = "Supply", Format = "0.0 °C", StartValue = -20, EndValue = 40 },
                Gauge2 = new GaugeData() { Title = "Ventilation", Format = @"0.0 \%", StartValue = 0, EndValue = 100 },
                Gauge3 = new GaugeData() { Title = "Level", Format = "0", StartValue = 0, EndValue = 4 }
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
                var json = _client.GetStringAsync("api/kwlec200/all").Result;
                var result = JsonConvert.DeserializeObject<KWLEC200Data>(json);

                if (result.Status.IsGood)
                {
                    Rows[0].Gauge1.Value = result.TemperatureOutdoor;
                    Rows[0].Gauge2.Value = result.TemperatureExhaust;
                    Rows[0].Gauge3.Value = result.TemperatureExtract;
                    Rows[1].Gauge1.Value = result.TemperatureSupply;
                    Rows[1].Gauge2.Value = result.VentilationPercentage;
                    Rows[1].Gauge3.Value = (int)result.VentilationLevel;

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

                    Message = $"Error updating KWLEC200: {result.Status.Explanation}.";
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

                Message = $"Exception updating KWLEC200: {ex.Message}.";
                return false;
            }
        }
    }
}
