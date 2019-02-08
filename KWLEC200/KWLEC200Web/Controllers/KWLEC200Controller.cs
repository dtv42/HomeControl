// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KWLEC200Controller.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200Web.Controllers
{
    #region Using Directives

    using System;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Swashbuckle.AspNetCore.Annotations;

    using BaseClassLib;
    using DataValueLib;
    using KWLEC200Lib;
    using KWLEC200Lib.Models;
    using KWLEC200Web.Models;

    #endregion

    /// <summary>
    /// The KWLEC200 controller for reading KWLEC200 data items.
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class KWLEC200Controller : BaseController<AppSettings>
    {
        #region Private Fields

        private readonly IKWLEC200 _kwlec200;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="KWLEC200Controller"/> class.
        /// </summary>
        /// <param name="kwlec200">The KWLEC200 instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application logger.</param>
        public KWLEC200Controller(IKWLEC200 kwlec200,
                                 IOptions<AppSettings> options,
                                 ILogger<KWLEC200Controller> logger)
            : base(logger, options)
        {
            _kwlec200 = kwlec200;
        }

        #endregion Constructors

        #region REST Methods

        /// <summary>
        /// Returns flag indicating that the data have been sucessfully initialized.
        /// </summary>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        [HttpGet("/api/isinitialized")]
        [SwaggerOperation(Tags = new[] { "KWLEC200 API" })]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult GetIsInitialized()
        {
            _logger?.LogDebug("GetIsInitialized()...");
            return Ok(_kwlec200?.IsInitialized);
        }

        /// <summary>
        /// Returns all KWLEC200 related data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("all")]
        [SwaggerOperation(Tags = new[] { "KWLEC200 API" })]
        [ProducesResponseType(typeof(KWLEC200Data), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetKWLEC200Data(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetKWLEC200Data()...");

                if (!_kwlec200.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var status = _kwlec200.ReadAll();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_kwlec200.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of KWLEC200 data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("overview")]
        [SwaggerOperation(Tags = new[] { "KWLEC200 API" })]
        [ProducesResponseType(typeof(OverviewData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetOverviewData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetOverviewData()...");

                if (!_kwlec200.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    _kwlec200.ReadOverviewData();

                    if (!_kwlec200.OverviewData.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _kwlec200.OverviewData.Status);
                    }
                }

                return Ok(_kwlec200.OverviewData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a single KWLEC200 property.
        /// </summary>
        /// <remarks>The property name is a CamelCase name.</remarks>
        /// <param name="name">The name of the property.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">If the property is invalid.</response>
        /// <response code="404">If the property cannot be found.</response>
        /// <response code="405">If the property is not readable.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("property/{name}")]
        [SwaggerOperation(Tags = new[] { "KWLEC200 API" })]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 405)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetKWLEC200Data(string name, bool update = false)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger?.LogDebug($"GetKWLEC200Data() invalid property.");
                return StatusCode(StatusCodes.Status400BadRequest, $"Property is invalid.");
            }

            try
            {
                _logger?.LogDebug($"GetKWLEC200Data({name})...");

                if (KWLEC200Data.IsProperty(name))
                {
                    if (update)
                    {
                        if (KWLEC200Data.IsReadable(name))
                        {
                            if (!_kwlec200.IsInitialized)
                            {
                                return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                            }

                            var status = _kwlec200.ReadData(name);

                            if (!status.IsGood)
                            {
                                return StatusCode(StatusCodes.Status502BadGateway, status);
                            }
                        }
                        else
                        {
                            _logger?.LogDebug($"GetKWLEC200Data('{name}') property not readable.");
                            return StatusCode(StatusCodes.Status405MethodNotAllowed, $"Property '{name}' not readable.");
                        }
                    }

                    return Ok(_kwlec200.GetPropertyValue(name));
                }
                else
                {
                    _logger?.LogDebug($"GetKWLEC200Data('{name}') property not found.");
                    return NotFound($"Property '{name}' not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes a single KWLEC200 property.
        /// </summary>
        /// <remarks>The property name is a CamelCase name.</remarks>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The value of the property.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Indicates successful operation.</response>
        /// <response code="400">If the property is invalid.</response>
        /// <response code="404">If the property cannot be found.</response>
        /// <response code="405">If the property is not writable.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the write procedure was unsuccessful.</response>
        [HttpPut("property/{name}")]
        [SwaggerOperation(Tags = new[] { "KWLEC200 API" })]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 405)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult PutKWLEC200Data(string name, [FromQuery] string value)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger?.LogDebug($"PutKWLEC200Data({name}, {value}) invalid property.");
                return StatusCode(StatusCodes.Status400BadRequest, $"Property name is invalid.");
            }

            if (string.IsNullOrEmpty(value))
            {
                _logger?.LogDebug($"PutKWLEC200Data({name}, {value}) invalid value.");
                return StatusCode(StatusCodes.Status400BadRequest, $"Property value is invalid.");
            }

            try
            {
                _logger?.LogDebug($"PutKWLEC200Data({name}, {value})...");

                if (KWLEC200Data.IsProperty(name))
                {
                    if (KWLEC200Data.IsWritable(name))
                    {
                        if (!_kwlec200.IsInitialized)
                        {
                            return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                        }

                        var status = _kwlec200.WriteData(name, value);

                        if (!status.IsGood)
                        {
                            return StatusCode(StatusCodes.Status502BadGateway, status);
                        }

                        return Ok();
                    }
                    else
                    {
                        _logger?.LogDebug($"PutKWLEC200Data('{name}, {value}') property not writable.");
                        return StatusCode(StatusCodes.Status405MethodNotAllowed, $"Property '{name}' not writable.");
                    }
                }
                else
                {
                    _logger?.LogDebug($"PutKWLEC200Data('{name}, {value}') property not found.");
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
