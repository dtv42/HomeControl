// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeData.cs" company="DTV-Online">
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
    using HomeControlLib.HomeData.Models;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    internal class HomeData : ServiceData, IServiceData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public HomeData(string address = "http://localhost:8008")
        {
            Name = "Home Data";
            Address = address;

            Rows.Add(new RowData()
            {
                Caption = "Power Total",
                Gauge1 = new GaugeData() { Title = "Load", Format = "0.000 kW", StartValue = 0, EndValue = 12 },
                Gauge2 = new GaugeData() { Title = "Demand", Format = "0.000 kW", StartValue = 0, EndValue = 12 },
                Gauge3 = new GaugeData() { Title = "Surplus", Format = "0.000 kW", StartValue = 0, EndValue = 12 }
            });

            Rows.Add(new RowData(){
                Caption = "Power Phase L1",
                Gauge1 = new GaugeData(){Title = "Load", Format = "0.000 kW", StartValue = 0,EndValue = 5 },
                Gauge2 = new GaugeData(){Title = "Demand", Format = "0.000 kW", StartValue = 0,EndValue = 5 },
                Gauge3 = new GaugeData(){Title = "Surplus", Format = "0.000 kW", StartValue = 0,EndValue = 5 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Power Phase L2",
                Gauge1 = new GaugeData(){Title = "Load", Format = "0.000 kW", StartValue = 0,EndValue = 5 },
                Gauge2 = new GaugeData(){Title = "Demand", Format = "0.000 kW", StartValue = 0,EndValue = 5 },                Gauge3 = new GaugeData(){Title = "Surplus", Format = "0.000 kW", StartValue = 0,EndValue = 5 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Power Phase L3 [kW]",                Gauge1 = new GaugeData(){Title = "Load", Format = "0.000 kW", StartValue = 0,EndValue = 5 },                Gauge2 = new GaugeData(){Title = "Demand", Format = "0.000 kW", StartValue = 0,EndValue = 5 },                Gauge3 = new GaugeData(){Title = "Surplus", Format = "0.000 kW", StartValue = 0,EndValue = 5 }
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
                var json = _client.GetStringAsync("api/homedata/all").Result;
                var result = JsonConvert.DeserializeObject<HomeValues>(json);

                if (result.IsGood)
                {
                    Rows[0].Gauge1.Value = result.Load;
                    Rows[0].Gauge2.Value = result.Demand;
                    Rows[0].Gauge3.Value = result.Surplus;
                    Rows[1].Gauge1.Value = result.LoadL1;
                    Rows[1].Gauge2.Value = result.DemandL1;
                    Rows[1].Gauge3.Value = result.SurplusL1;
                    Rows[2].Gauge1.Value = result.LoadL2;
                    Rows[2].Gauge2.Value = result.DemandL2;
                    Rows[2].Gauge3.Value = result.SurplusL2;
                    Rows[3].Gauge1.Value = result.LoadL3;
                    Rows[3].Gauge2.Value = result.DemandL3;
                    Rows[3].Gauge3.Value = result.SurplusL3;

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

                    Message = $"Error updating HomeData: {result.Status.Explanation}.";
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

                Message = $"Exception updating HomeData: {ex.Message}.";
                return false;
            }
        }
    }
}
