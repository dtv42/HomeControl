// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SwaggerOptions.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ETAPU11Web.Models
{
    #region Using Directives

    using Swashbuckle.AspNetCore.Swagger;

    #endregion

    /// <summary>
    /// The Swagger info data objects provides metadata about the web API.
    /// </summary>
    public class SwaggerOptions
    {
        #region Public Properties

        public Info Info { get; set; } = new Info { Title = "Web API", Version = "v1" };

        #endregion
    }
}
