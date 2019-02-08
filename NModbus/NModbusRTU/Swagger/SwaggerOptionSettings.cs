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

    public class SwaggerOptionSettings
    {
        #region Public Class

        public class GroupData
        {
            #region Public Properties

            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;

            #endregion
        }

        #endregion

        #region Public Properties

        public Info Info { get; set; } = new Info() { Title = "Web API", Version = "v1" };
        public GroupData Basic { get; set; } = new GroupData();
        public GroupData Single { get; set; } = new GroupData();
        public GroupData Array { get; set; } = new GroupData();

        #endregion
    }
}
