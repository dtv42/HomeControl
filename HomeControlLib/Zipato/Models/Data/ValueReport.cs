// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValueReport.cs" company="DTV-Online">
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

    public class ValueReport
    {
        public AttributeData Attribute { get; set; } = new AttributeData();
        public string Prev { get; set; }
        public string Next { get; set; }
        List<ValueReportData> Values { get; set; } = new List<ValueReportData> { };
    }
}
