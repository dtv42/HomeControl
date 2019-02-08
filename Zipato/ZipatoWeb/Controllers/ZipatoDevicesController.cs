// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoDataController.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoWeb.Controllers
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Swashbuckle.AspNetCore.Annotations;

    using BaseClassLib;
    using DataValueLib;
    using ZipatoLib;
    using ZipatoLib.Models;
    using ZipatoLib.Models.Devices;
    using ZipatoWeb.Models;

    using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

    #endregion

    /// <summary>
    /// The Zipato controller for reading Zipato devices.
    /// </summary>
    [Route("api/devices")]
    [Produces("application/json")]
    public class ZipatoDevicesController : BaseController<AppSettings>
    {
        #region Private Fields

        private readonly IZipato _zipato;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipatoDevicesController"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application logger.</param>
        public ZipatoDevicesController(IZipato zipato,
                                       IOptions<AppSettings> options,
                                       ILogger<ZipatoDevicesController> logger)
            : base(logger, options)
        {
            _zipato = zipato;
        }

        #endregion Constructors

        #region REST Methods

        /// <summary>
        /// Returns Zipato device data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(ZipatoDevices), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetDevicesAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetDevicesAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_zipato.Devices);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato dimmer devices data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("dimmer")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(List<Dimmer>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetDimmersAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetDimmersAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_zipato.Devices.Dimmers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato dimmer device data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("dimmer/{index}")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(Dimmer), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetDimmerAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetDimmerAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Devices.Dimmers.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.Dimmers.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.Dimmers.Count - 1))}]).");
                }

                return Ok(_zipato.Devices.Dimmers[index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato dimmer state data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("dimmer/{index}/intensity")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(ValueInfo<int>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetDimmerIntensityAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetDimmerIntensityAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Devices.Dimmers.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.Dimmers.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.Dimmers.Count - 1))}]).");
                }

                return Ok(_zipato.Devices.Dimmers[index].Intensity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes the Zipato dimmer intensity value data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="value">The intensity value.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The device cannot be found.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("dimmer/{index}/intensity")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(Dimmer), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult SetDimmerIntensity(int index, int value)
        {
            try
            {
                _logger?.LogDebug($"SetDimmerIntensity({index}, {value})...");

                if ((index < 0) || (index >= _zipato.Devices.Dimmers.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.Dimmers.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.Dimmers.Count - 1))}]).");
                }

                var device = _zipato.Devices.Dimmers[index];

                if (device.SetIntensity(value))
                {
                    return Ok(device);
                }
                else
                {
                    return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato dimmer devices data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("onoff")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(List<OnOff>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetOnOffSwitchesAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetOnOffSwitchesAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_zipato.Devices.OnOffSwitches);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato onoff device data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("onoff/{index}")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(Dimmer), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetOnOffSwitchAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetOnOffSwitchAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Devices.OnOffSwitches.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.OnOffSwitches.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.OnOffSwitches.Count - 1))}]).");
                }

                return Ok(_zipato.Devices.OnOffSwitches[index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato onoff device data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("onoff/{index}/state")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(ValueInfo<bool>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetOnOffSwitchStateAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetOnOffSwitchStateAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Devices.OnOffSwitches.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.OnOffSwitches.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.OnOffSwitches.Count - 1))}]).");
                }

                return Ok(_zipato.Devices.OnOffSwitches[index].State);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes the Zipato onoff switch state data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="value">The intensity value.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The device cannot be found.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("onoff/{index}/state")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(OnOff), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult SetOnOffSwitchState(int index, bool value)
        {
            try
            {
                _logger?.LogDebug($"SetOnOffSwitchState({index}, {value})...");

                if ((index < 0) || (index >= _zipato.Devices.OnOffSwitches.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.OnOffSwitches.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.OnOffSwitches.Count - 1))}]).");
                }

                var device = _zipato.Devices.OnOffSwitches[index];

                if (device.SetState(value))
                {
                    return Ok(device);
                }
                else
                {
                    return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato wallplug devices data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("plug")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(List<Plug>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetWallplugsAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetWallplugsAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_zipato.Devices.Wallplugs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato wallplug device data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("plug/{index}")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(Plug), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetWallplugAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetWallplugAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Devices.Wallplugs.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.Wallplugs.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.Wallplugs.Count - 1))}]).");
                }

                return Ok(_zipato.Devices.Wallplugs[index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato wallplug device data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("plug/{index}/state")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(ValueInfo<bool>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetWallplugStateAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetWallplugStateAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Devices.Wallplugs.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.Wallplugs.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.Wallplugs.Count - 1))}]).");
                }

                return Ok(_zipato.Devices.Wallplugs[index].State);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes the Zipato wallplug state data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="value">The intensity value.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The device cannot be found.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("plug/{index}/state")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(Plug), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult SetWallplugState(int index, bool value)
        {
            try
            {
                _logger?.LogDebug($"SetWallplugState({index}, {value})...");

                if ((index < 0) || (index >= _zipato.Devices.Wallplugs.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.Wallplugs.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.Wallplugs.Count - 1))}]).");
                }

                var device = _zipato.Devices.Wallplugs[index];

                if (device.SetState(value))
                {
                    return Ok(device);
                }
                else
                {
                    return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato switch devices data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("switch")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(List<Switch>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetSwitchesAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetSwitchesAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_zipato.Devices.Switches);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato switch device data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("switch/{index}")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(Switch), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetSwitchAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetSwitchAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Devices.Switches.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.Switches.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.Switches.Count - 1))}]).");
                }

                return Ok(_zipato.Devices.Switches[index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato switch device data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("switch/{index}/state")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(ValueInfo<bool>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetSwitchStateAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetSwitchStateAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Devices.Switches.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.Switches.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.Switches.Count - 1))}]).");
                }

                return Ok(_zipato.Devices.Switches[index].State);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato rgb control devices data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("rgb")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(List<RGBControl>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetRGBControlsAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetRGBControlsAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_zipato.Devices.RGBControls);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato rgb control device data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("rgb/{index}")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(RGBControl), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetRGBControlAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetRGBControlAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Devices.RGBControls.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.RGBControls.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.RGBControls.Count - 1))}]).");
                }

                return Ok(_zipato.Devices.RGBControls[index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato rgb control device data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("rgb/{index}/intensity")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(ValueInfo<int>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetRGBControlIntensityAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetRGBControlIntensityAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Devices.RGBControls.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.RGBControls.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.RGBControls.Count - 1))}]).");
                }

                return Ok(_zipato.Devices.RGBControls[index].Intensity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato rgb control device data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("rgb/{index}/coldwhite")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(ValueInfo<int>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetRGBControlColdWhiteAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetRGBControlColdWhiteAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Devices.RGBControls.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.RGBControls.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.RGBControls.Count - 1))}]).");
                }

                return Ok(_zipato.Devices.RGBControls[index].ColdWhite);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato rgb control device data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("rgb/{index}/warmwhite")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(ValueInfo<int>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetRGBControlWarmWhiteAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetRGBControlAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Devices.RGBControls.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.RGBControls.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.RGBControls.Count - 1))}]).");
                }

                return Ok(_zipato.Devices.RGBControls[index].WarmWhite);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato rgb control device data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("rgb/{index}/red")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(ValueInfo<int>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetRGBControlRedAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetRGBControlRedAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Devices.RGBControls.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.RGBControls.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.RGBControls.Count - 1))}]).");
                }

                return Ok(_zipato.Devices.RGBControls[index].Red);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato rgb control device data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("rgb/{index}/green")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(ValueInfo<int>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetRGBControlGreenAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetRGBControlGreenAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Devices.RGBControls.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.RGBControls.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.RGBControls.Count - 1))}]).");
                }

                return Ok(_zipato.Devices.RGBControls[index].Green);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato rgb control device data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("rgb/{index}/blue")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(RGBControl), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetRGBControlBlueAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetRGBControlBlueAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Devices.RGBControls.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.RGBControls.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.RGBControls.Count - 1))}]).");
                }

                return Ok(_zipato.Devices.RGBControls[index].Blue);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes the Zipato rgb control intensity value data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="value">The intensity value.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The device cannot be found.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("rgb/{index}/intensity")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(RGBControl), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult SetRGBControlIntensity(int index, int value)
        {
            try
            {
                _logger?.LogDebug($"SetRGBControlIntensity({index}, {value})...");

                if ((index < 0) || (index >= _zipato.Devices.RGBControls.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.RGBControls.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.RGBControls.Count - 1))}]).");
                }

                var device = _zipato.Devices.RGBControls[index];

                if (device.SetIntensity(value))
                {
                    return Ok(device);
                }
                else
                {
                    return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes the Zipato rgb control coldwhite value data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="value">The coldwhite value.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The device cannot be found.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("rgb/{index}/coldwhite")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(RGBControl), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult SetRGBControlColdWhite(int index, int value)
        {
            try
            {
                _logger?.LogDebug($"SetRGBControlColdWhite({index}, {value})...");

                if ((index < 0) || (index >= _zipato.Devices.RGBControls.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.RGBControls.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.RGBControls.Count - 1))}]).");
                }

                var device = _zipato.Devices.RGBControls[index];

                if (device.SetColdWhite(value))
                {
                    return Ok(device);
                }
                else
                {
                    return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes the Zipato rgb control warmwhite value data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="value">The warmwhite value.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The device cannot be found.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("rgb/{index}/warmwhite")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(RGBControl), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult SetRGBControlWarmWhite(int index, int value)
        {
            try
            {
                _logger?.LogDebug($"SetRGBControlWarmWhite({index}, {value})...");

                if ((index < 0) || (index >= _zipato.Devices.RGBControls.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.RGBControls.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.RGBControls.Count - 1))}]).");
                }

                var device = _zipato.Devices.RGBControls[index];

                if (device.SetWarmWhite(value))
                {
                    return Ok(device);
                }
                else
                {
                    return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes the Zipato rgb control red value data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="value">The red value.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The device cannot be found.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("rgb/{index}/red")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(RGBControl), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult SetRGBControlRed(int index, int value)
        {
            try
            {
                _logger?.LogDebug($"SetRGBControlRed({index}, {value})...");

                if ((index < 0) || (index >= _zipato.Devices.RGBControls.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.RGBControls.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.RGBControls.Count - 1))}]).");
                }

                var device = _zipato.Devices.RGBControls[index];

                if (device.SetRed(value))
                {
                    return Ok(device);
                }
                else
                {
                    return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes the Zipato rgb control green value data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="value">The green value.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The device cannot be found.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("rgb/{index}/green")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(RGBControl), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult SetRGBControlGreen(int index, int value)
        {
            try
            {
                _logger?.LogDebug($"SetRGBControlGreen({index}, {value})...");

                if ((index < 0) || (index >= _zipato.Devices.RGBControls.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.RGBControls.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.RGBControls.Count - 1))}]).");
                }

                var device = _zipato.Devices.RGBControls[index];

                if (device.SetGreen(value))
                {
                    return Ok(device);
                }
                else
                {
                    return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes the Zipato rgb control blue value data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="value">The blue value.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The device cannot be found.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("rgb/{index}/blue")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(RGBControl), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult SetRGBControlBlue(int index, int value)
        {
            try
            {
                _logger?.LogDebug($"SetRGBControlBlue({index}, {value})...");

                if ((index < 0) || (index >= _zipato.Devices.RGBControls.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.RGBControls.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.RGBControls.Count - 1))}]).");
                }

                var device = _zipato.Devices.RGBControls[index];

                if (device.SetBlue(value))
                {
                    return Ok(device);
                }
                else
                {
                    return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes the Zipato rgb control rgb value data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="value">The RGB value (hex).</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The device cannot be found.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("rgb/{index}/rgb")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(RGBControl), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult SetRGBControlRGB(int index, string value)
        {
            try
            {
                _logger?.LogDebug($"SetRGBControlRGB({index}, {value})...");

                if ((index < 0) || (index >= _zipato.Devices.RGBControls.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.RGBControls.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.RGBControls.Count - 1))}]).");
                }

                if (!Regex.Match(value, @"^[0-9A-F]{6}$", RegexOptions.IgnoreCase).Success)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Value '{value}' not a valid HEX string.");
                }

                var device = _zipato.Devices.RGBControls[index];

                if (device.SetRGB(value))
                {
                    return Ok(device);
                }
                else
                {
                    return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes the Zipato rgb control rgbw value data.
        /// </summary>
        /// <param name="index">The device index.</param>
        /// <param name="value">The RGBW value (hex).</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The device cannot be found.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("rgb/{index}/rgbw")]
        [SwaggerOperation(Tags = new[] { "Zipato Device API" })]
        [ProducesResponseType(typeof(RGBControl), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult SetRGBControlRGBW(int index, string value)
        {
            try
            {
                _logger?.LogDebug($"SetRGBControlRGBW({index}, {value})...");

                if ((index < 0) || (index >= _zipato.Devices.RGBControls.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Devices.RGBControls.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Devices.RGBControls.Count - 1))}]).");
                }

                if (!Regex.Match(value, @"^[0-9A-F]{8}$", RegexOptions.IgnoreCase).Success)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Value '{value}' not a valid HEX string.");
                }

                var device = _zipato.Devices.RGBControls[index];

                if (device.SetRGBW(value))
                {
                    return Ok(device);
                }
                else
                {
                    return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
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
