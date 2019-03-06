// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHttpClientSettings.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlIoT.Models
{
    internal interface IHttpClientSettings
    {
        string BaseAddress { get; set; }
        int Timeout { get; set; }
        int Retries { get; set; }
    }
}
