// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ETAPU11.cs" company="DTV-Online">
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
    using HomeControlLib.ETAPU11.Models;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    internal class ETAPU11 : ServiceData, IServiceData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public ETAPU11(string address = "http://localhost:8004")
        {
            Name = "ETA PU 11 Pellet Boiler";
            Address = address;

            Rows.Add(new RowData()
            {
                Caption = "Boiler",
                Gauge1 = new GaugeData() { Title = "Temperature", Format = "0.0 °C", StartValue = 0, EndValue = 100 },
                Gauge2 = new GaugeData() { Title = "Bottom", Format = "0.0 °C", StartValue = 0, EndValue = 100 },
                Gauge3 = new GaugeData() { Title = "Pressure", Format = "0.000 bar", StartValue = 0, EndValue = 3 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Boiler Data",
                Gauge1 = new GaugeData() { Title = "Fluegas", Format = "0.0", StartValue = 0, EndValue = 200 },
                Gauge2 = new GaugeData() { Title = "Fan Speed", Format = "0 rpm", StartValue = 0, EndValue = 2000 },
                Gauge3 = new GaugeData() { Title = "Residual O2", Format = @"0.0 \%", StartValue = 0, EndValue = 100 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Heating Temperatures",
                Gauge1 = new GaugeData() { Title = "Heating", Format = "0.0 °C", StartValue = 0, EndValue = 100 },
                Gauge2 = new GaugeData() { Title = "Flow", Format = "0.0 °C", StartValue = 0, EndValue = 100 },
                Gauge3 = new GaugeData() { Title = "Room", Format = "0.0 °C", StartValue = 0, EndValue = 40 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Hot Water Temperatures",
                Gauge1 = new GaugeData() { Title = "Water", Format = "0.0 °C", StartValue = 0, EndValue = 100 },
                Gauge2 = new GaugeData() { Title = "Target", Format = "0.0 °C", StartValue = 0, EndValue = 100 },
                Gauge3 = new GaugeData() { Title = "Charging", Format = "0.0 °C", StartValue = 0, EndValue = 100 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Storage Data",
                Gauge1 = new GaugeData() { Title = "Stock", Format = "0 kg", StartValue = 0, EndValue = 3000 },
                Gauge2 = new GaugeData() { Title = "Bin", Format = "0 kg", StartValue = 0, EndValue = 30 },
                Gauge3 = new GaugeData() { Title = "Motor", Format = "0 mA", StartValue = 0, EndValue = 5000 }
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
                var json = _client.GetStringAsync("api/etapu11/all").Result;
                var result = JsonConvert.DeserializeObject<ETAPU11Data>(json);

                if (result.Status.IsGood)
                {
                    Rows[0].Gauge1.Value = result.BoilerTemperature;
                    Rows[0].Gauge2.Value = result.BoilerBottom;
                    Rows[0].Gauge3.Value = result.BoilerPressure;
                    Rows[1].Gauge1.Value = result.FlueGasTemperature;
                    Rows[1].Gauge2.Value = result.DraughtFanSpeed;
                    Rows[1].Gauge3.Value = result.ResidualO2;
                    Rows[2].Gauge1.Value = result.HeatingTemperature;
                    Rows[2].Gauge2.Value = result.Flow;
                    Rows[2].Gauge3.Value = result.RoomTemperature;
                    Rows[3].Gauge1.Value = result.HotwaterTemperature;
                    Rows[3].Gauge2.Value = result.HotwaterTarget;
                    Rows[3].Gauge3.Value = result.ChargingTimesTemperature;
                    Rows[4].Gauge1.Value = result.Stock;
                    Rows[4].Gauge2.Value = result.HopperPelletBinContents / 10.0;
                    Rows[4].Gauge3.Value = result.DischargeScrewMotorCurr;

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

                    Message = $"Error updating ETAPU11: {result.Status.Explanation}.";
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

                Message = $"Exception updating ETAPU11: {ex.Message}.";
                return false;
            }
        }
    }
}
