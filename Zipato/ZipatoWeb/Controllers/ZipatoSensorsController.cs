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
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Swashbuckle.AspNetCore.Annotations;

    using BaseClassLib;
    using DataValueLib;
    using ZipatoLib;
    using ZipatoLib.Models;
    using ZipatoLib.Models.Sensors;
    using ZipatoWeb.Models;

    using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

    #endregion

    /// <summary>
    /// The Zipato controller for reading Zipato sensors (and meters).
    /// </summary>
    [Route("api/sensors")]
    [Produces("application/json")]
    public class ZipatoSensorsController : BaseController<AppSettings>
    {
        #region Private Fields

        private readonly IZipato _zipato;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipatoSensorsController"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application logger.</param>
        public ZipatoSensorsController(IZipato zipato,
                                       IOptions<AppSettings> options,
                                       ILogger<ZipatoSensorsController> logger)
            : base(logger, options)
        {
            _zipato = zipato;
        }

        #endregion Constructors

        #region REST Methods

        /// <summary>
        /// Returns Zipato sensor data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(ZipatoSensors), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetSensorsAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetSensorsAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_zipato.Sensors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato virtual meters data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("virtual")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(List<VirtualMeter>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetVirtualMetersAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetVirtualMetersAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_zipato.Sensors.VirtualMeters);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato virtual meter data.
        /// </summary>
        /// <param name="index">The meter index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("virtual/{index}")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(VirtualMeter), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetVirtualMeterAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetVirtualMeterAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Sensors.VirtualMeters.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Sensors.VirtualMeters.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Sensors.VirtualMeters.Count - 1))}]).");
                }

                return Ok(_zipato.Sensors.VirtualMeters[index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato virtual meter value data.
        /// </summary>
        /// <param name="index">The meter index.</param>
        /// <param name="value">The value index (1-16).</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("virtual/{index}/value{value}")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(ValueInfo<double>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetVirtualMeterValueAsync(int index, int value, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetVirtualMeterValueAsync()...");

                if ((value < 1) || (value > 16))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Value index {value} not valid ([1] - [16]).");
                }

                --value;

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Sensors.VirtualMeters.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Sensors.VirtualMeters.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Sensors.VirtualMeters.Count - 1))}]).");
                }

                return Ok(_zipato.Sensors.VirtualMeters[index].GetValue(value));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes the Zipato virtual meter value data.
        /// </summary>
        /// <param name="index">The sensor index.</param>
        /// <param name="value">The value index (1-16).</param>
        /// <param name="data">The value data.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The sensor cannot be found.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("virtual/{index}/value{value}")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(VirtualMeter), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult SetVirtualMeterValue(int index, int value, double data)
        {
            try
            {
                _logger?.LogDebug($"SetVirtualMeterValue({index}, {value})...");

                if ((index < 0) || (index >= _zipato.Sensors.VirtualMeters.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Sensors.VirtualMeters.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Sensors.VirtualMeters.Count - 1))}]).");
                }

                if ((value < 1) || (value > 16))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Value index {value} not valid ([1] - [16]).");
                }

                --value;

                var sensor = _zipato.Sensors.VirtualMeters[index];

                if (sensor.SetValue(value, data))
                {
                    return Ok(sensor);
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
        /// Writes all Zipato virtual meter value data.
        /// </summary>
        /// <param name="index">The sensor index.</param>
        /// <param name="data">The value data.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">The operation was successful.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="404">The sensor cannot be found.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpPut("virtual/{index}")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(VirtualMeter), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public IActionResult SetVirtualMeterValues(int index, [FromBody]double?[] data)
        {
            try
            {
                _logger?.LogDebug($"SetVirtualMeterValues({index}, data)", data);

                if ((index < 0) || (index >= _zipato.Sensors.VirtualMeters.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Sensors.VirtualMeters.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Sensors.VirtualMeters.Count - 1))}]).");
                }

                if ((data == null) || data.Length != 16)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Data not valid (array with 16 double?).");
                }

                var sensor = _zipato.Sensors.VirtualMeters[index];

                if (sensor.SetValues(data))
                {
                    return Ok(sensor);
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
        /// Returns Zipato consumption meters data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("meter")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(List<ConsumptionMeter>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetConsumptionMetersAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetConsumptionMetersAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_zipato.Sensors.ConsumptionMeters);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato consumption meter data.
        /// </summary>
        /// <param name="index">The meter index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("meter/{index}")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(ConsumptionMeter), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetConsumptionMeterAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetConsumptionMeterAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Sensors.ConsumptionMeters.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Sensors.ConsumptionMeters.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Sensors.ConsumptionMeters.Count - 1))}]).");
                }

                return Ok(_zipato.Sensors.ConsumptionMeters[index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato consumption meter data.
        /// </summary>
        /// <param name="index">The meter index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("meter/{index}/cummulative")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(ValueInfo<double>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetConsumptionMetersCummulativeAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetConsumptionMetersCummulativeAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Sensors.ConsumptionMeters.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Sensors.ConsumptionMeters.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Sensors.ConsumptionMeters.Count - 1))}]).");
                }

                return Ok(_zipato.Sensors.ConsumptionMeters[index].CummulativeConsumption);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato consumption meter data.
        /// </summary>
        /// <param name="index">The meter index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("meter/{index}/current")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(ValueInfo<double>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetConsumptionMetersCurrentAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetConsumptionMetersCurrentAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Sensors.ConsumptionMeters.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Sensors.ConsumptionMeters.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Sensors.ConsumptionMeters.Count - 1))}]).");
                }

                return Ok(_zipato.Sensors.ConsumptionMeters[index].CurrentConsumption);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato temperature sensors data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("temperature")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(List<TemperatureSensor>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetTemperatureSensorsAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetTemperatureSensorsAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_zipato.Sensors.TemperatureSensors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato temperature sensor data.
        /// </summary>
        /// <param name="index">The sensor index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("temperature/{index}")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(TemperatureSensor), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetTemperatureSensorAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetTemperatureSensorAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Sensors.TemperatureSensors.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Sensors.TemperatureSensors.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Sensors.TemperatureSensors.Count - 1))}]).");
                }

                return Ok(_zipato.Sensors.TemperatureSensors[index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato temperature sensor data.
        /// </summary>
        /// <param name="index">The sensor index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("temperature/{index}/value")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(ValueInfo<double>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetTemperatureSensorValueAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetTemperatureSensorValueAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Sensors.TemperatureSensors.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Sensors.TemperatureSensors.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Sensors.TemperatureSensors.Count - 1))}]).");
                }

                return Ok(_zipato.Sensors.TemperatureSensors[index].Temperature);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato humidity sensors data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("humidity")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(List<HumiditySensor>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetHumiditySensorsAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetHumiditySensorsAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_zipato.Sensors.HumiditySensors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato humidity sensor data.
        /// </summary>
        /// <param name="index">The sensor index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("humidity/{index}")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(LuminanceSensor), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetHumiditySensorAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetHumiditySensorAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Sensors.HumiditySensors.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Sensors.HumiditySensors.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Sensors.HumiditySensors.Count - 1))}]).");
                }

                return Ok(_zipato.Sensors.HumiditySensors[index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato humidity sensor data.
        /// </summary>
        /// <param name="index">The sensor index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("humidity/{index}/value")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(ValueInfo<double>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetHumiditySensorValueAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetHumiditySensorValueAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Sensors.HumiditySensors.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Sensors.HumiditySensors.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Sensors.HumiditySensors.Count - 1))}]).");
                }

                return Ok(_zipato.Sensors.HumiditySensors[index].Humidity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato luminance sensors data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("luminance")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(List<LuminanceSensor>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetLuminanceSensorsAsync(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetLuminanceSensorsAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_zipato.Sensors.LuminanceSensors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato luminance sensor data.
        /// </summary>
        /// <param name="index">The sensor index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("luminance/{index}")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(LuminanceSensor), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetLuminanceSensorAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetLuminanceSensorAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Sensors.LuminanceSensors.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Sensors.LuminanceSensors.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Sensors.LuminanceSensors.Count - 1))}]).");
                }

                return Ok(_zipato.Sensors.LuminanceSensors[index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns Zipato luminance sensor data.
        /// </summary>
        /// <param name="index">The sensor index.</param>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="400">The request has missing/invalid values.</response>
        /// <response code="500">An error or an unexpected exception occured.</response>
        /// <response code="502">The update procedure was unsuccessful.</response>
        [HttpGet("luminance/{index}/value")]
        [SwaggerOperation(Tags = new[] { "Zipato Sensor API" })]
        [ProducesResponseType(typeof(ValueInfo<double>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetLuminanceSensorValueAsync(int index, bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetLuminanceSensorValueAsync()...");

                if (update)
                {
                    var (values, status) = await _zipato.DataReadValuesAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                if ((index < 0) || (index >= _zipato.Sensors.LuminanceSensors.Count))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Index {index} not valid ({(_zipato.Sensors.LuminanceSensors.Count == 0 ? "[0]" : "[0] - [" + (_zipato.Sensors.LuminanceSensors.Count - 1))}]).");
                }

                return Ok(_zipato.Sensors.LuminanceSensors[index].Luminance);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion REST Methods
    }
}
