// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Rule.WhenClauseData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Info.Rule
{
    /// <summary>
    /// Class providing properties for Zipato rule when clause data.
    /// </summary>
    public class WhenClauseData
    {
        public string Type { get; set; }
        public PropertiesData Properties { get; set; } = new PropertiesData();
    }
}
