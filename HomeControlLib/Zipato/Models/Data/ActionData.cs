// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActionData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Data
{
    #region Using Directives

    using System.Collections.Generic;

    using HomeControlLib.Zipato.Models.Data.Action;

    #endregion

    public class ActionData
    {
        public List<FieldData> Fields { get; set; } = new List<FieldData> { };
        public string Name { get; set; }
    }
}
