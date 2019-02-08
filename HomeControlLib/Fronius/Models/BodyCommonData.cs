// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BodyCommonData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Fronius.Models
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public class BodyCommonData
    {
        public RawCommonData Data { get; set; } = new RawCommonData();
    }
}
