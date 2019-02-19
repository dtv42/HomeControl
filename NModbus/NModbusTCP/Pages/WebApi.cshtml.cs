// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApi.cshtml.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusTCP.Pages
{
    #region Using Directives

    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Authorization;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class WebApiModel : PageModel
    {
        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        public void OnGet()
        {
        }

        #endregion
    }
}
