// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Conversions.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControl2App
{
    #region Using Directives

    using static HomeControlLib.KWLEC200.Models.KWLEC200Data;

    #endregion

    public class Conversions
    {
        public static double ApplyScale(double scale, double value) => scale * value;
        public static double ApplyScale(double scale, int value) => scale * value;
        public static double ApplyScale(double scale, uint value) => scale * value;
        public static double ConvertEnum(FanLevels value) => (int)value;
        public static double ConvertAngle(double value) => value - 180.0;
    }
}
