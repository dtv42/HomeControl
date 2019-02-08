// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Rule.CodeData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Info.Rule
{
    #region Using Directives

    using System;

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// Class providing properties for Zipato rule code data.
    /// </summary>
    public class CodeData
    {
        public string Name { get; set; }
        public bool StopAfterExecution { get; set; }
        public WhenClauseData WhenClause { get; set; } = new WhenClauseData();
        public JObject[] ElementList { get; set; } = Array.Empty<JObject>();
    }
}
