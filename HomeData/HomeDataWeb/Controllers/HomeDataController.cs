// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusController.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeDataWeb.Controllers
{
    #region Using Directives

    using System;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Swashbuckle.AspNetCore.Annotations;

    using BaseClassLib;
    using DataValueLib;

    using HomeDataLib;
    using HomeDataLib.Models;

    using HomeDataWeb.Models;
    using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

    #endregion

    /// <summary>
    /// The Home Data controller for reading home data items.
    /// </summary>
    [Route("api/homedata")]
    [Produces("application/json")]
    public class HomeDataController : BaseController<AppSettings>
    {
        #region Private Fields

        private readonly IHomeData _homedata;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeDataController"/> class.
        /// </summary>
        /// <param name="homedata">The HomeData instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application logger.</param>
        public HomeDataController(IHomeData homedata,
                                 IOptions<AppSettings> options,
                                 ILogger<HomeDataController> logger)
            : base(logger, options)
        {
            _homedata = homedata;
        }

        #endregion Constructors

        #region REST Methods

        /// <summary>
        /// Returns flag indicating that the data have been sucessfully initialized.
        /// </summary>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        [HttpGet("/api/isinitialized")]
        [SwaggerOperation(Tags = new[] { "HomeData API" })]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult GetIsInitialized()
        {
            _logger?.LogDebug("GetIsInitialized()...");
            return Ok(_homedata?.IsInitialized);
        }

        /// <summary>
        /// Returns all Home related data.
        /// </summary>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("all")]
        [SwaggerOperation(Tags = new[] { "HomeData API" })]
        [ProducesResponseType(typeof(HomeValues), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetHomeData()
        {
            try
            {
                _logger?.LogDebug("GetHomeData()...");

                if (!_homedata.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (!_homedata.Data.IsGood)
                {
                    return StatusCode(StatusCodes.Status502BadGateway, _homedata.Data.Status);
                }

                return Ok(_homedata.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a single Home data property.
        /// </summary>
        /// <remarks>The property name is a CamelCase name.</remarks>
        /// <param name="name">The name of the property.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">If the property is invalid.</response>
        /// <response code="404">If the property cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("property/{name}")]
        [SwaggerOperation(Tags = new[] { "HomeData API" })]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetHomeData(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger?.LogDebug($"GetHomeData() invalid property.");
                return StatusCode(StatusCodes.Status400BadRequest, $"Property is invalid.");
            }

            try
            {
                _logger?.LogDebug($"GetHomeData({name})...");

                if (HomeValues.IsProperty(name))
                {
                    if (!_homedata.IsInitialized)
                    {
                        return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                    }

                    if (!_homedata.Data.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _homedata.Data.Status);
                    }

                    return Ok(_homedata.GetPropertyValue(name));
                }
                else
                {
                    _logger?.LogDebug($"GetHomeData('{name}') property not found.");
                    return NotFound($"Property '{name}' not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion REST Methods
    }
}
