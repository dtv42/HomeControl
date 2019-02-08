// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EM300LR.cs" company="DTV-Online">
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
    using HomeControlLib.EM300LR.Models;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    internal class EM300LR : ServiceData, IServiceData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public EM300LR(string address = "http://localhost:8005")
        {
            Name = "b-control EM300 LR";
            Address = address;

            Rows.Add(new RowData()
            {
                Caption = "Total Power +",
                Gauge1 = new GaugeData() { Title = "Apparent", Format = "0.000 kVA", StartValue = 0, EndValue = 8 },
                Gauge2 = new GaugeData() { Title = "Active", Format = "0.000 kW", StartValue = 0, EndValue = 8 },
                Gauge3 = new GaugeData() { Title = "Reactive", Format = "0.000 kvar", StartValue = 0, EndValue = 8 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Total Power -",
                Gauge1 = new GaugeData() { Title = "Apparent", Format = "0.000 kVA", StartValue = 0, EndValue = 8 },
                Gauge2 = new GaugeData() { Title = "Active", Format = "0.000 kW", StartValue = 0, EndValue = 8 },
                Gauge3 = new GaugeData() { Title = "Reactive", Format = "0.000 kvar", StartValue = 0, EndValue = 8 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Phase L1 Power +",
                Gauge1 = new GaugeData() { Title = "Apparent", Format = "0.000 kVA", StartValue = 0, EndValue = 8 },
                Gauge2 = new GaugeData() { Title = "Active", Format = "0.000 kW", StartValue = 0, EndValue = 8 },
                Gauge3 = new GaugeData() { Title = "Reactive", Format = "0.000 kvar", StartValue = 0, EndValue = 8 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Phase L1 Power -",
                Gauge1 = new GaugeData() { Title = "Apparent", Format = "0.000 kVA", StartValue = 0, EndValue = 8 },
                Gauge2 = new GaugeData() { Title = "Active", Format = "0.000 kW", StartValue = 0, EndValue = 8 },
                Gauge3 = new GaugeData() { Title = "Reactive", Format = "0.000 kvar", StartValue = 0, EndValue = 8 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Phase L2 Power +",
                Gauge1 = new GaugeData() { Title = "Apparent", Format = "0.000 kVA", StartValue = 0, EndValue = 8 },
                Gauge2 = new GaugeData() { Title = "Active", Format = "0.000 kW", StartValue = 0, EndValue = 8 },
                Gauge3 = new GaugeData() { Title = "Reactive", Format = "0.000 kvar", StartValue = 0, EndValue = 8 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Phase L2 Power -",
                Gauge1 = new GaugeData() { Title = "Apparent", Format = "0.000 kVA", StartValue = 0, EndValue = 8 },
                Gauge2 = new GaugeData() { Title = "Active", Format = "0.000 kW", StartValue = 0, EndValue = 8 },
                Gauge3 = new GaugeData() { Title = "Reactive", Format = "0.000 kvar", StartValue = 0, EndValue = 8 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Phase L3 Power +",
                Gauge1 = new GaugeData() { Title = "Apparent", Format = "0.000 kVA", StartValue = 0, EndValue = 8 },
                Gauge2 = new GaugeData() { Title = "Active", Format = "0.000 kW", StartValue = 0, EndValue = 8 },
                Gauge3 = new GaugeData() { Title = "Reactive", Format = "0.000 kvar", StartValue = 0, EndValue = 8 }
            });

            Rows.Add(new RowData()
            {
                Caption = "Phase L3 Power -",
                Gauge1 = new GaugeData() { Title = "Apparent", Format = "0.000 kVA", StartValue = 0, EndValue = 8 },
                Gauge2 = new GaugeData() { Title = "Active", Format = "0.000 kW", StartValue = 0, EndValue = 8 },
                Gauge3 = new GaugeData() { Title = "Reactive", Format = "0.000 kvar", StartValue = 0, EndValue = 8 }
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
                var json = _client.GetStringAsync("api/em300lr/total").Result;
                var total = JsonConvert.DeserializeObject<TotalData>(json);

                if (total.Status.IsGood)
                {
                    Rows[0].Gauge1.Value = total.ApparentPowerPlus / 1000.0;
                    Rows[0].Gauge2.Value = total.ActivePowerPlus / 1000.0;
                    Rows[0].Gauge3.Value = total.ReactivePowerPlus / 1000.0;
                    Rows[1].Gauge1.Value = total.ApparentPowerMinus / 1000.0;
                    Rows[1].Gauge2.Value = total.ActivePowerMinus / 1000.0;
                    Rows[1].Gauge3.Value = total.ReactivePowerMinus / 1000.0;

                    Rows[0].Gauge1.Header = Rows[0].Gauge1.Value.ToString(Rows[0].Gauge1.Format);
                    Rows[0].Gauge2.Header = Rows[0].Gauge2.Value.ToString(Rows[0].Gauge2.Format);
                    Rows[0].Gauge3.Header = Rows[0].Gauge3.Value.ToString(Rows[0].Gauge3.Format);
                    Rows[1].Gauge1.Header = Rows[1].Gauge1.Value.ToString(Rows[1].Gauge1.Format);
                    Rows[1].Gauge2.Header = Rows[1].Gauge2.Value.ToString(Rows[1].Gauge2.Format);
                    Rows[1].Gauge3.Header = Rows[1].Gauge3.Value.ToString(Rows[1].Gauge3.Format);
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

                    Message = $"Error updating EM300LR total: {total.Status.Explanation}.";
                    return false;
                }

                json = _client.GetStringAsync("api/em300lr/phase1").Result;
                var phase1 = JsonConvert.DeserializeObject<Phase1Data>(json);

                if (phase1.Status.IsGood)
                {
                    Rows[2].Gauge1.Value = phase1.ApparentPowerPlus / 1000.0;
                    Rows[2].Gauge2.Value = phase1.ActivePowerPlus / 1000.0;
                    Rows[2].Gauge3.Value = phase1.ReactivePowerPlus / 1000.0;
                    Rows[3].Gauge1.Value = phase1.ApparentPowerMinus / 1000.0;
                    Rows[3].Gauge2.Value = phase1.ActivePowerMinus / 1000.0;
                    Rows[3].Gauge3.Value = phase1.ReactivePowerMinus / 1000.0;

                    Rows[2].Gauge1.Header = Rows[2].Gauge1.Value.ToString(Rows[2].Gauge1.Format);
                    Rows[2].Gauge2.Header = Rows[2].Gauge2.Value.ToString(Rows[2].Gauge2.Format);
                    Rows[2].Gauge3.Header = Rows[2].Gauge3.Value.ToString(Rows[2].Gauge3.Format);
                    Rows[3].Gauge1.Header = Rows[3].Gauge1.Value.ToString(Rows[3].Gauge1.Format);
                    Rows[3].Gauge2.Header = Rows[3].Gauge2.Value.ToString(Rows[3].Gauge2.Format);
                    Rows[3].Gauge3.Header = Rows[3].Gauge3.Value.ToString(Rows[3].Gauge3.Format);
                }
                else
                {
                    Rows[2].Gauge1.Value = 0;
                    Rows[2].Gauge2.Value = 0;
                    Rows[2].Gauge3.Value = 0;
                    Rows[3].Gauge1.Value = 0;
                    Rows[3].Gauge2.Value = 0;
                    Rows[3].Gauge3.Value = 0;

                    Rows[2].Gauge1.Header = Rows[2].Gauge1.Value.ToString(Rows[2].Gauge1.Format);
                    Rows[2].Gauge2.Header = Rows[2].Gauge2.Value.ToString(Rows[2].Gauge2.Format);
                    Rows[2].Gauge3.Header = Rows[2].Gauge3.Value.ToString(Rows[2].Gauge3.Format);
                    Rows[3].Gauge1.Header = Rows[3].Gauge1.Value.ToString(Rows[3].Gauge1.Format);
                    Rows[3].Gauge2.Header = Rows[3].Gauge2.Value.ToString(Rows[3].Gauge2.Format);
                    Rows[3].Gauge3.Header = Rows[3].Gauge3.Value.ToString(Rows[3].Gauge3.Format);

                    Message = $"Error updating EM300LR phase 1: {total.Status.Explanation}.";
                    return false;
                }

                json = _client.GetStringAsync("api/em300lr/phase2").Result;
                var phase2 = JsonConvert.DeserializeObject<Phase2Data>(json);

                if (phase2.Status.IsGood)
                {
                    Rows[4].Gauge1.Value = phase2.ApparentPowerPlus / 1000.0;
                    Rows[4].Gauge2.Value = phase2.ActivePowerPlus / 1000.0;
                    Rows[4].Gauge3.Value = phase2.ReactivePowerPlus / 1000.0;
                    Rows[5].Gauge1.Value = phase2.ApparentPowerMinus / 1000.0;
                    Rows[5].Gauge2.Value = phase2.ActivePowerMinus / 1000.0;
                    Rows[5].Gauge3.Value = phase2.ReactivePowerMinus / 1000.0;

                    Rows[4].Gauge1.Header = Rows[4].Gauge1.Value.ToString(Rows[4].Gauge1.Format);
                    Rows[4].Gauge2.Header = Rows[4].Gauge2.Value.ToString(Rows[4].Gauge2.Format);
                    Rows[4].Gauge3.Header = Rows[4].Gauge3.Value.ToString(Rows[4].Gauge3.Format);
                    Rows[5].Gauge1.Header = Rows[5].Gauge1.Value.ToString(Rows[5].Gauge1.Format);
                    Rows[5].Gauge2.Header = Rows[5].Gauge2.Value.ToString(Rows[5].Gauge2.Format);
                    Rows[5].Gauge3.Header = Rows[5].Gauge3.Value.ToString(Rows[5].Gauge3.Format);
                }
                else
                {
                    Rows[4].Gauge1.Value = 0;
                    Rows[4].Gauge2.Value = 0;
                    Rows[4].Gauge3.Value = 0;
                    Rows[5].Gauge1.Value = 0;
                    Rows[5].Gauge2.Value = 0;
                    Rows[5].Gauge3.Value = 0;

                    Rows[4].Gauge1.Header = Rows[4].Gauge1.Value.ToString(Rows[4].Gauge1.Format);
                    Rows[4].Gauge2.Header = Rows[4].Gauge2.Value.ToString(Rows[4].Gauge2.Format);
                    Rows[4].Gauge3.Header = Rows[4].Gauge3.Value.ToString(Rows[4].Gauge3.Format);
                    Rows[5].Gauge1.Header = Rows[5].Gauge1.Value.ToString(Rows[5].Gauge1.Format);
                    Rows[5].Gauge2.Header = Rows[5].Gauge2.Value.ToString(Rows[5].Gauge2.Format);
                    Rows[5].Gauge3.Header = Rows[5].Gauge3.Value.ToString(Rows[5].Gauge3.Format);

                    Message = $"Error updating EM300LR phase 2: {total.Status.Explanation}.";
                    return false;
                }

                json = _client.GetStringAsync("api/em300lr/phase3").Result;
                var phase3 = JsonConvert.DeserializeObject<Phase3Data>(json);

                if (phase3.Status.IsGood)
                {
                    Rows[6].Gauge1.Value = phase3.ApparentPowerPlus / 1000.0;
                    Rows[6].Gauge2.Value = phase3.ActivePowerPlus / 1000.0;
                    Rows[6].Gauge3.Value = phase3.ReactivePowerPlus / 1000.0;
                    Rows[7].Gauge1.Value = phase3.ApparentPowerMinus / 1000.0;
                    Rows[7].Gauge2.Value = phase3.ActivePowerMinus / 1000.0;
                    Rows[7].Gauge3.Value = phase3.ReactivePowerMinus / 1000.0;

                    Rows[6].Gauge1.Header = Rows[6].Gauge1.Value.ToString(Rows[6].Gauge1.Format);
                    Rows[6].Gauge2.Header = Rows[6].Gauge2.Value.ToString(Rows[6].Gauge2.Format);
                    Rows[6].Gauge3.Header = Rows[6].Gauge3.Value.ToString(Rows[6].Gauge3.Format);
                    Rows[7].Gauge1.Header = Rows[7].Gauge1.Value.ToString(Rows[7].Gauge1.Format);
                    Rows[7].Gauge2.Header = Rows[7].Gauge2.Value.ToString(Rows[7].Gauge2.Format);
                    Rows[7].Gauge3.Header = Rows[7].Gauge3.Value.ToString(Rows[7].Gauge3.Format);

                    return true;
                }
                else
                {
                    Rows[6].Gauge1.Value = 0;
                    Rows[6].Gauge2.Value = 0;
                    Rows[6].Gauge3.Value = 0;
                    Rows[7].Gauge1.Value = 0;
                    Rows[7].Gauge2.Value = 0;
                    Rows[7].Gauge3.Value = 0;

                    Rows[6].Gauge1.Header = Rows[6].Gauge1.Value.ToString(Rows[6].Gauge1.Format);
                    Rows[6].Gauge2.Header = Rows[6].Gauge2.Value.ToString(Rows[6].Gauge2.Format);
                    Rows[6].Gauge3.Header = Rows[6].Gauge3.Value.ToString(Rows[6].Gauge3.Format);
                    Rows[7].Gauge1.Header = Rows[7].Gauge1.Value.ToString(Rows[7].Gauge1.Format);
                    Rows[7].Gauge2.Header = Rows[7].Gauge2.Value.ToString(Rows[7].Gauge2.Format);
                    Rows[7].Gauge3.Header = Rows[7].Gauge3.Value.ToString(Rows[7].Gauge3.Format);

                    Message = $"Error updating EM300LR phase 3: {total.Status.Explanation}.";
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
                Rows[7].Gauge1.Header = "???";
                Rows[7].Gauge2.Header = "???";
                Rows[7].Gauge3.Header = "???";

                Message = $"Exception updating EM300LR: {ex.Message}.";
                return false;
            }
        }
    }
}
