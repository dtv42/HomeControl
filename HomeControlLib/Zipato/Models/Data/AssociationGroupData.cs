// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssociationGroupData.cs" company="DTV-Online">
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

    #endregion

    public class AssociationGroupData
    {
        public string CmdListRaw { get; set; }
        public Dictionary<string, int> DevMap { get; set; } = new Dictionary<string, int> { };
        public Dictionary<string, EpMapData> EpMap { get; set; } = new Dictionary<string, EpMapData> { };
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
   }
}
