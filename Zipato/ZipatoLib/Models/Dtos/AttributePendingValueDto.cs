// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttributePendingValueDto.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Dtos
{
    #region Using Directives

    using System;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class AttributePendingValueDto : AttributeValueDto
    {
        public string PendingValue { get; set; }
        public DateTime PendingTimestamp { get; set; }
    }

}
