// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IndoorModuleData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoLib.Models
{
    public class IndoorModuleData : ModuleData
    {
        #region Public Properties

        public IndoorModuleDashboard DashboardData { get; set; } = new IndoorModuleDashboard();

        #endregion

        #region Public Methods

        public static BatteryLevel GetBatteryLevel(int batterylevel)
        {
            if (batterylevel >= 6000) return BatteryLevel.Max;
            else if (batterylevel >= 5640) return BatteryLevel.Full;
            else if (batterylevel >= 5280) return BatteryLevel.High;
            else if (batterylevel >= 4920) return BatteryLevel.Medium;
            else if (batterylevel >= 4560) return BatteryLevel.Low;
            else if (batterylevel < 4560) return BatteryLevel.VeryLow;
            else return BatteryLevel.Unknown;
        }

        #endregion
    }
}
