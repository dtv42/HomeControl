// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SYMO823MController.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MWeb.Controllers
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
    using SYMO823MLib;
    using SYMO823MLib.Models;
    using SYMO823MWeb.Models;

    #endregion

    /// <summary>
    /// The SYMO823M controller for reading SYMO823M data items.
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SYMO823MController : BaseController<AppSettings>
    {
        #region Private Fields

        private readonly ISYMO823M _symo823m;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SYMO823MController"/> class.
        /// </summary>
        /// <param name="symo823m">The SYMO823M instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application logger.</param>
        public SYMO823MController(ISYMO823M symo823m,
                                 IOptions<AppSettings> options,
                                 ILogger<SYMO823MController> logger)
            : base(logger, options)
        {
            _symo823m = symo823m;
        }

        #endregion Constructors

        #region REST Methods

        /// <summary>
        /// Returns flag indicating that the data have been sucessfully initialized.
        /// </summary>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        [HttpGet("/api/isinitialized")]
        [SwaggerOperation(Tags = new[] { "SYMO823M API" })]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult GetIsInitialized()
        {
            _logger?.LogDebug("GetIsInitialized()...");
            return Ok(_symo823m?.IsInitialized);
        }

        /// <summary>
        /// Returns all SYMO823M related data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("all")]
        [SwaggerOperation(Tags = new[] { "SYMO823M API" })]
        [ProducesResponseType(typeof(SYMO823MData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetSYMO823MData(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetSYMO823MData()...");

                if (!_symo823m.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    var status = await _symo823m.ReadBlockAsync();

                    if (!status.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, status);
                    }
                }

                return Ok(_symo823m.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of SYMO823M data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("common")]
        [SwaggerOperation(Tags = new[] { "SYMO823M API" })]
        [ProducesResponseType(typeof(CommonModelData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetCommonModel(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetCommonModel()...");

                if (!_symo823m.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _symo823m.ReadBlockAsync();

                    if (!_symo823m.CommonModel.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _symo823m.CommonModel.Status);
                    }
                }

                return Ok(_symo823m.CommonModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of SYMO823M data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("inverter")]
        [SwaggerOperation(Tags = new[] { "SYMO823M API" })]
        [ProducesResponseType(typeof(InverterModelData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetInverterModel(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetInverterModel()...");

                if (!_symo823m.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _symo823m.ReadBlockAsync();

                    if (!_symo823m.InverterModel.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _symo823m.InverterModel.Status);
                    }
                }

                return Ok(_symo823m.InverterModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of SYMO823M data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("nameplate")]
        [SwaggerOperation(Tags = new[] { "SYMO823M API" })]
        [ProducesResponseType(typeof(NameplateModelData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetNameplateModel(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetNameplateModel()...");

                if (!_symo823m.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _symo823m.ReadBlockAsync();

                    if (!_symo823m.NameplateModel.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _symo823m.NameplateModel.Status);
                    }
                }

                return Ok(_symo823m.NameplateModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of SYMO823M data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("settings")]
        [SwaggerOperation(Tags = new[] { "SYMO823M API" })]
        [ProducesResponseType(typeof(SettingsModelData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetSettingsModel(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetSettingsModel()...");

                if (!_symo823m.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _symo823m.ReadBlockAsync();

                    if (!_symo823m.SettingsModel.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _symo823m.SettingsModel.Status);
                    }
                }

                return Ok(_symo823m.SettingsModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of SYMO823M data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("extended")]
        [SwaggerOperation(Tags = new[] { "SYMO823M API" })]
        [ProducesResponseType(typeof(ExtendedModelData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetExtendedModel(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetExtendedModel()...");

                if (!_symo823m.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _symo823m.ReadBlockAsync();

                    if (!_symo823m.ExtendedModel.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _symo823m.ExtendedModel.Status);
                    }
                }

                return Ok(_symo823m.ExtendedModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of SYMO823M data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("control")]
        [SwaggerOperation(Tags = new[] { "SYMO823M API" })]
        [ProducesResponseType(typeof(ControlModelData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetControlModel(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetControlModel...");

                if (!_symo823m.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _symo823m.ReadBlockAsync();

                    if (!_symo823m.ControlModel.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _symo823m.ControlModel.Status);
                    }
                }

                return Ok(_symo823m.ControlModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of SYMO823M data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("multiple")]
        [SwaggerOperation(Tags = new[] { "SYMO823M API" })]
        [ProducesResponseType(typeof(MultipleModelData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetMultipleModel(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetMultipleModel...");

                if (!_symo823m.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _symo823m.ReadBlockAsync();

                    if (!_symo823m.MultipleModel.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _symo823m.MultipleModel.Status);
                    }
                }

                return Ok(_symo823m.MultipleModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a subset of SYMO823M data.
        /// </summary>
        /// <param name="update">Indicates if an update is requested.</param>
        /// <returns>The action method result.</returns>
        /// <response code="200">Returns the requested data.</response>
        /// <response code="406">An internal update is still in progress.</response>
        /// <response code="500">If an error or an unexpected exception occurs.</response>
        /// <response code="502">If the update procedure was unsuccessful.</response>
        [HttpGet("fronius")]
        [SwaggerOperation(Tags = new[] { "SYMO823M API" })]
        [ProducesResponseType(typeof(FroniusRegisterData), 200)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetFroniusRegister(bool update = false)
        {
            try
            {
                _logger?.LogDebug("GetFroniusRegister...");

                if (!_symo823m.IsInitialized)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                }

                if (update)
                {
                    await _symo823m.ReadBlockAsync();

                    if (!_symo823m.FroniusRegister.IsGood)
                    {
                        return StatusCode(StatusCodes.Status502BadGateway, _symo823m.FroniusRegister.Status);
                    }
                }

                return Ok(_symo823m.FroniusRegister);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a single SYMO823M property.
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
        [SwaggerOperation(Tags = new[] { "SYMO823M API" })]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 405)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> GetSYMO823MData(string name, bool update = false)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger?.LogDebug($"GetSYMO823MData() invalid property.");
                return StatusCode(StatusCodes.Status400BadRequest, $"Property is invalid.");
            }

            try
            {
                _logger?.LogDebug($"GetSYMO823MData({name})...");

                if (SYMO823MData.IsProperty(name))
                {
                    if (update)
                    {
                        if (SYMO823MData.IsReadable(name))
                        {
                            if (!_symo823m.IsInitialized)
                            {
                                return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                            }

                            var status = await _symo823m.ReadDataAsync(name);

                            if (!status.IsGood)
                            {
                                return StatusCode(StatusCodes.Status502BadGateway, status);
                            }
                        }
                        else
                        {
                            _logger?.LogDebug($"GetSYMO823MData('{name}') property not readable.");
                            return StatusCode(StatusCodes.Status405MethodNotAllowed, $"Property '{name}' not readable.");
                        }
                    }

                    return Ok(_symo823m.GetPropertyValue(name));
                }
                else
                {
                    _logger?.LogDebug($"GetSYMO823MData('{name}') property not found.");
                    return NotFound($"Property '{name}' not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Writes a single SYMO823M property.
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
        [SwaggerOperation(Tags = new[] { "SYMO823M API" })]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 405)]
        [ProducesResponseType(typeof(string), 406)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(DataStatus), 502)]
        public async Task<IActionResult> PutSYMO823MData(string name, [FromQuery] string value)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger?.LogDebug($"PutSYMO823MData({name}, {value}) invalid property.");
                return StatusCode(StatusCodes.Status400BadRequest, $"Property name is invalid.");
            }

            if (string.IsNullOrEmpty(value))
            {
                _logger?.LogDebug($"PutSYMO823MData({name}, {value}) invalid value.");
                return StatusCode(StatusCodes.Status400BadRequest, $"Property value is invalid.");
            }

            try
            {
                _logger?.LogDebug($"PutSYMO823MData({name}, {value})...");

                if (SYMO823MData.IsProperty(name))
                {
                    if (SYMO823MData.IsWritable(name))
                    {
                        if (!_symo823m.IsInitialized)
                        {
                            return StatusCode(StatusCodes.Status406NotAcceptable, "Initialization not yet finished.");
                        }

                        var status = await _symo823m.WriteDataAsync(name, value);

                        if (!status.IsGood)
                        {
                            return StatusCode(StatusCodes.Status502BadGateway, status);
                        }

                        return Ok();
                    }
                    else
                    {
                        _logger?.LogDebug($"PutSYMO823MData('{name}, {value}') property not writable.");
                        return StatusCode(StatusCodes.Status405MethodNotAllowed, $"Property '{name}' not writable.");
                    }
                }
                else
                {
                    _logger?.LogDebug($"PutSYMO823MData('{name}, {value}') property not found.");
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
