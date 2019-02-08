// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RuleInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Info
{
    #region Using Directives

    using ZipatoLib.Models.Data;
    using ZipatoLib.Models.Info.Rule;

    #endregion

    public class RuleInfo : RuleData
    {
        public CodeData Code { get; set; }
    }
}
