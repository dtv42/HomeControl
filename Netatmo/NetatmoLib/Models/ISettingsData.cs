// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISettingsData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoLib.Models
{
    public interface ISettingsData : IHttpClientSettings
    {
        string User { get; set; }
        string Password { get; set; }
        string ClientID { get; set; }
        string ClientSecret { get; set; }
        string Scope { get; set; }
    }
}