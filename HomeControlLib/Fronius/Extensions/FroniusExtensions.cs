// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusExtensions.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Fronius.Extensions
{
    #region Using Directives

    using HomeControlLib.Fronius.Models;

    #endregion

    public static class FroniusExtensions
    {
        public static StatusCodes ToStatusCode(this int code)
        {
            switch (code)
            {
                case int c when (c >= 0 && c < 7):
                    return StatusCodes.Startup;
                case 7:
                    return StatusCodes.Running;
                case 8:
                    return StatusCodes.Standby;
                case 9:
                    return StatusCodes.Bootloading;
                case 10:
                    return StatusCodes.Error;
                default:
                    return StatusCodes.Unknown;
            }
        }
    }
}
