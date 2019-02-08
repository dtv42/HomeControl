// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Data
{
    #region Using Directives

    using System;

    using ZipatoLib.Models.Enums;

    #endregion

    public class FileData
    {
        public string Id { get; set; }
        public int? FileSize { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ContentType { get; set; }
        public DateTime? Created { get; set; }
        public FileTypes? FileType { get; set; }
    }
}
