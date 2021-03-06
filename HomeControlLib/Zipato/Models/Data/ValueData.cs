﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValueData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Data
{
    #region Using Directives

    using System;

    using HomeControlLib.Zipato.Models.Dtos;

    #endregion

    public class ValueData
    {
        public Guid? Uuid { get; set; }
        public AttributeValueDto Value { get; set; }
    }
}
