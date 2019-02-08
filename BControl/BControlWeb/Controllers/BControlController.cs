// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BControlController.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace BControlWeb.Controllers
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
    using BControlLib;
    using BControlLib.Models;
    using BControlWeb.Models;

    #endregion

    /// <summary>
    /// The BControl controller for reading BControl data items.
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BControlController : BaseController<AppSettings>
    {
        #region Private Fields

        private readonly IBControl _bcontrol;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BControlController"/> class.
        /// </summary>
        /// <param name="bcontrol">The BControl instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application logger.</param>
        public BControlController(IBControl bcontrol,
                                 IOptions<AppSettings> options,
                                 ILogger<BControlController> logger)
            : base(logger, options)
        {
            _bcontrol = bcontrol;
        }

        #endregion Constructors

        #region REST Methods

        /// <summary>
        /// Returns flag indicating that the data have been sucessfully initialized.
        /// </summary>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        [HttpGet("/api/isinitialized")]
        [SwaggerOperation(Tags = new[] { "BControl API" })]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult GetIsInitialized()
        {
            _logger?.LogDebug("GetIsInitialized()...");
            return Ok(_bcontrol?.IsInitialized);
        }

        /// <summary>
        /// Returns all BControl related data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("all")]
        [SwaggerOperation(Tags = new[] { "BControl API" })]
        [ProducesResponseType(typeof(BControlData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetBControlData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetBControlData()...");

                if (!_bcontrol.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var status = _bcontrol.ReadAll();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_bcontrol.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of BControl data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("internal")]
        [SwaggerOperation(Tags = new[] { "BControl API" })]
        [ProducesResponseType(typeof(InternalData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetInternalData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetInternalData()...");

                if (!_bcontrol.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    _bcontrol.ReadInternalData();

                    if (!_bcontrol.InternalData.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _bcontrol.InternalData.Status);
                    }
                }

                return Ok(_bcontrol.InternalData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of BControl data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("energy")]
        [SwaggerOperation(Tags = new[] { "BControl API" })]
        [ProducesResponseType(typeof(EnergyData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetEnergyData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetEnergyData()...");

                if (!_bcontrol.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    _bcontrol.ReadEnergyData();

                    if (!_bcontrol.EnergyData.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _bcontrol.InternalData.Status);
                    }
                }

                return Ok(_bcontrol.EnergyData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of BControl data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("pnp")]
        [SwaggerOperation(Tags = new[] { "BControl API" })]
        [ProducesResponseType(typeof(PnPData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetPnPData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetPnPData()...");

                if (!_bcontrol.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    _bcontrol.ReadPnPData();

                    if (!_bcontrol.PnPData.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _bcontrol.InternalData.Status);
                    }
                }

                return Ok(_bcontrol.PnPData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of BControl data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("sunspec")]
        [SwaggerOperation(Tags = new[] { "BControl API" })]
        [ProducesResponseType(typeof(SunSpecData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetSunSpecData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetSunSpecData()...");

                if (!_bcontrol.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    _bcontrol.ReadSunSpecData();

                    if (!_bcontrol.SunSpecData.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _bcontrol.InternalData.Status);
                    }
                }

                return Ok(_bcontrol.SunSpecData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a single BControl property.
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
        [SwaggerOperation(Tags = new[] { "BControl API" })]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 405)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult GetBControlData(string name, bool update = false)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger?.LogDebug($"GetBControlData() invalid property.");
                return StatusCode(StatusCodes.Status400BadRequest, $"Property is invalid.");
            }

            try
            {
                _logger?.LogDebug($"GetBControlData({name})...");

                if (BControlData.IsProperty(name))
                {
                    if (update)
                    {
                        if (BControlData.IsReadable(name))
                        {
                            if (!_bcontrol.IsInitialized)
                            {
                                return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                            }

                            var status = _bcontrol.ReadData(name);

                            if (!status.IsGood)
                            {
                                return StatusCode(StatusCodes.Status502BadGateway, status);
                            }
                        }
                        else
                        {
                            _logger?.LogDebug($"GetBControlData('{name}') property not readable.");
                            return StatusCode(StatusCodes.Status405MethodNotAllowed, $"Property '{name}' not readable.");
                        }
                    }

                    return Ok(_bcontrol.GetPropertyValue(name));
                }
                else
                {
                    _logger?.LogDebug($"GetBControlData('{name}') property not found.");
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
