// --------------------------------------------------------------------------------------------------------------------
// <copyright file="About.cshtml.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeDataWeb.Pages
{
    #region Using Directives

    using Microsoft.AspNetCore.Mvc.RazorPages;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class AboutModel : PageModel
    {
        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        public void OnGet()
        {
            Message = "Your application description page.";
        }

        #endregion
    }
}
