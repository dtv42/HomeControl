// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetatmoController.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoWeb.Controllers
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
    using NetatmoLib;
    using NetatmoLib.Models;
    using NetatmoWeb.Models;

    using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

    #endregion

    /// <summary>
    /// The Netatmo controller for reading Netatmo data items.
    /// </summary>
    [ApiController]
    [Route("api/netatmo")]
    [Produces("application/json")]
    public class NetatmoController : BaseController<AppSettings>
    {
        #region Private Fields

        private readonly INetatmo _netatmo;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NetatmoController"/> class.
        /// </summary>
        /// <param name="netatmo">The Netatmo instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application logger.</param>
        public NetatmoController(INetatmo netatmo,
                                 IOptions<AppSettings> options,
                                 ILogger<NetatmoController> logger)
            : base(logger, options)
        {
            _netatmo = netatmo;
        }

        #endregion Constructors

        #region REST Methods

        /// <summary>
        /// Returns all Netatmo related data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("all")]
        [SwaggerOperation(Tags = new[] { "Netatmo API" })]
        [ProducesResponseType(typeof(NetatmoData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetNetatmoData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetNetatmoData()...");

                if (!_netatmo.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    var status = await _netatmo.ReadAllAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_netatmo.Station);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Netatmo data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("main")]
        [SwaggerOperation(Tags = new[] { "Netatmo API" })]
        [ProducesResponseType(typeof(StationDeviceDashboard), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetMainModuleData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetMainModuleData()...");

                if (!_netatmo.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    await _netatmo.ReadAllAsync();

                    if (!_netatmo.Station.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _netatmo.Station.Status);
                    }
                }

                return Ok(_netatmo.Station.Device.DashboardData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Netatmo data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("outdoor")]
        [SwaggerOperation(Tags = new[] { "Netatmo API" })]
        [ProducesResponseType(typeof(OutdoorModuleDashboard), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetOutdoorModuleData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetOutdoorModuleData()...");

                if (!_netatmo.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    await _netatmo.ReadAllAsync();

                    if (!_netatmo.Station.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _netatmo.Station.Status);
                    }
                }

                return Ok(_netatmo.Station.Device.OutdoorModule.DashboardData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Netatmo data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("indoor1")]
        [SwaggerOperation(Tags = new[] { "Netatmo API" })]
        [ProducesResponseType(typeof(IndoorModuleDashboard), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetIndoorModule1Data(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetIndoorModule1Data()...");

                if (!_netatmo.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    await _netatmo.ReadAllAsync();

                    if (!_netatmo.Station.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _netatmo.Station.Status);
                    }
                }

                return Ok(_netatmo.Station.Device.IndoorModule1.DashboardData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Netatmo data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("indoor2")]
        [SwaggerOperation(Tags = new[] { "Netatmo API" })]
        [ProducesResponseType(typeof(IndoorModuleDashboard), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetIndoorModule2Data(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetIndoorModule2Data()...");

                if (!_netatmo.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    await _netatmo.ReadAllAsync();

                    if (!_netatmo.Station.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _netatmo.Station.Status);
                    }
                }

                return Ok(_netatmo.Station.Device.IndoorModule2.DashboardData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Netatmo data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("indoor3")]
        [SwaggerOperation(Tags = new[] { "Netatmo API" })]
        [ProducesResponseType(typeof(IndoorModuleDashboard), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetIndoorModule3Data(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetIndoorModule3Data()...");

                if (!_netatmo.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    await _netatmo.ReadAllAsync();

                    if (!_netatmo.Station.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _netatmo.Station.Status);
                    }
                }

                return Ok(_netatmo.Station.Device.IndoorModule3.DashboardData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Netatmo data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("rain")]
        [SwaggerOperation(Tags = new[] { "Netatmo API" })]
        [ProducesResponseType(typeof(RainGaugeDashboard), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetRainGaugeData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetRainGaugeData()...");

                if (!_netatmo.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    await _netatmo.ReadAllAsync();

                    if (!_netatmo.Station.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _netatmo.Station.Status);
                    }
                }

                return Ok(_netatmo.Station.Device.RainGauge.DashboardData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of Netatmo data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("wind")]
        [SwaggerOperation(Tags = new[] { "Netatmo API" })]
        [ProducesResponseType(typeof(WindGaugeDashboard), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetWindGaugeData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetWindGaugeData()...");

                if (!_netatmo.IsLocked)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                }

                if (update)
                {
                    await _netatmo.ReadAllAsync();

                    if (!_netatmo.Station.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _netatmo.Station.Status);
                    }
                }

                return Ok(_netatmo.Station.Device.WindGauge.DashboardData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a single Netatmo property.
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
        [SwaggerOperation(Tags = new[] { "Netatmo API" })]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetNetatmoData(string name, bool update = false)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger?.LogDebug($"GetNetatmoData() invalid property.");
                return StatusCode(StatusCodes.Status400BadRequest, $"Property is invalid.");
            }

            try
            {
                _logger?.LogDebug($"GetNetatmoData({name})...");

                if (Netatmo.IsProperty(name))
                {
                    if (!_netatmo.IsLocked)
                    {
                        return StatusCode(StatusCodes.Status406NotAcceptable, "Locked: update not yet finished.");
                    }

                    if (update)
                    {
                        var status = await _netatmo.ReadAllAsync();

                        if (!status.IsGood)
                        {
                            return StatusCode(StatusCodes.Status502BadGateway, status);
                        }
                    }

                    return Ok(_netatmo.GetPropertyValue(name));
                }
                else
                {
                    _logger?.LogDebug($"GetNetatmoData('{name}') property not found.");
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
