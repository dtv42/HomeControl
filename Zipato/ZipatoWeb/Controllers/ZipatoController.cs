// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoController.cs" company="DTV-Online">
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
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Swashbuckle.AspNetCore.Annotations;

    using BaseClassLib;
    using DataValueLib;
    using ZipatoLib;
    using ZipatoLib.Models;
    using ZipatoLib.Models.Data;
    using ZipatoWeb.Models;

    using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;
    using ZipatoLib.Models.Enums;

    #endregion

    /// <summary>
    /// The Zipato controller for reading Zipato data items.
    /// </summary>
    [Route("api/zipato")]
    [Produces("application/json")]
    public class ZipatoController : BaseController<AppSettings>
    {
        #region Private Fields

        private readonly IZipato _zipato;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipatoController"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application logger.</param>
        public ZipatoController(IZipato zipato,
                                IOptions<AppSettings> options,
                                ILogger<ZipatoController> logger)
            : base(logger, options)
        {
            _zipato = zipato;
        }

        #endregion Constructors

        #region REST Methods

        /// <summary>
        /// Returns Zipato connection type.
        /// </summary>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        [HttpGet("/api/islocal")]
        [SwaggerOperation(Tags = new[] { "Zipato API" })]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult GetIsLocal()
        {
            _logger?.LogDebug("GetZipatoIsLocal()...");
            return Ok(_zipato.IsLocal);
        }

        /// <summary>
        /// Returns flag indicating that the data have been sucessfully initialized.
        /// </summary>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        [HttpGet("/api/isinitialized")]
        [SwaggerOperation(Tags = new[] { "Zipato API" })]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult GetIsInitialized()
        {
            _logger?.LogDebug("GetIsInitialized()...");
            return Ok(_zipato?.IsInitialized);
        }

        /// <summary>
        /// Returns Zipato data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("all")]
        [SwaggerOperation(Tags = new[] { "Zipato API" })]
        [ProducesResponseType(typeof(ZipatoData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetAll(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetAll()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var status = await _zipato.ReadAllAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_zipato.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato attribute values.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("values", Name = "GetValues")]
        [SwaggerOperation(Tags = new[] { "Zipato API" })]
        [ProducesResponseType(typeof(List<ValueData>), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetValues(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetValues()...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_zipato.Data.Values);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a single Zipato value.
        /// </summary>
        /// <param name="uuid">The UUID of the attribute.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The attribute cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("values/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato API" })]
        [ProducesResponseType(typeof(ValueData), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 405)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetValue(Guid uuid, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetValue({uuid})...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var (value, status) = await _zipato.DataReadValueAsync(uuid);

                    if (status.IsGood)
                    {
                        if (value != null)
                        {
                            return Ok(new ValueData() { Uuid = uuid, Value = value });
                        }
                        else
                        {
                            return NotFound($"Value for attribute with uuid '{uuid}' not found.");
                        }
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }
                else
                {
                    var value = _zipato.GetValue(uuid);

                    if (value != null)
                    {
                        return Ok(value);
                    }
                    else
                    {
                        return NotFound($"Value for attribute with uuid '{uuid}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a log of the Zipato value.
        /// </summary>
        /// <remarks>The values are alway read directly from the zipato home control.</remarks>
        /// <param name="uuid">The UUID of the attribute.</param>
        /// <param name="from">Time where the log starts.</param>
        /// <param name="until">Time where the log ends.</param>
        /// <param name="count">Number of items requested.</param>
        /// <param name="order">The sort order requested.</param>
        /// <param name="start">Start time for the paged data.</param>
        /// <param name="sticky">The sticky flag.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The attribute cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        public async Task<IActionResult> GetValueLog(Guid uuid, DateTime? from, DateTime? until, int count = 100, SortOrderTypes order = SortOrderTypes.DESC, DateTime? start = null, bool sticky = false)
        {
            try
            {
                _logger?.LogDebug($"GetValueLog({uuid}, {from}, {until}, {count}, {order}, {start}, {sticky})...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                var (report, status) = await _zipato.ReadLogAttributeAsync(uuid, from, until, count, order, start, sticky);

                if (status.IsGood)
                {
                    if (report != null)
                    {
                        return Ok(report);
                    }
                    else
                    {
                        return NotFound($"Value logs for attribute with uuid '{uuid}' not found.");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status502BadGateway, status);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes the Zipato attribute value data for an attribute.
        /// </summary>
        /// <param name="uuid">The attribute UUID.</param>
        /// <param name="value">The attribute value.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The attribute cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("values/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato API" })]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> PutValue(Guid uuid, string value)
        {
            try
            {
                _logger?.LogDebug($"PutValue({uuid}, {value})...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                var attribute = _zipato.GetAttribute(uuid);

                if (attribute?.Uuid == uuid)
                {
                    if (await _zipato.SetValueAsync(uuid, value))
                    {
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
                    }
                }
                else
                {
                    return NotFound($"Attribute with uuid '{uuid}' not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato attribute value data for a STATE attribute.
        /// </summary>
        /// <param name="uuid">The attribute UUID.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The attribute cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("states/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato API" })]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetState(Guid uuid, bool update = false)
        {
            try
            {
                _logger?.LogDebug($"GetState({uuid})...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var (value, status) = await _zipato.DataReadValueAsync(uuid);

                    if (status.IsGood)
                    {
                        return Ok(_zipato.GetState(uuid));
                    }
                    else
                    {
                        return NotFound($"State for attribute with uuid '{uuid}' not found.");
                    }
                }
                else
                {
                    var value = _zipato.GetState(uuid);

                    if (value.HasValue)
                    {
                        return Ok(value);
                    }
                    else
                    {
                        return NotFound($"State for attribute with uuid '{uuid}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes the Zipato attribute value data for a STATE attribute.
        /// </summary>
        /// <param name="uuid">The attribute UUID.</param>
        /// <param name="value">The attribute value.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The attribute cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("states/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato API" })]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> PutState(Guid uuid, bool value)
        {
            try
            {
                _logger?.LogDebug($"PutState({uuid}, {value})...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                var attribute = _zipato.GetAttribute(uuid);

                if (attribute?.Uuid == uuid)
                {
                    if (await _zipato.SetStateAsync(uuid, value))
                    {
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
                    }
                }
                else
                {
                    return NotFound($"Attribute with uuid '{uuid}' not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes the Zipato attribute value data for a RGB attribute.
        /// </summary>
        /// <param name="uuid">The attribute UUID.</param>
        /// <param name="value">The attribute value.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The attribute cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("colors/rgb/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato API" })]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> PutColorsRGB(Guid uuid, string value)
        {
            try
            {
                _logger?.LogDebug($"PutColorsRGB({uuid}, {value})...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                var attribute = _zipato.GetAttribute(uuid);

                if (attribute?.Uuid == uuid)
                {
                    if (await _zipato.SetValueAsync(uuid, value))
                    {
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
                    }
                }
                else
                {
                    return NotFound($"Attribute with uuid '{uuid}' not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes the Zipato attribute value data for a RGBW attribute.
        /// </summary>
        /// <param name="uuid">The attribute UUID.</param>
        /// <param name="value">The attribute value.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The attribute cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("colors/rgbw/{uuid}")]
        [SwaggerOperation(Tags = new[] { "Zipato API" })]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> PutColorsRGBW(Guid uuid, string value)
        {
            try
            {
                _logger?.LogDebug($"PutColorsRGBW({uuid}, {value})...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                var attribute = _zipato.GetAttribute(uuid);

                if (attribute?.Uuid == uuid)
                {
                    if (await _zipato.SetValueAsync(uuid, value))
                    {
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
                    }
                }
                else
                {
                    return NotFound($"Attribute with uuid '{uuid}' not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Runs the Zipato scene.
        /// </summary>
        /// <param name="uuid">The scene UUID.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The scene cannot be found.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("scenes/{uuid}/run")]
        [SwaggerOperation(Tags = new[] { "Zipato API" })]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> RunScene(Guid uuid)
        {
            try
            {
                _logger?.LogDebug($"RunScene({uuid})...");

                if (!_zipato.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                var scene = _zipato.GetScene(uuid);

                if (scene != null)
                {
                    var (data, status) = await _zipato.ReadSceneRunAsync(uuid);

                    if (status.IsGood)
                    {
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _zipato.Data.Status);
                    }
                }
                else
                {
                    return NotFound($"Scene with uuid '{uuid}' not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a single Zipato property.
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
        [SwaggerOperation(Tags = new[] { "Zipato API" })]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetProperty(string name, bool update = false)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger?.LogDebug($"GetData() invalid property.");
                return StatusCode(StatusCodes.Status400BadRequest, $"Property is invalid.");
            }

            try
            {
                _logger?.LogDebug($"GetData({name})...");

                if (Zipato.IsProperty(name))
                {
                    if (update)
                    {
                        if (!_zipato.IsInitialized)
                        {
                            return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                        }

                        var status = await _zipato.ReadPropertyAsync(name);

                        if (status != DataValue.Good)
                        {
                            return StatusCode(StatusCodes.Status502BadGateway, status);
                        }
                    }

                    return Ok(_zipato.GetPropertyValue(name));
                }
                else
                {
                    _logger?.LogDebug($"GetData('{name}') property not found.");
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
