// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISettingsData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlIoT.Models
{
    internal interface ISettingsData : IHttpClientSettings
    {
        string BucketKey { get; set; }
        string AccessKey { get; set; }
    }
}
