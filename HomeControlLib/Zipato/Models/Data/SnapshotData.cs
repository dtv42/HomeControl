// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SnapshotData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Data
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    public class SnapshotData
    {
        public string ControllerSerial { get; set; }
        public Guid? Uuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; } = new List<string> { };
        public bool? Full { get; set; }
        public bool? Valid { get; set; }
        public string User { get; set; }
        public DateTime? Created { get; set; }
        public bool? Restored { get; set; }
        public string RestoreTransaction { get; set; }
        public int? Networks { get; set; }
        public int? Devices { get; set; }
        public int? Rules { get; set; }
        public int? Users { get; set; }
        public DateTime? Completed { get; set; }
    }
}

