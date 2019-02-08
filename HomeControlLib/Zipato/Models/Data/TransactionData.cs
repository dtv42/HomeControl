// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransactionData.cs" company="DTV-Online">
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

    public class TransactionData
    {
        public string TransactionID { get; set; }
        public string Error { get; set; }
        public List<string> Errors { get; set; } = new List<string> {};
        public bool? Success { get; set; }
    }
}
