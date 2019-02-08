// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataValue.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace DataValueLib
{
    #region Using Directives

    using System;

    #endregion Using Directives

    /// <summary>
    /// The data value interface.
    /// </summary>
    public interface IDataValue
    {
        #region Properties

        DataStatus Status { get; set; }
        DateTimeOffset Timestamp { get; }
        bool IsGood { get; }
        bool IsBad { get; }
        bool IsUncertain { get; }

        #endregion Properties
    }
}