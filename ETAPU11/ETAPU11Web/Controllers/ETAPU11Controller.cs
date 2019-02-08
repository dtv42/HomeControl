// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ETAPU11Controller.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ETAPU11Web.Controllers
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Swashbuckle.AspNetCore.Annotations;

    using BaseClassLib;
    using DataValueLib;
    using ETAPU11Lib;
    using ETAPU11Lib.Models;
    using ETAPU11Web.Models;

    #endregion

    /// <summary>
    /// The ETAPU11 controller for reading ETAPU11 data items.
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ETAPU11Controller : BaseController<AppSettings>
    {
        #region Private Fields

        private readonly IETAPU11 _etapu11;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ETAPU11Controller"/> class.
        /// </summary>
        /// <param name="etapu11">The ETAPU11 instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application logger.</param>
        public ETAPU11Controller(IETAPU11 etapu11,
                                 IOptions<AppSettings> options,
                                 ILogger<ETAPU11Controller> logger)
            : base(logger, options)
        {
            _etapu11 = etapu11;
        }

        #endregion Constructors

        #region REST Methods

        /// <summary>
        /// Returns flag indicating that the data have been sucessfully initialized.
        /// </summary>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        [HttpGet("/api/isinitialized")]
        [SwaggerOperation(Tags = new[] { "ETAPU11 API" })]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult GetIsInitialized()
        {
            _logger?.LogDebug("GetIsInitialized()...");
            return Ok(_etapu11?.IsInitialized);
        }

        /// <summary>
        /// Returns all ETAPU11 related data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("all")]
        [SwaggerOperation(Tags = new[] { "ETAPU11 API" })]
        [ProducesResponseType(typeof(ETAPU11Data), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetETAPU11Data(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetETAPU11Data()...");

                if (!_etapu11.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var status = await _etapu11.ReadBlockAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_etapu11.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of ETAPU11 data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("boiler")]
        [SwaggerOperation(Tags = new[] { "ETAPU11 API" })]
        [ProducesResponseType(typeof(BoilerData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetBoilerData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetBoilerData()...");

                if (!_etapu11.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _etapu11.ReadBlockAsync();

                    if (!_etapu11.BoilerData.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _etapu11.BoilerData.Status);
                    }
                }

                return Ok(_etapu11.BoilerData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of ETAPU11 data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("hotwater")]
        [SwaggerOperation(Tags = new[] { "ETAPU11 API" })]
        [ProducesResponseType(typeof(HotwaterData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetHotwaterData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetHotwaterData()...");

                if (!_etapu11.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _etapu11.ReadBlockAsync();

                    if (!_etapu11.HotwaterData.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _etapu11.HotwaterData.Status);
                    }
                }

                return Ok(_etapu11.HotwaterData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of ETAPU11 data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("heating")]
        [SwaggerOperation(Tags = new[] { "ETAPU11 API" })]
        [ProducesResponseType(typeof(HeatingData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetHeatingData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetHeatingData()...");

                if (!_etapu11.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _etapu11.ReadBlockAsync();

                    if (!_etapu11.HeatingData.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _etapu11.HeatingData.Status);
                    }
                }

                return Ok(_etapu11.HeatingData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of ETAPU11 data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("storage")]
        [SwaggerOperation(Tags = new[] { "ETAPU11 API" })]
        [ProducesResponseType(typeof(StorageData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetStorageData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetStorageData()...");

                if (!_etapu11.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _etapu11.ReadBlockAsync();

                    if (!_etapu11.StorageData.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _etapu11.StorageData.Status);
                    }
                }

                return Ok(_etapu11.StorageData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of ETAPU11 data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("system")]
        [SwaggerOperation(Tags = new[] { "ETAPU11 API" })]
        [ProducesResponseType(typeof(SystemData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetSystemData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetSystemData()...");

                if (!_etapu11.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _etapu11.ReadBlockAsync();

                    if (!_etapu11.SystemData.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _etapu11.SystemData.Status);
                    }
                }

                return Ok(_etapu11.SystemData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a single ETAPU11 property.
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
        [SwaggerOperation(Tags = new[] { "ETAPU11 API" })]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 405)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetETAPU11Data(string name, bool update = false)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger?.LogDebug($"GetETAPU11Data() invalid property.");
                return StatusCode(StatusCodes.Status400BadRequest, $"Property is invalid.");
            }

            try
            {
                _logger?.LogDebug($"GetETAPU11Data({name})...");

                if (ETAPU11Data.IsProperty(name))
                {
                    if (update)
                    {
                        if (ETAPU11Data.IsReadable(name))
                        {
                            if (!_etapu11.IsInitialized)
                            {
                                return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                            }

                            var status = await _etapu11.ReadDataAsync(name);

                            if (status.IsGood)
                            {
                                return StatusCode(StatusCodes.Status502BadGateway, status);
                            }
                        }
                        else
                        {
                            _logger?.LogDebug($"GetETAPU11Data('{name}') property not readable.");
                            return StatusCode(StatusCodes.Status405MethodNotAllowed, $"Property '{name}' not readable.");
                        }
                    }

                    return Ok(_etapu11.GetPropertyValue(name));
                }
                else
                {
                    _logger?.LogDebug($"GetETAPU11Data('{name}') property not found.");
                    return NotFound($"Property '{name}' not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes a single ETAPU11 property.
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
        [SwaggerOperation(Tags = new[] { "ETAPU11 API" })]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 405)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> PutETAPU11Data(string name, [FromQuery] string value)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger?.LogDebug($"PutETAPU11Data({name}, {value}) invalid property.");
                return StatusCode(StatusCodes.Status400BadRequest, $"Property name is invalid.");
            }

            if (string.IsNullOrEmpty(value))
            {
                _logger?.LogDebug($"PutETAPU11Data({name}, {value}) invalid value.");
                return StatusCode(StatusCodes.Status400BadRequest, $"Property value is invalid.");
            }

            try
            {
                _logger?.LogDebug($"PutETAPU11Data({name}, {value})...");

                if (ETAPU11Data.IsProperty(name))
                {
                    if (ETAPU11Data.IsWritable(name))
                    {
                        if (!_etapu11.IsInitialized)
                        {
                            return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                        }

                        var status = await _etapu11.WriteDataAsync(name, value);

                        if (!status.IsGood)
                        {
                            return StatusCode(StatusCodes.Status502BadGateway, status);
                        }

                        return Ok();
                    }
                    else
                    {
                        _logger?.LogDebug($"PutETAPU11Data('{name}, {value}') property not writable.");
                        return StatusCode(StatusCodes.Status405MethodNotAllowed, $"Property '{name}' not writable.");
                    }
                }
                else
                {
                    _logger?.LogDebug($"PutETAPU11Data('{name}, {value}') property not found.");
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
