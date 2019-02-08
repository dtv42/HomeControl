// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoStatusTypes.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Enums
{
    /// <summary>
    /// The supported Zipato status types.
    /// </summary>
    public enum ZipatoStatusTypes
    {
        REGISTERED,
        WAITING_REGISTRATION,
        INACTIVE,
        FACTORY,
        REDIRECTED,
        DEAD
    }
}
