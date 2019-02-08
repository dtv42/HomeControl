// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SwaggerAuthorizeExtension.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//  Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusTCP.Swagger
{
    #region Using Directives

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;

    #endregion

    /// <summary>
    /// This static class contains extension methods defined for types implementing the IApplicationBuilder interface.
    /// </summary>
    public static class SwaggerAuthorizeExtensions
    {
        #region Public Class

        /// <summary>
        /// Helper class implementing an additional authorization check for Swagger pages.
        /// </summary>
        public class SwaggerAuthorizedMiddleware
        {
            #region Private Data Members

            private readonly RequestDelegate _next;

            #endregion

            #region Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="SwaggerAuthorizedMiddleware"/> class.
            /// </summary>
            /// <param name="next">The next request delegate.</param>
            public SwaggerAuthorizedMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            #endregion

            #region Public Methods

            /// <summary>
            /// This method checks if the user is authenticated (IsUserAuthenticated == true).
            /// If not authenticated the request is redirected to the default page.
            /// </summary>
            /// <param name="context">The HTTP context.</param>
            /// <returns>A task that represents the asynchronous Invoke operation.</returns>
            public async Task Invoke(HttpContext context)
            {
                if (context.Request.Path.StartsWithSegments("/swagger") &&
                    !context.User.Identity.IsAuthenticated)
                {
                    context.Response.Redirect("/Identity/Account/AccessDenied");
                    return;
                }

                await _next.Invoke(context);
            }

            #endregion
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Extension method to add the Swagger authentication check to the request pipeline.
        /// </summary>
        /// <param name="builder">The IApplicationBuilder instance.</param>
        /// <returns>The IApplicationBuilder instance.</returns>
        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerAuthorizedMiddleware>();
        }

        #endregion
    }
}
