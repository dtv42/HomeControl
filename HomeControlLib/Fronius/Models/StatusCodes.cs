// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatusCodes.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Fronius.Models
{
    public enum StatusCodes
    {
        Unknown,
        Startup,
        Running,
        Standby,
        Bootloading,
        Error
    }
}
