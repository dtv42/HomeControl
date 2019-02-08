// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.RealmData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Data.User
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using ZipatoLib.Models.Enums;

    #endregion

    public class RealmData
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
