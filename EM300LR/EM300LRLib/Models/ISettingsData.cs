// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISettingsData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRLib.Models
{
    public interface ISettingsData : IHttpClientSettings
    {
        string Password { get; set; }
        string SerialNumber { get; set; }
    }
}