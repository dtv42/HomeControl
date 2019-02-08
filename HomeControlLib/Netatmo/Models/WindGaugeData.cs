// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindGaugeData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Netatmo.Models
{
    public class WindGaugeData : ModuleData
    {
        #region Public Properties

        public WindGaugeDashboard DashboardData { get; set; } = new WindGaugeDashboard();

        #endregion

        #region Public Methods

        public static BatteryLevel GetBatteryLevel(int batterylevel)
        {
            if (batterylevel >= 6000) return BatteryLevel.Max;
            else if (batterylevel >= 5590) return BatteryLevel.Full;
            else if (batterylevel >= 5180) return BatteryLevel.High;
            else if (batterylevel >= 4770) return BatteryLevel.Medium;
            else if (batterylevel >= 4360) return BatteryLevel.Low;
            else if (batterylevel < 4360) return BatteryLevel.VeryLow;
            else return BatteryLevel.Unknown;
        }

        #endregion
    }
}
