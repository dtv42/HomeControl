// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SwaggerOptions.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//  Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusRTU.Swagger
{
    #region Using Directives

    using Swashbuckle.AspNetCore.Swagger;

    #endregion

    /// <summary>
    /// The Swagger option settings provide specific metadata for the Swagger .
    /// </summary>
    public class SwaggerOptionSettings
    {
        #region Public Class

        /// <summary>
        /// Helper class for Swagger groups.
        /// </summary>
        public class GroupData
        {
            #region Public Properties

            /// <summary>
            /// The Swagger group name.
            /// </summary>
            public string Name { get; set; } = string.Empty;

            /// <summary>
            /// The Swagger group description.
            /// </summary>
            public string Description { get; set; } = string.Empty;

            #endregion
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The Info object provides metadata about the API.
        /// </summary>
        public Info Info { get; set; } = new Info() { Title = "Web API", Version = "v1" };

        /// <summary>
        /// The Basic group API metadata.
        /// </summary>
        public GroupData Basic { get; set; } = new GroupData();

        /// <summary>
        /// The Single group API metadata.
        /// </summary>
        public GroupData Single { get; set; } = new GroupData();

        /// <summary>
        /// The Array group API metadata.
        /// </summary>
        public GroupData Array { get; set; } = new GroupData();

        #endregion
    }
}
