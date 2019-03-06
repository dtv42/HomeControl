// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusController.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusWeb.Controllers
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Swashbuckle.AspNetCore.Annotations;

    using BaseClassLib;
    using DataValueLib;
    using FroniusLib;
    using FroniusLib.Models;
    using FroniusWeb.Models;

    using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

    #endregion

    /// <summary>
    /// The Fronius controller for reading Fronius data items.
    /// </summary>
    [ApiController]
    [Route("api/fronius")]
    [Produces("application/json")]
    public class FroniusController : BaseController<AppSettings>
    {
        #region Private Fields

        private readonly IFronius _fronius;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FroniusController"/> class.
        /// </summary>
        /// <param name="fronius">The Fronius instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application logger.</param>
        public FroniusController(IFronius fronius,
                                 IOptions<AppSettings> options,
                                 ILogger<FroniusController> logger)
            : base(logger, options)
        {
            _fronius = fronius;
        }

        #endregion Constructors

        #region REST Methods

        /// <summary>
        /// Returns all Fronius related data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("all")]
        [SwaggerOperation(Tags = new[] { "Fronius API" })]
        [ProducesResponseType(typeof(FroniusData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetFroniusData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetFroniusData()...");

                if (!_fronius.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var status = await _fronius.ReadAllAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_fronius.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Fronius data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("common")]
        [SwaggerOperation(Tags = new[] { "Fronius API" })]
        [ProducesResponseType(typeof(CommonData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetCommonData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetCommonData()...");

                if (!_fronius.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    await _fronius.ReadCommonDataAsync();

                    if (!_fronius.CommonData.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _fronius.CommonData.Status);
                    }
                }

                return Ok(_fronius.CommonData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Fronius data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("inverter")]
        [SwaggerOperation(Tags = new[] { "Fronius API" })]
        [ProducesResponseType(typeof(InverterInfo), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetInverterInfo(bool update = false)
        {
            try
            {
                _logger?.LogDebug("InverterInfo()...");

                if (!_fronius.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    await _fronius.ReadInverterInfoAsync();

                    if (!_fronius.InverterInfo.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _fronius.InverterInfo.Status);
                    }
                }

                return Ok(_fronius.InverterInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Fronius data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("logger")]
        [SwaggerOperation(Tags = new[] { "Fronius API" })]
        [ProducesResponseType(typeof(LoggerInfo), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetLoggerInfo(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetLoggerInfo()...");

                if (!_fronius.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    await _fronius.ReadLoggerInfoAsync();

                    if (!_fronius.LoggerInfo.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _fronius.LoggerInfo.Status);
                    }
                }

                return Ok(_fronius.LoggerInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Fronius data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("minmax")]
        [SwaggerOperation(Tags = new[] { "Fronius API" })]
        [ProducesResponseType(typeof(MinMaxData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetMinMaxData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetMinMaxData()...");

                if (!_fronius.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    await _fronius.ReadMinMaxDataAsync();

                    if (!_fronius.MinMaxData.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _fronius.MinMaxData.Status);
                    }
                }

                return Ok(_fronius.MinMaxData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Fronius data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("phase")]
        [SwaggerOperation(Tags = new[] { "Fronius API" })]
        [ProducesResponseType(typeof(PhaseData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetPhaseData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetPhaseData()...");

                if (!_fronius.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    await _fronius.ReadPhaseDataAsync();

                    if (!_fronius.PhaseData.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _fronius.PhaseData.Status);
                    }
                }

                return Ok(_fronius.PhaseData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a single Fronius property.
        /// </summary>
        /// <remarks>The property name is a CamelCase name.</remarks>
        /// <param name="name">The name of the property.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">If the property is invalid.</response>
        /// <response code="404">If the property cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("property/{name}")]
        [SwaggerOperation(Tags = new[] { "Fronius API" })]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetFroniusData(string name, bool update = false)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger?.LogDebug($"GetFroniusData() invalid property.");
                return StatusCode(StatusCodes.Status400BadRequest, $"Property is invalid.");
            }

            try
            {
                _logger?.LogDebug($"GetFroniusData({name})...");

                if (Fronius.IsProperty(name))
                {
                    if (!_fronius.IsLocked)
                    {
                        return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                    }

                    if (update)
                    {
                        var status = await _fronius.ReadPropertyAsync(name);

                        if (!status.IsGood)
                        {
                            return StatusCode(StatusCodes.Status502BadGateway, status);
                        }
                    }

                    return Ok(_fronius.GetPropertyValue(name));
                }
                else
                {
                    _logger?.LogDebug($"GetFroniusData('{name}') property not found.");
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
