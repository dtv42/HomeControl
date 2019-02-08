// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRtuClientSettings.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusLib.Models
{
    public interface IRtuClientSettings
    {
        RtuMasterData RtuMaster { get; set; }
        RtuSlaveData RtuSlave { get; set; }
    }
}
