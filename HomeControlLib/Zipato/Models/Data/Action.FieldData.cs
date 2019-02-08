// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Action.FieldData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Data.Action
{
    #region Using Directives

    using System.Collections.Generic;

    using HomeControlLib.Zipato.Models.Data.Action;

    #endregion

    public class FieldData
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<CheckData> Checks { get; set; } = new List<CheckData> { };
        public bool? Readable { get; set; }
        public bool? Writable { get; set; }
        public bool? Required { get; set; }
    }
}
